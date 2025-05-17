using AutoMapper;
using BL.Dto;
using BL.Interfase;
using DataAccessLayer.Interfsce;
using Domain;


namespace BL.Sevices
{
    public class CityService : BaseService<TbCity, CityDto>, ICity
    {
        public CityService(ITableRepository<TbCity> repo, IMapper mapper, IUserService userService) : base(repo, mapper, userService)
        {

        }
    }
}
