﻿using WebStore.Data;
using WebStore.Domain.Entities;
using WebStore.Services.Interfaces;

namespace WebStore.Services
{
    public class InMemoryProductData : IProductData
    {
        public IEnumerable<Brand> GetBrand() => TestData.Brands;

        public IEnumerable<Section> GetSections() => TestData.Sections;
    }
}