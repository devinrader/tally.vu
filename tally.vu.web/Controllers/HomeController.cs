using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Twilio;
using tallyvu.DataAccess;
using tallyvu.Models;

namespace tallyvu.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository _repository;

        public HomeController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Poll poll)
        {
            var client = new TwilioRestClient(Credentials.AccountSid, Credentials.AuthToken);
            var result = client.AddIncomingPhoneNumber(new PhoneNumberOptions() {
                PhoneNumber = poll.PhoneNumber, 
                SmsUrl = "http://verdict.io/Polls/Tally", 
                SmsMethod = "POST" 
            });

            if (result.RestException != null)
            {
                // TODO: Fix this 
                return new EmptyResult(); 
            }

            poll.PhoneNumber = result.PhoneNumber;

            _repository.Add(poll);

            return Redirect(string.Format("/{0}", poll.Key));
        }
    }
}
