using System.ComponentModel.DataAnnotations;
using AutoMapper;
using FirebaseAdmin.Auth;
using FMentorAPI.BusinessLogic.DTOs;
using FMentorAPI.BusinessLogic.DTOs.RequestModel;
using FMentorAPI.BusinessLogic.DTOs.RequestModel.UpdateRequestModel;
using FMentorAPI.BusinessLogic.Services;
using FMentorAPI.BusinessLogic.Utils;
using FMentorAPI.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FMentorAPI.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly FMentorDBContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IFireBaseService _fireBaseService;

        public UsersController(FMentorDBContext context, IMapper mapper, IConfiguration configuration,
            IFireBaseService fireBaseService)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
            _fireBaseService = fireBaseService;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserResponseModel>>> GetUsers()
        {
            return _mapper.Map<List<UserResponseModel>>(await _context.Users.ToListAsync());
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserResponseModel>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var jobs = await _context.Jobs.Where(j => j.UserId == user.UserId).OrderBy(j => j.StartDate).ToListAsync();
            var educations = await _context.Educations.Where(j => j.UserId == user.UserId).OrderBy(j => j.StartDate)
                .ToListAsync();
            user.Jobs = jobs;
            user.Educations = educations;
            return _mapper.Map<UserResponseModel>(user);
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, UpdateUserRequestModel model)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound();
            if (model == null)
                return BadRequest("Model is empty!");
            if (!ModelState.IsValid)
                return BadRequest("Model is not valid!");
            if (model.Age != null)
                user.Age = (int)model.Age;
            if (model.Description != null)
                user.Description = model.Description;
            if (model.Photo != null)
                user.Photo = model.Photo;
            if (model.VideoIntroduction != null) user.VideoIntroduction = model.VideoIntroduction;
            user.Name = model.Name;
            try
            {
                var entity = _context.Users.Update(user);
                await _context.SaveChangesAsync();
                return Ok(_mapper.Map<UserResponseModel>(user));
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost]
        [Route("signin")]
        public async Task<ActionResult<UserResponseModel>> SignIn(SignInRequestModel model, [Required] string token)
        {
            var user = _context.Users.Where(u => u.Email == model.Email)
                .Include(m => m.Mentees)
                .Include(m => m.Mentors)
                .Include(m => m.Jobs)
                .Include(m => m.Educations)
                .Include(m => m.Wallets)
                .Include(m => m.ReviewReviewees)
                .Include(m => m.ReviewReviewers)
                .Include(m => m.UserSpecialties)
                .Include(m => m.IsMentorNavigation)
                .FirstOrDefault();
            if (user == null)
            {
                return NotFound();
            }

            if (PasswordHashUtil.VerifyPassword(user.Password, model.Password))
            {
                return BadRequest();
            }

            if (_context.UserTokens.FirstOrDefault(u => u.UserId == user.UserId && u.Token.Equals(token)) == null)
            {
                _context.UserTokens.Add(new UserToken { UserId = user.UserId, Token = token });
                _context.SaveChanges();
            }

            #region Generate JWT

            var accessToken = AccessTokenManager.GenerateJwtToken(user.Email,
                new[] { user.IsMentor.ToString() }, user.UserId.ToString(), _configuration);

            var result = _mapper.Map<UserResponseModel>(user);
            result.AccessToken = accessToken;

            #endregion

            return Ok(result);
        }

        [HttpPost]
        [Route("firebase")]
        public async Task<ActionResult<UserResponseModel>> SignInByFirebase(string idToken, [Required] string token)
        {
            string accessToken;
            UserRecord userRecord = await _fireBaseService.GetUserRecordByIdToken(idToken);


            var user = _context.Users.Where(u => u.Email == userRecord.Email)
                .Include(m => m.Mentees)
                .Include(m => m.Mentors)
                .Include(m => m.Jobs)
                .Include(m => m.Educations)
                .Include(m => m.Wallets)
                .Include(m => m.ReviewReviewees)
                .Include(m => m.ReviewReviewers)
                .Include(m => m.UserSpecialties)
                .Include(m => m.IsMentorNavigation)
                .FirstOrDefault();
            
            if (user == null)
            {
                try
                {
                    if (_context.Users.FirstOrDefault(u => u.Email == userRecord.Email) != null)
                        return BadRequest("The email address is already exist!");
                    User newUser = new User
                    {
                        Email = userRecord.Email, Name = userRecord.DisplayName, Password = " ",
                        Description = " ", IsMentor = 0,
                        Photo = " ", VideoIntroduction = " "
                    };
                    var entity = _context.Users.Add(newUser).Entity;
                    _context.SaveChanges();
                    Mentee mentee = new Mentee { UserId = entity.UserId };
                    _context.Mentees.Add(mentee);
                    _context.Wallets.Add(new Wallet()
                    {
                        Balance = 0,
                        UserId = entity.UserId
                    });
                    _context.SaveChanges();

                    var jobs = await _context.Jobs.Where(j => j.UserId == entity.UserId).OrderBy(j => j.StartDate)
                        .ToListAsync();
                    var educations = await _context.Educations.Where(j => j.UserId == entity.UserId).OrderBy(j => j.StartDate)
                        .ToListAsync();
                    entity.Jobs = jobs;
                    entity.Educations = educations;
                    
                    #region Generate JWT

                     accessToken = AccessTokenManager.GenerateJwtToken(entity.Email,
                        new[] { entity.IsMentor.ToString() }, entity.UserId.ToString(), _configuration);

                    var resultCreated = _mapper.Map<UserResponseModel>(user);
                    resultCreated.AccessToken = accessToken;

                    #endregion

                    return Ok(resultCreated);
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }

            if (_context.UserTokens.FirstOrDefault(u => u.UserId == user.UserId && u.Token.Equals(token)) == null)
            {
                _context.UserTokens.Add(new UserToken { UserId = user.UserId, Token = token });
                _context.SaveChanges();
            }
            
            #region Generate JWT

             accessToken = AccessTokenManager.GenerateJwtToken(user.Email,
                new[] { user.IsMentor.ToString() }, user.UserId.ToString(), _configuration);

            var result = _mapper.Map<UserResponseModel>(user);
            result.AccessToken = accessToken;

            #endregion

            return Ok(result);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("add-specialties/{id}")]
        public async Task<ActionResult<bool>> SignUp([FromBody] List<String> specialtiesName, int id)
        {
            if (specialtiesName == null)
            {
                return BadRequest("List is null!");
            }

            if (_context.Users.Find(id) == null)
                return BadRequest("Id is not exist!");
            if (specialtiesName.Count == 0)
                return Ok(true);
            try
            {
                foreach (var name in specialtiesName)
                {
                    var specialty = await _context.Specialties.FirstOrDefaultAsync(s => s.Name.Equals(name));
                    var userSpecialty = _context.UserSpecialties.FirstOrDefault(u =>
                        u.UserId == id && u.SpecialtyId == specialty.SpecialtyId);
                    if (specialty != null && userSpecialty == null)
                    {
                        _context.UserSpecialties.Add(new UserSpecialty
                            { SpecialtyId = specialty.SpecialtyId, UserId = id });
                        _context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error");
            }

            return Ok(true);
        }

        [HttpPost("sign-up")]
        public async Task<ActionResult<UserResponseModel>> SignUp(SignUpRequestModel model)
        {
            if (model == null)
            {
                return BadRequest("Model is empty!");
            }

            if (!ModelState.IsValid)
                return BadRequest("Model is not valid!");
            if (!model.Password.Equals(model.ConfirmPassword))
                return BadRequest("Password is not equal Confirm Password!");
            if (_context.Users.FirstOrDefault(u => u.Email == model.Email) != null)
                return BadRequest("The email address is already exist!");
            User user = new User
            {
                Email = model.Email, Name = model.Name, Password = PasswordHashUtil.HashPassword(model.Password),
                Description = " ", IsMentor = 0,
                Photo = " ", VideoIntroduction = " "
            };
            try
            {
                var entity = _context.Users.Add(user);
                await _context.SaveChangesAsync();
                Mentee mentee = new Mentee { UserId = user.UserId };
                _context.Mentees.Add(mentee);
                _context.Wallets.Add(new Wallet()
                {
                    WalletId = _context.Wallets.Max(x => x.WalletId) + 1,
                    Balance = 0,
                    UserId = user.UserId
                });
                await _context.SaveChangesAsync();

                var jobs = await _context.Jobs.Where(j => j.UserId == user.UserId).OrderBy(j => j.StartDate)
                    .ToListAsync();
                var educations = await _context.Educations.Where(j => j.UserId == user.UserId).OrderBy(j => j.StartDate)
                    .ToListAsync();
                user.Jobs = jobs;
                user.Educations = educations;

                #region Generate JWT

                var accessToken = AccessTokenManager.GenerateJwtToken(user.Email,
                    new[] { user.IsMentor.ToString() }, user.UserId.ToString(), _configuration);

                var result = _mapper.Map<UserResponseModel>(user);
                result.AccessToken = accessToken;

                #endregion

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}