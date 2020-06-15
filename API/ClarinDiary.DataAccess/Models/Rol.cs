using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClarinDiary.DataAccess.Models
{
    public partial class Rol
    {
        public Rol()
        {
            Person = new HashSet<Person>();
        }

        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Person> Person { get; set; }
    }
}

