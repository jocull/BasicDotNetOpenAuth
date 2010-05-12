using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace OpenID
{
    /// <summary>
    /// Login page for Facebook authentication.
    /// </summary>
    public partial class FacebookLogin : System.Web.UI.Page
    {
        private Facebook.Rest.Api api;
        private Facebook.Session.ConnectSession connectSession;

        protected string FBAPIKey = "YOUR_FACEBOOK_API_KEY";
        private string FBSecretKey = "YOUR_FACEBOOK_SECRET_KEY";

        protected void Page_Load(object sender, EventArgs e)
        {
            // NOTE: The following code must be encapsulated in a try/catch block, otherwise old FB session
            //       keys will cause an exception. Let the FBML/JS on the front-end look for an expired cookie
            //       and re-authenticate accordingly.
            try
            {
                connectSession = new Facebook.Session.ConnectSession(FBAPIKey, FBSecretKey);
                if (!connectSession.IsConnected())
                {
                    // "Please sign-in with Facebook."
                    // Perform all UI functionality here -- user is not yet authenticated.
                }
                else
                {
                    // Once connected, get current user info from the FB API.
                    api = new Facebook.Rest.Api(connectSession);
                    Facebook.Schema.user usr = api.Users.GetInfo();

                    //The page does some reloads and refreshes, dumping out any local scope variables.
                    //We throw them in the sessions so that they can be stored for later.
                    Session["email"] = usr.proxied_email;
                    Session["first"] = usr.first_name;
                    Session["last"] = usr.last_name;
                    Session["full"] = usr.name;

                    // Set custom site variables -- "friendly login name" & identifier
                    OpenID.State.FriendlyLoginName = usr.uid.ToString();
                    OpenID.State.Identifier = usr.uid.ToString();

                    // Create FormsAuthentication ticket...
                    FormsAuthentication.RedirectFromLoginPage(usr.uid.ToString(), false);
                }
            }
            catch (Exception ex)
            {
                // Do nothing...We're expecting an exception.
            }
        }
    }
}