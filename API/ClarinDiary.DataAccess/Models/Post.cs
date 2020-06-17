using System;
using System.Collections.Generic;

namespace ClarinDiary.DataAccess.Models
{
    public partial class Post
    {
        public Post()
        {
            PostComment = new HashSet<PostComment>();
        }

        public Guid Id { get; set; }
        public Guid IdAuthor { get; set; }
        public Guid? IdPublisher { get; set; }
        public DateTime PostDate { get; set; }
        public int State { get; set; }
        public string PostContent { get; set; }
        public string PostTitle { get; set; }

        public virtual Person IdAuthorNavigation { get; set; }
        public virtual Person IdPublisherNavigation { get; set; }
        public virtual ICollection<PostComment> PostComment { get; set; }
    }
}
