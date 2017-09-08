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
        /// <summary>
        /// постраничное отображенеи категорий на старнице
        /// </summary>
        /// <param name="categories">модель с данными поиска и списка категорий на старнцие</param>
        /// <returns></returns>
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
        /// <summary>
        /// редактирование категории
        /// </summary>
        /// <param name="category">модель категории</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditCategory(EditCreateCategory category)
        {
            _ModeratorService.EditCategory(category.Id, category.Name);
            return RedirectToAction("/Categories");
        }
        /// <summary>
        /// создание новой категории
        /// </summary>
        /// <param name="category">модель категории</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateCategory(EditCreateCategory category)
        {
            _ModeratorService.CreateCategory(category.Name);
            return RedirectToAction("/Categories");
        }
        /// <summary>
        /// удаление выбранной категории
        /// </summary>
        /// <param name="Id">id категории которую надо удалить</param>
        /// <returns></returns>
        public ActionResult DeleteCategory(int Id)
        {
            _ModeratorService.DeleteCategory(Id);
            return RedirectToAction("/Categories");
        }
    }
}