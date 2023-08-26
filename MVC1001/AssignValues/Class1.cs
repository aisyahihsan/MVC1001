using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC1001.AssignValues
{
    public class PosLaju
    {
        public string[] weightCategories =
            {"<0.50kg", "<0.75kg", "<1.00kg", "1.25kg", "<1.50kg",
               "<1.75kg", "<2.00kg", "<2.50kg", "<3.00kg"};

        public string[] zones = { "West Malaysia", "Sarawak", "Sabah" };

        public double[,] rates =
        {
            {6.00, 8.50, 9.00 },
            {7.00, 10.50, 12.00 },
            { 8.50, 12.50, 14.50},
            {10.00, 14.50, 17.00 },
            {11.00, 16.50, 20.00 },
            {12.50, 18.50, 22.50 },
            {14.00, 20.50, 25.00 },
            {21.00, 34.50, 41.00},
            {24.00, 39.00, 46.00 }
        };
    }

    public class SoftwareHouse
    {
        public string[] employees = { "Sarah", "Hafiz", "Amin" };

        public string[][] skills =
        {
            new string[] {"C#", "VB", "Java", "PHP"},
            new string[] {"Python", "R"},
            new string[] {"React", "Ionic", "Flutter"}
        };
    }

    public class CourseGrade
    {
        public string[] students = { "Sarah", "Hafiz", "Amin" };

        public string[][,] courses =
        {
            new string[,] { { "C#", "A"} , { "VB", "B+"},
                 {"Java", "B" }, {"PHP", "A-"} },
            new string[,] { { "Python", "B"}, { "R", "A-"} },
            new string[,] { { "React", "C+"}, { "Ionic", "B"},
                 {"Flutter", "A" }}
        };
    }

}
