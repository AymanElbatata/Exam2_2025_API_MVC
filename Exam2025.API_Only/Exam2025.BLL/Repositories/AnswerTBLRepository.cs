using Exam2025.BLL.Interfaces;
using Exam2025.DAL.Contexts;
using Exam2025.DAL.Entities;

namespace Exam2025.BLL.Repositories
{
    public class AnswerTBLRepository : GenericRepository<AnswerTBL>, IAnswerTBLRepository
    {
        private readonly ExamDbContext _context;

        public AnswerTBLRepository(ExamDbContext context) :base(context)
        {
            _context = context;
        }

    }
}
