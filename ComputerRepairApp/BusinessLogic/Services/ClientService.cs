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
    internal class ClientService : IClientService
    {
        private readonly IRepository<Client, int> _clientRepository;
        private readonly IMapper _mapper;

        public ClientService(IRepository<Client, int> clientRepository)
        {
            _clientRepository = clientRepository;

            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Client, ClientDto>().ReverseMap();
            }).CreateMapper();
        }

        public int Add(ClientDto item)
        {
            item.CreateValidation();
            return _clientRepository.Create(_mapper.Map<ClientDto, Client>(item));
        }

        public void Update(ClientDto item)
        {
            item.CreateValidation();
            var client = _clientRepository.GetById(item.Id);
            client.FindValidation();
            _clientRepository.Update(_mapper.Map<ClientDto, Client>(item));
        }

        public void Delete(int id)
        {
            var client = _clientRepository.GetById(id);
            client.FindValidation();
            _clientRepository.Delete(id);
        }

        public IEnumerable<ClientDto> GetAll()
        {
            var clients = _clientRepository.GetAll();
            if (!clients.Any())
            {
                return new List<ClientDto>();
            }

            return _mapper.Map<IEnumerable<Client>, IEnumerable<ClientDto>>(clients);
        }

        public ClientDto GetById(int id)
        {
            var client = _clientRepository.GetById(id);
            client.FindValidation();
            return _mapper.Map<Client, ClientDto>(client);
        }
    }
}
