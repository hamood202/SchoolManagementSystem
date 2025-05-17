using AutoMapper;
using BL.Interfase;
using BL.Dto;
using DataAccessLayer.Interfsce;
using Domain;

namespace BL.Sevices
{
    public class RefreshTokenService : BaseService<TbRefreshTokens, RefreshTokenDto>, IRefreshToken
    {
        private ITableRepository<TbRefreshTokens> _repo;
        IMapper _mapper;

        public RefreshTokenService(ITableRepository<TbRefreshTokens> repo, IMapper mapper, IUserService userService) : base(repo, mapper, userService)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public RefreshTokenDto GetByToken(string token)
        {
            var refreshToken = _repo.GetFirstOrDefault(a => a.Token == token);
            return _mapper.Map<TbRefreshTokens, RefreshTokenDto>(refreshToken);
        }

        public bool Refresh(RefreshTokenDto tokenDto)
        {
            var tokenList = _repo.GetList(a => a.UserId == tokenDto.UserId && a.CurrentState == 1);
            foreach (var dbToken in tokenList)
            {
                _repo.ShangeStatus(dbToken.Id, Guid.Parse(tokenDto.UserId), 2);
            }
            var dToken = _mapper.Map<RefreshTokenDto, TbRefreshTokens>(tokenDto);
            _repo.Add(dToken);
            return true;
        }
    }
}
