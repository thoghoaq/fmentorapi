using CorePush.Google;
using FMentorAPI.DTOs;
using FMentorAPI.DTOs.RequestModel;
using Microsoft.Extensions.Options;
using static FMentorAPI.DTOs.RequestModel.GoogleNotification;

namespace FMentorAPI.Extensions.FCMNotification
{
    public interface INotificationService
    {
        Task<NotificationResponseModel> SendNotification(NotificationRequestModel notificationModel);
    }
    public class NotificationService : INotificationService
    {
        private readonly FcmNotificationSetting _fcmNotificationSetting;
        public NotificationService(IOptions<FcmNotificationSetting> settings)
        {
            _fcmNotificationSetting = settings.Value;
        }
        public async Task<NotificationResponseModel> SendNotification(NotificationRequestModel notificationModel)
        {
            NotificationResponseModel response = new NotificationResponseModel();
            try
            {
                if (notificationModel.IsAndroiodDevice)
                {
                    FcmSettings settings = new FcmSettings()
                    {
                        SenderId = _fcmNotificationSetting.SenderId,
                        ServerKey = _fcmNotificationSetting.ServerKey,
                    };
                    HttpClient httpClient = new HttpClient();

                    string authorizationKey = string.Format("keyy={0}", settings.ServerKey);
                    string deviceToken = notificationModel.DeviceId;

                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", authorizationKey);
                    httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    DataPayload dataPayload = new DataPayload();
                    dataPayload.Title = notificationModel.Title;
                    dataPayload.Body = notificationModel.Body;

                    GoogleNotification notification = new GoogleNotification();
                    notification.Data = dataPayload;
                    notification.Notification = dataPayload;
                   
                    var fcm = new FcmSender(settings, httpClient);
                    var fcmSendResponse = await fcm.SendAsync(deviceToken, notification);

                    if (fcmSendResponse.IsSuccess())
                    {
                        response.IsSuccess = true;
                        response.Message = "Notification sent successfully";
                        return response;
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = fcmSendResponse.Results[0].Error;
                        return response;
                    }
                    
                }
                    
                else
                {

                }
                return response;
            }
            catch(Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
                return response;
            }
        }
    }
}
