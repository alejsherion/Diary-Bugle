using ClarinDiary.Business.Contract;
using ClarinDiary.Business.DTO;
using ClarinDiary.Business.Helper;
using ClarinDiary.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClarinDiary.ClientWS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController: ControllerBase
    {
        #region Members
        private readonly IPersonAppSevice _personAppService;
        #endregion

        #region Builder
        public PersonController(IPersonAppSevice personAppSevice):base() => _personAppService = personAppSevice; 
        #endregion

        #region Methods
        /// <summary>
        /// Get a list of persons
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Get")]
        ResponseResult<IEnumerable<Person>> Get() => _personAppService.Get();

        /// <summary>
        /// Get a person by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetById")]
        public ResponseResult<PersonDTO> GetById(Guid id) => _personAppService.GetById(id);

        /// <summary>
        /// Get Person based on azure user
        /// </summary>
        /// <param name="identification">azure user id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetByIdentification")]
        public ResponseResult<PersonDTO> GetByIdentification(string identification) => _personAppService.GetByIdentification(identification);

        /// <summary>
        /// Add person
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Add")]
        public ResponseResult<Person> Add(Person person) => _personAppService.Add(person);

        /// <summary>
        /// Update a person
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Update")]
        ResponseResult<Person> Update(Person person) => _personAppService.Update(person);

        /// <summary>
        /// Remove person from bd
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Remove")]
        ResponseResult<Person> Remove(Person person) => _personAppService.Remove(person); 
        #endregion
    }
}
