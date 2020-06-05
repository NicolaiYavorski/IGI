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
    internal class OrderService : IOrderService
    {
        private readonly IRepository<Order, int> _orderRepository;
        private readonly IMapper _mapper;

        public OrderService(IRepository<Order, int> orderRepository)
        {
            _orderRepository = orderRepository;

            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Order, OrderDto>().ReverseMap();
            }).CreateMapper();
        }

        public int Add(OrderDto item)
        {
            item.CreateValidation();
            return _orderRepository.Create(_mapper.Map<OrderDto, Order>(item));
        }

        public void Update(OrderDto item)
        {
            item.CreateValidation();
            var order = _orderRepository.GetById(item.Id);
            order.FindValidation();
            _orderRepository.Update(_mapper.Map<OrderDto, Order>(item));
        }

        public void Delete(int id)
        {
            var order = _orderRepository.GetById(id);
            order.FindValidation();
            _orderRepository.Delete(id);
        }

        public IEnumerable<OrderDto> GetAll()
        {
            var orders = _orderRepository.GetAll();
            if (!orders.Any())
            {
                return new List<OrderDto>();
            }

            return _mapper.Map<IEnumerable<Order>, IEnumerable<OrderDto>>(orders);
        }

        public OrderDto GetById(int id)
        {
            var order = _orderRepository.GetById(id);
            order.FindValidation();
            return _mapper.Map<Order, OrderDto>(order);
        }
    }
}
