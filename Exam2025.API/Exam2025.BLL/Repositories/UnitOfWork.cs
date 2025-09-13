using Exam2025.BLL.Interfaces;
using Exam2025.DAL.BaseEntity;
using Exam2025.DAL.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Exam2025.BLL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public IAnswerTBLRepository AnswerTBLRepository { get; }
        public IExamTBLRepository ExamTBLRepository { get; }
        public IQuestionTBLRepository QuestionTBLRepository { get; }
        public IUserExamDetailTBLRepository UserExamDetailTBLRepository { get; }
        public IUserExamTBLRepository UserExamTBLRepository { get; }
        public SignInManager<AppUser> SignInManager { get; }
        public RoleManager<AppRole> RoleManager { get; }
        public UserManager<AppUser> UserManager { get; }
        public IMySPECIALGUID MySPECIALGUID { get; }
        public ITokenService TokenService { get; set; }
        public IAppSession AppSession { get; set; }


        public UnitOfWork(IAnswerTBLRepository AnswerTBLRepository, IExamTBLRepository ExamTBLRepository,
            IQuestionTBLRepository QuestionTBLRepository, IUserExamDetailTBLRepository UserExamDetailTBLRepository,
            IUserExamTBLRepository UserExamTBLRepository, SignInManager<AppUser> SignInManager,
            RoleManager<AppRole> RoleManager, UserManager<AppUser> UserManager, IMySPECIALGUID MySPECIALGUID,
            ITokenService TokenService,
            IAppSession AppSession    )
        {
            this.AnswerTBLRepository = AnswerTBLRepository;
            this.ExamTBLRepository = ExamTBLRepository;
            this.QuestionTBLRepository = QuestionTBLRepository;
            this.UserExamDetailTBLRepository = UserExamDetailTBLRepository;
            this.UserExamTBLRepository = UserExamTBLRepository;
            this.SignInManager = SignInManager;
            this.RoleManager = RoleManager;
            this.UserManager = UserManager;
            this.MySPECIALGUID = MySPECIALGUID;
            this.TokenService = TokenService;
            this.AppSession = AppSession;
        }
    }
}
