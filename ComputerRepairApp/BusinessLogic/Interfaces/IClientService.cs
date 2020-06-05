using BusinessLogic.DTO;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces
{
    public interface IClientService
    {
        int Add(ClientDto item);

        void Delete(int id);

        IEnumerable<ClientDto> GetAll();

        ClientDto GetById(int id);

        void Update(ClientDto item);
    }
}
