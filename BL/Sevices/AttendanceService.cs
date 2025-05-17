using AutoMapper;
using BL.Dto;
using BL.Interfase;
using DataAccessLayer.Interfsce;
using Domain;


namespace BL.Sevices
{
    public class AttendanceService : BaseService<TbAttendance, AttendanceDto>, IAttendance
    {
        public AttendanceService(ITableRepository<TbAttendance> repo, IMapper mapper, IUserService userService) : base(repo, mapper, userService)
        {

        }
    }
}
