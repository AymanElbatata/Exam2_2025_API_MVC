using Exam2025.DTO;
using Exam2025.PL.DTO;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Exam2025.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountController(HttpClient httpClient, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;

            _httpClient.BaseAddress = new Uri(_configuration["APISettings:BaseApiUrl"]);
            //_httpClient.BaseAddress = new Uri(new AppSettingsHelper(_configuration, _httpContextAccessor).GetBaseUrl());
        }

        #region Login
        [AllowAnonymous]
        public IActionResult Login(string? Message)
        {
            if (!string.IsNullOrEmpty(Message))
            {
                ViewBag.Message = Message;
            }
            ViewBag.URLAPI = _httpClient.BaseAddress;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = await _httpClient.PostAsJsonAsync("Account/Login", model);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadFromJsonAsync<ReturnValue_GenericDTO<UserDTO>>();
                    if (responseContent != null && responseContent.data.Token != null)
                    {
                        var token = responseContent.data.Token;

                        var handler = new JwtSecurityTokenHandler();
                        var jwt = handler.ReadJwtToken(token);

                        var claims = jwt.Claims.ToList();

                        if (!claims.Any(c => c.Type == ClaimTypes.Role))
                        {
                            var roleClaim = jwt.Claims.FirstOrDefault(c =>
                                c.Type == "role" || c.Type.EndsWith("/role"));
                            if (roleClaim != null)
                            {
                                claims.Add(new Claim(ClaimTypes.Role, roleClaim.Value));
                            }
                        }
                        claims.Add(new Claim("JWT", token));


                        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var principal = new ClaimsPrincipal(identity);

                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                        if (responseContent.data.UserRole == "Admin")
                        return RedirectToAction("Index", "Admin");

                        return RedirectToAction("Index", "User");
                    }
                    else
                    {
                        ModelState.AddModelError("", responseContent?.message);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Api doesn't reply");
                }
            }
            else
            {
                ModelState.AddModelError("", "Model is not valid");
            }
            return View(model);
        }
        #endregion

        #region Logout
        public async Task<IActionResult> Logout()
        {
            var response = await _httpClient.PostAsJsonAsync("Account/Logout", new { });
            if (response.IsSuccessStatusCode)
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        #endregion


        #region Register

        [AllowAnonymous]
        public IActionResult Register()
        {
            // Check if user is already authenticated
            if (User.Identity.IsAuthenticated)
            {
                // Redirect to Home page (or Dashboard, etc.)
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = await _httpClient.PostAsJsonAsync("Account/Register", model);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadFromJsonAsync< ReturnValue_GenericDTO<RegisterDTO>>();

                    if (responseContent?.type == "Success")
                    {
                        return RedirectToAction("Login", "Account", new { Message = "Your account has been created successfully!" });
                    }
                    else
                    {
                        ModelState.AddModelError("", responseContent?.message);
                    }
                }
            }

            return View(model);
        }
        #endregion

    }
}
