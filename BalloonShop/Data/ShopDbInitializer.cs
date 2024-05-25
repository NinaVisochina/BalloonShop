using BalloonShop.Models;
using Microsoft.EntityFrameworkCore;

namespace BalloonShop.Data
{
    public static class ShopDbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ShopDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ShopDbContext>>()))
            {
                // Check if the database is already seeded
                if (context.Categories.Any())
                {
                    return;   // DB has been seeded
                }

                var categories = new Category[]
                {
                    new Category { Name = "Повітряні кульки", Description = "Різноманітні кульки для святкувань", Image = "balloons.jpg" },
                    new Category { Name = "Товари до свята", Description = "Все для святкування", Image = "festive_goods.jpg" }
                };
                context.Categories.AddRange(categories);
                context.SaveChanges();

                var subCategories = new SubCategory[]
                {
                    new SubCategory { Name = "Кульки без малюнків", Description = "Однотонні кульки без малюнків", Image = "https://gemar.it/wp-content/uploads/2019/04/G90_80_O.jpg", CategoryId = categories[0].CategoryId },
                    new SubCategory { Name = "Кульки для моделювання", Description = "Кульки для створення різних форм", Image = "https://products.gemar.it/9891-home_default/classic-assorted.jpg", CategoryId = categories[0].CategoryId },
                    new SubCategory { Name = "Свічки", Description = "Свічки для торта та інших свят", Image = "candles.jpg", CategoryId = categories[1].CategoryId },
                    new SubCategory { Name = "Хлопавки", Description = "Хлопавки для веселих свят", Image = "poppers.jpg", CategoryId = categories[1].CategoryId }
                };
                context.SubCategories.AddRange(
                   new SubCategory
                   {
                       Name = "Кульки без малюнків",
                       Description = "Однотонні кульки без малюнків",
                       Image = "https://gemar.it/wp-content/uploads/2019/04/G90_80_O.jpg",
                       CategoryId = 1
                   },
                   new SubCategory
                   {
                       Name = "Кульки для моделювання",
                       Description = "Кульки для створення різних форм",
                       Image = "https://products.gemar.it/9891-home_default/classic-assorted.jpg",
                       CategoryId = 2
                   },
                   new SubCategory
                   {
                       Name = "Свічки",
                       Description = "Свічки для торта",
                       Image = "image2.png",
                       CategoryId = 3
                   },
                   new SubCategory
                   {
                       Name = "Хлопавки",
                       Description = "Хлопавки для веселих свят",
                       Image = "image2.png",
                       CategoryId = 4
                   }
               );
                context.SaveChanges();

                var products = new Product[]
                {
                    new BalloonProduct { Name = "Кулька червона", Manufacturer = "КулькиКом",Size = "10'",Color = "Red", Price = 1.99m, IsAvailable = true, Model = "Red1", Type = "Латекс", QuantityInPack = 100, Form = "Кругла", DateAdded = DateTime.Now, DateModified = null, Image = "red_balloon.jpg", Description = "Червона латексна кулька", SubCategoryId = subCategories[0].SubCategoryId },
                    new BalloonProduct { Name = "Кулька синя", Manufacturer = "КулькиКом",Size = "10'", Color = "Blue",Price = 1.99m, IsAvailable = true, Model = "Blue1", Type = "Латекс", QuantityInPack = 100, Form = "Кругла", DateAdded = DateTime.Now, DateModified = null, Image = "blue_balloon.jpg", Description = "Синя латексна кулька", SubCategoryId = subCategories[0].SubCategoryId },
                    new BalloonProduct { Name = "Моделююча кулька", Manufacturer = "МодельКом",Size = "10'",Color = "Green", Price = 2.99m, IsAvailable = true, Model = "Model1", Type = "Латекс", QuantityInPack = 100, Form = "Довга", DateAdded = DateTime.Now, DateModified = null, Image = "modeling_balloon.jpg", Description = "Латексна кулька для моделювання", SubCategoryId = subCategories[1].SubCategoryId },
                    new Product { Name = "Свічка велика", Manufacturer = "СвятКом", Size = "10'",Color = "Red",Price = 3.99m, IsAvailable = true, Model = "Candle1",QuantityInPack = 1, DateAdded = DateTime.Now, DateModified = null, Image = "large_candle.jpg", Description = "Велика святкова свічка", SubCategoryId = subCategories[2].SubCategoryId },
                    new Product { Name = "Хлопавка", Manufacturer = "СвятКом",Size = "10'",Color = "Red", Price = 4.99m, IsAvailable = true, Model = "Popper1",QuantityInPack = 1, DateAdded = DateTime.Now, DateModified = null, Image = "popper.jpg", Description = "Весела святкова хлопавка", SubCategoryId = subCategories[3].SubCategoryId }
                };
                context.Products.AddRange(products);
                context.SaveChanges();
            }
        }
    }
}
