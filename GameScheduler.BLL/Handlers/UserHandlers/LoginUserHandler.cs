using GameScheduler.BLL.Abstractions;
using GameScheduler.BLL.Exceptions;
using GameScheduler.BLL.Models.UserModels;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace GameScheduler.BLL.Handlers.UserHandlers
{
    public class LoginUserHandler : BaseHandler<LoginUserCommand, Unit>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPasswordHasher _passwordHasher;
        
        public LoginUserHandler(
            IAppDbContext appDbContext,
            IAuthorizationService authorizationService,
            IPasswordHasher passwordHasher,
            IHttpContextAccessor httpContextAccessor) : base(appDbContext, authorizationService)
        {
            _httpContextAccessor = httpContextAccessor;
            _passwordHasher = passwordHasher;
        }

        public async override Task<Unit> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _appDbContext.Users
                .FirstOrDefaultAsync(x => x.Name == request.Name, cancellationToken);

            if (existingUser == null)
                throw new ApplicationSystemNullException<LoginUserHandler>(nameof(existingUser));

            bool isPasswordCorrect = _passwordHasher.VerifyHash
                (request.Password, existingUser.PasswordHash);

            if (!isPasswordCorrect)
                throw new ApplicationSystemNullException<LoginUserHandler>(nameof(isPasswordCorrect));

            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, existingUser.Name.ToString()),
                    new Claim(ClaimTypes.Role, Enum.GetName(existingUser.RoleType))
                };

            ClaimsIdentity claimsIdentity = new(claims, "Cookies");

            await _httpContextAccessor.HttpContext
                .SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
            return Unit.Value;
        }
    }
}
