using ClarinDiary.Business.Helper;
using ClarinDiary.DataAccess.Models;
using System.Collections.Generic;

namespace ClarinDiary.Business.Contract
{
    public interface IRolAppService
    {
        /// <summary>
        /// Add a role
        /// </summary>
        /// <param name="rol"></param>
        /// <returns></returns>
        ResponseResult<Rol> Add(Rol rol);

        /// <summary>
        /// Get all rols
        /// </summary>
        /// <returns></returns>
        ResponseResult<IEnumerable<Rol>> Get();
    }
}
