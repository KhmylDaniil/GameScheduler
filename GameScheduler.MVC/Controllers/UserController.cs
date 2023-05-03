using GameScheduler.BLL.Exceptions;
using GameScheduler.BLL.Models.UserModels;
using GameScheduler.BLL.Helpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GameScheduler.MVC.Controllers
{
    public class UserController : BaseController
    {
        public UserController(IMediator mediator) : base(mediator)
        {
        }

        public async Task<IActionResult> Index(GetUserQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _mediator.SendValidated(request, cancellationToken);

                return View(response);
            }
            catch (RequestValidationException ex)
            {
                TempData["ErrorMessage"] = ex.Message;

                return View(await _mediator.Send(new GetUserQuery(), cancellationToken));
            }
            catch (Exception ex) { return RedirectToErrorPage<UserController>(ex); }
        }

        public async Task<IActionResult> Details(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _mediator.SendValidated(request, cancellationToken);

                return View(response);
            }
            catch (RequestValidationException ex)
            {
                TempData["ErrorMessage"] = ex.Message;

                return View(await _mediator.Send(new GetUserQuery(), cancellationToken));
            }
            catch (Exception ex) { return RedirectToErrorPage<UserController>(ex); }
        }


        public ActionResult Edit(EditUserCommand command) => View(command);

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditUserCommand command, CancellationToken cancellationToken)
        {
            try
            {
                await _mediator.SendValidated(command, cancellationToken);

                return RedirectToAction(nameof(Details), new GetUserByIdQuery() { Id = command.Id });
            }
            catch (RequestValidationException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View(command);
            }
            catch (Exception ex) { return RedirectToErrorPage<UserController>(ex); }
        }

        public ActionResult Delete(DeleteUserCommand command) => View(command);

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(DeleteUserCommand command, CancellationToken cancellationToken)
        {
            try
            {
                await _mediator.SendValidated(command, cancellationToken);

                return RedirectToAction(nameof(Index), new GetUserQuery());
            }
            catch (RequestValidationException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View(command);
            }
            catch (Exception ex) { return RedirectToErrorPage<UserController>(ex); }
        }
    }
}
