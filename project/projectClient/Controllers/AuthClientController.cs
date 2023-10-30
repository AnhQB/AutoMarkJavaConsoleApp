using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using project.DTO;

namespace projectClient.Controllers
{
    public class AuthClientController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        public async Task<IActionResult> PostLoginAsync(LoginDTO login)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Login");
            }
            string link = "http://localhost:5000/api/Auth";
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage res = await client.PostAsJsonAsync(link, login))
                {
                    if (res.IsSuccessStatusCode)
                    {
                        using (HttpContent content = res.Content)
                        {
                            string username = await content.ReadAsStringAsync();

                            // Lưu thông tin user vào Session
                            HttpContext.Session.SetString("UserName", username);

                            return RedirectToAction("Index", "Home");
                        }

                    }
                    else
                    {
                        return RedirectToAction("Login");
                    }
                }
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
