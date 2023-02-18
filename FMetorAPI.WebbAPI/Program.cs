using AutoMapper;
using CorePush.Apple;
using CorePush.Google;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Quartz;
using System.Configuration;
using FMentorAPI.BusinessLogic.AutoMapper;
using FMentorAPI.BusinessLogic.FCMNotification;
using FMentorAPI.DataAccess.Models;
using FMentorAPI.WebAPI.Extensions.Cron;
using FMentorAPI.WebAPI.Extensions.ZoomAPI;

var builder = WebApplication.CreateBuilder(args);
{
    // Add services to the container.
    builder.Services.AddDbContext<FMentorDBContext>(
        options => options.UseSqlServer("name=ConnectionStrings:DefaultConnection"));
    builder.Services.AddScoped<IZoomExtension, ZoomExtension>();
    builder.Services.AddScoped<INotificationService, NotificationService>();
    builder.Services.AddHttpClient<FcmSender>();
    builder.Services.AddHttpClient<ApnSender>();
    //var appSettingsSection = Configuration.GetSection("FcmNotification");
    builder.Services.Configure<FcmNotificationSetting>(builder.Configuration.GetSection("FcmNotification"));
    builder.Services.AddAutoMapper();
    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
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
{
    // Configure the HTTP request pipeline.
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseCors(builder => builder
        .AllowAnyHeader()
        .AllowAnyMethod()
        .SetIsOriginAllowed(origin => true)
        .AllowCredentials()
    );
    app.UseRouting();

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

    app.MapControllers();

    app.Run();
}