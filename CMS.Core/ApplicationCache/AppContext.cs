using TBDCMS.Core.Helper;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;

namespace TBDCMS.Core.ApplicationCache
{
    public sealed class AppContext
    {

        HttpContext _httpContext = null;

        private void Initialize(NameValueCollection qs, Uri uri, string rawUrl, string siteUrl)
        {
            _queryString = qs;
            _siteUrl = siteUrl;
            _currentUri = uri;
            _rawUrl = rawUrl;

        }

      

        [ThreadStatic]
        private static AppContext currentContext = null;

        private static readonly string dataKey = "AppContextStore";
        private HttpContext httpContext;
        private bool p;

        public AppContext(HttpContext context, bool includeQS)
        {
            this._httpContext = context;

            if (includeQS)
            {
                Initialize(new NameValueCollection(context.Request.QueryString), context.Request.Url, context.Request.RawUrl, GetSiteUrl());
            }
            else
            {
                Initialize(null, context.Request.Url, context.Request.RawUrl, GetSiteUrl());
            }
        }

        private string GetSiteUrl()
        {
            string hostName = _httpContext.Request.Url.Host.Replace("www.", string.Empty);
            string applicationPath = _httpContext.Request.ApplicationPath;

            if (applicationPath.EndsWith("/"))
                applicationPath = applicationPath.Remove(applicationPath.Length - 1, 1);

            return hostName + applicationPath;
        }


        public NameValueCollection _queryString = null;

        public string _siteUrl = null;

        public Uri _currentUri;

        public string _rawUrl;

        private int _languageId = 0; //default language (vietnamese)

    }
}
