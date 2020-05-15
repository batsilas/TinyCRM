using TinyCrm.Core.Data;
using TinyCrm.Core.Services;
using TinyCrm.Core.Services.Options;

namespace TinyCrm
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new TinyCrmDbContext())
            {
                ICustomerService customerService = new CustomerService(
                    context);
            
               // IProductService productService = new ProductService(
               //      context);

               // IOrderService orderService = new OrderService(
               //      context, customerService, productService);
                 
                // CREATE CUSTOMER
                var customer = customerService.CreateCustomer(
                    new CreateCustomerOptions()
                    {
                        //Firstname = "Sakis",
                        //Lastname = "Batsilas",
                        //Email = "batsilas94@hotmail.gr",
                        VatNumber = "098765432"
                    });
                /*
                // SEARCH CUSTOMERS
                var customers = customerService.SearchCustomers(
                new SearchCustomerOptions { 
                    Firstname = "Sakis"
                }).ToList();
                
                // UPDATE CUSTOMERS
                var customer = customerService.UpdateCustomer(
                new UpdateCustomerOptions { 
                    CustomerId = 9,
                    Firstname = "Kevin"
                });
                
                // GET CUSTOMERS BY ID
                var customer = customerService.GetCustomerById(
                    new GetCustomerByIdOptions() { 
                        CustomerId = 2
                    });
               
                // CREATE PRODUCTS
                var product = productService.CreateProduct(
                     new CreateProductOptions()
                     {
                        ProductId = "PRCT100",
                        Name = "xiaomi",
                        Price = 60.0m,
                        Category = ProductCategory.Headphones,
                        Description = "product 100"
                     });

                // SEARCH PRODUCTS
                var products = productService.SearchProducts(
                    new SearchProductOptions
                {
                    ProductId = "PRCT100"
                }).ToList();
                
                //UPDATE PRODUCTS
                var customer = productService.UpdateProduct(
                new UpdateProductOptions { 
                    ProductId = "PRCT100",
                    Name = "lenovo",
                    Price = 35.0m,
                    Category = ProductCategory.Printers
                });
                
                // GET PRODUCTS BY ID
                var product = productService.GetProductById(
                    new GetProductByIdOptions()
                    {
                        ProductId = "PDCT3"
                    });
                
                // CREATE ORDER
                var order = orderService.CreateOrder(
                    new CreateOrderOptions()
                    {
                        CustomerId = 2,
                        DeliveryAddress = "Los Angeles",
                        ProductIds = { "PDCT5","PDCT6","PRCT9"}
                    });
                 
                // SEARCH ORDERS
                var orders = orderService.SearchOrders(
                    new SearchOrderOptions
                    {
                        OrderId = 5
                    }).ToList();
                
                //UPDATE ORDERS
                var orders = orderService.UpdateOrder(
                new UpdateOrderOptions
                {
                    OrderId = 2,
                    DeliveryAddress = "Krania"
                });
                */

            }
        }

        }
    }
