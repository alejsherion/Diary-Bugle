using System;
using System.Collections.Generic;

namespace ClarinDiary.DataAccess.Models
{
    public partial class PostComment
    {
        public Guid Id { get; set; }
        public Guid IdPost { get; set; }
        public Guid? IdPerson { get; set; }
        public string Comment { get; set; }

        public virtual Person IdPersonNavigation { get; set; }
        public virtual Post IdPostNavigation { get; set; }
    }
}
