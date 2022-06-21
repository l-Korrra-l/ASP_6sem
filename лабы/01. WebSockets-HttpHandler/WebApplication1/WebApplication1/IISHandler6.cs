using System;
using System.Web;

namespace WebApplication1
{
    public class IISHandler6 : IHttpHandler
    {
        /// <summary>
        /// You will need to configure this handler in the Web.config file of your 
        /// web and register it with IIS before being able to use it. For more information
        /// see the following link: https://go.microsoft.com/?linkid=8101007
        /// </summary>
        #region IHttpHandler Members

        public bool IsReusable
        {
            // Return false in case your Managed Handler cannot be reused for another request.
            // Usually this would be false in case you have some state information preserved per request.
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            HttpResponse res = context.Response;

            if (context.Request.HttpMethod == "GET")
            {
                res.ContentType = "text/html";
                res.WriteFile("../task6.html");
            }
            else if (context.Request.HttpMethod == "POST")
            {
                res.Write(int.Parse(context.Request.Form["x"]) * int.Parse(context.Request.Form["y"]));
            }
        }

        #endregion
    }
}
