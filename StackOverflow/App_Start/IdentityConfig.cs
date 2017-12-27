using DAL.DBContext;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Model.Models;
using Models.Models;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace StackOverflow.App_Start
{
    public class IdentityConfig
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext(() => new SOContext());
            app.CreatePerOwinContext<SOUserManager>(SOUserManager.Create);
            app.CreatePerOwinContext<SOSignInManager>(SOSignInManager.Create);
         
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,                
                LoginPath = new PathString("/User/Register"),
            });
        }        
    }

    public class SOUserManager : UserManager<User>
    {
        public SOUserManager(IUserStore<User> store) : base(store) {

            this.UserValidator = new UserValidator<User>(this)
            {
                RequireUniqueEmail = true,       
                AllowOnlyAlphanumericUserNames = true         
            };
        }

        public static SOUserManager Create(IdentityFactoryOptions<SOUserManager> options, IOwinContext context)
        {
            var manager = new SOUserManager(new UserStore<User>(context.Get<SOContext>()));
            
            manager.PasswordValidator = new PasswordValidator
            {
                RequireDigit = true,
                RequireUppercase = true,
                RequireLowercase = true,
            };

            return manager;
        }
    }

    public class SOSignInManager : SignInManager<User, string>
    {
        
        public SOSignInManager(SOUserManager userManager, IAuthenticationManager authenticationManager) //<TUser, TKey>
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(User user)
        {
            return user.GenerateUserIdentityAsync((SOUserManager)UserManager);
        }

        public static SOSignInManager Create(IdentityFactoryOptions<SOSignInManager> options, IOwinContext context)
        {
            return new SOSignInManager(context.GetUserManager<SOUserManager>(), context.Authentication);
        }
    }


}