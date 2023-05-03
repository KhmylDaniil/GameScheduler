using GameScheduler.BLL.Abstractions;
using GameScheduler.BLL.Entities;
using GameScheduler.BLL.Exceptions;
using GameScheduler.BLL.Models.UserModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameScheduler.BLL.Handlers.UserHandlers
{
    public class DeleteUserHandler : BaseHandler<DeleteUserCommand, Unit>
    {
        public DeleteUserHandler(IAppDbContext appDbContext, IAuthorizationService authorizationService) : base(appDbContext, authorizationService)
        {
        }

        public async override Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            _authorizationService.AuthorizationCheck(Constants.RoleType.Admin);

            var user = await _appDbContext.Users.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
                ?? throw new EntityNotFoundException<User>(request.Id);

            _appDbContext.Users.Remove(user);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
