using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TodoList_v1.Controllers
{
    public class ListsApiController : ApiController
    {
        [HttpPost]
        [ActionName("createTask")]
        public dynamic createTask (string nameCreate)
        {
            return new { success = true, data = nameCreate };
        }
    }
}
