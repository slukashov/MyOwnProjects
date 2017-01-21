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
            _carRepository.Create(new Car
            {
                CarType = CarType.Passanger,
                Id = Guid.NewGuid(),
                IssueYear = DateTime.Today,
                Mark = "1",
                Model = "2"
            });
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}