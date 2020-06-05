using BusinessLogic.DTO;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces
{
    public interface IMasterService
    {
        int Add(MasterDto item);

        void Delete(int id);

        IEnumerable<MasterDto> GetAll();

        MasterDto GetById(int id);

        void Update(MasterDto item);
    }
}
