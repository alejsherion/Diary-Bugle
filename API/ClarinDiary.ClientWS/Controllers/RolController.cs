using ClarinDiary.Business.Contract;
using ClarinDiary.Business.Helper;
using ClarinDiary.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ClarinDiary.ClientWS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolController: ControllerBase
    {
        #region Members
        private readonly IRolAppService _rolAppService; 
        #endregion

        #region Builder
        public RolController(IRolAppService rolAppService) : base() => _rolAppService = rolAppService; 
        #endregion

        #region Methods
        /// <summary>
        /// Get all rols
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Get")]
        public ResponseResult<IEnumerable<Rol>> Get() => _rolAppService.Get();

        /// <summary>
        /// Add a role
        /// </summary>
        /// <param name="rol"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Add")]
        public ResponseResult<Rol> Add(Rol rol) => _rolAppService.Add(rol); 
        #endregion
    }
}
