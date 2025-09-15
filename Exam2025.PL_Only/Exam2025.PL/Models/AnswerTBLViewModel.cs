using Exam2025.PL.Models.BaseEntities;
using Exam2025.PL.Models.Entities;

namespace Exam2025.PL.Models
{
    public class AnswerTBLViewModel : BaseEntity<int>
    {
        public int? QuestionTBLId { get; set; }
        public string? Name { get; set; } 
        public bool IsRight { get; set; }

        public virtual QuestionTBL? QuestionTBL { get; set; }
    }
}
