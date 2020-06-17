using System;
using System.Collections.Generic;
using System.Text;

namespace ClarinDiary.Business.DTO
{
    public class PostCommentDTO
    {
        public Guid? Id { get; set; }
        public Guid IdPost { get; set; }
        public Guid? IdPerson { get; set; }
        public string Comment { get; set; }
        public string Author { get; set; }
    }
}
