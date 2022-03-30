using Microsoft.AspNetCore.Mvc;
using WebStore.Domain;
using WebStore.Services.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IProductData productData;

        public CatalogController(IProductData productData) => this.productData = productData;
        public IActionResult Index(int? sectionId, int? brandId)
        {
            var filter = new ProductFilter()
            {
                BrandId = brandId,
                SectionId = sectionId,
            };

            var products = productData.GetProducts(filter);

            var viewModel = new CatalogViewModel()
            {
                BrandId = brandId,
                SectionId = sectionId,
                Products = products
                .OrderBy(p => p.Order)
                .Select(p => new ProductViewModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    ImageUrl = p.ImageUrl,
                })
            };

            return View(viewModel);
        }
    }
}
