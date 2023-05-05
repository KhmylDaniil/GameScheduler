using GameScheduler.BLL.Exceptions;
using GameScheduler.BLL.Helpers;
using GameScheduler.BLL.Models.GameFileModels;
using GameScheduler.BLL.Models.GameModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GameScheduler.MVC.Controllers
{
    [Route("[controller]/[action]/{gameId}")]
    public class GameFileController : BaseController
    {
        public GameFileController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public ActionResult Create(AddGameFileCommand command)
        {
            return View(command);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddGameFileCommand command, CancellationToken cancellationToken)
        {
            try
            {
                await _mediator.SendValidated(command, cancellationToken);

                return RedirectToAction(nameof(GameController.Details), "Game", new GetGameByIdQuery() { Id = command.GameId });
            }
            catch (RequestValidationException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View(command);
            }
            catch (Exception ex) { return RedirectToErrorPage<GameFileController>(ex); }
        }

        [HttpGet]
        public ActionResult Delete(DeleteGameFileCommand command)
        {
            return View(command);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(DeleteGameFileCommand command, CancellationToken cancellationToken)
        {
            try
            {
                await _mediator.SendValidated(command, cancellationToken);

                return RedirectToAction(nameof(GameController.Details), "Game", new GetGameByIdQuery() { Id = command.GameId });
            }
            catch (RequestValidationException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View(command);
            }
            catch (Exception ex) { return RedirectToErrorPage<GameFileController>(ex); }
        }
    }
}
