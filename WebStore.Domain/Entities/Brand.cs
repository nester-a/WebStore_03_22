using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using WebStore.Domain.Entities.Base;
using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore.Domain.Entities
{
    [Index(nameof(Name), /*nameof(Order), */IsUnique = true)]
    //[Table("Brands")]
    public class Brand : NamedEntity, IOrderedEntity
    {
        //[Column("BrandOrder")]
        public int Order {get;set;}
    }
}
