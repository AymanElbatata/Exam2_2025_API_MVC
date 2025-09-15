using Exam2025.BLL.Interfaces;
using Exam2025.BLL.Repositories;
using Exam2025.DAL.Interfaces;
using Exam2025.DAL.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Exam2025.API.Helpers
{
    public static class ApplicationService
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions(x =>
             x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
            //services.AddControllers().AddNewtonsoftJson(options =>
            //{
            //    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            //});
            //services.AddControllers().AddJsonOptions(options =>
            //{
            //    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
            //    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            //});
            services.AddHttpContextAccessor();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddAutoMapper(typeof(MappingProfiles));
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAppSession, AppSession>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAnswerTBLRepository, AnswerTBLRepository>();
            services.AddScoped<IExamTBLRepository, ExamTBLRepository>();
            services.AddScoped<IQuestionTBLRepository, QuestionTBLRepository>();
            services.AddScoped<IUserExamDetailTBLRepository, UserExamDetailTBLRepository>();
            services.AddScoped<IUserExamTBLRepository, UserExamTBLRepository>();
            services.AddScoped<IMySPECIALGUID, MySPECIALGUID>();
            services.AddScoped<IAppErrorTBLRepository, AppErrorTBLRepository>();
            //services.AddSingleton<IUpdatedCurrentUserPackage, UpdatedCurrentUserPackage>();
            //services.AddSingleton<IHostedService, UpdatedCurrentUserPackage>();
            services.AddSingleton<AppSettingsHelper>();

            return services;
        }
    }
}
