using Microsoft.AspNetCore.Mvc;
using Microsoft.OData.Edm;
using Newtonsoft.Json;
using project.DTO;
using project.Models;
using System.Collections.Generic;

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

        public async Task<IActionResult> DetailAsync(int id, int studentId)
        {
            ViewData["examResultId"] = id;
            ViewData["studentId"] = studentId;
            List<GradeDetailStudent> data = new List<GradeDetailStudent>();
            string link = "http://localhost:5000/api/Grade/GradeDetail";
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage res = await client.GetAsync(link + "?examresultId=" + id))
                {
                    using (HttpContent content = res.Content)
                    {
                        string result = await content.ReadAsStringAsync();

                        data = JsonConvert.DeserializeObject<List<GradeDetailStudent>>(result);
                        ViewData["details"] = data;
                    }
                }
            }
            return View();
        }
    }
}
