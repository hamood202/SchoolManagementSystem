using AutoMapper;
using BL.Dto;
using BL.Interfase;
using DataAccessLayer.Interfsce;
using Domain;


namespace BL.Sevices
{
    public class TeacherService : BaseService<TbTeacher, TeacherDto>, ITeacher
    {
        public TeacherService(ITableRepository<TbTeacher> repo, IMapper mapper, IUserService userService) : base(repo, mapper, userService)
        {

        }
    }
}
