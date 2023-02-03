using AutoMapper;

namespace FMentorAPI.Extensions.AutoMapper
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
