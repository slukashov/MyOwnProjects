using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarOwners.Entities.Entities;
using CarOwners.Repositories.Repositories.Interfaces;

namespace CarOwners.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGenericRepository<Car> _carRepository;

        public HomeController(IGenericRepository<Car> carRepository)
        {
            _carRepository = carRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
           

            return View();
        }

        public ActionResult Contact()
        {

            return View();
        }
    }
}