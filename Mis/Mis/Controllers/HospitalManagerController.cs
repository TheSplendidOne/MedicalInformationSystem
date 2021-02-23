using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Mis.Data;
using Mis.Models;

namespace Mis.Controllers
{
    public class HospitalManagerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HospitalManagerController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Hospital.ToListAsync());
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AddHospital()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult AddHospital(Hospital hospital)
        {
            if(hospital != null)
            {
                hospital.Id = Guid.NewGuid().ToString();
                _context.Hospital.Add(hospital);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult ManageHospital(string hospitalId)
        {
            return View(_context.Hospital.Find(hospitalId));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult ManageHospital(Hospital hospital)
        {
            if(hospital != null)
            {
                _context.Hospital.Update(hospital);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
