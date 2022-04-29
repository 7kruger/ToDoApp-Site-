using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebToDoApp.Models;
using WebToDoApp.Models.Data;
using WebToDoApp.Models.Entities;

namespace WebToDoApp.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private int userId;
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult CurrentTasks()
        {
            userId = GetCurrentUserId();
            return View(db.ToDoList.Where(model => model.IsDone != true && model.UserId == userId).ToList());
        }

        [Authorize]
        public ActionResult CompletedTasks()
        {
            userId = GetCurrentUserId();
            return View(db.ToDoList.Where(model => model.IsDone == true && model.UserId == userId).ToList());
        }

        [Authorize]
        public ActionResult AddTask()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> AddTask(ToDoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            ToDoItem item = new ToDoItem
            {
                Title = model.Title,
                Description = model.Description,
                IsDone = false,
                UserId = db.Users.FirstOrDefault(u => u.Name == User.Identity.Name).Id
            };
            db.ToDoList.Add(item);
            await db.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }
        [Authorize]
        public async Task<ActionResult> Delete(int id)
        {
            db.ToDoList.Remove(await db.ToDoList.FindAsync(id));
            await db.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }
        [Authorize]
        public async Task<ActionResult> Edit(int id)
        {
            ToDoItem item = await db.ToDoList.FindAsync(id);
            EditViewModel model = new EditViewModel { Id = item.Id, Title = item.Title,
             Description = item.Description };
            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult> Edit(EditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            ToDoItem newItem = new ToDoItem
            {
                Id = model.Id,
                Title = model.Title,
                Description = model.Description,
                IsDone = model.IsDone,
                UserId = GetCurrentUserId()
            };
            db.Entry(newItem).State = System.Data.Entity.EntityState.Modified;
            await db.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }
        [Authorize]
        public async Task<ActionResult> AddToArchive(int id)
        {
            var item = await db.ToDoList.FindAsync(id);
            item.IsDone = true;
            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
            await db.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }

        public int GetCurrentUserId()
        {
            return db.Users.FirstOrDefault(u => u.Name == User.Identity.Name).Id;
        }
    }
}