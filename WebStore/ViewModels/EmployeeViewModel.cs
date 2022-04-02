using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebStore.ViewModels
{
    public class EmployeeViewModel : IValidatableObject
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "Имя")]
        [Required(ErrorMessage = "Имя не указано")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Длина не может быть меньше двух и больше 20 букв")]
        [RegularExpression(@"([А-ЯЁ][а-яё]+)|([A-Z][a-z]+)", ErrorMessage = "Неправильный формат имени")]
        public string FirstName { get; set; }

        [Display(Name = "Фамилия")]
        [Required(ErrorMessage = "Фамилия не указана")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Длина не может быть меньше двух и больше 20 букв")]
        [RegularExpression(@"([А-ЯЁ][а-яё]+)|([A-Z][a-z]+)", ErrorMessage = "Неправильный формат фамилии")]
        public string LastName { get; set; }

        [Display(Name = "Отчество")]
        public string Patronymic { get; set; }

        [Display(Name = "Возраст")]
        [Range(18, 80, ErrorMessage = "Возраст должен быть от 18 до 80 лет")]
        public int Age { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            switch (validationContext.MemberName)
            {
                default: return new[] { ValidationResult.Success };
                case nameof(Age):
                    if (Age < 15 || Age > 90) 
                        return new[] { new ValidationResult("Странный возраст", new[] { nameof(Age) }) };
                    return new[] { ValidationResult.Success };
            }
        }
    }
}
