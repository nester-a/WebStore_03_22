using Microsoft.AspNetCore.Mvc;
using WebStore.Services.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Components
{
    public class SectionsViewComponent : ViewComponent
    {
        private readonly IProductData productData;

        public SectionsViewComponent(IProductData productData) => this.productData = productData;
        public IViewComponentResult Invoke()
        {
            var sections = productData.GetSections();

            var parent_sections = sections.Where(s => s.ParentId is null);

            var parent_sections_views = parent_sections
                .Select(s => new SectionViewModel()
                {
                    Id = s.Id,
                    Name = s.Name,
                    Order = s.Order,
                }).ToList();

            foreach (var parent_section in parent_sections_views)
            {
                var childs = sections.Where(s => s.ParentId == parent_section.Id);

                foreach (var child_sections in childs)
                {
                    parent_section.ChildSections.Add(new SectionViewModel
                    {
                        Id = child_sections.Id,
                        Name = child_sections.Name,
                        Order = child_sections.Order,
                        Parent = parent_section,
                    });
                }
                parent_section.ChildSections.Sort((a, b) => Comparer<int>.Default.Compare(a.Order, b.Order));
            }

            parent_sections_views.Sort((a, b) => Comparer<int>.Default.Compare(a.Order, b.Order));

            return View(parent_sections_views);
        }
    }
}
