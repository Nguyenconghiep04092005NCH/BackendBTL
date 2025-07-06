using DNU_QnA_MVC_App.Data;
using DNU_QnA_MVC_App.Repositories;
using DNU_QnA_MVC_App.Repositories.Implementations;
using DNU_QnA_MVC_App.Services.Interfaces;
using DNU_QnA_MVC_App.Services.Implementations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// --------------------------------------------------------
// 1. Add DbContext
// --------------------------------------------------------
builder.Services.AddDbContext<DnuQnADbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// --------------------------------------------------------
// 2. Add Repositories
// --------------------------------------------------------
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
builder.Services.AddScoped<IAnswerRepository, AnswerRepository>();
builder.Services.AddScoped<ITagRepository, TagRepository>();
builder.Services.AddScoped<IVoteRepository, VoteRepository>();

// THÊM DÒNG NÀY → đăng ký dịch vụ Session
builder.Services.AddSession();

// --------------------------------------------------------
// 3. Add Services
// --------------------------------------------------------
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IQuestionService, QuestionService>();
builder.Services.AddScoped<IAnswerService, AnswerService>();
builder.Services.AddScoped<ITagService, TagService>();

// --------------------------------------------------------
// 4. Add MVC and Configuration
// --------------------------------------------------------
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

var app = builder.Build();

// --------------------------------------------------------
// Configure HTTP pipeline
// --------------------------------------------------------
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// THÊM DÒNG NÀY → kích hoạt Session Middleware
app.UseSession();

app.UseAuthorization();

// --------------------------------------------------------
// Default route
// --------------------------------------------------------
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Guest}/{action=Index}/{id?}");

app.Run();
