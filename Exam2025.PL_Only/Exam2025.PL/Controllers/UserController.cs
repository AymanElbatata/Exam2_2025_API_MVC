using Exam2025.DTO;
using Exam2025.PL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Exam2025.PL.Controllers
{
    [Authorize(Roles = "User")]
    public class UserController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserController(HttpClient httpClient, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
            _httpClient.BaseAddress = new Uri(_configuration["APISettings:BaseApiUrl"]);
            //_httpClient.BaseAddress = new Uri(new AppSettingsHelper(_configuration, _httpContextAccessor).GetBaseUrl());

        }
        public async Task<IActionResult> Index(string? message = null)
        {
            if (!string.IsNullOrEmpty(message))
                ViewBag.Message = message;

            AddTokenToheader();
            var response = await _httpClient.GetAsync("User/GetAllMarksforCurrentUser");
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadFromJsonAsync<ReturnValue_GenericDTO<UserExamTBLViewModel>>();
                if (responseContent != null && responseContent.list.Count() > 0 )
                {
                    return View(responseContent.list);
                }
            }
            return View(new List<UserExamTBLViewModel>());
        }

        private void AddTokenToheader()
        {
            var token = User.Claims.FirstOrDefault(c => c.Type == "JWT")?.Value;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }
}
