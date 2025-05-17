using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Interfase
{
    public interface IBaseService<T, DTO>
    {
        List<DTO> GetAll();
        DTO GetById(Guid id);
        bool Add(DTO entity);
        bool Update(DTO entity);
        bool ShangeStatus(Guid id, int status = 1);
    }
}
