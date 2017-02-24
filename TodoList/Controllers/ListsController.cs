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
        public ActionResult CreateTask(List<string> names)
        {
            foreach(string name in names)
            {
                tbLists tmp = new tbLists();
                tmp.cName = name;
                tmp.isDone = false;
                dbContext.tbLists.Add(tmp);
            }
           
            dbContext.SaveChanges();
            ModelState.Clear();

            return View("Index", dbContext.tbLists.ToList());
        }
    }
}
