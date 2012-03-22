using System;
using Microsoft.SPOT;
using System.Collections;
using MicroWebServer.Web;

namespace MicroWebServer.Api
{
    public class TasksController : IApiController
    {
        ArrayList tasks = new ArrayList();

        public TasksController()
        {
            tasks.Add(new Task { Id = 1, Name = "Do \"\" Stuff", IsDone = true });
            tasks.Add(new Task { Id = 2, Name = "Do Stuff 2", IsDone = false });
        }

        public IEnumerable Get()
        {
            return tasks;
        }

        public void Put(object item)
        {
            throw new NotImplementedException();
        }

        public void Post(object item)
        {
            throw new NotImplementedException();
        }

        public void Delete(object item)
        {
            throw new NotImplementedException();
        }
    }

    public class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDone { get; set; }
    }
}
