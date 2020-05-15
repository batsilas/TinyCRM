using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyCrm.Core.Data;
using TinyCrm.Core.Model;
using TinyCrm.Core.Services;
using TinyCrm.Core.Services.Options;

namespace TinyCrm
{
    public class OrderService : IOrderService
    {
        private TinyCrmDbContext context;
        private ICustomerService customerService;
        private IProductService productService;
        public OrderService(
            TinyCrmDbContext contextByProgram, 
            ICustomerService custService,
            IProductService prodService)
        {
            context = contextByProgram;
            customerService = custService;
            productService = prodService;
        }

        public Order CreateOrder(
            CreateOrderOptions options)
        {
            var customer = customerService.GetCustomerById(
                new GetCustomerByIdOptions() { 
                    CustomerId = options.CustomerId
            });
            if (customer == null)
            {
                return null;
            }
            var order = new Order() { 
                DeliveryAddress = options.DeliveryAddress
            };

            foreach (var item in options.ProductIds)
            {
               
                var product = productService.SearchProducts(
                    new SearchProductOptions()
                    {
                        ProductId = item
                    }).SingleOrDefault();

                if (product == null)
                {
                    
                    return null;
                }
                order.OrderProducts.Add(new OrderProduct()
                {
                    ProductId = product.ProductId
                });

            }
            Console.WriteLine(order.OrderProducts.Count);
            customer.Orders.Add(order);

            context.Add(order);

            if (context.SaveChanges() > 0)
            {
                return order;
            }

            return null;
            
        }

        public IQueryable<Order> SearchOrders(
            SearchOrderOptions options)
        {
            if (options == null) 
            {
                return null;
            }

            var query = context
                .Set<Order>()
                .AsQueryable();

            if (options.OrderId != null) 
            {
                query = query.Where(ord => ord.OrderId == options.OrderId);               
            }

            if (options.CreatedFrom != null)
            {
                query = query.Where(ord => ord.Created >= options.CreatedFrom);
            }

            if (options.CreatedTo != null)
            {
                query = query.Where(ord => ord.Created <= options.CreatedTo);
            }

            if (!string.IsNullOrWhiteSpace(options.DeliveryAddress))
            {
                query = query.Where(ord => ord.DeliveryAddress == options.DeliveryAddress);
            }

            
            return query;
        }


        public Order UpdateOrder(
            UpdateOrderOptions options)
        {

            if (options == null)
            {
                return null;
            }

            var order = SearchOrders(
                new SearchOrderOptions()
                {
                    OrderId = options.OrderId
                }).SingleOrDefault();

            order.Created = DateTime.Now;
            order.DeliveryAddress = options.DeliveryAddress;

            context.SaveChanges();

            return order;
        }


    }
}
