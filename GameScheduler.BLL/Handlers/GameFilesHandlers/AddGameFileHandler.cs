using GameScheduler.BLL.Abstractions;
using GameScheduler.BLL.Entities;
using GameScheduler.BLL.Exceptions;
using GameScheduler.BLL.Models.GameFileModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameScheduler.BLL.Handlers.GameFilesHandlers
{
    public class AddGameFileHandler : BaseHandler<AddGameFileCommand, Unit>
    {
        public AddGameFileHandler(IAppDbContext appDbContext, IAuthorizationService authorizationService)
            : base(appDbContext, authorizationService)
        {
        }

        public async override Task<Unit> Handle(AddGameFileCommand request, CancellationToken cancellationToken)
        {
            _authorizationService.AuthorizationCheck(Constants.RoleType.User);

            var game = await _appDbContext.Games.Include(g => g.GameFiles)
                .FirstOrDefaultAsync(x => x.Id == request.GameId, cancellationToken)
                ?? throw new EntityNotFoundException<Game>(request.GameId);

            if (game.GameFiles.Any(x => x.Name == request.Name))
                throw new RequestValidationException("Файл с таким названием уже есть");

            var newPath = SaveGameFile();
            var newGamefile = new GameFile
            {
                Name = request.Name,
                Path = newPath,
            };

            game.GameFiles.Add(newGamefile);

            await _appDbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;

            string SaveGameFile()
            {
                var fileInfo = new FileInfo(request.Path);
                var extension = Path.GetExtension(request.Path);

                if (!fileInfo.Exists)
                    throw new RequestValidationException("Неверный адрес файла.");

                var savedFileInfo = fileInfo.CopyTo($@"D:\\testStorage\{game.Name}\{request.Name}{extension}");

                return savedFileInfo.FullName;
            }
        }
    }
}
