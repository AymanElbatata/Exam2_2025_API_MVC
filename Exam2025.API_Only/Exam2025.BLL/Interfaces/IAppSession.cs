using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam2025.DAL.Interfaces
{
    public interface IAppSession
    {
        string AuthorizationToken { get; }
        //string UserID { get; }
        //string UserName { get; }
        //string UserEmail { get; }
        //string UserRole { get; }

        List<string> User1 { get; }

    }
}
