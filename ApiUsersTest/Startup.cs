using DataAccess;
using DataAccess.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Services;
using Services.Contracts;
using System;
using System.Collections.Generic;

namespace ApiUsersTest
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DBContext>(opt => opt.UseInMemoryDatabase("MyDatabase"));
            services.AddScoped<DBContext>();

            services.AddScoped<IUserManager, UserManager>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            SeedDatabase(app);
        }

        private static void SeedDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<DBContext>();
                // Seed the Database
                var userRepo = context.UserRepository();
                var users = new List<User>()
                {
                    new User { ID = 6, FirstName = "Adrian", LastName = "Perez", Username = "adri", FullName = "Adrian Perez", CreationDate = DateTime.Now},
                    new User { ID = 5, FirstName = "Beatriz Adriana", LastName = "García", Username = "adrina", FullName = "Beatriz Adriana García", CreationDate = DateTime.Now},
                    new User { ID = 1, FirstName = "John", LastName = "Doe", Username = "johndoe", FullName = "John Doe", CreationDate = DateTime.Now},
                    new User { ID = 2, FirstName = "Pedro", LastName = "Garcés", Username = "peter", FullName = "Pedro Garcés", CreationDate = DateTime.Now},
                    new User { ID = 3, FirstName = "Juan", LastName = "Domínguez", Username = "nacho", FullName = "Juan Domínguez", CreationDate = DateTime.Now},
                    new User { ID = 4, FirstName = "Elías", LastName = "Noevas", Username = "eliasjesus", FullName = "Elías Jesus Noevas", CreationDate = DateTime.Now},
                };

                foreach (var user in users)
                {
                    userRepo.Insert(user);
                }

                context.SaveChanges();
            }
        }
    }
}
