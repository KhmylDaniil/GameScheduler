using GameScheduler.BLL.Abstractions;
using GameScheduler.BLL.Entities;
using GameScheduler.BLL.Exceptions;
using GameScheduler.BLL.Models.GameModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameScheduler.BLL.Handlers.GameHandlers
{
    public class DeleteGameHandler : BaseHandler<DeleteGameCommand, Unit>
    {
        public DeleteGameHandler(IAppDbContext appDbContext, IAuthorizationService authorizationService) : base(appDbContext, authorizationService)
        {
        }

        public async override Task<Unit> Handle(DeleteGameCommand request, CancellationToken cancellationToken)
        {
            _authorizationService.AuthorizationCheck(Constants.RoleType.User);

            var game = await _appDbContext.Games.Include(g => g.Users).FirstOrDefaultAsync(x => x.Id == request.Id)
                ?? throw new EntityNotFoundException<Game>(request.Id);

            game.DeleteStorageFolder();

            _appDbContext.Games.Remove(game);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
