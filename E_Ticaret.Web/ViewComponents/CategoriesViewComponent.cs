using E_Ticaret.Business.Abstract;
using E_Ticaret.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

namespace E_Ticaret.Web.ViewComponents
{
    public class CategoriesViewComponent:ViewComponent
    {
        private ICategoryService _categoryService;

        public CategoriesViewComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IViewComponentResult Invoke()
        {
            if (RouteData.Values["category"] != null)
                ViewBag.SelectedCategory = RouteData?.Values["category"];

            return View(_categoryService.GetAll());
        }
    }
}
