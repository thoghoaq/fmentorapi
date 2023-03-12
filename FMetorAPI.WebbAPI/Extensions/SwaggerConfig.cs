using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.OpenApi.Models;
using NTQ.Sdk.Core.Custom;

namespace FMentorAPI.WebAPI.Extensions
{
    public static class SwaggerConfig
    {
        public static void ConfigureSwaggerServices(this IServiceCollection services, string appName)
        {
            services.AddApiVersioning(setup =>
            {
                setup.DefaultApiVersion = new ApiVersion(1, 0);
                setup.AssumeDefaultVersionWhenUnspecified = true;
                setup.ReportApiVersions = true;
            });

            services.AddVersionedApiExplorer(setup =>
            {
                setup.GroupNameFormat = "'v'VVV";
                setup.SubstituteApiVersionInUrl = true;
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = appName, Version = "v1",
                        Description = "This is Restful APIs for Capstone Application",
                        Contact = new OpenApiContact()
                        {
                            Email = "peanut@gmail.com",
                            Name = "Peanut Team",
                            // Url = new Uri("https://www.facebook.com/quangnt1702")
                        }
                    });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme.
                      Enter 'Bearer' [space] and then your token in the text input below.
                      Example: 'Bearer iJIUzI1NiIsInR5cCI6IkpXVCGlzI9d9zc2'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
                // Config xml
                var xmlCommentFile = $"{Assembly.GetEntryAssembly()?.GetName().Name}.xml";
                var cmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentFile);
                c.IncludeXmlComments(cmlCommentsFullPath);
                c.OperationFilter<CustomHeaderSwaggerAttribute>();
            });

            services.TryAddEnumerable(ServiceDescriptor.Transient<IApiDescriptionProvider,
                KebabQueryParametersApiDescriptionProvider>());
        }

        public static void ConfigureSwaggerApps(this IApplicationBuilder app, IApiVersionDescriptionProvider provider)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                foreach (var providerApiVersionDescription in provider.ApiVersionDescriptions)
                {
                    c.SwaggerEndpoint($"/swagger/{providerApiVersionDescription.GroupName}/swagger.json",
                        providerApiVersionDescription.GroupName.ToUpperInvariant());
                }
            });
        }
    }
}