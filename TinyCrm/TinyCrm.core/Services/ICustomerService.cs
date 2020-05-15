using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyCrm.Core.Model;
using TinyCrm.Core.Services.Options;

namespace TinyCrm.Core.Services
{
    public interface ICustomerService
    {

        Customer CreateCustomer(
            CreateCustomerOptions options);

        IQueryable<Customer> SearchCustomers(
            SearchCustomerOptions options);
        bool UpdateCustomer(
            UpdateCustomerOptions options);
       
        Customer GetCustomerById(
            GetCustomerByIdOptions options);
                    
    }
}