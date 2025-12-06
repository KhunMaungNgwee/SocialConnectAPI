using BAL.IService;
using BAL.Service;
using Microsoft.EntityFrameworkCore;
using MODEL;
using MODEL.CommonConfig;
using REPOSITORY.UnitOfWork;
using SocialConnectAPI.BAL.IService;
using SocialConnectAPI.BAL.Service;

namespace BAL
    {
        public class ServiceManager
        {
            public static void SetServiceInfo(IServiceCollection services, AppSetting appSettings)
            {
                services.AddDbContextPool<DataContext>(optionsAction => {
                    optionsAction.UseSqlServer(appSettings.ConnectionString);
                });
                services.AddScoped<IUnitOfWork, UnitOfWork>();
                services.AddScoped<ILoginService, LoginService>();
                services.AddScoped<IPostService, PostService>();
                services.AddScoped<IProfileService, ProfileService>();
                services.AddScoped<INewsfeedService, NewsfeedService>();
            }
        }
    }
