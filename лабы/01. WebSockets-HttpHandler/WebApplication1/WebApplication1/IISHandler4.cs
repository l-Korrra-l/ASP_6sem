﻿using System;
using System.Web;

namespace WebApplication1
{
    public class IISHandler4 : IHttpHandler
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
            res.ContentType = "text/plain";
            res.Write(int.Parse(context.Request.QueryString["X"]) + int.Parse(context.Request.QueryString["Y"]));
        }

        #endregion
    }
}
