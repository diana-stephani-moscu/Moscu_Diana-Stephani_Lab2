using Microsoft.EntityFrameworkCore;
using Moscu_Diana_Stephani_Lab2.Models;

namespace Moscu_Diana_Stephani_Lab2.Data
{
    public static class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new
            LibraryContext(serviceProvider.GetRequiredService<DbContextOptions<LibraryContext>>()))
            {
                if (context.Books.Any())
                // codul din Lab2
                {
                    return; // BD a fost creata anterior
                }

                Authors Author1 = new Authors
                {
                    FirstName = "Mihail",
                    LastName = "Sadoveanu"
                };

                Authors Author2 = new Authors
                {
                    FirstName = "George",
                    LastName = "Calinescu"
                };

                Authors Author3 = new Authors
                {
                    FirstName = "Mircea",
                    LastName = "Eliade"
                };
                context.Authors.AddRange(Author1, Author2, Author3);


                context.Books.AddRange(
                    new Book
                    {
                        Title = "Baltagul",
                        Author = Author1,
                        Price = Decimal.Parse("22")
                    },

                    new Book
                    {
                        Title = "Enigma Otiliei",
                        Author = Author2,
                        Price = Decimal.Parse("18")
                    },

                    new Book
                    {
                        Title = "Maytrei",
                        Author = Author3,
                        Price = Decimal.Parse("27")
                    }

                    );

                context.Customers.AddRange
                    (new Customer
                        {
                            Name = "Popescu Marcela",
                            Adress = "Str. Plopilor, nr. 24",
                            BirthDate = DateTime.Parse("1979-09-01")
                        }, 
                    new Customer
                        {
                            Name = "Mihailescu Cornel",
                            Adress = "Str. Bucuresti, nr.45,ap. 2",
                            BirthDate=DateTime.Parse("1969 - 07 - 08")
                        } 
                    ); 
                
                context.SaveChanges();
                
            } 
        }
    }
}
