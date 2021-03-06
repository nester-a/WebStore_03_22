using WebStore.Domain.Entities.Base;

namespace WebStore.Models
{
    public class Employee : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public int Age { get; set; }
    }
}
