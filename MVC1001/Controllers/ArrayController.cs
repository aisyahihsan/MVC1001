using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVC1001.AssignValues;

namespace MVC1001.Controllers
{
    public class ArrayController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult IntegerArray()
        {
            int[] numbers = { 6, 3, 1, 2, 5, 4 };
            ViewBag.Numbers = numbers;
            ViewBag.CountNumber = numbers.Count();
            ViewBag.MaxNumber = numbers.Max();
            ViewBag.MinNumber = numbers.Min();
            ViewBag.SumNumbers = numbers.Sum();
            ViewBag.AverageNumbers = numbers.Average();
            return View();
        }

        public IActionResult StringArray()
        {
            string[] pets = { "turtle", "dog", "rabbit", "cat", "hamster"  };
            ViewBag.Pets = pets;

            ViewBag.Result1 = pets.OrderBy(x => x);
            ViewBag.Result2 = pets.OrderByDescending(x => x);
            return View();
        }

        public IActionResult PosLajuRatesArray()
        {
            PosLaju posLaju = new PosLaju();
            ViewBag.WeightCategories = posLaju.weightCategories;
            ViewBag.Zones = posLaju.zones;
            ViewBag.Rates = posLaju.rates;
            return View();
        }

        public IActionResult MultiplicationArray()
        {
            int[,] multiplications = new int[13, 13];
            for (int i = 1; i <= 12; i++)
            {
                for (int j = 1; j <= 12; j++)
                {
                    multiplications[i, j] = i * j;
                }
            }
            ViewBag.Multiplications = multiplications;
            return View();
        }

        public IActionResult Jagged1DimensionalArray()
        {
            SoftwareHouse softHouse = new SoftwareHouse();
            ViewBag.Employees = softHouse.employees;
            ViewBag.Skills = softHouse.skills;
            return View();
        }

        public IActionResult Jagged2DimensionalArray()
        {
            CourseGrade cGrade = new CourseGrade();
            ViewBag.Students = cGrade.students;
            ViewBag.Courses = cGrade.courses;
            return View();
        }
    }
}
