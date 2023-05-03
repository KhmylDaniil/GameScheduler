using GameScheduler.BLL.Exceptions;
using MediatR;

namespace GameScheduler.BLL.Helpers
{
    public static class MediatrHelper
    {
        public static Task<TResponse> SendValidated<TResponse>(
            this IMediator mediator, IRequest<TResponse> request, CancellationToken cancellationToken = default)
        {
            if (request is null)
                throw new RequestValidationException("Пришел нулевой запрос.");

            return mediator.Send(request, cancellationToken);
        }
    }
}
