using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using projectClient.DTO;
using System.Security.Policy;

namespace projectClient.Controllers
{
    public class ExamController : Controller
    {
        public async Task<IActionResult> IndexAsync()
        {
            Dictionary<string, List<ExamDTO>> data = new Dictionary<string, List<ExamDTO>>();
            string link = "http://localhost:5000/api/Exam/All";
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage res = await client.GetAsync(link))
                {
                    using (HttpContent content = res.Content)
                    {
                        string result = await content.ReadAsStringAsync();

                        data = JsonConvert.DeserializeObject<Dictionary<string, List<ExamDTO>>>(result);
                        ViewData["exams"] = data;
                    }
                }
            }
            return View();
        }

        public IActionResult Add()
        {
            return View();
        }

        public async Task<ActionResult> PostAddAsync(ExamDTO pub)
        {
            string link = "http://localhost:5000/api/Exam";
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage res = await client.PostAsJsonAsync(link, pub))
                {
                    if (res.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Added");
                        TempData["Message"] = "added";
                    }
                    else
                    {
                        Console.WriteLine("Fail");
                        TempData["Message"] = "Fail";
                    }
                }
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> EditAsync(int id, int paperNo)
        {
            ExamDTO author = new ExamDTO();
            string link = "http://localhost:5000/api/Exam/";
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage res = await client.GetAsync(link + id + "/" + paperNo))
                {
                    using (HttpContent content = res.Content)
                    {
                        string result = await content.ReadAsStringAsync();
                        author = JsonConvert.DeserializeObject<ExamDTO>(result);
                    }
                }
            }
            return View(author);
        }

        public async Task<IActionResult> PostUpdateAsync(ExamDTO pub)
        {
            string link = "http://localhost:5000/api/Exam";
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage res = await client.PutAsJsonAsync(link, pub))
                {
                    if (res.IsSuccessStatusCode)
                    {
                        //Console.WriteLine("Updated");
                        TempData["Message"] = "Updated Success";
                    }
                    else
                    {
                        //Console.WriteLine("Fail");
                        TempData["Message"] = "Fail";
                    }
                }
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id, int paperNo)
        {
            string link = "http://localhost:5000/api/Exam";
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage res = await client.DeleteAsync(link + id + "/" + paperNo))
                {
                    if (res.IsSuccessStatusCode)
                    {
                        ViewBag.Message = "Delete success";
                    }
                    else
                    {
                        ViewBag.Message = "Delete fail";
                    }
                }

            }
            return RedirectToAction("Index");
        }
    }
}
