using Microsoft.AspNetCore.Mvc;
using DNU_QnA_MVC_App.Models.ViewModels;
using DNU_QnA_MVC_App.Data;
using DNU_QnA_MVC_App.Models.Entities;
using DNU_QnA_MVC_App.Models.InputModels;
using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Configuration;
using BCrypt.Net;

namespace DNU_QnA_MVC_App.Controllers
{
    public class AccountController : Controller
    {
        private readonly DnuQnADbContext _context;
        private readonly IConfiguration _configuration;

        public AccountController(DnuQnADbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            string email = model.Email;
            if (!email.EndsWith("@gmail.com"))
            {
                ModelState.AddModelError("Email", "Email phải có định dạng @gmail.com");
                return View(model);
            }

            var userExists = _context.Users.Any(u => u.Email == email);
            if (userExists)
            {
                ModelState.AddModelError("", "Email đã tồn tại.");
                return View(model);
            }

            var emailToken = Guid.NewGuid().ToString();
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);

            var user = new User
            {
                Email = model.Email,
                PasswordHash = passwordHash,
                FullName = model.FullName,
                Reputation = 0,
                Role = "User",
                EmailConfirmationToken = emailToken,
                EmailConfirmed = false
            };

            try
            {
                _context.Users.Add(user);
                _context.SaveChanges();

                var confirmLink = Url.Action("ConfirmEmail", "Account",
                    new { email = user.Email, token = emailToken }, Request.Scheme);

                SendVerificationEmail(model.Email, confirmLink);

                TempData["Message"] = $"Link xác nhận đã được gửi tới {email}. Vui lòng kiểm tra email của bạn.";
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Đã xảy ra lỗi khi đăng ký. Vui lòng thử lại sau.");
                // Ghi log lỗi nếu cần: _logger.LogError(ex, "Lỗi khi đăng ký người dùng.");
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult ConfirmEmail(string email, string token)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email && u.EmailConfirmationToken == token);
            if (user == null)
            {
                return NotFound("Token không hợp lệ hoặc người dùng không tồn tại.");
            }

            user.EmailConfirmed = true;
            user.EmailConfirmationToken = null;
            _context.SaveChanges();

            TempData["Message"] = "Xác nhận email thành công!";
            return RedirectToAction("Login");
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            string email = model.Email;
            if (!email.EndsWith("@gmail.com"))
            {
                ModelState.AddModelError("Email", "Email phải có định dạng @gmail.com");
                return View(model);
            }

            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
            {
                ModelState.AddModelError("", "Email hoặc mật khẩu không đúng.");
                return View(model);
            }

            // if (!user.EmailConfirmed)
            // {
            //     ModelState.AddModelError("", "Email chưa được xác thực. Vui lòng kiểm tra email.");
            //     return View(model);
            // }

            // Lưu tạm email vào session (nếu muốn nhớ đăng nhập)
            HttpContext.Session.SetString("UserEmail", user.Email);

            TempData["Message"] = "Đăng nhập thành công!";
            return RedirectToAction("Index", "Home");
        }


        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Guest");
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ForgotPassword(ForgotPasswordInputModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            string email = model.Email;
            if (!email.EndsWith("@gmail.com"))
            {
                ModelState.AddModelError("Email", "Email phải có định dạng @gmail.com");
                return View(model);
            }

            var user = _context.Users.FirstOrDefault(u => u.Email == model.Email);

            TempData["Message"] = $"Nếu email tồn tại, liên kết đặt lại mật khẩu đã được gửi tới {model.Email}.";

            if (user != null)
            {
                try
                {
                    var token = Guid.NewGuid().ToString();
                    user.ResetToken = token;
                    user.ResetTokenExpiry = DateTime.UtcNow.AddHours(1);
                    _context.SaveChanges();

                    var resetLink = Url.Action("ResetPassword", "Account",
                        new { token = token }, Request.Scheme);

                    SendVerificationEmail(model.Email, resetLink, "Đặt lại mật khẩu - DNU Q&A Hub");
                }
                catch (Exception ex)
                {
                    // Ghi log lỗi nếu cần: _logger.LogError(ex, "Lỗi khi gửi email đặt lại mật khẩu.");
                    TempData["Message"] = "Đã xảy ra lỗi khi gửi email đặt lại mật khẩu. Vui lòng thử lại sau.";
                }
            }

            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult ResetPassword(string token)
        {
            var model = new ResetPasswordInputModel
            {
                Token = token
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordInputModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (!model.Email.EndsWith("@gmail.com"))
            {
                ModelState.AddModelError("Email", "Email phải có định dạng @gmail.com");
                return View(model);
            }

            var user = _context.Users
                .FirstOrDefault(u => u.Email == model.Email
                                     && u.ResetToken == model.Token
                                     && u.ResetTokenExpiry.HasValue
                                     && u.ResetTokenExpiry.Value > DateTime.UtcNow);

            if (user == null)
            {
                ModelState.AddModelError("", "Token không hợp lệ hoặc đã hết hạn.");
                return View(model);
            }

            try
            {
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);
                user.ResetToken = null;
                user.ResetTokenExpiry = null;
                _context.SaveChanges();

                TempData["Message"] = "Đặt lại mật khẩu thành công. Hãy đăng nhập.";
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Đã xảy ra lỗi khi đặt lại mật khẩu. Vui lòng thử lại sau.");
                // Ghi log lỗi nếu cần: _logger.LogError(ex, "Lỗi khi đặt lại mật khẩu.");
                return View(model);
            }
        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        private void SendVerificationEmail(string email, string link, string subject = "Xác nhận email")
        {
            var smtpSettings = _configuration.GetSection("Smtp").Get<SmtpSettings>();
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(smtpSettings.SenderName, smtpSettings.SenderEmail));
            message.To.Add(new MailboxAddress("", email));
            message.Subject = subject;

            var builder = new BodyBuilder();
            builder.HtmlBody = $"<p>Click vào link sau để thực hiện thao tác:</p><p><a href='{link}'>{link}</a></p>";
            message.Body = builder.ToMessageBody();

            try
            {
                using (var client = new SmtpClient())
                {
                    client.Connect(smtpSettings.Host, smtpSettings.Port, MailKit.Security.SecureSocketOptions.StartTls);
                    client.Authenticate(smtpSettings.Username, smtpSettings.Password);
                    client.Send(message);
                    client.Disconnect(true);
                }
            }
            catch (Exception ex)
            {
                // Ghi log lỗi nếu cần: _logger.Log)|^Error(ex, "Lỗi khi gửi email xác nhận.");
                throw;
            }
            
        }
        
        public class SmtpSettings
        {
            public string Host { get; set; }
            public int Port { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
            public string SenderName { get; set; }
            public string SenderEmail { get; set; }
        }
    }
}