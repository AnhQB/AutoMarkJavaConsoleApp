using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using project.Models;
using project.Repositories;
using project.Repositories.IRepositories;
using project.Services;
using project.Services.IServices;

namespace project
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddControllers().AddJsonOptions(option =>
            {
                option.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
            });


            // config Add context
            builder.Services.AddDbContext<project_PRN231Context>(options => 
                options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"))
            );

            // config add OData
            builder.Services.AddControllers().AddOData(option => option.Select().Filter()
                .Count().OrderBy().Expand());

            //Add-start
            builder.Services.AddSwaggerGen();
            builder.Services.AddControllers();
            //builder.Services.AddCors();
            builder.Services.AddCors(act =>
            {
                act.AddPolicy("_MainPolicy", options =>
                {
                    options.AllowAnyHeader();
                    options.AllowAnyMethod();
                    options.AllowAnyOrigin();
                });
            });

            



            //Add-end
            /*
             services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.WithOrigins("http://localhost:5226/")
                                      .WithHeaders("Content-Type", "Authorization")
                                      .AllowAnyMethod());
            });
            */
            /*
                        builder.Services.AddCors(options =>
                        {
                            options.AddDefaultPolicy(
                                policy =>
                                {
                                    policy.WithOrigins("http://localhost:5226/")
                                            .AllowAnyHeader()
                                            .AllowAnyMethod();
                                });
                        });*/


            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.MapControllers();

            app.UseCors("_MainPolicy");
            /*app.UseCors(builder => builder
                .AllowAnyHeader()
                .AllowAnyMethod()
                .SetIsOriginAllowed((host) => true)
                .AllowCredentials()
            );*/

            //app.MapGet("/", () => "Hello World!");


            /*app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/echo",
                    context => context.Response.WriteAsync("echo"))
                    .RequireCors(MyAllowSpecificOrigins);

                endpoints.MapControllers()
                         .RequireCors(MyAllowSpecificOrigins);

                endpoints.MapGet("/echo2",
                    context => context.Response.WriteAsync("echo2"));

                endpoints.MapRazorPages();
            });*/

            app.Run();
        }
    }
}