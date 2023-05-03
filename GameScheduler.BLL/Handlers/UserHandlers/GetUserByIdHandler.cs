using GameScheduler.BLL.Abstractions;
using GameScheduler.BLL.Entities;
using GameScheduler.BLL.Exceptions;
using GameScheduler.BLL.Models.UserModels;
using Microsoft.EntityFrameworkCore;

namespace GameScheduler.BLL.Handlers.UserHandlers
{
    public class GetUserByIdHandler : BaseHandler<GetUserByIdQuery, GetUserByIdResponse>
    {
        public GetUserByIdHandler(IAppDbContext appDbContext, IAuthorizationService authorizationService) : base(appDbContext, authorizationService)
        {
        }

        public async override Task<GetUserByIdResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            _authorizationService.AuthorizationCheck(Constants.RoleType.Admin);

            var user = await _appDbContext.Users.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
                ?? throw new EntityNotFoundException<User>(request.Id);

            return new GetUserByIdResponse
            {
                Id = request.Id,
                Name = user.Name,
                RegistrationDateTime = user.CreatedOn,
                RoleType = user.RoleType,
                Games = user.Games,
            };
        }
    }
}
