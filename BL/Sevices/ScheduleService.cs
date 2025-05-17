using AutoMapper;
using BL.Dto;
using BL.Interfase;
using DataAccessLayer.Interfsce;
using Domain;


namespace BL.Sevices
{
    public class ScheduleService : BaseService<TbSchedule, ScheduleDto>, ISchedule
    {
        public ScheduleService(ITableRepository<TbSchedule> repo, IMapper mapper, IUserService userService) : base(repo, mapper, userService)
        {

        }
    }
}
