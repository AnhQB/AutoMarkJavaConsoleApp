namespace projectClient
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSession();

            app.Use(async (context, next) =>
            {
                var path = context.Request.Path.Value;
                if (!path.StartsWith("/AuthClient/Login") && 
                    !context.Session.TryGetValue("UserName", out byte[] value) &&
                    !path.StartsWith("/Home") &&
                    !path.StartsWith("/AuthClient/PostLogin")
                    )
                {
                    context.Response.Redirect("/AuthClient/Login");
                }
                else
                {
                    await next.Invoke();
                }
            });


            app.MapControllerRoute(
                name: "/Home",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}