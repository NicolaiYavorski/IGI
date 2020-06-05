using BusinessLogic.DTO;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces
{
    public interface IOrderService
    {
        int Add(OrderDto item);

        void Delete(int id);

        IEnumerable<OrderDto> GetAll();

        OrderDto GetById(int id);

        void Update(OrderDto item);
    }
}
