using Exam2025.PL.Models.BaseEntities;
using Exam2025.PL.Models.Entities;

namespace Exam2025.PL.Models
{
    public class UserExamDetailTBLViewModel : BaseEntity<int>
    {
        public int? UserExamTBLId { get; set; }
        public int? QuestionTBLId { get; set; }
        public int? AnswerTBLId { get; set; }

        public virtual UserExamTBL? UserExamTBL { get; set; }
        public virtual QuestionTBL? QuestionTBL { get; set; }
        public virtual AnswerTBL? AnswerTBL { get; set; }
    }
}
