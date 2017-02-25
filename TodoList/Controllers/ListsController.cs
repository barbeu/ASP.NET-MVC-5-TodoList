using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TodoList.lib.Entity;

namespace TodoList.Controllers
{
    public class ListsController : Controller
    {
        private dbTodoListEntities dbContext;

        public ListsController()
        {
            dbContext = new dbTodoListEntities();
        }

        public ActionResult Index()
        {
            return View(dbContext.tbLists.ToList());
        }

        [HttpPost]
        public RedirectResult CreateTask(string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                tbLists tmp = new tbLists();
                tmp.cName = name;
                tmp.isDone = false;
                dbContext.tbLists.Add(tmp);

                dbContext.SaveChanges();
            }

            return Redirect("/Lists/Index");
        }

        [HttpPost]
        public RedirectResult DoneTrue(int id)
        {
            dbContext.tbLists.Where(l => l.id == id).First().isDone = true;
            dbContext.SaveChanges();

            return Redirect("/Lists/Index");
        }

        [HttpPost]
        public RedirectResult DoneFalse(int id)
        {
            dbContext.tbLists.Where(l => l.id == id).First().isDone = false;
            dbContext.SaveChanges();

            return Redirect("/Lists/Index");
        }

        [HttpPost]
        public RedirectResult DeleteTask(int id)
        {
            tbLists remove = dbContext.tbLists.Where(l => l.id == id).FirstOrDefault();
            if (remove != null)
            {
                dbContext.tbLists.Remove(dbContext.tbLists.Where(l => l.id == id).First());
                dbContext.SaveChanges();
            }

            return Redirect("/Lists/Index");
        }
    }
}
