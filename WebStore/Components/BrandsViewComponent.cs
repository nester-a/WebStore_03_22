using Microsoft.AspNetCore.Mvc;
using WebStore.Services.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Components
{
    public class BrandsViewComponent : ViewComponent
    {
        private readonly IProductData productData;

        public BrandsViewComponent(IProductData productData) => this.productData = productData;

        public IViewComponentResult Invoke() => View(GetBrands());

        public IEnumerable<BrandViewModel> GetBrands() =>
            productData.GetBrand()
            .OrderBy(b => b.Order)
            .Select(b => new BrandViewModel
            {
                Id = b.Id,
                Name = b.Name,
            });

    }
}
