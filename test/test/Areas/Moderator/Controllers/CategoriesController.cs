using IService.Models;
using IService.Models.Moderator;
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
            int pageSize = 10;
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
        /// <param name="id"></param>
        /// <returns>представление с данными категории для редактирвоания</returns>
        [HttpPost]
        public ActionResult EditCategoryPartial(int id)
        {
            EditCreateCategoryModel category = _ModeratorService.GetEditCategory(id);
            return View("_EditCategory", category);
        }

        /// <summary>
        /// редактирование категории
        /// </summary>
        /// <param name="category">модель категории</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditCategory(EditCreateCategoryModel category)
        {
            if (category.ParentId == 0) category.ParentId = null;
            _ModeratorService.EditCategory(category.Id, category.Name, category.UrlName, category.ParentId);
            return RedirectToAction("/Categories");
        }

        /// <summary>
        /// создание категории
        /// </summary>       
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateCategoryPartial()
        {
            EditCreateCategoryModel category = _ModeratorService.GetCreateCategory();
            //var list = category.ListCategories;           
            //var start = list.Where(a => a.ParentId == null).ToList();
            //foreach (var item in start)
            //{             
            //    l1.Add(item);
            //    Parent(1, item, list);
            //}
            //category.ListCategories = l1;
            return View("_CreateCategory", category);
        }
        //public List<CategoryModel> l1 = new List<CategoryModel>();
        //public void Parent(int level, CategoryModel parent, List<CategoryModel> l)
        //{
        //    var t = l.Where(a => a.ParentId == parent.Id);
        //    if (t.Count() != 0)
        //    {
        //        foreach (var item in t)
        //        {
        //            var pp = item;                    
        //            pp.Name = level.ToString() + item.Name;
        //            l1.Add(pp);

        //            Parent(level + 1, item, l);
        //        }
        //    }            
        //}

        /// <summary>
        /// создание новой категории
        /// </summary>
        /// <param name="category">модель категории</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateCategory(EditCreateCategoryModel category)
        {
            if (category.ParentId == 0) category.ParentId = null;
            _ModeratorService.CreateCategory(category.Name, category.UrlName, category.ParentId);
            return RedirectToAction("/Categories");
        }

        /// <summary>
        /// удаление выбранной категории
        /// </summary>
        /// <param name="Id">id категории которую надо удалить</param>
        /// <returns></returns>
        public ActionResult Delete(int Id)
        {
            _ModeratorService.DeleteCategory(Id);
            return RedirectToAction("/Categories");
        }
    }
}