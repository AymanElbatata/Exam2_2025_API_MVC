using Exam2025.BLL.Interfaces;
using Exam2025.DAL.Contexts;
using Exam2025.DAL.Entities;

namespace Exam2025.BLL.Repositories
{
    public class UserExamTBLRepository : GenericRepository<UserExamTBL>, IUserExamTBLRepository
    {
        private readonly ExamDbContext _context;

        public UserExamTBLRepository(ExamDbContext context) :base(context)
        {
            _context = context;
        }
        public int AddAndReturnNewRowID(UserExamTBL obj)
        {
            var NewRow = _context.UserExamTBLs.Add(obj);
            _context.SaveChanges();
            return obj.ID;
        }
    }
}
