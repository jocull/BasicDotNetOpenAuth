using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using DotNetOpenAuth.OpenId.Extensions.AttributeExchange;
using DotNetOpenAuth.OpenId.Extensions.ProviderAuthenticationPolicy;
using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;

namespace OpenID
{
    /// <summary>
    /// Strong-typed bag of session state.
    /// </summary>
    /// <remarks>
    /// DO NOT PUT IN APP_CODE FOLDER OF AN ASP.NET WEB APPLICATION -- will cause identical compilation error.
    /// </remarks>
    public class State
    {
        public static ClaimsResponse ProfileFields
        {
            get { return HttpContext.Current.Session["ProfileFields"] as ClaimsResponse; }
            set { HttpContext.Current.Session["ProfileFields"] = value; }
        }

        public static string Identifier
        {
            get { return HttpContext.Current.Session["Identifier"] as string; }
            set { HttpContext.Current.Session["Identifier"] = value; }
        }

        public static FetchResponse FetchResponse
        {
            get { return HttpContext.Current.Session["FetchResponse"] as FetchResponse; }
            set { HttpContext.Current.Session["FetchResponse"] = value; }
        }

        public static string FriendlyLoginName
        {
            get { return HttpContext.Current.Session["FriendlyUsername"] as string; }
            set { HttpContext.Current.Session["FriendlyUsername"] = value; }
        }

        public static PolicyResponse PapePolicies
        {
            get { return HttpContext.Current.Session["PapePolicies"] as PolicyResponse; }
            set { HttpContext.Current.Session["PapePolicies"] = value; }
        }

        public static string GoogleAccessToken
        {
            get { return HttpContext.Current.Session["GoogleAccessToken"] as string; }
            set { HttpContext.Current.Session["GoogleAccessToken"] = value; }
        }

        public static void Clear()
        {
            ProfileFields = null;
            FetchResponse = null;
            FriendlyLoginName = null;
            PapePolicies = null;
            GoogleAccessToken = null;
        }
    }
}