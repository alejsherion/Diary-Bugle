using System;

namespace ClarinDiary.Business.DTO
{
    public class PersonDTO
    {
        public Guid Id { get; set; }
        public string Identification { get; set; }
        public string FullName { get; set; }
        public Guid IdRol { get; set; }

        public string RolCode { get; set; }
    }
}
