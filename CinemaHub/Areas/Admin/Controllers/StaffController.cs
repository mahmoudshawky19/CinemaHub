using CinemaHub.Repository;
using DataAccess.IRepository;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace CinemaHub.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class StaffController : Controller
    {
        private readonly IStaffRepository staffRepository;
        public StaffController(IStaffRepository staffRepository)
        {
            this.staffRepository = staffRepository;
        }
        public IActionResult Index()
        {
          var staffs =   staffRepository.GetAll().ToList();
            return View(staffs);
        }
        [HttpGet]
        public IActionResult Create()
        {
            Staff staff = new Staff();
            return View(staff);
        }
        [HttpPost]
        public IActionResult Create(Staff staff)
        {
            if(ModelState.IsValid)
            {
                staffRepository.Add(staff);
                staffRepository.Commit();
                TempData["success"] = "Add Staff Successfully";
                return RedirectToAction("Index");


            }
            return View(staff);
        }
        public IActionResult Edit(int staffId)
        {
            var staff = staffRepository.GetOne([], e=>e.Id == staffId).FirstOrDefault();
            return View(staff);
        }
        [HttpPost]
        public IActionResult Edit(Staff staff)
        {
            if (ModelState.IsValid)
            {
                staffRepository.Edit(staff);
                staffRepository.Commit();
                TempData["success"] = "Edit Staff Successfully";
                return RedirectToAction("Index");


            }
            return View(staff);
        }
        public IActionResult Delete(int staffId)
        {
            Staff c = new Staff() { Id = staffId };
            staffRepository.Delete(c);
            staffRepository.Commit();
            TempData["success"] = "Delete staff Successfully";

            return RedirectToAction(nameof(Index));

        }


    }
}
