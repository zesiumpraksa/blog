using Business.Interfaces;
using Business.Services;
using DAL.DBContext;
using DAL.Interfaces;
using Microsoft.Practices.Unity;
using System;
using Unity.Wcf;
using WcfService;

namespace HostApp
{
    class Program
    {
        static void Main(string[] args)
        {


            IUnityContainer objContainer = new UnityContainer();
            objContainer.RegisterType<IBlogService, BlogService>();
            objContainer.RegisterType<IAutorService, AuthorService>();
            objContainer.RegisterType<ISOContext, SOContext>();
            objContainer.RegisterType<IWcfService, WcfService.WcfService>();
            objContainer.RegisterType<IBlogWcfService, WcfService.WcfService>();
            objContainer.RegisterType<IAuthorWcfService, WcfService.WcfService>();

            using (UnityServiceHost host = new UnityServiceHost(objContainer, typeof(WcfService.WcfService)))
            {
                //raad configuration file(endpoint parameters)
                //and open communication chanel for clients

                host.Open();
                Console.WriteLine("Host started @" + DateTime.Now.ToString());
                Console.ReadKey();
            }
        }
    }
}
