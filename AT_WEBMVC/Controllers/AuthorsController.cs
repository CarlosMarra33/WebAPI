using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AT_Core.Models;
using AT_Data;
using AT_WEBMVC.Models;

namespace AT_WEBMVC.Controllers
{
    public class AuthorsController : Controller
    {
        public async Task<ActionResult> Index()
        {
            IEnumerable<AuthorViewModel> books = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:49528/api/");
                var responseTask = await client.GetAsync("AuthorsApi");
                if (responseTask.IsSuccessStatusCode)
                {
                    var readTask = await responseTask.Content.ReadAsAsync<IList<AuthorViewModel>>();
                    books = readTask;
                }
                else 
                {
                    books = Enumerable.Empty<AuthorViewModel>();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(books);
        }

        public ActionResult create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult create(AuthorViewModel author)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:49528/api/");
                var postTask = client.PostAsJsonAsync<AuthorViewModel>("AuthorsApi", author);
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
            return View(author);
        }

        public ActionResult Edit(int id)
        {
            AuthorViewModel author = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:49528/api/");
                var responseTask = client.GetAsync("AuthorsApi?AuthorId=" + id.ToString());
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<AuthorViewModel>();
                    readTask.Wait();
                    author= readTask.Result;
                }
            }
            return View(author);
        }

        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:49528/api/");
                var deleteTask = client.DeleteAsync("AuthorsApi/" + id.ToString());
                deleteTask.Wait();
                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult Details(int? id)
        {
            AuthorViewModel author = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:49528/api/");
                var responseTask = client.GetAsync("AuthorsApi?id=" + id.ToString());
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<AuthorViewModel>();
                    readTask.Wait();
                    author = readTask.Result;
                }
            }

            return View(author);
        }
    }
}
