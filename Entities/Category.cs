using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Entities
{
    public class Category
    {
        public int Id { get; set; }

        public string CategoryName { get; set; }
    }
}