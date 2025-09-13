using Exam2025.BLL.Interfaces;
using Exam2025.DAL.Contexts;
using Exam2025.DAL.Entities;

namespace Exam2025.BLL.Repositories
{
    public class UserExamDetailTBLRepository : GenericRepository<UserExamDetailTBL>, IUserExamDetailTBLRepository
    {
        private readonly ExamDbContext _context;

        public UserExamDetailTBLRepository(ExamDbContext context) :base(context)
        {
            _context = context;
        }

    }
}
