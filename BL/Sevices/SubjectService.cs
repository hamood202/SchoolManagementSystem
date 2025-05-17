using AutoMapper;
using BL.Dto;
using BL.Interfase;
using DataAccessLayer.Interfsce;
using Domain;


namespace BL.Sevices
{
    public class SubjectService : BaseService<TbSubject, SubjectDto>, ISubject
    {
        public SubjectService(ITableRepository<TbSubject> repo, IMapper mapper, IUserService userService) : base(repo, mapper, userService)
        {

        }
    }
}
