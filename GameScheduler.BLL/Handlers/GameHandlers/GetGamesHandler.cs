using GameScheduler.BLL.Abstractions;
using GameScheduler.BLL.Entities;
using GameScheduler.BLL.Exceptions;
using GameScheduler.BLL.Models.GameModels;
using Microsoft.EntityFrameworkCore;

namespace GameScheduler.BLL.Handlers.GameHandlers
{
    public class GetGamesHandler : BaseHandler<GetGamesQuery, GetGamesResponse>
    {
        public GetGamesHandler(IAppDbContext appDbContext, IAuthorizationService authorizationService) : base(appDbContext, authorizationService)
        {
        }

        public async override Task<GetGamesResponse> Handle(GetGamesQuery request, CancellationToken cancellationToken)
        {
            _authorizationService.AuthorizationCheck(Constants.RoleType.User);

            if (request.MinDateTime > request.MaxDateTime)
                throw new RequestValidationException("Неверное указание времени");

            if (request.MaxDateTime == default)
                request.MaxDateTime = DateTime.MaxValue;

            var filter = _appDbContext.Games.Where(g => (request.Name == null || g.Name.Contains(request.Name))
                && g.DateTime >= request.MinDateTime && g.DateTime <= request.MaxDateTime);

            var number = await filter.CountAsync(cancellationToken);

            var list = await OrderGamesBySelected(filter, request.FilterByTime)
                .Skip(request.PageSize * (request.PageNumber - 1))
                .Take(request.PageSize)
                .Select(x => new GetGamesResponseItem()
                {
                    Name = x.Name,
                    Id = x.Id,
                    DateTime = x.DateTime,
                }).ToListAsync(cancellationToken);

            return new GetGamesResponse
            {
                Items = list,
                PreviousPageExist = request.PageNumber > 1,
                NextPageExist = number > request.PageSize * request.PageNumber,
                History = request
            };
        }

        private static IOrderedQueryable<Game> OrderGamesBySelected(IQueryable<Game> query, bool orderByTime) =>
            orderByTime ? query.OrderBy(x => x.DateTime) : query.OrderBy(x => x.Name);
    }
}
