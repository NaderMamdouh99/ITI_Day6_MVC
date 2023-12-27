using Day_7.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Day_7.Controllers
{
    public class Department1 : Controller
    {
        ItiContext context = new ItiContext();
        public IActionResult Index()
        {
            return View(context.Departments.ToList());
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Instructor = new SelectList(context.Instructors.ToList(), "InsId", "InsName");
            return View(new Department());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Department departmentt) 
        {
            
            if (ModelState.IsValid)
            {
                try
                {
                    context.Departments.Add(departmentt);
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }catch (Exception ex)
                {
                    ViewBag.Instructor = new SelectList(context.Instructors.ToList(), "InsId", "InsName");
                    ModelState.AddModelError("DatabaseError", "Error has any body");
                    return View(departmentt);
                }

            }
            ViewBag.Instructor = new SelectList(context.Instructors.ToList(), "InsId", "InsName");
            return View(departmentt);
        }

        public IActionResult Delete(int id)
        {
            var found = context.Departments.Where(D => D.DeptId == id).Select(D=>D).FirstOrDefault();
            var found2 = context.Instructors.Where(D => D.DeptId == id).ToList();
            if(found != null)
            {
                context.Departments.Remove(found);
                context.Instructors.RemoveRange(found2);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {
            var found = context.Departments.Where(D => D.DeptId == id).FirstOrDefault();
            if (found != null )
            {
                ViewBag.Instructor = new SelectList(context.Instructors.ToList(), "InsId", "InsName" ,found.DeptManager);
                return View(found);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Update(Department department)
        {
            var found = context.Departments.Where(D => D.DeptId == department.DeptId).FirstOrDefault();
            if (ModelState.IsValid)
            {
                found.DeptId = department.DeptId;
                found.DeptName = department.DeptName;
                found.DeptDesc = department.DeptDesc;
                found.DeptLocation = department.DeptLocation;
                found.DeptManager = department.DeptManager;
                found.Hiredate = department.Hiredate;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Instructor = new SelectList(context.Instructors.ToList(), "InsId", "InsName", found.DeptManager);
            return View(found);


        }
    }
}
