using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moscu_Diana_Stephani_Lab2.Models
{
    public class Book
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public Authors Author { get; set; }
        public decimal Price { get; set; }
        
        public int AuthorID { get; set; }
        public ICollection<Order> Order { get; set; }
        
    }
}
