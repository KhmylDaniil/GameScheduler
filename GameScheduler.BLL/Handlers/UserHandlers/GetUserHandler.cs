using GameScheduler.BLL.Abstractions;
using GameScheduler.BLL.Models.UserModels;
using Microsoft.EntityFrameworkCore;

namespace GameScheduler.BLL.Handlers.UserHandlers
{
    public class GetUserHandler : BaseHandler<GetUserQuery, IEnumerable<GetUserResponse>>
    {
        public GetUserHandler(IAppDbContext appDbContext, IAuthorizationService authorizationService) : base(appDbContext, authorizationService)
        {
        }

        public async override Task<IEnumerable<GetUserResponse>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            _authorizationService.AuthorizationCheck(Constants.RoleType.User);

            var filter = _appDbContext.Users
                .Where(u => request.Name == null || u.Name.Contains(request.Name, StringComparison.InvariantCultureIgnoreCase));

            return await filter.Select(x => new GetUserResponse
            {
                Id = x.Id,
                Name = x.Name,
                RegistrationDateTime = x.CreatedOn,
                Role = Enum.GetName(x.RoleType)
            }).ToListAsync(cancellationToken);
        }
    }
}
