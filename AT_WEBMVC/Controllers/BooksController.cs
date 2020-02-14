using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using AT_WEBMVC.Models;

namespace AT_WEBMVC.Controllers
{
    public class BooksController : Controller
    {
        public async Task<ActionResult> Index()
        {
            IEnumerable<BookViewModel> books = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:49528/api/");
                //HTTP GET
                var responseTask = await client.GetAsync("BooksApi");


                if (responseTask.IsSuccessStatusCode)
                {
                    var readTask = await responseTask.Content.ReadAsAsync<IList<BookViewModel>>();

                    books = readTask;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    books = Enumerable.Empty<BookViewModel>();

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
        public ActionResult create(BookViewModel books)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:49528/api/");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<BookViewModel>("BooksApi", books);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(books);
        }

        public ActionResult Edit(int id)
        {
            BookViewModel books = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:49528/api/");
                //HTTP GET
                var responseTask = client.GetAsync("BooksApi?BookId=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<BookViewModel>();
                    readTask.Wait();

                    books = readTask.Result;
                }
            }

            return View(books);
        }

        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:49528/api/");

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("BooksApi/" + id.ToString());
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            BookViewModel books = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:49528/api/");
                var responseTask = client.GetAsync("AuthorsApi?AuthorId=" + id.ToString());
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<BookViewModel>();
                    readTask.Wait();
                    books = readTask.Result;
                }
            }

            return View(books);
        }

    }

}




