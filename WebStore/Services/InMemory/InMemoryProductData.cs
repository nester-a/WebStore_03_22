using WebStore.Data;
using WebStore.Domain;
using WebStore.Domain.Entities;
using WebStore.Services.Interfaces;

namespace WebStore.Services.InMemory
{
    public class InMemoryProductData : IProductData
    {
        public IEnumerable<Brand> GetBrand() => TestData.Brands;

        public IEnumerable<Product> GetProducts(ProductFilter filter = null)
        {
            IEnumerable<Product> query = TestData.Products;

            //if (filter?.SectionId != null)
            //    query = query.Where(p => p.SectionId == filter.SectionId);

            if (filter?.SectionId is { } section_id)
                query = query.Where(p => p.SectionId == section_id);

            if (filter?.SectionId is { } brand_id)
                query = query.Where(p => p.BrandId == brand_id);

            return query;
        }

        public IEnumerable<Section> GetSections() => TestData.Sections;
    }
}
