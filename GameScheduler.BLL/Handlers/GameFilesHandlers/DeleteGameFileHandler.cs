using GameScheduler.BLL.Abstractions;
using GameScheduler.BLL.Entities;
using GameScheduler.BLL.Exceptions;
using GameScheduler.BLL.Models.GameFileModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameScheduler.BLL.Handlers.GameFilesHandlers
{
    public class DeleteGameFileHandler : BaseHandler<DeleteGameFileCommand, Unit>
    {
        public DeleteGameFileHandler(IAppDbContext appDbContext, IAuthorizationService authorizationService) : base(appDbContext, authorizationService)
        {
        }

        public async override Task<Unit> Handle(DeleteGameFileCommand request, CancellationToken cancellationToken)
        {
            _authorizationService.AuthorizationCheck(Constants.RoleType.User);

            var game = await _appDbContext.Games.Include(g => g.GameFiles)
                .FirstOrDefaultAsync(x => x.Id == request.GameId, cancellationToken)
                ?? throw new EntityNotFoundException<Game>(request.GameId);

            var file = game.GameFiles.FirstOrDefault(x => x.Name == request.Name)
                ?? throw new RequestValidationException("Файла с таким названием нет");

            DeleteGameFile();

            game.GameFiles.Remove(file);

            await _appDbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;

            void DeleteGameFile()
            {
                var fileInfo = new FileInfo(file.Path);

                if (!fileInfo.Exists)
                    throw new RequestValidationException("Неверный адрес файла.");

                fileInfo.Delete();
            }
        }
    }
}
