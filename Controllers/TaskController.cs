using ADOTaskbook.DataHelper;
using ADOTaskbook.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ADOTaskbook.Controllers
{
    public class TaskController : Controller
    {
        private readonly ConnectionHelper _connectionHelper;
        public TaskController(ConnectionHelper connectionHelper)
        {
            _connectionHelper = connectionHelper;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Success()
        {
            return View();
        }
        public IActionResult Error()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        #region InsertData
        [HttpPost]
        public IActionResult Add(TaskModel task)
        {

            if (ModelState.IsValid)
            {
                _connectionHelper.Add(task);
                return View("Success");
            }

            return View(task);
        }
        #endregion

        public IActionResult View()
        {
            List<TaskModel> tasks = _connectionHelper.View();
            return View(tasks);
        }

        public IActionResult Update(int id)
        {
            TaskModel Update = _connectionHelper.View().FirstOrDefault(task => task.Id == id);

            if (Update == null)
            {
                return NotFound();
            }

            return View("Update",Update);
        }

        [HttpPost]
        public IActionResult Update(TaskModel updatedTask)
        {
            if (ModelState.IsValid)
            {
                _connectionHelper.Update(updatedTask);
                return RedirectToAction("View");
            }

            return View(updatedTask);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            TaskModel Delete = _connectionHelper.View().FirstOrDefault(task => task.Id == id);

            if (Delete == null)
            {
                return NotFound();
            }

            return View(Delete);
        }

        [HttpPost,ActionName("DeleteConfirmed")]
        public IActionResult DeleteConfirmed(int id)
        {
            _connectionHelper.Delete(id);

            return RedirectToAction("View");
        }

    }
}
