using AutoMapper;
using CorePush.Apple;
using CorePush.Google;
using FMentorAPI.Extensions.AutoMapper;
using FMentorAPI.Extensions.FCMNotification;
using FMentorAPI.Extensions.ZoomAPI;
using FMentorAPI.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

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

var app = builder.Build();

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

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.MapControllers();

app.Run();
