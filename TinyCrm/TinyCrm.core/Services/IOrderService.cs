using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyCrm.Core.Model;
using TinyCrm.Core.Services.Options;

namespace TinyCrm.Core.Services
{
    public interface IOrderService
    {
        Order CreateOrder(
            CreateOrderOptions options);
        
        IQueryable<Order> SearchOrders(
            SearchOrderOptions options);
        
        Order UpdateOrder(
            UpdateOrderOptions options);
        /*
        Order GetOrderById(
            GetOrderByIdOptions options);
            */

    }
}