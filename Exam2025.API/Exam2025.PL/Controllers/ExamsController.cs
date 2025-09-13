using AutoMapper;
using AYMDating.Blazor.Data.DTO;
using Exam2025.API.DTO;
using Exam2025.API.Models;
using Exam2025.BLL.Interfaces;
using Exam2025.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace Exam2025.PL.Controllers
{
    [Authorize(Roles = "User")]
    public class ExamsController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public ExamsController(HttpClient httpClient, IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(_configuration["APISettings:BaseApiUrl"]);
        }
        public async Task<IActionResult> Index()
        {
            AddTokenToheader();
            var response = await _httpClient.GetAsync("Exams/GetAllValidExamsforCurrentUser");
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadFromJsonAsync<ReturnValue_GenericDTO<ExamTBLViewModel>>();
                if (responseContent?.type == "Success" && responseContent?.list != null)
                {
                    return View(responseContent.list);
                }
            }
            return View(new List<ExamTBLViewModel>());
        }

        public async Task<IActionResult> StartExam(int ID = 0)
        {
            if (ID < 1)
            {
                return BadRequest();
            }
            AddTokenToheader();
            var response = await _httpClient.GetAsync("Exams/UserStartExam/"+ID);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadFromJsonAsync<ReturnValue_GenericDTO<TakeExamModel>>();
                if (responseContent?.type == "Success" && responseContent.data != null)
                {
                    return View(responseContent.data);
                }
            }
           
            return View(new TakeExamModel());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitExam(ExamSubmissionModel model)
        {
            if (model == null)
                return BadRequest();

            var jsonString = JsonSerializer.Serialize(model);

            AddTokenToheader();
            var response = await _httpClient.PostAsJsonAsync("Exams/SubmitCurrentUserTest", jsonString);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                if (responseContent == "Success")
                {
                    return RedirectToAction("Index", "User");
                }
            }
            return RedirectToAction("Index", "User", new { message = "Error in latest Exam!"});
        }


        private void AddTokenToheader()
        {
            var token = User.Claims.FirstOrDefault(c => c.Type == "JWT")?.Value;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }
}
