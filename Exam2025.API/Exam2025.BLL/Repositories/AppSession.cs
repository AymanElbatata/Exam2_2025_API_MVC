using Exam2025.DAL.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Identity.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace Exam2025.DAL.Repositories
{
    public class AppSession : IAppSession
    {
        //protected IHttpContextAccessor HttpContextAccessor;
        private IHttpContextAccessor HttpContextAccessor;

        public AppSession(IHttpContextAccessor httpContextAccessor)
        {
            HttpContextAccessor = httpContextAccessor;
        }

        public string AuthorizationToken
        {
            get
            {
                if (HttpContextAccessor.HttpContext.Request.Headers["Authorization"].Any())
                    //return HttpContextAccessor.HttpContext.Request.Headers["Authorization"].FirstOrDefault();
                return HttpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();


                return string.Empty;
            }
        }

        //public string UserID => HttpContextAccessor?.HttpContext?.User.Claims.FirstOrDefault().ToString().Split(" ")[1];
        //public string UserName => HttpContextAccessor?.HttpContext?.User.GetDisplayName();
        //public string UserEmail => HttpContextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.Email);
        //public string UserRole => HttpContextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.Role);


        public List<string> User1 => GetInfoFromUser();

        private List<string>  GetInfoFromUser()
        {
            List<string> info = new List<string>();
            foreach (var item in HttpContextAccessor?.HttpContext?.User.Identities.FirstOrDefault().Claims)
            {
                info.Add(item.Value);
            }
            return info;
        }

    }
}
