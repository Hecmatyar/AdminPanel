using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using test.Models;
using test.AuthCustom;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using System.Net.Mail;
using System.Net;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using IService;
using System.IO;
using IService.Models;
using System.Net.Configuration;
using test.Letters;

namespace test.Controllers
{
    public class AccountController : ControllerBase
    {
        SendingLetters letter = new SendingLetters();

        public AccountController()
        {

        }
        /// <summary>
        /// страница входа на ресурс
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        
        /// <summary>
        /// log on system existing user
        /// </summary>
        /// <param name="model">login and password</param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            var user = new UserModel();
            if (_AuthenticationRequest.UserIsExist(model.UserName))
            {
                user = _AuthenticationRequest.GetUserByLP(model.UserName, model.UserPassword);
                if (user.UserConfirmedEmail)
                {
                    manager.Login(user, model.RememberMe);
                    if (IsAuthenticated)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            return View("Login");
        }

        /// <summary>
        /// log off curernt user
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            manager.Logoff();
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public ActionResult Register(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        /// <summary>
        /// регистрация нового пользвоаететя в системе
        /// </summary>
        /// <param name="model"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Exclude = "UserPhoto")] RegisterViewModel model, string returnUrl)
        { 
            //есть ли такой пользователь не зарегестрирован
            if (!_AuthenticationRequest.UserIsExist(model.UserName))
            {               
                try
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
                    //регистрация нового пользователя
                    _AuthenticationRequest.RegisterUser(model.UserName, model.UserPassword, model.UserEmail, imageData, model.UserBirth);
                    
                    LetterConfirmViewModel confirm = new LetterConfirmViewModel
                    {
                        UserName = model.UserName,
                        UserEmail = model.UserEmail,
                        UserActivationLink = Url.Action("ConfirmEmail", "Account", new
                        {
                            Token = _AuthenticationRequest.GetUserByLP(model.UserName, model.UserPassword).UserToken,
                            Email = model.UserEmail,
                            RememberMe = model.RememberMe
                        }, Request.Url.Scheme)
                    };

                    //отправка письма для подтверждения почтового адреса                    
                    letter.SendConfirmMail(confirm, this.ControllerContext);
                }
                catch (Exception e)
                {
                    string point = e.ToString();
                }
            }
            return RedirectToAction("Confirm", "Account", new { Email = model.UserEmail });
        }

        [AllowAnonymous]
        public ActionResult Confirm(string Email)
        {
            ViewBag.Email = Email;
            return View();
        }

        /// <summary>
        /// подтверждение почтового адреса
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="Email"></param>
        /// <param name="rememberMe"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult ConfirmEmail(string Token, string Email, bool rememberMe)
        {
            try
            {
                //подтверждение адреса почты
                _AuthenticationRequest.UserConfirmedEmail(Token, Email);
                UserModel user = _AuthenticationRequest.GetUserByToken(Token);
                //вход пользователя в систему
                manager.Login(user, rememberMe);
                if (IsAuthenticated)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception e)
            {
                string point = e.ToString();
            }
            return RedirectToAction("Confirm", "Account", new { Email = Email });
        }

        [AuthenticateAttribute]
        public ActionResult Manage()
        {
            return View();
        }

        /// <summary>
        /// смена пароля пользователя в его "личном кабинете"
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AuthenticateAttribute]
        [ValidateAntiForgeryToken]
        public ActionResult Manage(ChangePasswordViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                UserModel user = CurrentUser;
                _AuthenticationRequest.ChangePassword(
                    user.UserName,
                    model.OldPassword,
                    model.NewPassword,
                    user.UserToken);
            }
            catch (Exception ex)
            {
                string e = ex.ToString();
            }
            return RedirectToAction("Index", "Home");
        }


        [AllowAnonymous]
        public ActionResult Reset()
        {
            return View();
        }

        /// <summary>
        /// сброс пароля пользователя
        /// </summary>
        /// <param name="model"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Reset(ResetPasswordViewModel model, string returnUrl)
        {
            //отправка письма на почту для сброса пароля
            try
            {
                UserModel user = _AuthenticationRequest.GetUserByEmail(model.Email);
                LetterResetPasswordViewModel reset = new LetterResetPasswordViewModel
                {
                    UserName = user.UserName,
                    UserEmail = model.Email,
                    UserActivationLink = Url.Action("NewPassword", "Account", new
                    {
                        Token = user.UserToken,
                        Email = user.UserEmail
                    }, Request.Url.Scheme)
                };
                //отправка письма
                letter.SendResetPasswordMail(reset, this.ControllerContext);
            }
            catch (Exception e)
            {
                string point = e.ToString();
            }
            return RedirectToAction("Confirm", "Account", new { Email = model.Email });
        }

        /// <summary>
        /// новый пароль для пользователя, который его сбрасывает
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="Email"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult NewPassword(string Token, string Email)
        {
            ViewBag.Token = Token;
            return View();
        }        
        /// <summary>
        /// установка нового пароля
        /// </summary>
        /// <param name="model">модель с данными нового пароля</param>
        /// <param name="Token">токен</param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult SetNewPassword(SetPasswordViewModel model, string Token)
        {
            UserModel user = _AuthenticationRequest.GetUserByToken(Token);
            _AuthenticationRequest.ResetPassword(Token, model.NewPassword);
            return RedirectToAction("Login", "Account");
        }
        /// <summary>
        /// имя уже сущетсвует
        /// </summary>
        /// <param name="name">имя пользователя</param>
        /// <returns>занято ли введенное имя</returns>
        public bool NameIsEngaged(string name)
        {
            return !_AuthenticationRequest.UserIsExist(name);
        }
    }
}