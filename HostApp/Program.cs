using Business.Interfaces;
using Business.Services;
using DAL.DBContext;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.Injection;
using Unity.Resolution;

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
            objContainer.RegisterType<ServiceHost>(new InjectionConstructor(typeof(object), typeof(Uri[])));



            using (ServiceHost host = new ServiceHost(typeof(WcfService.TestService)))
            //using (ServiceHost host = objContainer.Resolve<ServiceHost>(new ParameterOverride("serviceType", typeof(WcfService.TestService))))
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
