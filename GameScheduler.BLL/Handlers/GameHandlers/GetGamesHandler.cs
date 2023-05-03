using GameScheduler.BLL.Abstractions;
using GameScheduler.BLL.Exceptions;
using GameScheduler.BLL.Models.GameModels;
using Microsoft.EntityFrameworkCore;

namespace GameScheduler.BLL.Handlers.GameHandlers
{
    public class GetGamesHandler : BaseHandler<GetGamesQuery, IEnumerable<GetGamesResponse>>
    {
        private readonly IDateTimeProvider _timeProvider;
        public GetGamesHandler(IAppDbContext appDbContext, IAuthorizationService authorizationService) : base(appDbContext, authorizationService)
        {
        }

        public async override Task<IEnumerable<GetGamesResponse>> Handle(GetGamesQuery request, CancellationToken cancellationToken)
        {
            _authorizationService.AuthorizationCheck(Constants.RoleType.User);

            if (request.MinDateTime > request.MaxDateTime)
                throw new RequestValidationException("Неверное указание времени");

            if (request.MaxDateTime == default)
                request.MaxDateTime = DateTime.MaxValue;

            var filter = _appDbContext.Games.Where(g => (request.Name == null || g.Name.Contains(request.Name))
                && g.DateTime >= request.MinDateTime && g.DateTime <= request.MaxDateTime);

            //Func<Game, TKey> selector = 

            var list = await filter
                .OrderBy(g => request.Name)
                //.Skip(request.PageSize * (request.PageNumber - 1))
                //.Take(request.PageSize)
                .Select(x => new GetGamesResponse()
                {
                    Name = x.Name,
                    Id = x.Id,
                    DateTime = x.DateTime,
                }).ToListAsync(cancellationToken);

            return list;
        }

        //private Func<>
    }
}
