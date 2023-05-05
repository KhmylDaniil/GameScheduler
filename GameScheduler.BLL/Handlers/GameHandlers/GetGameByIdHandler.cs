using GameScheduler.BLL.Abstractions;
using GameScheduler.BLL.Entities;
using GameScheduler.BLL.Exceptions;
using GameScheduler.BLL.Models.GameModels;
using Microsoft.EntityFrameworkCore;

namespace GameScheduler.BLL.Handlers.GameHandlers
{
    public class GetGameByIdHandler : BaseHandler<GetGameByIdQuery, GetGameByIdResponse>
    {
        public GetGameByIdHandler(IAppDbContext appDbContext, IAuthorizationService authorizationService) : base(appDbContext, authorizationService)
        {
        }

        public async override Task<GetGameByIdResponse> Handle(GetGameByIdQuery request, CancellationToken cancellationToken)
        {
            _authorizationService.AuthorizationCheck(Constants.RoleType.User);

            var game = await _appDbContext.Games
                .Include(g => g.Users)
                .Include(g => g.GameFiles)
                .FirstOrDefaultAsync(x => x.Id == request.Id)
                ?? throw new EntityNotFoundException<Game>(request.Id);

            return new GetGameByIdResponse
            {
                Id = game.Id,
                Name = game.Name,
                Description = game.Description,
                DateTime = game.DateTime,
                Users = game.Users.ToDictionary(x => x.Id, x => x.Name),
                GameFiles = game.GameFiles
            };
        }
    }
}
