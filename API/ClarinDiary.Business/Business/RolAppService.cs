using ClarinDiary.Business.Contract;
using ClarinDiary.Business.Helper;
using ClarinDiary.DataAccess.Models;
using ClarinDiary.DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClarinDiary.Business.Business
{
    public class RolAppService : IRolAppService
    {
        #region Members
        private readonly IRepository<Rol> RolRepository; 
        #endregion

        #region Builder
        public RolAppService(IRepository<Rol> rolRepository)
        {
            RolRepository = rolRepository ?? throw new ArgumentNullException(nameof(rolRepository));
        }
        #endregion

        #region Methods
        /// <summary>
        /// Add a role
        /// </summary>
        /// <param name="rol"></param>
        /// <returns></returns>
        public ResponseResult<Rol> Add(Rol rol)
        {
            try
            {
                rol.Id = new Guid();
                rol = RolRepository.Add(rol);
                RolRepository.Save();

                return ResponseResult<Rol>.Success(rol);
            }
            catch (Exception ex)
            {
                return ResponseResult<Rol>.Error(ex.Message);
            }
        }

        /// <summary>
        /// Get all rols
        /// </summary>
        /// <returns></returns>
        public ResponseResult<IEnumerable<Rol>> Get()
        {
            try
            {
                return ResponseResult<IEnumerable<Rol>>.Success(RolRepository.Get());
            }
            catch (Exception ex)
            {
                return ResponseResult<IEnumerable<Rol>>.Error(ex.Message);
            }
        } 
        #endregion
    }
}
