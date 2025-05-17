using AutoMapper;
using BL.Interfase;
using DataAccessLayer.Interfsce;
using Domain;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Sevices
{
    public class BaseService<T, DTO> : IBaseService<T, DTO> where T : BaseTable
    {
        private readonly ITableRepository<T> _repo;
        private readonly IMapper _mapper;
        private readonly IUserService _UserService;

        public BaseService(ITableRepository<T> repo, IMapper mapper, IUserService UserService) 
        {
            _repo = repo;
            _mapper = mapper;
            _UserService = UserService;
        }

        public List<DTO> GetAll()
        {
            var list = _repo.GetAll();
           
            return _mapper.Map<List<T>,List<DTO>>(list);
        } 

        public DTO GetById(Guid id)
        {
            var obj= _repo.GetById(id);
            return _mapper.Map<T,DTO>(obj);
        }  
    
        public bool Add(DTO entity)
        {
            var dbObject = _mapper.Map<DTO,T>(entity);
            dbObject.CreatedBy = _UserService.GetLoggedInUser();
            dbObject.CurrentState = 1;
            dbObject.CreatedDate = DateTime.Now;
            dbObject.Id = Guid.NewGuid();

            return _repo.Add(dbObject);
        }

        public bool Update(DTO entity)
        {
            var dbObject = _mapper.Map<DTO, T>(entity);
            dbObject.UpdatedBy = _UserService.GetLoggedInUser(); 
            return _repo.Update(dbObject);
        }

        public bool ShangeStatus(Guid id, int status = 1)
        {
            return _repo.ShangeStatus(id, _UserService.GetLoggedInUser(), status);
        }           
    }
}
