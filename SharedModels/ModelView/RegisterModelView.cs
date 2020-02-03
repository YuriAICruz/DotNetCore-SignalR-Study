using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebServerStudy.Controllers.ModelView
{
    public class RegisterModelView : LoginModelView
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}