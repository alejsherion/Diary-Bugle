using ClarinDiary.Business.DTO;
using ClarinDiary.Business.Helper;
using ClarinDiary.DataAccess.Models;
using System;
using System.Collections.Generic;

namespace ClarinDiary.Business.Contract
{
    public interface IPersonAppSevice
    {
        /// <summary>
        /// Add person
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        ResponseResult<Person> Add(Person person);

        /// <summary>
        /// Update a person
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        ResponseResult<Person> Update(Person person);

        /// <summary>
        /// Remove person from bd
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        ResponseResult<Person> Remove(Person person);

        /// <summary>
        /// Get a list of persons
        /// </summary>
        /// <returns></returns>
        ResponseResult<IEnumerable<Person>> Get();

        /// <summary>
        /// Get a person by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ResponseResult<PersonDTO> GetById(Guid id);

        /// <summary>
        /// Get Person based on azure user
        /// </summary>
        /// <param name="identification">azure user id</param>
        /// <returns></returns>
        ResponseResult<PersonDTO> GetByIdentification(string identification);
    }
}
