using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebMVC.Service.IService;

namespace WebMVC.Areas.Identity.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginRequestDTO loginRequestDTO)
        {

            bool isValid = await _loginService.IsValid(loginRequestDTO);

            if (isValid && ModelState.IsValid)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Role, await _loginService.GetRole(loginRequestDTO)),
                    new Claim(ClaimTypes.Name, loginRequestDTO.Username),
                    new Claim(ClaimTypes.NameIdentifier, (await _loginService.GetIdAsync(loginRequestDTO)).ToString())  
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your-secret-key123your-secret-key"));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    issuer: "your-app",
                    audience: "your-app",
                    claims: claims,
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: creds);

                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
                HttpContext.Response.Cookies.Append("jwt", tokenString, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTimeOffset.Now.AddHours(1)
                });

                return RedirectToAction("Index", "Home", new { area = "Customer" });
            }

            return View(loginRequestDTO);
        }
        [Authorize]
        public IActionResult Logout()
        {
            // Ví dụ: nếu bạn lưu trong cookie
            Response.Cookies.Delete("jwt");


            return RedirectToAction("Index", "Login");
        }

        [Authorize(Policy = "Policy1")]
        public IActionResult Register()
        {
            return View();
        }

        [Authorize(Policy = "Policy1")]
        [HttpPost] 
        public async Task<IActionResult> Register(RegisterRequestDTO registerRequestDTO)
        {
            bool check = await _loginService.AddUser(registerRequestDTO);
            if (check && ModelState.IsValid 
                && registerRequestDTO.ConfirmPassword == registerRequestDTO.Password)
            {
                TempData["SuccessMessage"] = "Đăng ký thành công!";
                return RedirectToAction("Index", "Home", new {area = "Customer"});
            }
            return View(registerRequestDTO);
        }
    }
}
