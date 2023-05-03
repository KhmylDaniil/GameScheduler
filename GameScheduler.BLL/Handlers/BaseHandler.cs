using GameScheduler.BLL.Abstractions;
using MediatR;

namespace GameScheduler.BLL.Handlers
{
    public abstract class BaseHandler<TRequest, Tout> : IRequestHandler<TRequest, Tout> where TRequest : IRequest<Tout>
    {

        protected readonly IAppDbContext _appDbContext;
        protected readonly IAuthorizationService _authorizationService;


        protected BaseHandler(IAppDbContext appDbContext, IAuthorizationService authorizationService)
        {
            _appDbContext = appDbContext;
            _authorizationService = authorizationService;
        }

        public abstract Task<Tout> Handle(TRequest request, CancellationToken cancellationToken);
    }
}
