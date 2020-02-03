using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace WebServerStudy.Controllers.ModelView
{
    public class UserViewModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }

        public UserViewModel()
        {
            
        }
        
        public UserViewModel(string name, string email)
        {
            UserName = name;
            Email = email;
        }
    }
}