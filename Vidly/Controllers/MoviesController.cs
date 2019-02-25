using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using System.Data.Entity;
using Vidly.ViewModel;
using Vidly.ViewModels;

namespace Vidly.Controllers
{

    public class MoviesController : Controller
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
                       _context.Dispose();
        }
        //GET: Movies
        public ActionResult Index()
        {
            //            var movie = GetMovies();
            //            return View(movie);
           // var movie = _context.Movies.Include(m => m.Genre).ToList();
            //return View(movie);
            if (User.IsInRole(RoleName.CanManageMovies))
                return View("List");

            return View("ReadOnlyList");
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        //GET: Movies/details
        public ActionResult Details(int id)
        {
            var movie = _context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == id);

            //http://localhost:50806/Customers/Details/3
            //No user with id 3 so return not 404
            if (movie == null)
                return HttpNotFound();

            return View(movie);
        }

       [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult New()
        {
            var genres = _context.Genres.ToList();
            var viewModel = new MovieFormViewModel
            {
                //without his the id won't get generated so import Movie model into this
               // Movie = new Movie(),
                Genres = genres
            };
            return View("MovieForm",viewModel);
        }

       [Authorize(Roles = RoleName.CanManageMovies)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new MovieFormViewModel(movie)
                {
                    Genres = _context.Genres.ToList()
                };
                return View("MovieForm", viewModel);
            }
            if (movie.Id == 0)
            { 
                movie.DateAdded = DateTime.Now;
                _context.Movies.Add(movie);
            }
            else
            {
                var movieInDb = _context.Movies.Single(c => c.Id == movie.Id);

                movieInDb.Name = movie.Name;
                movieInDb.GenreId = movie.GenreId;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.NumberInStock = movie.NumberInStock;
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Movies");
        }

       [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Edit(int id)
        {

            var movie = _context.Movies.SingleOrDefault(c => c.Id == id);
            if (movie == null)
                HttpNotFound();
            var viewModel = new MovieFormViewModel(movie)
            {
                Genres = _context.Genres.ToList()
            };

            //it search for the view with the name Edit, so use "" to overwrite it
            return View("MovieForm", viewModel);
        }

        //        private IEnumerable<Movie> GetMovies()
        //        {
        //            return new List<Movie>
        //            {
        //                new Movie {Id= 1 ,Name = "Shrek"},
        //                new Movie {Id= 2 ,Name = "Wall-e"}
        //
        //            };
        //        }
        //        // GET: Movies/random
        //        public ActionResult Random()
        //        {
        //            var movie = new Movie() {Name = "Sherk!!"};
        //            var customers = new List<Customer>()
        //            {
        //                new Customer{ Name = "Customer 1"},
        //                new Customer{ Name = "Customer 2"}
        //            };
        //            var viewModel = new RandomMovieViewModel
        //            {
        //                Movie = movie,
        //                Customers = customers
        //            };
        //            return View(viewModel);
        //        }
        //
        //        // Movies/Edit/1
        //        public ActionResult Edit(int id)
        //        {
        //            return Content("id= " + id);
        //        }
        //
        //        //Index with optional parameters
        //        public ActionResult Index(int? pageIndex, string sortBy)
        //        {
        //            if (!pageIndex.HasValue)
        //                pageIndex = 1;
        //            if (String.IsNullOrWhiteSpace(sortBy))
        //                sortBy = "Name";
        //            return Content(String.Format("pageIndex={0}&sortBy={1}", pageIndex, sortBy));
        //        }
        //
        //        //Attribute routing
        //        //define 2 constraints by seperating by :
        //        [Route("movies/released/{year}/{month:regex(\\d{2}):range(1,12)}")]
        //        public ActionResult ByReleaseDate(int year, int month)
        //        {
        //            return Content(year + "/" + month);
        //        }
    }
}