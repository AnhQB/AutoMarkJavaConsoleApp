using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using projectClient.DTO;
using System.Net.Http.Headers;
using System.Text.Json;

namespace projectClient.Controllers
{
    public class GradeClientController : Controller
    {
        public async Task<IActionResult> IndexAsync()
        {
            List<ExamDTO> data = new List<ExamDTO>();
            string link = "http://localhost:5000/api/Exam";
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage res = await client.GetAsync(link))
                {
                    using (HttpContent content = res.Content)
                    {
                        string result = await content.ReadAsStringAsync();

                        data = JsonConvert.DeserializeObject<List<ExamDTO>>(result);
                        ViewData["exams"] = data;
                    }
                }
            }
            return View();
        }

        public async Task<IActionResult> AutoScoreAsync(IFormFile file, int exam, bool isTest)
        {
            FileGradePEDTO fileGrade = new FileGradePEDTO
            {
                file = file,
                examId = exam,
            };

            if (file == null || file.FileName == null)
            {
                TempData["Message"] = "Invalid file";
                return RedirectToAction("Index");
            }

            string link = "http://localhost:5000/api/Grade";
            using (var content = new MultipartFormDataContent())
            {
                var fileContent = new StreamContent(file.OpenReadStream());
                fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                {
                    Name = "file",
                    FileName = file.FileName
                };
                content.Add(fileContent);

                var examContent = new StringContent(exam.ToString());
                examContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                {
                    Name = "examId"
                };
                content.Add(examContent);

                var typeTest = new StringContent(isTest ? "1" : "0");
                examContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                {
                    Name = "type"
                };
                content.Add(typeTest);

                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage res = await client.PostAsJsonAsync(link, content))
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
            }
            return RedirectToAction("Index");
        }
    }
}
