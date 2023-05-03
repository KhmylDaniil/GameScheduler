using GameScheduler.BLL.Abstractions;
using GameScheduler.BLL.Entities;
using GameScheduler.BLL.Exceptions;
using GameScheduler.BLL.Models.UserModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameScheduler.BLL.Handlers.UserHandlers
{
    public class EditUserHandler : BaseHandler<EditUserCommand, Unit>
    {
        private readonly IPasswordHasher _passwordHasher;
        public EditUserHandler(
            IAppDbContext appDbContext,
            IAuthorizationService authorizationService,
            IPasswordHasher passwordHasher)
            : base(appDbContext, authorizationService)
        {
            _passwordHasher = passwordHasher;
        }

        public async override Task<Unit> Handle(EditUserCommand request, CancellationToken cancellationToken)
        {
            _authorizationService.AuthorizationCheck(Constants.RoleType.Admin);

            var user = await _appDbContext.Users.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
                ?? throw new EntityNotFoundException<User>(request.Id);

            if (_appDbContext.Users.Any(x => x.Name == request.Name && x.Id != user.Id))
                throw new RequestValidationException("Выбранное имя уже используется.");

            user.Name = request.Name;

            if (request.SwitchRole)
                user.RoleType = user.RoleType == Constants.RoleType.Admin ? Constants.RoleType.User : Constants.RoleType.Admin;

            if (!string.IsNullOrEmpty(request.SetNewPassword))
                user.PasswordHash = _passwordHasher.Hash(request.SetNewPassword);

            await _appDbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
