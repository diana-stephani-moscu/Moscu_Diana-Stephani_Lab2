using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Moscu_Diana_Stephani_Lab2.Models
{
    public class Customer
    {
        public int ID { get; set; } //am schimbat din CustomerID in doar ID si am facut migrare dar nu s-a actualizat baza de date
        public string Name { get; set; }
        public string Adress { get; set; }
        public DateTime BirthDate { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
