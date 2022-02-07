using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class CategoryController : Controller
    {

        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            this._db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _db.Categories.ToList();
            return View(objCategoryList);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        //Use in post method inside FORMS!
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            //Modelstate = checks if the data annotations like required etc are true...
            //If no = Error in Create.cshtml
            //If yes = GO!
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The displayorder cannot exactly match the name!");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                //Tempdata => message that shows of when the method was succesfully executed
                TempData["succes"] = "Category created succesfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET => Edit
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //Shorter version first or default! =>
            var categoryFromDb = _db.Categories.Find(id);


            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        //POST
        [HttpPost]
        //Use in post method inside FORMS!
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The displayorder cannot exactly match the name!");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                //Tempdata => message that shows of when the method was succesfully executed
                TempData["succes"] = "Category updated succesfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET => Delete
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //Shorter version first or default! =>
            var categoryFromDb = _db.Categories.Find(id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        //Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.Categories.Find(id);
            if(obj == null)
            {
                return NotFound();
            }
            _db.Categories.Remove(obj);
            _db.SaveChanges();
            //Tempdata => message that shows of when the method was succesfully executed
            TempData["succes"] = "Category deleted succesfully";
            return RedirectToAction("Index");
        }

    }
}
