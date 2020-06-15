using ClarinDiary.Business.Contract;
using ClarinDiary.Business.Helper;
using ClarinDiary.DataAccess.Models;
using ClarinDiary.DataAccess.Repository;
using System;
using System.Collections.Generic;

namespace ClarinDiary.Business.Business
{
    public class PersonAppSevice: IPersonAppSevice
    {
        #region Members
        private readonly IRepository<Person> PersonRepository; 
        #endregion

        #region Builder
        public PersonAppSevice(IRepository<Person> personRepository)
        {
            PersonRepository = personRepository ?? throw new ArgumentNullException(nameof(personRepository));
        }
        #endregion

        #region Methods
        /// <summary>
        /// Add person
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        public ResponseResult<Person> Add(Person person)
        {
            try
            {
                // Validate person
                var validate = PersonRepository.GetById(person.Id);
                if (validate != null)
                    return ResponseResult<Person>.Error("Person already exist!.");

                // Insert person in bd
                person = PersonRepository.Add(person);
                PersonRepository.Save();
                return ResponseResult<Person>.Success(person);
            }
            catch (Exception ex)
            {
                return ResponseResult<Person>.Error(ex.Message);
            }
        }

        /// <summary>
        /// Update a person
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        public ResponseResult<Person> Update(Person person)
        {
            try
            {
                var validate = PersonRepository.GetById(person.Id);
                if (validate == null)
                    return ResponseResult<Person>.Error("Person does not exist!");

                PersonRepository.Update(person);
                PersonRepository.Save();
                return ResponseResult<Person>.Success(person);
            }
            catch (Exception ex)
            {
                return ResponseResult<Person>.Error(ex.Message);
            }
        }

        /// <summary>
        /// Remove person from bd
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        public ResponseResult<Person> Remove(Person person)
        {
            try
            {
                var validate = PersonRepository.GetById(person.Id);
                if (validate == null)
                    return ResponseResult<Person>.Error("Person does not exist!");

                PersonRepository.Remove(person);
                PersonRepository.Save();
                return ResponseResult<Person>.Success();
            }
            catch (Exception ex)
            {
                return ResponseResult<Person>.Error(ex.Message);
            }
        }

        /// <summary>
        /// Get a list of persons
        /// </summary>
        /// <returns></returns>
        public ResponseResult<IEnumerable<Person>> Get()
        {
            try
            {
                return ResponseResult<IEnumerable<Person>>.Success(PersonRepository.Get());
            }
            catch (Exception ex)
            {
                return ResponseResult<IEnumerable<Person>>.Error(ex.Message);
            }
        }

        /// <summary>
        /// Get a person by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseResult<Person> GetById(Guid id)
        {
            try
            {
                return ResponseResult<Person>.Success(PersonRepository.GetById(id));
            }
            catch (Exception ex)
            {
                return ResponseResult<Person>.Error(ex.Message);
            }
        } 
        #endregion
    }
}
