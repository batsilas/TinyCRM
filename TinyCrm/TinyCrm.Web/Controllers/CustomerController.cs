﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TinyCrm.Core.Data;
using TinyCrm.Core.Model;
using TinyCrm.Core.Services;
using TinyCrm.Core.Services.Options;

namespace TinyCrm.Web.Controllers
{
    public class CustomerController : Controller
    {
        private TinyCrmDbContext dbContext_;
        private ICustomerService customerService_;

        public CustomerController()
        {
            dbContext_ = new TinyCrmDbContext();
            customerService_ = new CustomerService(dbContext_);
        }

        public IActionResult Index()
        {
            var customerList = customerService_
                .SearchCustomers(new SearchCustomerOptions())
                .ToList();

            return Json(customerList);
        }

        public IActionResult Search(SearchCustomerOptions options)
        {
            if (options == null)
            {
                return BadRequest();
            }

            var query = dbContext_
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

            if (query==null) 
            {
                return NotFound();
            }
            return Json(query.ToList());

        }

        public IActionResult GetById(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var customer = customerService_
                .SearchCustomers(
                    new SearchCustomerOptions()
                    {
                        CustomerId = id
                    }).SingleOrDefault();

            if (customer == null)
            {
                return NotFound();
            }

            return Json(customer);
        }
    }
}
//400 Bad Request
//403 Forbidden
//404 Not Found
//500 Internal Server Error