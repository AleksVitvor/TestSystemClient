using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Services.Answer;
using Services.CryptoService;
using Services.Login.Login;
using Services.Login.Registration;
using Services.Mark;
using Services.QuestionService;
using Services.TestService.Tests;
using Services.User;
using System;

namespace TestSystemClient
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
            services.AddControllersWithViews();
            services.AddKendo();

            services
                .AddMvc()
                .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

            services.AddTransient<IRegistrationService, RegistrationService>();
            services.AddTransient<ILoginService, LoginService>();
            services.AddTransient<ITestService, TestService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IQuestionService, QuestionService>();
            services.AddTransient<IAnswerService, AnswerService>();
            services.AddTransient<IMarkService, MarkService>();
            services.AddTransient<ICrypto, CryptoService>();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new PathString("/Login/Login");
                    options.AccessDeniedPath = new PathString("/Login/Login");
                });
#if DEBUG
            services.AddHttpClient("notauthorized", c =>
            {
                c.BaseAddress = new Uri("http://localhost:3000/");
            });
            services.AddHttpClient("authorized", c =>
            {
                c.BaseAddress = new Uri("http://localhost:3000/");
            });
#else
            services.AddHttpClient("notauthorized", c =>
            {
                c.BaseAddress = new Uri("https://courcestage.herokuapp.com/");
            });

            services.AddHttpClient("authorized", c =>
            {
                c.BaseAddress = new Uri("https://courcestage.herokuapp.com/");
            });
#endif

            services.AddHttpContextAccessor();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Login/Login");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Login}/{action=Login}");
            });
        }
    }
}
