using AutoMapper;
using BL.Dto;
using BL.Interfase;
using DataAccessLayer.Interfsce;
using Domain;


namespace BL.Sevices
{
    public class ClassService : BaseService<TbClass, ClassDto>, IClass
    {
        public ClassService(ITableRepository<TbClass> repo, IMapper mapper, IUserService userService) : base(repo, mapper, userService)
        {

        }
    }
}
