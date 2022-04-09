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

            logger.LogInformation("Инициализация БД информацией о сотрудниках...");
            await InitializeEmployeesAsync();

            logger.LogInformation("Инициализация БД информацией о блогах...");
            await InitializeBlogsAsync();

            logger.LogInformation("Инициализация БД информацией о товарах...");
            await InitializeProductsAsync();
            logger.LogInformation("Инициализация БД завершена");
        }

        private async Task InitializeProductsAsync()
        {
            if (db.Products.Any())
            {
                logger.LogInformation("Инициализация БД информацией о товарах не требуется");
                return;
            }

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
        
        private async Task InitializeBlogsAsync()
        {
            if (db.Blogs.Any())
            {
                logger.LogInformation("Инициализация БД информацией о блогах не требуется");
                return;
            }

            logger.LogInformation("Запись блогов...");
            await using (await db.Database.BeginTransactionAsync())
            {
                db.Blogs.AddRange(TestData.Blogs);

                await db.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Blogs] ON");

                await db.SaveChangesAsync();

                await db.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Blogs] OFF");

                await db.Database.CommitTransactionAsync();
            }
            logger.LogInformation("Запись блогов выполнена");
        }

        private async Task InitializeEmployeesAsync()
        {
            if (db.Employees.Any())
            {
                logger.LogInformation("Инициализация БД информацией о сотрудниках не требуется");
                return;
            }

            logger.LogInformation("Запись сотрудников...");
            await using (await db.Database.BeginTransactionAsync())
            {
                db.Employees.AddRange(TestData.Employees);

                await db.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Employees] ON");

                await db.SaveChangesAsync();

                await db.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Employees] OFF");

                await db.Database.CommitTransactionAsync();
            }
            logger.LogInformation("Запись сотрудников выполнена");
        }
    }
}
