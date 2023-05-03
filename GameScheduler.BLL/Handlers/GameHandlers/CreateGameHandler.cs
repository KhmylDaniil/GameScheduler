using GameScheduler.BLL.Abstractions;
using GameScheduler.BLL.Entities;
using GameScheduler.BLL.Exceptions;
using GameScheduler.BLL.Models.GameModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameScheduler.BLL.Handlers.GameHandlers
{
    public class CreateGameHandler : BaseHandler<CreateGameCommand, Unit>
    {
        private readonly IDateTimeProvider _dateTimeProvider;

        public CreateGameHandler(
            IAppDbContext appDbContext,
            IAuthorizationService authorizationService,
            IDateTimeProvider dateTimeProvider)
            : base(appDbContext, authorizationService)
        {
            _dateTimeProvider = dateTimeProvider;
        }

        public async override Task<Unit> Handle(CreateGameCommand request, CancellationToken cancellationToken)
        {
            _authorizationService.AuthorizationCheck(Constants.RoleType.User);

            var users = await CheckAndFormData(request, cancellationToken);

            var newGame = new Game(
                name: request.Name,
                description: request.Description,
                dateTime: request.DateTime,
                users: users);

            _appDbContext.Games.Add(newGame);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }

        private async Task<List<User>> CheckAndFormData(CreateGameCommand request, CancellationToken cancellationToken)
        {
            if(await _appDbContext.Games.AnyAsync(x => x.Name == request.Name, cancellationToken))
                throw new RequestValidationException("Игра с таким названием уже есть");

            if (request.DateTime < _dateTimeProvider.Now)
                throw new RequestValidationException("Игра не может быть запланирована на прошлое.");

            var users = await _appDbContext.Users.Where(x => request.Users.Contains(x.Id)).ToListAsync(cancellationToken);

            if (users.Count != request.Users.Count)
                throw new ApplicationSystemBaseException("не все пользователи из списка найдены в базе данных");

            return users;
        }
    }
}
