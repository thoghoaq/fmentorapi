using FirebaseAdmin.Auth;

namespace FMentorAPI.BusinessLogic.Services
{
    public interface IFireBaseService
    {
        Task<UserRecord> GetUserRecordByIdToken(string idToken);
    }

    public class FireBaseService : IFireBaseService
    {
        public async Task<UserRecord> GetUserRecordByIdToken(string idToken)
        {
            try
            {
                var auth = FirebaseAuth.DefaultInstance;
                FirebaseToken decodedToken = await auth.VerifyIdTokenAsync(idToken);
                UserRecord userRecord = await auth.GetUserAsync(decodedToken.Uid);
                return userRecord;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}