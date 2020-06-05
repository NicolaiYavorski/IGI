using AutoMapper;
using BusinessLogic.DTO;
using BusinessLogic.Extensions;
using BusinessLogic.Interfaces;
using DataAccess.Entities;
using DataAccess.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Services
{
    internal class MasterService : IMasterService
    {
        private readonly IRepository<Master, int> _masterRepository;
        private readonly IMapper _mapper;

        public MasterService(IRepository<Master, int> masterRepository)
        {
            _masterRepository = masterRepository;

            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Master, MasterDto>().ReverseMap();
            }).CreateMapper();
        }

        public int Add(MasterDto item)
        {
            item.CreateValidation();
            return _masterRepository.Create(_mapper.Map<MasterDto, Master>(item));
        }

        public void Update(MasterDto item)
        {
            item.CreateValidation();
            var master = _masterRepository.GetById(item.Id);
            master.FindValidation();
            _masterRepository.Update(_mapper.Map<MasterDto, Master>(item));
        }

        public void Delete(int id)
        {
            var master = _masterRepository.GetById(id);
            master.FindValidation();
            _masterRepository.Delete(id);
        }

        public IEnumerable<MasterDto> GetAll()
        {
            var masters = _masterRepository.GetAll();
            if (!masters.Any())
            {
                return new List<MasterDto>();
            }

            return _mapper.Map<IEnumerable<Master>, IEnumerable<MasterDto>>(masters);
        }

        public MasterDto GetById(int id)
        {
            var master = _masterRepository.GetById(id);
            master.FindValidation();
            return _mapper.Map<Master, MasterDto>(master);
        }
    }
}
