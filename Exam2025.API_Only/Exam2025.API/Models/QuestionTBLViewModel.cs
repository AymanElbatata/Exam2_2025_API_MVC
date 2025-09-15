using Exam2025.DAL.BaseEntity;
using Exam2025.DAL.Entities;

namespace Exam2025.API.Models
{
    public class QuestionTBLViewModel : BaseEntity<int>
    {
        public int? ExamTBLId { get; set; }
        public string? Title { get; set; }

        public virtual ExamTBL? ExamTBL { get; set; }
    }
}
