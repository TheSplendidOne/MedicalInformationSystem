using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mis.Data;
using Mis.Models;

namespace Mis.Controllers
{
    public class HospitalProfileController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HospitalProfileController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(String hospitalId)
        {
            var hospital = _context.Hospital.Find(hospitalId);
            if(hospital == null)
            {
                return NotFound($"Unable to load hospital with ID '{hospitalId}'.");
            }
            _context.Entry(hospital).Collection("Doctors").Load();
            foreach (var doctor in hospital.Doctors)
            {
                _context.Entry(doctor).Reference("User").Load();
                _context.Entry(doctor).Reference("Hospital").Load();
            }
            return View(hospital);
        }
    }
}
