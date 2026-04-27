using BilkentCatering.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace BilkentCatering.DataAccess.Context
{
    public static class DbInitializer
    {
        public static void Seed(BilkentCateringContext context)
        {
            context.Database.Migrate();

            if (!context.Admins.Any())
            {
                context.Admins.Add(new Admin
                {
                    Username = "admin",
                    FullName = "Admin",
                    Password = BCrypt.Net.BCrypt.HashPassword("123456"),
                    CreatedDate = DateTime.Now,
                    IsDeleted = false
                });

                context.SaveChanges();
            }
        }
    }
}