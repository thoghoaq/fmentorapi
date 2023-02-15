﻿using FMentorAPI.DTOs.RequestModel;
using FMentorAPI.Extensions.FCMNotification;
using FMentorAPI.Models;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json.Linq;
using NuGet.Common;
using Quartz;

namespace FMentorAPI.Extensions.Cron
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
                var ovedue = _dbcontext.Appointments.Where(a => a.StartTime.AddMinutes(a.Duration) >= DateTime.Now && a.Status.Equals("Accepted")).ToList();
                foreach (var item in ovedue)
                {
                    item.Status = "Completed";
                    var entity = _dbcontext.Appointments.Update(item);
                    _dbcontext.SaveChanges();
                    NotificationRequestModel notificationModel;
                    var mentee = _dbcontext.Mentees.FirstOrDefault(m => m.MenteeId == entity.Entity.MenteeId);
                    var mentor = _dbcontext.Mentors.FirstOrDefault(m => m.MentorId == entity.Entity.MentorId);
                    if (mentee != null && mentor != null)
                    {
                        {
                            var token1 = _dbcontext.UserTokens.FirstOrDefault(u => u.UserId == mentor.UserId);
                            var token = _dbcontext.UserTokens.FirstOrDefault(u => u.UserId == mentee.UserId);
                            if (token1 != null)
                            {
                                notificationModel = new NotificationRequestModel
                                {
                                    DeviceId = token1.Token,
                                    IsAndroiodDevice = true,
                                    Title = "The appointment is ended!",
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
                                    Title = "The appointment is ended!",
                                    Body = "The appointment is ended!",
                                    Route = "mentee"
                                };
                                _notificationService.SendNotification(notificationModel);
                            }
                            
                        }
                    }
                }
            }catch(Exception ex)
            {
                throw;
            }
            //Write your custom code here
            return Task.FromResult(true);
        }
    }
}
