using System.ComponentModel.DataAnnotations;

namespace TheTown.ViewModels.AppUser
{
    public class UserLoginVm
    {
        [Required]
        public string UserName { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
        public bool IsParsistance { get; set; }
    }
}
