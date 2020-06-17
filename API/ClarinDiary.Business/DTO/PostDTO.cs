using System;
using System.Collections.Generic;
using System.Text;

namespace ClarinDiary.Business.DTO
{
    public class PostDTO
    {
        public Guid? Id { get; set; }
        public Guid IdAuthor { get; set; }
        public string Author { get; set; }
        public string PostContent { get; set; }
        public string PostTitle { get; set; }
        public DateTime PostDate { get; set; }
    }
}
