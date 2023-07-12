using ProjektAPI.Models;
using System.Collections.Generic;
using System.Linq;

public static class AppDbContextInitializer
{
    public static void Initialize(AppDbContext context)
    {
        if (!context.Database.CanConnect())
        {
            context.Database.EnsureCreated();
        }

        if (!context.Categories.Any())
        {
            // Tworzenie węzłów drzewa
            var categories = new List<Category>
                {
                    new Category { Name = "Rachunki", UserId = null },
                    new Category { Name = "Zakupu spożywcze", UserId = null },
                    new Category { Name = "Narkotyki", UserId = null },
                    new Category { Name = "Wakacje", UserId = null },
                    new Category { Name = "Gry wideo", UserId = null }
                };

            foreach (var category in categories)
            {
                context.Categories.Add(category);
            }

            context.SaveChanges();
        }
    }
}


