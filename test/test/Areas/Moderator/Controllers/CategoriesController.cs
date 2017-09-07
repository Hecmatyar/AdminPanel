using IService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using test.Areas.Moderator.Models;
using test.AuthCustom;

namespace test.Areas.Moderator.Controllers
{
    [AuthenticateAttribute(RolesEnum.Moderator)]
    public class CategoriesController : ControllerBase
    {
        // GET: Moderator/Categories
        public ActionResult Categories(CategoryViewModel categories)
        {
            int pageSize = 5;
            int pageNumber = (categories.PageNumber ?? 1);
            if (categories.SearchField != null)
                pageNumber = 1;
            int countPage = _ModeratorService.GetPageCountCategory(
                categories.SearchField,                
                pageSize);

            if (pageNumber > countPage)
                pageNumber = countPage;

            categories.PageCount = countPage;
            categories.PageNumber = pageNumber;

            categories.Categories = _ModeratorService.GetCategoryList(categories.SearchField,
                pageSize,
                pageNumber);
            return View(categories);
        }
        [HttpGet]
        public ActionResult EditCategory(int id)
        {
            return View();
        }
        [HttpGet]
        public ActionResult CreateCategory()
        {
            return View();
        }
    }
}