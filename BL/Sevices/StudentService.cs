using AutoMapper;
using BL.Dto;
using BL.Interfase;
using DataAccessLayer.Interfsce;
using Domain;


namespace BL.Sevices
{
    public class StudentService : BaseService<TbStudent, StudentDto>, IStudent
    {
        public StudentService(ITableRepository<TbStudent> repo, IMapper mapper, IUserService userService) : base(repo, mapper, userService)
        {

        }
    }
}
