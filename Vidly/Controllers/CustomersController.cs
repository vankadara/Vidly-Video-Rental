using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult Index()
        {
            //var customer = GetCustomers();
            //include the membership table 
            //var customer = _context.Customers.Include(c => c.MembershipType).ToList();
            //return View(customer);
            return View();
        }

        public ActionResult New()
        {
            var membershipTypes = _context.MembershipTypes.ToList();
            var viewModel = new CustomerFormViewModel
            {
                //import customer model to generate the customer id 
                Customer = new Customer(),
                MembershipTypes = membershipTypes
            };
            return View("CustomerForm",viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer)
        {
            //if the form data is not valid redirect to the same form and popup the error like "Name is Required"
            if (!ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipTypes = _context.MembershipTypes.ToList()
                };
                return View("CustomerForm", viewModel);
            }
            //If the customer is new the id is zero, so create a new customer
            if (customer.Id == 0)
                _context.Customers.Add(customer);
            else
            {
                var customerInDb = _context.Customers.Single(c => c.Id == customer.Id);

                customerInDb.Name = customer.Name;
                customerInDb.BirthDate = customer.BirthDate;
                customerInDb.MembershipTypeid = customer.MembershipTypeid;
                customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Customers");
        }

        public ActionResult Details(int id)
        {
            //            var customer = GetCustomers().SingleOrDefault(c => c.Id == id);
            //include the membership table 
            var customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);

            //http://localhost:50806/Customers/Details/3
            //No user with id 3 so return not 404
            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }

//        private IEnumerable<Customer> GetCustomers()
//        {
//            return new List<Customer>
//            {
//                new Customer { Id = 1, Name = "John Smith" },
//                new Customer { Id = 2, Name = "Mary Williams" }
//            };
//        }
        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
                return HttpNotFound();
            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList()
            };
            //it search for the view with the name Edit, so use "" to overwrite it
            return View("CustomerForm", viewModel);
        }
    }
    }