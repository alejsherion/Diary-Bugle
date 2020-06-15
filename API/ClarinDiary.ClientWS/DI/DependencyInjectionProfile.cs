using ClarinDiary.Business.Business;
using ClarinDiary.Business.Contract;
using ClarinDiary.DataAccess.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClarinDiary.ClientWS.DI
{
    public static class DependencyInjectionProfile
    {
        /// <summary>
        /// Registra el perfil de producción
        /// </summary>
        /// <param name="services">Colección de servicios</param>
        public static void RegisterProfile(IServiceCollection services)
        {
            #region Repositories
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            #endregion

            #region Business
            services.AddTransient<IRolAppService, RolAppService>();
            services.AddTransient<IPostAppService, PostAppService>();
            services.AddTransient<IPersonAppSevice, PersonAppSevice>();
            services.AddTransient<ICommentsAppService, CommentsAppService>();            
            #endregion
        }
    }
}
