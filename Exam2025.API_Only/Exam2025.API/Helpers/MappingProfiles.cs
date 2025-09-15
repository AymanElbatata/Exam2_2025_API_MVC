using AutoMapper;
using Exam2025.API.Models;
using Exam2025.DAL.Entities;

namespace Exam2025.API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<ExamTBL, ExamTBLViewModel>().ReverseMap();
            CreateMap<AnswerTBL, AnswerTBLViewModel>().ReverseMap();
            CreateMap<QuestionTBL, QuestionTBLViewModel>().ReverseMap();
            CreateMap<UserExamTBL, UserExamTBLViewModel>().ReverseMap();
            CreateMap<UserExamDetailTBL, UserExamDetailTBLViewModel>().ReverseMap();
        }

    }
}
