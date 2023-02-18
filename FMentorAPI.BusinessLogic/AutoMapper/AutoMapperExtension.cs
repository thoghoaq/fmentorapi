using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace FMentorAPI.BusinessLogic.AutoMapper
{
    public static class AutoMapperExtension
    {
        public static void AddAutoMapper(this IServiceCollection services)
        {
            MapperConfiguration mapperConfiguration = new MapperConfiguration(mc =>
            {
                mc.ConfigUserModule();
            });
            IMapper mapper = mapperConfiguration.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
