using AutoMapper;
using BL.Dto;
using BL.Interfase;
using DataAccessLayer.Interfsce;
using Domain;


namespace BL.Sevices
{
    public class GradeService : BaseService<TbGrade, GradeDto>, IGrade
    {
        public GradeService(ITableRepository<TbGrade> repo, IMapper mapper, IUserService userService) : base(repo, mapper, userService)
        {

        }
    }
}
