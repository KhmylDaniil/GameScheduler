using AutoMapper;
using GameScheduler.BLL.Exceptions;
using GameScheduler.BLL.Helpers;
using GameScheduler.BLL.Models.GameModels;
using GameScheduler.BLL.Models.UserModels;
using GameScheduler.MVC.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace GameScheduler.MVC.Controllers
{
    [Authorize]
    public class GameController : BaseController
    {
        private readonly IMapper _mapper;

        public GameController(IMediator mediator, IMapper mapper, IMemoryCache memoryCache) : base(mediator)
        {
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(GetGamesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _mediator.SendValidated(request, cancellationToken);

                return View(response);
            }
            catch (RequestValidationException ex)
            {
                TempData["ErrorMessage"] = ex.Message;

                return View(await _mediator.Send(new GetGamesQuery(), cancellationToken));
            }
            catch (Exception ex) { return RedirectToErrorPage<GameController>(ex); }
        }

        public async Task<IActionResult> Details(GetGameByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _mediator.SendValidated(request, cancellationToken);

                return View(response);
            }
            catch (RequestValidationException ex)
            {
                TempData["ErrorMessage"] = ex.Message;

                return View(await _mediator.Send(new GetGamesQuery(), cancellationToken));
            }
            catch (Exception ex) { return RedirectToErrorPage<GameController>(ex); }
        }

        [HttpGet]
        public async Task<IActionResult> Create(CreateGameViewModel viewModel, CancellationToken cancellationToken)
        {
            return View(await CreateVM(viewModel, cancellationToken));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateGameCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _mediator.SendValidated(command, cancellationToken);

                return RedirectToAction(nameof(Details), new GetGameByIdQuery() { Id = result });
            }
            catch (RequestValidationException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View(command);
            }
            catch (Exception ex) { return RedirectToErrorPage<GameController>(ex); }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(EditGameViewModel viewModel, CancellationToken cancellationToken)
        {
            return View(await CreateVM(viewModel, cancellationToken));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditGameCommand command, CancellationToken cancellationToken)
        {
            try
            {
                await _mediator.SendValidated(command, cancellationToken);

                return RedirectToAction(nameof(Details), new GetGameByIdQuery() { Id = command.Id });
            }
            catch (RequestValidationException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View(command);
            }
            catch (Exception ex) { return RedirectToErrorPage<GameController>(ex); }
        }

        public ActionResult Delete(DeleteGameCommand command) => View(command);

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(DeleteGameCommand command, CancellationToken cancellationToken)
        {
            try
            {
                await _mediator.SendValidated(command, cancellationToken);

                return RedirectToAction(nameof(Index), new GetGamesQuery());
            }
            catch (RequestValidationException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View(command);
            }
            catch (Exception ex) { return RedirectToErrorPage<GameController>(ex); }
        }

        async Task<CreateGameViewModel> CreateVM(CreateGameCommand command, CancellationToken cancellationToken)
        {
            var viewModel = command is CreateGameViewModel vm
                ? vm
                : _mapper.Map<CreateGameViewModel>(command);

            viewModel.UsersList = await GetUsersForViewModel(cancellationToken);

            return viewModel;
        }

        async Task<EditGameViewModel> CreateVM(EditGameCommand command, CancellationToken cancellationToken)
        {
            var viewModel = command is EditGameViewModel vm
                ? vm
                : _mapper.Map<EditGameViewModel>(command);

            viewModel.UsersList = await GetUsersForViewModel(cancellationToken);

            return viewModel;
        }

        private async Task<Dictionary<Guid, string>> GetUsersForViewModel(CancellationToken cancellationToken)
        {
            var users = await _mediator.SendValidated(new GetUserQuery(), cancellationToken);

            return users.ToDictionary(x => x.Id, x => x.Name);
        }
    }
}
