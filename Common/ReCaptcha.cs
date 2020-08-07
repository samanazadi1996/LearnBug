using Newtonsoft.Json.Linq;
using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;

namespace Common.ReCaptcha
{
    [Serializable]
    public class InvalidKeyException : ApplicationException
    {
        public InvalidKeyException() { }
        public InvalidKeyException(string message) : base(message) { }
        public InvalidKeyException(string message, Exception inner) : base(message, inner) { }
    }

    public class ReCaptchaAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                var privateKey = System.Configuration.ConfigurationManager.AppSettings["ReCaptcha.PrivateKey"];
                object gRecaptchaResponse = filterContext.ActionParameters["foo"];
                HttpClient httpClient = new HttpClient();
                var res = httpClient.GetAsync($"https://www.google.com/recaptcha/api/siteverify?secret={privateKey}&response={gRecaptchaResponse}").Result;
                if (res.StatusCode != HttpStatusCode.OK)
                    ((Controller)filterContext.Controller).ModelState.AddModelError("Message", "Captcha words typed incorrectly");

                string JSONres = res.Content.ReadAsStringAsync().Result;
                dynamic JSONdata = JObject.Parse(JSONres);

                if (JSONdata.success != "true")
                    ((Controller)filterContext.Controller).ModelState.AddModelError("Message", "Captcha words typed incorrectly");

                base.OnActionExecuting(filterContext);

            }
            catch (Exception)
            {
                ((Controller)filterContext.Controller).ModelState.AddModelError("Message", "Errore reCaptcha");
            }
        }
    }
    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString GenerateCaptcha(this HtmlHelper helper)
        {
            var publicKey = ConfigurationManager.AppSettings["ReCaptcha.PublicKey"];
            string htmlInjectString = @"<input type='hidden' id='foo' name='foo' />
     <script src = 'https://www.google.com/recaptcha/api.js?render=" + publicKey + @"'></script>
    <script>
         grecaptcha.ready(function() {
                grecaptcha.execute('" + publicKey + @"', { action: 'ReCaptcha' }).then(function(token) {
                    document.getElementById('foo').value = token;
                });
            });
     </script>   

";

            if (string.IsNullOrWhiteSpace(publicKey))
                throw new InvalidKeyException("ReCaptcha.PublicKey missing from appSettings");


            return MvcHtmlString.Create(htmlInjectString);
        }
    }
}
