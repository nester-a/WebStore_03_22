using Microsoft.EntityFrameworkCore;
using WebStore.DAL.Context;

namespace WebStore.Data
{
    public class WebStoreDbInitializer
    {
        private readonly WebStoreDB db;
        private readonly ILogger<WebStoreDbInitializer> logger;

        public WebStoreDbInitializer(WebStoreDB db, ILogger<WebStoreDbInitializer> logger)
        {
            this.db = db;
            this.logger = logger;
        }


        public async Task InitializeAsync()
        {
            logger.LogInformation("Запуск инициализации БД");
            var pending_migrations = await db.Database.GetPendingMigrationsAsync();

            var applied_migrations = await db.Database.GetAppliedMigrationsAsync();

            if (pending_migrations.Any())
            {
                logger.LogInformation($"Приминение миграций: {string.Join(", ", pending_migrations)}");
                await db.Database.MigrateAsync();
            }

            await InitializeProductsAsync();
            logger.LogInformation("Инициализация БД завершена");
        }

        private async Task InitializeProductsAsync()
        {
            logger.LogInformation("Запись секций...");
            await using (await db.Database.BeginTransactionAsync())
            {
                db.Sections.AddRange(TestData.Sections);

                await db.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Sections] ON");

                await db.SaveChangesAsync();

                await db.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Sections] OFF");

                await db.Database.CommitTransactionAsync();
            }
            logger.LogInformation("Запись секций выполнена");

            logger.LogInformation("Запись брендов...");
            await using (await db.Database.BeginTransactionAsync())
            {
                db.Brands.AddRange(TestData.Brands);

                await db.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Brands] ON");

                await db.SaveChangesAsync();

                await db.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Brands] OFF");

                await db.Database.CommitTransactionAsync();
            }
            logger.LogInformation("Запись брендов выполнена");

            logger.LogInformation("Запись товаров...");
            await using (await db.Database.BeginTransactionAsync())
            {
                db.Products.AddRange(TestData.Products);

                await db.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Products] ON");

                await db.SaveChangesAsync();

                await db.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Products] OFF");

                await db.Database.CommitTransactionAsync();
            }
            logger.LogInformation("Запись товаров выполнена");
        }
        
    }
}
