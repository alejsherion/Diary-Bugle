using System;
using System.Collections.Generic;

namespace ClarinDiary.DataAccess.Repository
{
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Generic method for get a register by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>one register a T type</returns>
        T GetById(Guid id);

        /// <summary>
        /// Generic method for get all register
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> Get();
        
        /// <summary>
        /// Generic method for add a register
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        T Add(T t);

        /// <summary>
        /// Generic method for update a register
        /// </summary>
        /// <param name="t"></param>
        void Update(T t);

        /// <summary>
        /// Generic method for remove a register
        /// </summary>
        /// <param name="t"></param>
        void Remove(T t);

        /// <summary>
        /// Generic method for save changes
        /// </summary>
        void Save();
    }
}
