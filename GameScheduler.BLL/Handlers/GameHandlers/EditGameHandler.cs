using GameScheduler.BLL.Abstractions;
using GameScheduler.BLL.Entities;
using GameScheduler.BLL.Exceptions;
using GameScheduler.BLL.Models.GameModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameScheduler.BLL.Handlers.GameHandlers
{
    public class EditGameHandler : BaseHandler<EditGameCommand, Unit>
    {
        private readonly IDateTimeProvider _dateTimeProvider;
        public EditGameHandler(
            IAppDbContext appDbContext,
            IAuthorizationService authorizationService,
            IDateTimeProvider dateTimeProvider) : base(appDbContext, authorizationService)
        {
            _dateTimeProvider = dateTimeProvider;
        }

        public async override Task<Unit> Handle(EditGameCommand request, CancellationToken cancellationToken)
        {
            _authorizationService.AuthorizationCheck(Constants.RoleType.User);

            var game = await _appDbContext.Games.Include(g => g.Users).FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
                ?? throw new EntityNotFoundException<Game>(request.Id);

            var users = await CheckAndFormData(request, cancellationToken);

            game.ChangeGame(
                name: request.Name,
                description: request.Description,
                dateTime: request.DateTime,
                users: users);

            await _appDbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }

        private async Task<List<User>> CheckAndFormData(EditGameCommand request, CancellationToken cancellationToken)
        {
            if (await _appDbContext.Games.AnyAsync(x => x.Name == request.Name && x.Id != request.Id, cancellationToken))
                throw new RequestValidationException("Игра с таким названием уже есть");

            if (request.DateTime < _dateTimeProvider.Now)
                throw new RequestValidationException("Игра не может быть запланирована на прошлое.");

            var users = await _appDbContext.Users.Where(x => request.Users.Contains(x.Id)).ToListAsync(cancellationToken);

            if (users.Count != request.Users.Count)
                throw new ApplicationSystemBaseException("Не все пользователи из списка найдены в базе данных");

            return users;
        }
    }
}
