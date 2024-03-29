﻿using AutoMapper;
using FMentorAPI.BusinessLogic.DTOs;
using FMentorAPI.DataAccess.Models;

namespace FMentorAPI.BusinessLogic.AutoMapper
{
    public static class Module
    {
        public static void ConfigUserModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<Appointment, DTOs.AppointmentResponseModel>().ReverseMap();
            mc.CreateMap<Booking, DTOs.BookingResponseModel>().ReverseMap();
            mc.CreateMap<Course, DTOs.CourseResponseModel>().ReverseMap();
            mc.CreateMap<Education, DTOs.EducationResponseModel>().ReverseMap();
            mc.CreateMap<Job, DTOs.JobResponseModel>().ReverseMap();
            mc.CreateMap<Mentee, DTOs.MenteeResponseModel>().ReverseMap();
            mc.CreateMap<Mentor, DTOs.MentorResponseModel>().ReverseMap();
            mc.CreateMap<Mentee, DTOs.MenteeResponseModel2>().ReverseMap();
            mc.CreateMap<Mentor, DTOs.MentorResponseModel2>().ReverseMap();
            mc.CreateMap<MentorAvailability, DTOs.MentorAvailabilityResponseModel>().ReverseMap();
            mc.CreateMap<MentorWorkingTime, DTOs.MentorWorkingTimeResponseModel>().ReverseMap();
            mc.CreateMap<Review, DTOs.ReviewResponseModel>().ReverseMap();
            mc.CreateMap<Specialty, DTOs.SpecialtyResponseModel>().ReverseMap();
            mc.CreateMap<User, DTOs.UserResponseModel>().ReverseMap();
            mc.CreateMap<UserPermission, DTOs.UserPermissionResponseModel>().ReverseMap();
            mc.CreateMap<UserSpecialty, DTOs.UserSpecialtyResponseModel>().ReverseMap();

            mc.CreateMap<FollowedMentor, DTOs.FollowMentorResponseModel>().ReverseMap();
            mc.CreateMap<FavoriteCourse, DTOs.FavoriteCourseResponseModel>().ReverseMap();

            mc.CreateMap<Payment, DTOs.PaymentResponseModel>().ReverseMap();
            mc.CreateMap<Ranking, DTOs.RankingResponseModel>().ReverseMap();
            mc.CreateMap<Wallet, DTOs.WalletResponseModel>().ReverseMap();

            mc.CreateMap<Booking, DTOs.RequestModel.BookingRequestModel>().ReverseMap();
            mc.CreateMap<Appointment, DTOs.RequestModel.AppointmentRequestModel>().ReverseMap();

            mc.CreateMap<Booking, DTOs.RequestModel.UpdateRequestModel.UpdateBookingRequestModel>().ReverseMap();

            mc.CreateMap<User, ReviewerInfo>().ReverseMap();
            mc.CreateMap<User, UserInfo>().ReverseMap();

            mc.CreateMap<Review, DTOs.RequestModel.ReviewRequestModel>().ReverseMap();
        }
    }
}
