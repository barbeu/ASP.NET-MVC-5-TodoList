using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TodoList_v1.lib.Entity;

namespace TodoList_v1.Controllers
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
            int id = 0;
            if (Session["id"] != null)
                id = (int)Session["id"];

            return View(dbContext.tbLists.Where(l => l.cUserId == id).ToList());
        }

        [HttpPost]
        public RedirectResult CreateTask(string nameCreate)
        {           
            if (!string.IsNullOrWhiteSpace(nameCreate))
            {
                tbLists tmp = new tbLists();
                tmp.cName = nameCreate;
                tmp.isDone = false;
                tmp.cUserId = (int)Session["id"];
                dbContext.tbLists.Add(tmp);

                dbContext.SaveChanges();
            }

            return Redirect("/Lists/Index");
        }

        [HttpPost]
        public RedirectResult DoneTrue(int id)
        {
            int idSession = (int)Session["id"];
            dbContext.tbLists.Where(l => (l.id == id) && (l.cUserId == idSession)).First().isDone = true;
            dbContext.SaveChanges();

            return Redirect("/Lists/Index");
        }

        [HttpPost]
        public RedirectResult DoneFalse(int id)
        {
            int idSession = (int)Session["id"];
            dbContext.tbLists.Where(l => (l.id == id) && (l.cUserId == idSession)).First().isDone = false;
            dbContext.SaveChanges();

            return Redirect("/Lists/Index");
        }

        [HttpPost]
        public RedirectResult DeleteTask(int id)
        {
            int idSession = (int)Session["id"];
            tbLists remove = dbContext.tbLists.Where(l => (l.id == id) && (l.cUserId == idSession)).FirstOrDefault();
            if (remove != null)
            {
                dbContext.tbLists.Remove(dbContext.tbLists.Where(l => (l.id == id) && (l.cUserId == idSession)).First());
                dbContext.SaveChanges();
            }

            return Redirect("/Lists/Index");
        }

        [HttpPost]
        public RedirectResult RemoveTasks (bool boolean)
        {
            int idSession = (int)Session["id"];
            if (dbContext.tbLists.Where(l => (l.isDone == boolean) && (l.cUserId == idSession)).Count() > 0)
            {
                if (dbContext.tbLists.Where(l => (l.isDone == boolean) && (l.cUserId == idSession)).Count() > 1)
                    dbContext.tbLists.RemoveRange(dbContext.tbLists.Where(l => (l.isDone == boolean) && (l.cUserId == idSession)).ToList());
                else
                    dbContext.tbLists.Remove(dbContext.tbLists.Where(l => (l.isDone == boolean) && (l.cUserId == idSession)).FirstOrDefault());

                dbContext.SaveChanges();
            }

            return Redirect("/Lists/Index");
        }
    }
}
