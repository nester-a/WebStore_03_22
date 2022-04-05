using WebStore.DAL.Context;
using WebStore.Domain;
using WebStore.Domain.Entities;
using WebStore.Services.Interfaces;

namespace WebStore.Services.InSQL
{
    public class SqlProductData : IProductData
    {
        private readonly WebStoreDB db;

        public SqlProductData(WebStoreDB db) => this.db = db;
        public IEnumerable<Brand> GetBrand() => db.Brands;

        public IEnumerable<Product> GetProducts(ProductFilter filter = null)
        {
            IQueryable<Product> query = db.Products;

            if (filter?.SectionId is { } section_id)
                query = query.Where(p => p.SectionId == section_id);

            if (filter?.SectionId is { } brand_id)
                query = query.Where(p => p.BrandId == brand_id);

            return query;
        }

        public IEnumerable<Section> GetSections() => db.Sections;
    }
}
