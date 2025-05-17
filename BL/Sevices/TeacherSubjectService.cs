using AutoMapper;
using BL.Dto;
using BL.Interfase;
using DataAccessLayer.Interfsce;
using Domain;


namespace BL.Sevices
{
    public class TeacherSubjectService : BaseService<TbTeacherSubject, TeacherSubjectDto>, ITeacherSubject
    {
        public TeacherSubjectService(ITableRepository<TbTeacherSubject> repo, IMapper mapper, IUserService userService) : base(repo, mapper, userService)
        {

        }
    }
}
