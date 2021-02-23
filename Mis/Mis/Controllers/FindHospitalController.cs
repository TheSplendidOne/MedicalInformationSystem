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
    public class FindHospitalController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FindHospitalController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var hospitals = _context.Hospital.ToList();
            var model = new FindHospitalViewModel() { Hospitals = hospitals };
            return View(model);
        }

        public IActionResult Find(FindHospitalViewModel model)
        {
            model.Hospitals = _context
                .Hospital
                .Where(hospital => (String.IsNullOrEmpty(model.Name) || hospital.Name.Contains(model.Name)) &&
                               (String.IsNullOrEmpty(model.Address) || hospital.Address.Contains(model.Address)))
                .ToList();
            return View("Index", model);
        }
    }
}
