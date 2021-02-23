using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mis.Data;
using Mis.Models;

namespace Mis.Controllers
{
    public class FindDoctorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FindDoctorController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var doctors = _context
                .DoctorInfo
                .Include(info => info.Hospital)
                .Include(info => info.User)
                .ToList();
            var model = new FindDoctorViewModel() { Doctors = doctors };
            return View(model);
        }

        public IActionResult Find(FindDoctorViewModel model)
        {
            model.Doctors = _context
                .DoctorInfo
                .Include(info => info.Hospital)
                .Include(info => info.User)
                .Where(info => (String.IsNullOrEmpty(model.Position) || info.Position.Contains(model.Position)) &&
                               (String.IsNullOrEmpty(model.HospitalName) || info.Hospital.Name.Contains(model.HospitalName)) && 
                               (String.IsNullOrEmpty(model.FirstName) || info.User.FirstName.Contains(model.FirstName)) &&
                               (String.IsNullOrEmpty(model.LastName) || info.User.LastName.Contains(model.LastName)))
                .ToList();
            return View("Index", model);
        }
    }
}
