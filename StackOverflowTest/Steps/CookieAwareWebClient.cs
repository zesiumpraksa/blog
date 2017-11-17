using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflowTest.Steps
{
    class CookieAwareWebClient: WebClient
    {
        //pokusaj singletona
        private static CookieAwareWebClient instanceCookie = null;
        private CookieContainer  cookieContainer= new CookieContainer();
        private CookieCollection responseCookies = new CookieCollection();

        //private CookieAwareWebClient()
        //{

        //}

        public static CookieAwareWebClient InstanceCookie
        {
            get
            {
                if (instanceCookie == null)
                {
                    instanceCookie = new CookieAwareWebClient();


                }
                return instanceCookie;
            }
        }

       // public static CookieAwareWebClient Cooke { get; set; }

        private CookieAwareWebClient()
        {  
        }


        //public CookieContainer CookieContainer { get; private set; }

        //public CookieCollection ResponseCookies { get; set; }

        protected override WebRequest GetWebRequest(Uri address)
        {
            var request = (HttpWebRequest)base.GetWebRequest(address);
            request.CookieContainer = cookieContainer;
            return request;
        }

        protected override WebResponse GetWebResponse(WebRequest request)
        {
            var response = (HttpWebResponse)base.GetWebResponse(request);
            
            responseCookies = response.Cookies;
            return response;
        }
    }
}
