using Exam2025.BLL.Interfaces;
using Exam2025.DAL.Contexts;
using Exam2025.DAL.Entities;

namespace Exam2025.BLL.Repositories
{
    public class QuestionTBLRepository : GenericRepository<QuestionTBL>, IQuestionTBLRepository
    {
        private readonly ExamDbContext _context;

        public QuestionTBLRepository(ExamDbContext context) :base(context)
        {
            _context = context;
        }

    }
}
