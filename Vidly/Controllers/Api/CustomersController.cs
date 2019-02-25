using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Data.Entity;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Web.Http;
using AutoMapper;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class CustomersController : ApiController
    {
        //get the dbcontext to access the DB
        private ApplicationDbContext _context;

        //initialize the Db context
        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }


        //CRUD(Create, Read, Update, Delete) actions

        //Read all
        //GET /api/customers
        //        public IHttpActionResult GetCustomers()
        //        {
        //            //return the list of customers
        //            //return _context.Customers.ToList();
        //
        //            //Map the list of customers to cusztomerDto
        //            var customerDto = _context.Customers
        //                .Include(c => c.MembershipType)
        //                .ToList()
        //                .Select(Mapper.Map<Customer, CustomerDto>);
        //
        //            return Ok(customerDto);
        //        }
        //this is modified for rental autofill feature
        public IHttpActionResult GetCustomers(string query = null)
        {

            var customersQuery = _context.Customers.Include(c => c.MembershipType);

            if (!String.IsNullOrWhiteSpace(query))
                customersQuery = customersQuery.Where(c => c.Name.Contains(query));

            var customerDtos = customersQuery.ToList()
                .Select(Mapper.Map<Customer, CustomerDto>);

            return Ok(customerDtos);

        }

        //Read 1
        //GET /api/customers/1
        public IHttpActionResult GetCustomer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            //if the customer not found in the DB
            if (customer == null)
            {
                return NotFound();
            }

            //return customer;

            //Here we are returning only one object
            return Ok(Mapper.Map<Customer,CustomerDto>(customer));
        }

        //Create
        //POST /api/customers
        [HttpPost]
        public IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {
           //if the entered data is not valid
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            //Mapper the customerDto to domain object
            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);

            _context.Customers.Add(customer);
            _context.SaveChanges();

            //This will produce an customer with id so we need to add this to our Dto and sedn to the client
            customerDto.Id = customer.Id;

            //•	As a part if restful coventions We need to return the URI(*.*/api/customers/10) of the created customer and the created object
            return Created(new Uri(Request.RequestUri + "/" + customer.Id), customerDto );
        }

        //Update
        //PUT /api/customers/1
        [HttpPut]
        public IHttpActionResult UpdateCustomer(int id, CustomerDto customerDto)
        {
            //if the entered data is not valid
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            //check whether the customer is in db or not
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customerInDb == null)
                return NotFound();

            //Map a Customer Dto to customer
            // pass the source objct and already existing object
            Mapper.Map(customerDto, customerInDb);

//            customerInDb.Name = customer.Name;
//            customerInDb.BirthDate = customer.BirthDate;
//            customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
//            customerInDb.MembershipTypeid = customer.MembershipTypeid;

            _context.SaveChanges();
            return Ok();
        }

        //DELETE /api/customers/1
        [HttpDelete]
        public IHttpActionResult DeleteCustomer(int id)
        {
            //check whether the customer exist in DB
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customerInDb == null)
                return NotFound();

            _context.Customers.Remove(customerInDb);
            _context.SaveChanges();

            return Ok();
        }
    }
}
