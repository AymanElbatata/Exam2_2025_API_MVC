using AutoMapper;
using Exam2025.API.Helpers;
using Exam2025.API.Models;
using Exam2025.BLL.Interfaces;
using Exam2025.BLL.Repositories;
using Exam2025.DAL.BaseEntity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;

namespace Exam2025.PL.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AdminController(HttpClient httpClient, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
            _httpClient.BaseAddress = new Uri(_configuration["APISettings:BaseApiUrl"]);
            //_httpClient.BaseAddress = new Uri(new AppSettingsHelper(_configuration, _httpContextAccessor).GetBaseUrl());
        }
        public IActionResult Index()
        {
            return View();
        }


      

    }
}

