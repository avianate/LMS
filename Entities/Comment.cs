using System;
using System.Collections.Generic;

namespace LMS.Entities
{
    public class Comment
    {
        public int Id { get; set; }

        public string Body { get; set; }

        public DateTime PublishDate { get; set; }

        public bool IsApproved { get; set; }

        public string UserEmail { get; set; }

        public string UserName { get; set; }
    }
}