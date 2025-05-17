using BL.Dto;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Interfase
{
    public interface IRefreshToken : IBaseService<TbRefreshTokens, RefreshTokenDto>
    {

        public RefreshTokenDto GetByToken(string token);
        public bool Refresh(RefreshTokenDto tokenDto);

    }
}
