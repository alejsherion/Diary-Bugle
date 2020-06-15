using ClarinDiary.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClarinDiary.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        #region Members
        private readonly ClarinDiaryContext Context;
        #endregion

        #region Builder
        public Repository(ClarinDiaryContext _context) => Context = _context;
        #endregion

        #region Methods
        /// <summary>
        /// Generic method for add a register
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public T Add(T t) => Context.Set<T>().Add(t).Entity;

        /// <summary>
        /// Generic method for get a register by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>one register a T type</returns>
        public T GetById(Guid id) => Context.Set<T>().Find(id);

        /// <summary>
        /// Generic method for get all register
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> Get() => Context.Set<T>().ToList();

        /// <summary>
        /// Generic method for update a register
        /// </summary>
        /// <param name="t"></param>
        public void Update(T t) => Context.Set<T>().Attach(t);

        /// <summary>
        /// Generic method for remove a register
        /// </summary>
        /// <param name="t"></param>
        public void Remove(T t) => Context.Set<T>().Remove(t);

        /// <summary>
        /// Generic method for save changes
        /// </summary>
        public void Save() => Context.SaveChanges(); 
        #endregion
    }
}
