using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace WebServerStudy.Models
{
    public class ErrorMessage
    {
        public IEnumerable<string> Errors { get; set; }

        public ErrorMessage(IEnumerable<string> errors)
        {
            Errors = errors;
        }

        public ErrorMessage(string message)
        {
            Errors = new[] {message};
        }
    }
}