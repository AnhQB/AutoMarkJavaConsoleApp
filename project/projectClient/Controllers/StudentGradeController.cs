using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using project.DTO;
using project.Models;

namespace projectClient.Controllers
{
    public class StudentGradeController : Controller
    {
        public async Task<IActionResult> IndexAsync()
        {
            Dictionary<string, ExamResultStudent> data = new Dictionary<string, ExamResultStudent>();
            string link = "http://localhost:5000/api/Grade";
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage res = await client.GetAsync(link))
                {
                    using (HttpContent content = res.Content)
                    {
                        string result = await content.ReadAsStringAsync();

                        data = JsonConvert.DeserializeObject<Dictionary<string, ExamResultStudent>>(result);
                        ViewData["examResults"] = data;
                    }
                }
            }
            return View();
        }
    }
}
