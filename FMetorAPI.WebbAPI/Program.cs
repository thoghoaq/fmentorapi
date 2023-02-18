using AutoMapper;
using CorePush.Apple;
using CorePush.Google;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Quartz;
using System.Configuration;
using System.Reflection;
using FMentorAPI.BusinessLogic.AutoMapper;
using FMentorAPI.BusinessLogic.FCMNotification;
using FMentorAPI.BusinessLogic.Services;
using FMentorAPI.DataAccess.Models;
using FMentorAPI.WebAPI.Extensions;
using FMentorAPI.WebAPI.Extensions.Cron;
using FMentorAPI.WebAPI.Extensions.ZoomAPI;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
{
    // Add services to the container.
    builder.Services.AddDbContext<FMentorDBContext>(
        options => options.UseSqlServer("name=ConnectionStrings:DefaultConnection"));
    builder.Services.AddScoped<IZoomExtension, ZoomExtension>();
    builder.Services.AddScoped<INotificationService, NotificationService>();
    builder.Services.AddScoped<IPaymentService, PaymentService>();
    builder.Services.AddHttpClient<FcmSender>();
    builder.Services.AddHttpClient<ApnSender>();
    //var appSettingsSection = Configuration.GetSection("FcmNotification");
    builder.Services.Configure<FcmNotificationSetting>(builder.Configuration.GetSection("FcmNotification"));
    builder.Services.AddAutoMapper();
    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.ConfigureSwaggerServices("FMentor.APIs");
    builder.Services.ConfigureAuthServices(builder.Configuration);

    builder.Services.AddCors();
    builder.Services.AddQuartz(q =>
        {
            q.UseMicrosoftDependencyInjectionScopedJobFactory();
            var jobKey = new JobKey("UpdateAppointmentStatus");
            q.AddJob<UpdateAppointmentStatus>(opts => opts.WithIdentity(jobKey));

            q.AddTrigger(opts => opts
                .ForJob(jobKey)
                .WithIdentity("UpdateAppointmentStatus-trigger")
                .WithCronSchedule("0/10 * * * * ?"));
        }
    );
    builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
}


var app = builder.Build();
var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
{
    // Configure the HTTP request pipeline.
   
    app.ConfigureSwaggerApps(provider);
    
    app.UseCors(policyBuilder => policyBuilder
        .AllowAnyHeader()
        .AllowAnyMethod()
        .SetIsOriginAllowed(origin => true)
        .AllowCredentials()
    );
    app.UseRouting();

    app.UseHttpsRedirection();

    app.ConfigureAuthApps();
    
    app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

    app.MapControllers();

    app.Run();
}