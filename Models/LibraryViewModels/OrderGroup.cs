using System;
using System.ComponentModel.DataAnnotations;

namespace Moscu_Diana_Stephani_Lab2.Models.LibraryViewModels
{
    public class OrderGroup //Adaugat in Lab4
    {
        [DataType(DataType.Date)] 
        public DateTime? OrderDate { get; set; }
        public int BookCount { get; set; }
    }


}
