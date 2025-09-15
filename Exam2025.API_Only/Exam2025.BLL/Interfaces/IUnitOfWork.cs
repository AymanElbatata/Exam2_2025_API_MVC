using Exam2025.DAL.BaseEntity;
using Exam2025.DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam2025.BLL.Interfaces
{
    public interface IUnitOfWork
    {
        IAnswerTBLRepository AnswerTBLRepository { get; }
        IExamTBLRepository ExamTBLRepository { get; }
        IQuestionTBLRepository QuestionTBLRepository { get; }
        IUserExamDetailTBLRepository UserExamDetailTBLRepository { get; }
        IUserExamTBLRepository UserExamTBLRepository { get; }
        SignInManager<AppUser> SignInManager { get; }
        RoleManager<AppRole> RoleManager { get; }
        UserManager<AppUser> UserManager { get; }
        IMySPECIALGUID MySPECIALGUID { get; }
        ITokenService TokenService { get; }
        IAppSession AppSession { get; }

    }
}
