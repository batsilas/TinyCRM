using Microsoft.EntityFrameworkCore.Internal;
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
    public class CustomerService : ICustomerService
    {
        private TinyCrmDbContext context;

        public CustomerService(TinyCrmDbContext contextByProgram) {
            context = contextByProgram;
        }
        public Customer CreateCustomer(
            CreateCustomerOptions options)
        {
            if (options == null)
            {
                return null;
            }
            var customer = new Customer() {
                Created = DateTimeOffset.Now,
                Firstname = options.Firstname,
                Lastname = options.Lastname,
                Email = options.Email,
                VatNumber = options.VatNumber,
                IsActive = true
            };

            context.Add(customer);

            if (context.SaveChanges() > 0)
            {
                return customer;
            }

            return null;
        }

        public IQueryable<Customer> SearchCustomers(
            SearchCustomerOptions options) {

            if (options == null)
            {
                return null;
            }

            var query = context
                .Set<Customer>()
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(options.Firstname))
            {
                query = query.Where(c => c.Firstname == options.Firstname);
            }

            if (!string.IsNullOrWhiteSpace(options.Lastname))
            {
                query = query.Where(c => c.Lastname == options.Lastname);
            }

            if (!string.IsNullOrWhiteSpace(options.VatNumber))
            {
                query = query.Where(c => c.VatNumber == options.VatNumber);
            }

            if (!string.IsNullOrWhiteSpace(options.Email))
            {
                query = query.Where(c => c.Email == options.Email);
            }

            if (options.CustomerId != null)
            {
                query = query.Where(c => c.CustomerId == options.CustomerId.Value);
            }

            if (options.CreateFrom != null)
            {
                query = query.Where(c => c.Created >= options.CreateFrom);
            }

            if (options.CreateTo != null)
            {
                query = query.Where(c => c.Created <= options.CreateTo);
            }
            
            query = query.Take(500);

            return query;

        }

        public bool UpdateCustomer(
            UpdateCustomerOptions options) {

            if (options == null || options.CustomerId==null)
            {
                return false;
            }

            var customer = SearchCustomers(new SearchCustomerOptions() { 
                CustomerId = options.CustomerId
            }).SingleOrDefault();

            if (!string.IsNullOrWhiteSpace(options.Firstname)) { 
                customer.Firstname = options.Firstname;
            }

            if (!string.IsNullOrWhiteSpace(options.Lastname))
            {
                customer.Lastname = options.Lastname;
            }

            customer.IsActive = options.IsActive;
            
            if (!string.IsNullOrWhiteSpace(options.Email))
            {
                customer.Email = options.Email;
            }

            if (context.SaveChanges() > 0)
            {
                return true;
            }

            return false;
        }

        public Customer GetCustomerById(
            GetCustomerByIdOptions options) {

            if (options == null) 
            {
                return null;
            }

            var customer = context
                .Set<Customer>()
                .Where(cust => cust.CustomerId == options.CustomerId)
                .SingleOrDefault();
            if (customer != null) {
                return customer;
            }
            return null;
        }
    }
}