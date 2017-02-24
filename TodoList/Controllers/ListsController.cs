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

        //GET: Lists
        public ActionResult Index()
        {
            return View(dbContext.tbLists.ToList());
        }
    }
}
