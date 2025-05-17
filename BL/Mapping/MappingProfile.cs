using AutoMapper;
using BL.Dto;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TbAttendance, AttendanceDto>().ReverseMap();
            CreateMap<TbCity, CityDto>().ReverseMap();
            CreateMap<TbClass, ClassDto>().ReverseMap();
            CreateMap<TbStudent, StudentDto>().ReverseMap();
            CreateMap<TbSubject, SubjectDto>().ReverseMap();
            CreateMap<TbTeacher, TeacherDto>().ReverseMap();
            CreateMap<TbTeacherSubject, TeacherSubjectDto>().ReverseMap();
            CreateMap<TbRefreshTokens, RefreshTokenDto>().ReverseMap();
            CreateMap<TbSchedule, ScheduleDto>().ReverseMap();
            CreateMap<TbGrade, GradeDto>().ReverseMap();

        }
    }
}
