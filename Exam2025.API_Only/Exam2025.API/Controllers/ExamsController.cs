using AutoMapper;
using AYMDating.Blazor.Data.DTO;
using Exam2025.API.DTO;
using Exam2025.API.Models;
using Exam2025.BLL.Interfaces;
using Exam2025.BLL.Repositories;
using Exam2025.DAL.BaseEntity;
using Exam2025.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Exam2025.API.Controllers
{
    [Authorize(Roles = "User")]
    public class ExamsController : BaseController
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public ExamsController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        [HttpGet("GetAllValidExamsforCurrentUser")]
        public async new Task<ActionResult<ReturnValue_GenericDTO<ExamTBLViewModel>>> GetAllValidExamsforCurrentUser()
        {
            try
            {
                var Model = unitOfWork.ExamTBLRepository.GetAllCustomized(
                    filter: a => a.IsDeleted == false && a.IsPublished == true).OrderByDescending(a => a.CreationDate).ToList();
                return Ok(new ReturnValue_GenericDTO<ExamTBLViewModel>() { type = ReturnType.Success.ToString(), message = " ", list = mapper.Map<List<ExamTBLViewModel>>(Model) });

            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpGet("UserStartExam/{ID}")]
        public async new Task<ActionResult<ReturnValue_GenericDTO<TakeExamModel>>> UserStartExam(int ID)
        {
            try
            {
                var Exam = unitOfWork.ExamTBLRepository.GetAllCustomized(
                    filter: a => a.IsDeleted == false && a.IsPublished == true && a.ID == ID).FirstOrDefault();
                if (Exam != null)
                {
                    var takeExamModel = new TakeExamModel();
                    takeExamModel.ExamTBLId = Exam.ID;
                    takeExamModel.Name = Exam.Name;
                    takeExamModel.Body = Exam.Body;
                    takeExamModel.DurationInnMinutes = Exam.DurationInMinutes;
                    takeExamModel.SuccessRate = Exam.SuccessRate;
                    takeExamModel.NumberofQuestions = Exam.NumberofQuestions;

                    var Questions = unitOfWork.QuestionTBLRepository.GetAllCustomized(
                    filter: a=> a.IsDeleted == false && a.ExamTBLId == Exam.ID).OrderBy(a => a.ID);
                    foreach (var item in Questions)
                    {
                        var Answers = unitOfWork.AnswerTBLRepository.GetAllCustomized(
                                    filter: a => a.IsDeleted == false && a.QuestionTBLId == item.ID);

                        var questionViewModel = new QuestionViewModel();
                        questionViewModel.Title = item.Title;
                        questionViewModel.QuestionTBLId = item.ID;
                        takeExamModel.Questions.Add(questionViewModel);
                        foreach (var item2 in Answers)
                        {
                            var answersViewModel = new AnswersViewModel();
                            answersViewModel.AnswerTBLId = item2.ID;
                            answersViewModel.Name = item2.Name;
                            questionViewModel.Answers.Add(answersViewModel);
                        }
                    }
                    return Ok(new ReturnValue_GenericDTO<TakeExamModel>() { type = ReturnType.Success.ToString(), message = "", data = takeExamModel });

                }
                return Ok(new ReturnValue_GenericDTO<TakeExamModel>() { type = ReturnType.Failed.ToString(), message = "General Error", data = new TakeExamModel() });
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }


        [HttpPost("SubmitCurrentUserTest")]
        public async Task<ActionResult<string>> SubmitCurrentUserTest([FromBody] /*ExamSubmissionModel model*/ string jsonModel)
        {
            try
            {
                var model = JsonSerializer.Deserialize<ExamSubmissionModel>(jsonModel);

                var UserExamDetailsTBLList = new List<UserExamDetailTBL>();

                var Exam = unitOfWork.ExamTBLRepository.GetById(model.ExamTBLId);
                int RightAnswersCounter = 0;

                var UserExamTBLRecord = new UserExamTBL();
                UserExamTBLRecord.ExamTBLId = model.ExamTBLId;
                UserExamTBLRecord.IsFinished = true;
                UserExamTBLRecord.CreatedUserID = User.FindFirstValue(ClaimTypes.NameIdentifier);
                int SAVEDUserExamTBLRecordID = unitOfWork.UserExamTBLRepository.AddAndReturnNewRowID(UserExamTBLRecord);

                foreach (var item in model.Questions)
                {
                    if (item.SelectedAnswerId != null)
                    {
                        if (AnswerIsRight(Convert.ToInt32(item.SelectedAnswerId)))
                            RightAnswersCounter++;
                    }
                    var UserExamDetailsTBLRecord = new UserExamDetailTBL();
                    UserExamDetailsTBLRecord.UserExamTBLId = model.ExamTBLId;
                    UserExamDetailsTBLRecord.QuestionTBLId = item.QuestionTBLId;
                    if (item.SelectedAnswerId != null)
                    {
                        UserExamDetailsTBLRecord.AnswerTBLId = item.SelectedAnswerId;
                    }
                    UserExamDetailsTBLRecord.CreatedUserID = UserExamTBLRecord.CreatedUserID;
                    UserExamDetailsTBLRecord.UserExamTBLId = SAVEDUserExamTBLRecordID;
                    unitOfWork.UserExamDetailTBLRepository.Add(UserExamDetailsTBLRecord);
                }

                UserExamTBLRecord.UserRate = (int)(((double)RightAnswersCounter / Exam.NumberofQuestions) * 100);
                UserExamTBLRecord.Result = UserExamTBLRecord.UserRate >= Exam.SuccessRate ? "Passed" : "Failed";
                unitOfWork.UserExamTBLRepository.Update(UserExamTBLRecord);

                return Ok(ReturnType.Success.ToString());
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }

        }


        private bool AnswerIsRight(int Answer)
        {
            return unitOfWork.AnswerTBLRepository.GetById(Answer).IsRight;
        }
    }
}
