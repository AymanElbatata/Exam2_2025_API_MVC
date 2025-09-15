using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exam2025.PL.Models.BaseEntities;

namespace Exam2025.PL.Models.Entities
{
    public class QuestionTBL : BaseEntity<int>
    {
        //[ForeignKey(nameof(ExamTBL))]
        public int? ExamTBLId { get; set; }
        public string? Title { get; set; } = null!;

        public virtual ExamTBL? ExamTBL { get; set; }

    }
}
