using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using InstagramApplication.Models;
using InstagramWrapper.EndPoints;
using InstagramWrapper.Model;
using InstagramWrapper.Service;
using Newtonsoft.Json;

namespace InstagramApplication.Controllers
{
    public class InstagramController : Controller
    {
        // GET: Instagram
        public ActionResult Index()
        {
            HomeModel userModel = new HomeModel(); 
            Users userEndpoints = new Users();
            if (!Request.IsAuthenticated)
            {
                var code = Request.QueryString["code"];
                if (!string.IsNullOrEmpty(code))
                {
                    InstagramAuth instagramAuth = new InstagramAuth();
                    InstaConfig instagramConfig = new InstaConfig();
                    instagramConfig.redirect_uri = "http://localhost:58065/Instagram/Index";
                    instagramConfig.client_secret = "f975c1bf53f64aecb7bf63dda0d996d7";
                    instagramConfig.client_id = "01b344efcdb445068a20f30391bea917";
                    userModel.user = instagramAuth.GetAccessToken(code, instagramConfig);
                    
                    FormsAuthentication.SetAuthCookie(userModel.user.access_token, true);
                    userModel.UserFeed = userEndpoints.GetUserMedia("2368860595", userModel.user.access_token);
                }
            }
            else
            {
                userModel.user = new OuthUser();
                userModel.user.access_token = HttpContext.User.Identity.Name;
                userModel.UserFeed = userEndpoints.GetUserMedia("2368860595", userModel.user.access_token);
            }
            return View(userModel.UserFeed);
        }

       

    }
}