using AutoMapper;
using GameScheduler.BLL.Models.GameModels;
using GameScheduler.MVC.Models;

namespace GameScheduler.MVC
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreateGameCommand, CreateGameViewModel>();
            CreateMap<EditGameCommand, EditGameViewModel>();
        }
    }
}
