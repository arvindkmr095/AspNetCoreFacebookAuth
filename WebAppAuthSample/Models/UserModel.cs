using System.ComponentModel.DataAnnotations;

namespace WebAppAuthSample.Models
{
    public class UserModel
    {
        [Required(ErrorMessage = "Please enter user name")]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
