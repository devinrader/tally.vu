using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tallyvu.DataAccess;
using tallyvu.Models;
using Twilio;
using SignalR;
using SignalR.Hubs;

namespace tallyvu.Controllers
{
    public class PollsController : Controller
    {
        private readonly IRepository _repository;

        public PollsController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult Index(string key)
        {
            var poll = _repository.Single<Poll>(key, new string[] { "Options", "Votes" });

            if (poll == null)
                throw new HttpException(404, "The requested Poll was not found");

            this.ViewBag.Votes = CountVotes(poll);

            return View(poll);
        }

        [HttpGet]
        public ActionResult List()
        {
            var polls = _repository.All<Poll>(new string[] { "Options", "Votes" });

            return View(polls);
        }

        //[HttpPost]
        public ActionResult Tally(TwilioViewModel vm)
        {
            var poll = _repository.Query<Poll>(p => p.PhoneNumber == vm.To, new string[] { "Options", "Votes" }).FirstOrDefault();

            if (poll == null)
                throw new HttpException(404, "The requested Poll was not found");

            var value = vm.Body.Trim().ToLower();

            if (poll != null)
            {
                if ( poll.Options.Where(o=>o.Shortcut == value).FirstOrDefault() !=null)
                {
                    poll.Votes.Add(new Vote() { From = vm.From, Value = value });

                    var context = GlobalHost.ConnectionManager.GetHubContext<PollHub>();

                    context.Clients[poll.Key].updateVotes( CountVotes(poll).ToArray() );
                }
            }

            return new EmptyResult();
        }
 
        private IEnumerable<PollResultViewModel> CountVotes(Poll poll)
        {
            var results = from o in poll.Options
                          orderby o.Shortcut descending
                          select new PollResultViewModel() {
                            Option = o.Answer,
                            Count = (from v in poll.Votes where v.Value == o.Shortcut select v).Count()
                          };
            return results;
        }
    }
}
