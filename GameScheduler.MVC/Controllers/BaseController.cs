using GameScheduler.MVC.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace GameScheduler.MVC.Controllers
{

	public abstract class BaseController : Controller
    {
        protected readonly IMediator _mediator;

        protected BaseController(IMediator mediator)
        {
            _mediator = mediator;
        }

        protected ActionResult RedirectToErrorPage<TController>(Exception ex) where TController : BaseController
        {
            var myLog = Log.ForContext<TController>();
            myLog.Error(ex.Message);
            return View("Error", new ErrorViewModel(ex));
        }

    }
}
