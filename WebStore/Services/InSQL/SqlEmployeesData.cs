using WebStore.DAL.Context;
using WebStore.Models;
using WebStore.Services.Interfaces;

namespace WebStore.Services.InSQL
{
    public class SqlEmployeesData : IEmployeesData
    {
        private readonly WebStoreDB db;
        private readonly ILogger<SqlEmployeesData> logger;

        public SqlEmployeesData(WebStoreDB db, ILogger<SqlEmployeesData> logger)
        {
            this.db = db;
            this.logger = logger;
        }
        public int Add(Employee employee)
        {
            return AddAsync(employee).Result;
        }

        public async Task<int> AddAsync(Employee employee)
        {
            if (employee is null) throw new ArgumentNullException(nameof(employee));

            if (db.Employees.Contains(employee)) return employee.Id;

            logger.LogInformation("Запись сотрудника в БД...");
            await using (await db.Database.BeginTransactionAsync())
            {
                db.Employees.Add(employee);

                await db.SaveChangesAsync();

                await db.Database.CommitTransactionAsync();
            }
            logger.LogInformation("Запись сотрудника в БД выполнена");

            return employee.Id;
        }

        public bool Delete(int id)
        {
            return DeleteAsync(id).Result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var employee = db.Employees.SingleOrDefault(b => b.Id == id);
            if (employee is null) throw new ArgumentNullException(nameof(employee));

            if (db.Employees.Contains(employee))
            {
                logger.LogInformation("Удаление сотрудника из БД...");
                await using (await db.Database.BeginTransactionAsync())
                {
                    db.Employees.Remove(employee);

                    await db.SaveChangesAsync();

                    await db.Database.CommitTransactionAsync();
                }
                logger.LogInformation("Удаление сотрудника из БД выполнено");
                return true;
            }

            logger.LogInformation("Сотрудник в БД не найден");
            return false;

        }

        public IEnumerable<Employee> GetAll()
        {
            return db.Employees;
        }

        public Employee GetById(int id)
        {
            Employee query = db.Employees.SingleOrDefault(b => b.Id == id);
            return query;
        }

        public void Update(Employee employee)
        {
            UpdateAsync(employee).Wait();
        }

        public async Task UpdateAsync(Employee employee)
        {
            if (employee is null)
            {
                logger.LogInformation("Сотрудник в БД не найден");
                throw new ArgumentNullException(nameof(employee));
            }

            if (db.Employees.Contains(employee))
            {
                logger.LogInformation("Обновление сотрудника в БД...");
                await using (await db.Database.BeginTransactionAsync())
                {
                    db.Employees.Update(employee);

                    await db.SaveChangesAsync();

                    await db.Database.CommitTransactionAsync();
                }
                logger.LogInformation("Обновление сотрудника в БД выполнено");
            }
        }
    }
}
