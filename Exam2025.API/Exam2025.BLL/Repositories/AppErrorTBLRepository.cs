using Exam2025.BLL.Interfaces;
using Exam2025.DAL.Contexts;
using Exam2025.DAL.Entities;

namespace Exam2025.BLL.Repositories
{
    public class AppErrorTBLRepository : GenericRepository<AppErrorTBL>, IAppErrorTBLRepository
    {
        private readonly ExamDbContext _context;

        public AppErrorTBLRepository(ExamDbContext context) :base(context)
        {
            _context = context;
        }

    }
}
