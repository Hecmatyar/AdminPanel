using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IService;
using test.AuthCustom;
using test.Controllers;
using test.Areas.Admin.Models;
using System.IO;
using IService.Models;

namespace test.Areas.Admin.Controllers
{
    [AuthenticateAttribute(RolesEnum.Admin)]
    public class UsersController : test.Controllers.ControllerBase
    {
        public UsersController()
        {

        }
        // GET: Admin/Users
        /// <summary>
        /// загрузка начальной страницы
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// загрузка таблицы пользователей
        /// </summary>
        /// <param name="page">номер страницы</param>
        /// <param name="searchString">строка поиска</param>
        /// <returns>таблица пользователей на указаннной страницей</returns>
        [HttpPost]
        public PartialViewResult UserTable(int? page, string searchString)
        {
            int pageSize = 2;
            int pageNumber = (page ?? 1);           
            int countPage = _AdminService.GetPageCount(searchString, pageSize);
            if (pageNumber > countPage)
                pageNumber = countPage;
            ViewBag.page = pageNumber;
            ViewBag.countPage = countPage;
            List<UserModel> userlist = _AdminService.GetUserList(searchString, pageSize, pageNumber);
            return PartialView("ListUserPartial", userlist);
        }
        /// <summary>
        /// создание нового пользователя в панели администратора
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            var ur = Enum.GetValues(typeof(RolesEnum)).Cast<RolesEnum>().ToList();
            ViewBag.UnRoles = ur;
            return View();
        }
        /// <summary>
        /// создание нового опльзователя
        /// </summary>
        /// <param name="model">createuser модель</param>
        /// <param name="selectedRole">список выбранных ролей</param>
        /// <returns>страница добавления пользователя</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {                
                _AdminService.RegisterUser(model.UserName, model.UserPassword);
                return RedirectToAction("/Index");
            }
            return View(model);
        }

        /// <summary>
        /// удаление выбранного пользователя 
        /// </summary>
        /// <param name="Token">токен пользователя</param>
        /// <returns>confirm на удаление пользователя</returns>
        public ActionResult Delete(string Token)
        {
            _AdminService.DeleteUser(Token);
            return RedirectToAction("/Index");
        }
        /// <summary>
        /// выход
        /// </summary>
        /// <returns>переход на стартовую страницу</returns>
        public ActionResult LogOff()
        {
            manager.Logoff();
            return Redirect(Url.Content("~/Home/Index"));
        }

        /// <summary>
        /// частичное представление для изменения личной информации пользователя
        /// </summary>
        /// <param name="id">id пользователя</param>
        /// <returns>модальное окно личной информации</returns>
        [HttpPost]
        public PartialViewResult EditUserPartial(int id)
        {
            UserModel user = _AuthenticationRequest.GetUserById(id);
            EditUserViewModel edit = new EditUserViewModel
            {
                UserId = id,
                UserName = user.UserName,
                UserEmail = user.UserEmail,
                UserPhoto = user.UserPhoto
            };
            return PartialView("EditUserPartial", edit);
        }
        /// <summary>
        /// изменение личной информации пользователя
        /// </summary>
        /// <param name="edit">модель редактирования личных данных</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditUserInfo(EditUserViewModel edit)
        {
            byte[] userphoto = null;
            if (edit.File[0] != null)
            {
                MemoryStream target = new MemoryStream();
                edit.File.First().InputStream.CopyTo(target);
                userphoto = target.ToArray();
            }           
            _AdminService.ChangeUserInfo(edit.UserId, edit.UserEmail, DateTime.Now, userphoto);
            return RedirectToAction("/Index");
            //return RedirectToAction("/Index", new RouteValueDictionary(
            //    new { controller = "Users", action = "Index", page = page }));
        }
        /// <summary>
        /// частичное представление для сброса пароля пользователя
        /// </summary>
        /// <param name="id">id пользователя</param>
        /// <returns>модальное окно сброса пароля</returns>
        [HttpPost]
        public PartialViewResult ResetPasswordPartial(int id)
        {
            UserModel user = _AuthenticationRequest.GetUserById(id);
            EditUserViewModel edit = new EditUserViewModel
            {
                UserId = id,
                UserName = user.UserName,
                NewUserPassword = ""
            };
            return PartialView("EditPasswordPartial", edit);
        }
        /// <summary>
        /// сброс пароля пользователя на новый
        /// </summary>
        /// <param name="edit">модель редактирования пароля данных</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ResetPassword(EditUserViewModel edit)
        {
            _AdminService.ChangeUserPassword(edit.UserId, edit.NewUserPassword);
            return RedirectToAction("/Index");
        }

        /// <summary>
        /// редактирование ролей пользователя в системе
        /// </summary>
        /// <param name="id">id пользователя</param>
        /// <returns>модальное окно изменения ролей пользователя</returns>
        [HttpPost]
        public PartialViewResult EditRolePartial(int id)
        {
            UserModel user = _AuthenticationRequest.GetUserById(id);
            EditUserViewModel edit = new EditUserViewModel
            {
                UserId = id,
                UserName = user.UserName,
                UserRoles = user.UserRoles,
            };
            var ur = Enum.GetValues(typeof(RolesEnum)).Cast<RolesEnum>().ToList();
            ViewBag.UnRoles = ur.Where(a => !user.UserRoles.Any(r => r.Name == a));
            return PartialView("EditRolePartial", edit);
        }
        /// <summary>
        /// редактирование ролей пользователя
        /// </summary>
        /// <param name="edit">модель редактирования ролей пользователя</param>
        /// <param name="selectedRole">выбранные роли</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditUserRoles(EditUserViewModel edit)
        {
            var roles = new RolesEnum[edit.selectedRole.Length];
            for (int i = 0; i < edit.selectedRole.Length; i++)
            {
                roles[i] = (RolesEnum)Enum.ToObject(typeof(RolesEnum), edit.selectedRole[i] - 1);
            }
            _AdminService.ChangeUserRoles(edit.UserId, roles);
            return RedirectToAction("/Index");
        }
    }
}