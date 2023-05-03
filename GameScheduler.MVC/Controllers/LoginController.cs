using GameScheduler.BLL.Exceptions;
using GameScheduler.BLL.Helpers;
using GameScheduler.BLL.Models.UserModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GameScheduler.MVC.Controllers
{
    public class LoginController : BaseController
    {
        public LoginController(IMediator mediator) : base(mediator) { }

        public IActionResult Index() => View();

        public IActionResult Register() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _mediator.SendValidated(request, cancellationToken);

                await _mediator.Send(new LoginUserCommand() { Name = request.Name, Password = request.Password }, cancellationToken);

                return RedirectToAction("Index", "Home");
            }
            catch (RequestValidationException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
            catch (Exception ex) { return RedirectToErrorPage<LoginController>(ex); }
        }

        public IActionResult Login() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _mediator.SendValidated(request, cancellationToken);

                return RedirectToAction("Index", "Home");
            }
            catch (RequestValidationException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
            catch (Exception ex) { return RedirectToErrorPage<LoginController>(ex); }
        }
    }
}
