using ClarinDiary.Business.Contract;
using ClarinDiary.Business.DTO;
using ClarinDiary.Business.Helper;
using ClarinDiary.DataAccess.Models;
using ClarinDiary.DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClarinDiary.Business.Business
{
    public class PersonAppSevice : IPersonAppSevice
    {
        #region Members
        private readonly IRepository<Person> PersonRepository;
        private readonly IRepository<Rol> RolRepository;
        #endregion

        #region Builder
        public PersonAppSevice(IRepository<Person> personRepository, IRepository<Rol> rolRepository)
        {
            PersonRepository = personRepository ?? throw new ArgumentNullException(nameof(personRepository));
            RolRepository = rolRepository ?? throw new ArgumentNullException(nameof(RolRepository));
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
        public ResponseResult<PersonDTO> GetById(Guid id)
        {
            try
            {
                var person = PersonRepository.GetById(id);
                var personDTO = new PersonDTO()
                {
                    Id = person.Id,
                    FullName = person.FullName,
                    Identification = person.Identification,
                    IdRol = person.IdRol,
                    RolCode = RolRepository.GetById(person.IdRol).Code
                };
                return ResponseResult<PersonDTO>.Success(personDTO);
            }
            catch (Exception ex)
            {
                return ResponseResult<PersonDTO>.Error(ex.Message);
            }
        }

        /// <summary>
        /// Get Person based on azure user
        /// </summary>
        /// <param name="identification">azure user id</param>
        /// <returns></returns>
        public ResponseResult<PersonDTO> GetByIdentification(string identification)
        {
            try
            {
                var person = PersonRepository.Get().FirstOrDefault(p => p.Identification == identification);
                var personDTO = new PersonDTO()
                {
                    Id = person.Id,
                    FullName = person.FullName,
                    Identification = person.Identification,
                    IdRol = person.IdRol,
                    RolCode = RolRepository.GetById(person.IdRol).Code
                };
                return ResponseResult<PersonDTO>.Success(personDTO);
            }
            catch (Exception ex)
            {
                return ResponseResult<PersonDTO>.Error(ex.Message);
            }
        }
        #endregion
    }
}
