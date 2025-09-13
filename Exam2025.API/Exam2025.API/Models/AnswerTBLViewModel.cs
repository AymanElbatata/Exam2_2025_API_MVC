using Exam2025.DAL.BaseEntity;
using Exam2025.DAL.Entities;

namespace Exam2025.API.Models
{
    public class AnswerTBLViewModel : BaseEntity<int>
    {
        public int? QuestionTBLId { get; set; }
        public string? Name { get; set; } 
        public bool IsRight { get; set; }

        public virtual QuestionTBL? QuestionTBL { get; set; }
    }
}
