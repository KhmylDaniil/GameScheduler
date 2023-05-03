using GameScheduler.BLL.Abstractions;
using GameScheduler.BLL.Constants;
using GameScheduler.BLL.Entities;
using GameScheduler.BLL.Exceptions;
using GameScheduler.BLL.Models.UserModels;
using MediatR;

namespace GameScheduler.BLL.Handlers.UserHandlers
{
    public class RegisterUserHandler : BaseHandler<RegisterUserCommand, Unit>
    {
        private readonly IPasswordHasher _passwordHasher;

        public RegisterUserHandler(
            IAppDbContext appDbContext,
            IAuthorizationService authorizationService,
            IPasswordHasher passwordHasher)
            : base(appDbContext, authorizationService)
        {
            _passwordHasher = passwordHasher;
        }

        public override async Task<Unit> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            _authorizationService.AuthorizationCheck();

            if (_appDbContext.Users.Any(x => x.Name == request.Name))
                throw new RequestValidationException("Пользователь с таким именем уже зарегистрирован.");

            var user = new User(
                name: request.Name,
                passwordHash: _passwordHasher.Hash(request.Password),
                roleType: request.AsAdmin ? RoleType.Admin : RoleType.User);

            await _appDbContext.Users.AddAsync(user, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }    
}
