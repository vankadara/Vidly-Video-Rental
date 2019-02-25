using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.ViewModel
{
    public class RandomMovieViewModel
    {
        //public type and random name
        public Movie Movie{ get; set; }
        //public type and random name
        public List<Customer> Customers{ get; set; }
    }
}