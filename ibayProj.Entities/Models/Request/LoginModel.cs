using System.ComponentModel.DataAnnotations;

namespace ibayProj.Entities.Models.Request
    {
        public class LoginModel
        {
            [Required]
            [EmailAddress]
            [StringLength(50)]
            public string Email {get; set;}
            
            [Required]
            [StringLength(20, ErrorMessage = "make sure password length is between 8 and 20", MinimumLength = 8)]
            // Password should be strong, One  Upper letter, lower letter, a number and a special charcter
            [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[#$^+=!*()@%&]).{8,}$", ErrorMessage = "Password Should have minimum 8 with at least one upper, lower, number and special character")]
            public string Password {get;set;}
        }
    }

