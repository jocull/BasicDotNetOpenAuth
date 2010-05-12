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
using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OpenId;
using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;
using DotNetOpenAuth.OpenId.RelyingParty;
using DotNetOpenAuth.OpenId.Extensions.AttributeExchange;

namespace OpenIDWebsite
{
    public partial class _Default : System.Web.UI.Page
    {
        #region Page Variables
        protected static string googleEndpoint = "https://www.google.com/accounts/o8/id";
        protected static string YahooEndpoint = "https://me.yahoo.com";
        protected static string AOLEndpoint = "http://openid.aol.com";

        protected string userDefinedEndpoint
        {
            get;
            set;
        }
        #endregion

        #region Page Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated)
            {
                // Do UI stuff...
                pnlSelectProvider.Visible = true;
                pnlUserInfo.Visible = false;

                // OpenID API setup...
                OpenIdRelyingParty openid = this.createRelyingParty();

                // Retrieve response from request header and input into OpenIDAuth framework.
                var response = openid.GetResponse();

                // Perform logic.
                if (response != null)
                {
                    switch (response.Status)
                    {
                        case AuthenticationStatus.Authenticated:
                            // This is where you would look for any OpenID extension responses included
                            // in the authentication assertion.

                            var fetch = response.GetExtension<FetchResponse>();
                            if (fetch != null)
                            {
                                //The page does some reloads and refreshes, dumping out any local scope variables.
                                //We throw them in the sessions so that they can be stored for later.
                                Session["email"] = fetch.GetAttributeValue(WellKnownAttributes.Contact.Email);
                                Session["first"] = fetch.GetAttributeValue(WellKnownAttributes.Name.First);
                                Session["last"] = fetch.GetAttributeValue(WellKnownAttributes.Name.Last);
                                Session["full"] = fetch.GetAttributeValue(WellKnownAttributes.Name.FullName);
                                Session["alias"] = fetch.GetAttributeValue(WellKnownAttributes.Name.Alias);
                            }

                            // Use FormsAuthentication to tell ASP.NET that the user is now logged in,
                            // with the OpenID Claimed Identifier as their username.
                            FormsAuthentication.RedirectFromLoginPage(response.ClaimedIdentifier, false);

                            OpenID.State.FriendlyLoginName = response.FriendlyIdentifierForDisplay;
                            OpenID.State.Identifier = response.ClaimedIdentifier.ToString();

                            break;
                        case AuthenticationStatus.Canceled:
                            // No functionality implemented for this status in this example.
                            break;
                        case AuthenticationStatus.Failed:
                            // No functionality implemented for this status in this example.
                            break;
                    }
                }
            }
            else
            {
                // Perform more UI logic if we're authenticated.
                pnlSelectProvider.Visible = false;
                pnlUserInfo.Visible = true;
                lblFirstName.Text = "<strong>First Name:</strong> " + Session["first"];
                lblLastName.Text = "<strong>Last Name:</strong> " + Session["last"];
                lblFullName.Text = "<strong>Full Name:</strong> " + Session["full"];
                lblEmail.Text = "<strong>Email:</strong> " + Session["email"];
                lblAlias.Text = "<strong>Alias:</strong> " + Session["alias"];
                lblFriendlyIdentifier.Text = "<strong>Friendly Identifier:</strong> " + OpenID.State.FriendlyLoginName;
                lblIdentifier.Text = "<strong>True Identifier:</strong> " + OpenID.State.Identifier;
            }
        }

        protected void openIDGoogle_Click(object sender, EventArgs e)
        {
            loginWithOpenID(googleEndpoint);
        }

        protected void openIDAOL_Click(object sender, EventArgs e)
        {
            loginWithOpenID(AOLEndpoint);
        }

        protected void openIDFB_Click(object sender, EventArgs e)
        {
            Server.Transfer("~/FacebookLogin.aspx");
        }

        protected void openIDYahoo_Click(object sender, EventArgs e)
        {
            loginWithOpenID(YahooEndpoint);
        }

        protected void logout_Click(object sender, EventArgs e)
        {
            // Kill ASP.NET FormsAuth cookie.
            FormsAuthentication.SignOut();

            // Signout from OpenID Provider.
            DotNetOpenAuth.OpenId.RelyingParty.OpenIdRelyingPartyControlBase.LogOff();

            //Dump our current session.
            Session.Abandon();

            // Redirect to clean UI.
            Response.Redirect("~/");
        }
        #endregion

        #region Custom Methods
        private void loginWithOpenID(string openIDEndpoint)
        {
            try
            {
                using (OpenIdRelyingParty openid = this.createRelyingParty())
                {
                    IAuthenticationRequest request = openid.CreateRequest(openIDEndpoint);

                    var fetch = new FetchRequest();
                    fetch.Attributes.AddRequired(WellKnownAttributes.Contact.Email);
                    fetch.Attributes.AddRequired(WellKnownAttributes.Name.FullName);
                    fetch.Attributes.AddRequired(WellKnownAttributes.Name.First);
                    fetch.Attributes.AddRequired(WellKnownAttributes.Name.Last);
                    fetch.Attributes.AddRequired(WellKnownAttributes.Name.Alias);
                    request.AddExtension(fetch);

                    // Send your visitor to their Provider for authentication.
                    request.RedirectToProvider();
                }
            }
            catch (Exception ex)
            {
                // Do nothing -- No code implemented in this example.
            }
        }

        private OpenIdRelyingParty createRelyingParty()
        {
            OpenIdRelyingParty openid = new OpenIdRelyingParty();
            int minsha, maxsha, minversion;
            if (int.TryParse(Request.QueryString["minsha"], out minsha))
            {
                openid.SecuritySettings.MinimumHashBitLength = minsha;
            }
            if (int.TryParse(Request.QueryString["maxsha"], out maxsha))
            {
                openid.SecuritySettings.MaximumHashBitLength = maxsha;
            }
            if (int.TryParse(Request.QueryString["minversion"], out minversion))
            {
                switch (minversion)
                {
                    case 1: openid.SecuritySettings.MinimumRequiredOpenIdVersion = ProtocolVersion.V10; break;
                    case 2: openid.SecuritySettings.MinimumRequiredOpenIdVersion = ProtocolVersion.V20; break;
                    default: throw new ArgumentOutOfRangeException("minversion");
                }
            }
            return openid;
        }
        #endregion
    }
}