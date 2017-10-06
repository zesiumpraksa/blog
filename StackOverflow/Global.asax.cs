using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace StackOverflow
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exc = Server.GetLastError();

            HttpException httpException = exc as HttpException;
            if (httpException != null)
            {
                //bool notfound;
                Server.ClearError();
                Response.Redirect("/Error/Index?id=" + exc.Message);


                //switch (httpException.GetHttpCode())
                //{
                //    case 400:
                //        Server.ClearError();
                //        Response.Redirect("/Error/BadRequest?="+ exc.Message);
                //        break;
                //    case 404:
                //        Server.ClearError();
                //        Response.Redirect("/Error/NotFound?id=" + exc.Message);
                //        //Response.RedirectToRoute("Error", new {
                //        //    controller = "Error",
                //        //    action = "NotFound" ,
                //        //    id = exc.Message
                //        //});                     

                //        break;
                //    default:
                //        Server.ClearError();
                //        Response.Redirect("/Error/Index?id="+ exc.Message);
                //        break;
                //}
                // Server.ClearError();
                //if (notfound)
                //    Response.Redirect("/Error/NotFound");
                //    //this.Response.RedirectToRoute("Default", new { controller = "Error", action = "NotFound" });
                //else
                //    this.Response.RedirectToRoute("Default", new { controller = "Error", action = "Error" });
            }


            Server.ClearError();
            //Response.Redirect("/Error/Index");

        }
    }
}
