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
using System.Web.Routing;

namespace test.Areas.Admin.Controllers
{
    [AuthenticateAttribute(RolesEnum.Admin)]
    public class UsersController : test.Controllers.ControllerBase
    {
        public UsersController()
        {

        }
        // GET: Admin/Users
        public ActionResult Index()
        {            
            return View();
        }
        [HttpPost]
        public PartialViewResult UserTable(int? page, string searchString)
        {
            int pageSize = 2;
            int pageNumber = (page ?? 1);
            ViewBag.page = pageNumber;
            ViewBag.countPage = _AdminService.GetPageCount(searchString, pageSize);
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Exclude = "UserPhoto")] CreateUserViewModel model, int[] selectedRole)
        {
            if (ModelState.IsValid)
            {
                byte[] imageData = null;
                if (Request.Files.Count > 0)
                {
                    HttpPostedFileBase poImgFile = Request.Files["UserPhoto"];

                    using (var binary = new BinaryReader(poImgFile.InputStream))
                    {
                        imageData = binary.ReadBytes(poImgFile.ContentLength);
                    }
                }
                RolesEnum[] roles = new RolesEnum[selectedRole.Length];
                for (int i = 0; i < selectedRole.Length; i++)
                {
                    roles[i] = (RolesEnum)Enum.ToObject(typeof(RolesEnum), selectedRole[i] - 1);
                }

                _AdminService.RegisterUser(model.UserName,
                    model.UserPassword,
                    model.UserEmail, roles, imageData);
                return RedirectToAction("/Index");
            }

            List<RolesEnum> ur = Enum.GetValues(typeof(RolesEnum)).Cast<RolesEnum>().ToList();
            ViewBag.UnRoles = ur;
            return View(model);
        }

        /// <summary>
        /// удаление выбранного пользователя 
        /// </summary>
        /// <param name="Token"></param>
        /// <returns></returns>
        public ActionResult Delete(string Token)
        {
            _AdminService.DeleteUser(Token);
            return RedirectToAction("/Index");
        }
        public ActionResult LogOff()
        {
            manager.Logoff();
            return Redirect(Url.Content("~/Home/Index"));
        }

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
        [HttpPost]
        public ActionResult EditUserInfo([Bind(Exclude = "NewUserPhoto")]EditUserViewModel edit)
        {
            byte[] imageData = null;
            if (Request.Files.Count > 0)
            {
                HttpPostedFileBase poImgFile = Request.Files["NewUserPhoto"];
                using (var binary = new BinaryReader(poImgFile.InputStream))
                {
                    imageData = binary.ReadBytes(poImgFile.ContentLength);
                }
            }
            _AdminService.ChangeUserInfo(edit.UserId, edit.UserEmail, DateTime.Now, imageData);
            return RedirectToAction("/Index");
            //return RedirectToAction("/Index", new RouteValueDictionary(
            //    new { controller = "Users", action = "Index", page = page }));
        }

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
        [HttpPost]
        public ActionResult ResetPassword(EditUserViewModel edit)
        {
            _AdminService.ChangeUserPassword(edit.UserId, edit.NewUserPassword);
            return RedirectToAction("/Index");
        }

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
        [HttpPost]
        public ActionResult EditUserRoles(EditUserViewModel edit, int[] selectedRole)
        {
            var roles = new RolesEnum[selectedRole.Length];
            for (int i = 0; i < selectedRole.Length; i++)
            {
                roles[i] = (RolesEnum)Enum.ToObject(typeof(RolesEnum), selectedRole[i] - 1);
            }
            _AdminService.ChangeUserRoles(edit.UserId, roles);
            return RedirectToAction("/Index");
        }
    }
}