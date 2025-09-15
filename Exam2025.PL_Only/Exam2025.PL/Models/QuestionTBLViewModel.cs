using Exam2025.PL.Models.BaseEntities;
using Exam2025.PL.Models.Entities;

namespace Exam2025.PL.Models
{
    public class QuestionTBLViewModel : BaseEntity<int>
    {
        public int? ExamTBLId { get; set; }
        public string? Title { get; set; }

        public virtual ExamTBL? ExamTBL { get; set; }
    }
}
