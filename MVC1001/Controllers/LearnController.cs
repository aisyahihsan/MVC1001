using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVC1001.Models;

namespace MVC1001.Controllers
{
    public class LearnController : Controller
    {
        public IActionResult Index()
        {
            string country = "Malaysia";
            ViewBag.Country = country;
            ViewBag.University = "UniKL";
            return View();
        }

        [HttpGet]
        public IActionResult BodyMassIndex1()
        {
            return View();
        }

        [HttpPost]
        public IActionResult BodyMassIndex1(Person friend)
        {
            double bmi;
            string bmiClass;

            ViewBag.Name = friend.Name;
            ViewBag.Weight = friend.Weight;
            ViewBag.Height = friend.Height;

            // Calculate bmi
            bmi = ViewBag.Weight / Math.Pow(ViewBag.Height, 2);

            // Calculate bmi class
            if (bmi < 18.5)
                bmiClass = "Underweight";
            else if (bmi < 25)
                bmiClass = "Good";
            else if (bmi < 29)
                bmiClass = "Overweight";
            else
                bmiClass = "Obese";

            ViewBag.Bmi = bmi;
            ViewBag.BmiClass = bmiClass;

            return View("BodyMassIndex1Result");
        }

        [HttpGet]
        public IActionResult BodyMassIndex2()
        {
            return View();
        }

        [HttpPost]
        public IActionResult BodyMassIndex2(Person friend)
        {
            return View("BodyMassIndex2Result", friend);
        }
    }
}
