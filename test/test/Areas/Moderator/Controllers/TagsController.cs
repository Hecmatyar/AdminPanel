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
    public class TagsController : ControllerBase
    {        
        /// <summary>
        /// постраничное отображенеи категорий на старнице
        /// </summary>
        /// <param name="categories">модель с данными поиска и списка категорий на старнцие</param>
        /// <returns></returns>
        public ActionResult Tags(TagViewModel tags)
        {
            int pageSize = 11;
            int pageNumber = (tags.PageNumber ?? 1);
            if (tags.SearchField != null)
                pageNumber = 1;
            int countPage = _ModeratorService.GetPageCountCategory(
                tags.SearchField,
                pageSize);

            if (pageNumber > countPage)
                pageNumber = countPage;

            tags.PageCount = countPage;
            tags.PageNumber = pageNumber;

            tags.Tags = _ModeratorService.GetTagList(tags.SearchField,
                pageSize,
                pageNumber);
            return View(tags);
        }
        /// <summary>
        /// редактирование тэга
        /// </summary>
        /// <param name="tag">модель тэга</param>
        /// <returns>редирект на стартовую страницу</returns>
        [HttpPost]
        public ActionResult EditTag(EditCreateTag tag)
        {
            _ModeratorService.EditTag(tag.Id, tag.Name);
            return RedirectToAction("/Tags");
        }
        /// <summary>
        /// создание нового тэга
        /// </summary>
        /// <param name="tag">модель тэга</param>
        /// <returns>редирект на стартовую страницу</returns>
        [HttpPost]
        public ActionResult CreateTag(EditCreateTag tag)
        {
            _ModeratorService.CreateTag(tag.Name);
            return RedirectToAction("/Tags");
        }
        /// <summary>
        /// удаление выбранного тэга
        /// </summary>
        /// <param name="Id">id тэга который надо удалить</param>
        /// <returns>редирект на стартовую страницу</returns>
        public ActionResult DeleteTag(int Id)
        {
            _ModeratorService.DeleteTag(Id);
            return RedirectToAction("/Tags");
        }
    }
}