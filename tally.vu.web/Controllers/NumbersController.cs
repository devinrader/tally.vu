using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Twilio;

namespace tallyvu.Controllers
{
    public class NumbersController : Controller
    {
        [HttpPost]
        public ActionResult Index(string query)
        {
            var client = new TwilioRestClient(Credentials.AccountSid, Credentials.AuthToken);
            var result = client.ListAvailableLocalPhoneNumbers("US", new AvailablePhoneNumberListRequest() { AreaCode = query });

            if (result.RestException != null)
                return Json(null);

            return Json(result.AvailablePhoneNumbers.Take(7));
        }
    }
}
