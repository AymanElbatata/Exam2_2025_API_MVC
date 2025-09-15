using AutoMapper;
using Exam2025.API.DTO;
using Exam2025.API.Models;
using Exam2025.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Exam2025.API.Controllers
{
    [Authorize(Roles = "User")]
    public class UserController : BaseController
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public UserController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        [HttpGet("GetAllMarksforCurrentUser")]
        public async new Task<ActionResult<ReturnValue_GenericDTO<UserExamTBLViewModel>>> GetAllMarksforCurrentUser()
        {
            try
            {
                var Model = unitOfWork.UserExamTBLRepository.GetAllCustomized(
                    filter: a => a.IsDeleted == false && a.IsFinished == true && a.CreatedUserID == User.FindFirstValue(ClaimTypes.NameIdentifier),
                    includes: ue => ue.ExamTBL);
                return Ok(new ReturnValue_GenericDTO<UserExamTBLViewModel>() { type = ReturnType.Success.ToString(), message = "", list = mapper.Map<IEnumerable<UserExamTBLViewModel>>(Model) });
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }
    }
}
