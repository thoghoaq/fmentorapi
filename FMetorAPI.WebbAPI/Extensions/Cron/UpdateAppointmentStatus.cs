using FMentorAPI.BusinessLogic.DTOs.RequestModel;
using FMentorAPI.BusinessLogic.FCMNotification;
using FMentorAPI.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Quartz;

namespace FMentorAPI.WebAPI.Extensions.Cron
{
    public class UpdateAppointmentStatus : IJob
    {
        private readonly FMentorDBContext _dbcontext;
        private readonly INotificationService _notificationService;

        public UpdateAppointmentStatus(FMentorDBContext context, INotificationService notificationService)
        {
            this._dbcontext = context;
            this._notificationService = notificationService;
        }

        public Task Execute(IJobExecutionContext context)
        {
            try
            {
                var overdue = _dbcontext.Appointments.Where(a =>
                    a.EndTime >= DateTime.UtcNow.AddHours(7) && a.Status.Equals("Happening")).ToList();
                foreach (var item in overdue)
                {
                    item.Status = "Completed";
                    var entity = _dbcontext.Appointments.Update(item);
                    _dbcontext.SaveChanges();
                    var mentee = _dbcontext.Mentees.FirstOrDefault(m => m.MenteeId == entity.Entity.MenteeId);
                    var mentor = _dbcontext.Mentors.FirstOrDefault(m => m.MentorId == entity.Entity.MentorId);
                    if (mentee != null && mentor != null)
                    {
                        {
                            var token1 = _dbcontext.UserTokens.FirstOrDefault(u => u.UserId == mentor.UserId);
                            var token = _dbcontext.UserTokens.FirstOrDefault(u => u.UserId == mentee.UserId);
                            NotificationRequestModel notificationModel;
                            if (token1 != null)
                            {
                                notificationModel = new NotificationRequestModel
                                {
                                    DeviceId = token1.Token,
                                    IsAndroiodDevice = true,
                                    Title = $"The appointment with {mentee.User.Name}!",
                                    Body = "The appointment is ended!",
                                    Route = token1.Token
                                };
                                _notificationService.SendNotification(notificationModel);
                            }

                            if (token != null)
                            {
                                notificationModel = new NotificationRequestModel
                                {
                                    DeviceId = token.Token,
                                    IsAndroiodDevice = true,
                                    Title = $"The appointment with {mentor.User.Name}!",
                                    Body = "The appointment is ended!",
                                    Route = "mentee"
                                };
                                _notificationService.SendNotification(notificationModel);
                            }
                        }
                    }
                }

                UpdateHappening().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw;
            }

            //Write your custom code here
            return Task.FromResult(true);
        }

        public Task UpdateHappening()
        {
                var appointments = _dbcontext.Appointments.Where(a =>
                a.StartTime >= DateTime.UtcNow.AddHours(7) && a.Status.Equals("Accepted")).ToList();

            foreach (var item in appointments)
            {
                item.Status = "Happening";
                var entity = _dbcontext.Appointments.Update(item);
                _dbcontext.SaveChanges();
                var mentee = _dbcontext.Mentees.Include(x => x.User)
                    .FirstOrDefault(m => m.MenteeId == entity.Entity.MenteeId);
                var mentor = _dbcontext.Mentors.Include(x => x.User)
                    .FirstOrDefault(m => m.MentorId == entity.Entity.MentorId);
                if (mentee != null && mentor != null)
                {
                    {
                        var token1 = _dbcontext.UserTokens.FirstOrDefault(u => u.UserId == mentor.UserId);
                        var token = _dbcontext.UserTokens.FirstOrDefault(u => u.UserId == mentee.UserId);
                        NotificationRequestModel notificationModel;
                        if (token1 != null)
                        {
                            notificationModel = new NotificationRequestModel
                            {
                                DeviceId = token1.Token,
                                IsAndroiodDevice = true,
                                Title = $"The appointment with {mentee.User.Name}!",
                                Body = $"The appointment with {mentee.User.Name} has been started!",
                                Route = token1.Token
                            };
                            _notificationService.SendNotification(notificationModel);
                        }

                        if (token != null)
                        {
                            notificationModel = new NotificationRequestModel
                            {
                                DeviceId = token.Token,
                                IsAndroiodDevice = true,
                                Title = $"The appointment with {mentor.User.Name}!",
                                Body = $"The appointment with {mentor.User.Name} has been started",
                                Route = "mentee"
                            };
                            _notificationService.SendNotification(notificationModel);
                        }
                    }
                }
            }
            return Task.FromResult(true);
        }
    }
}