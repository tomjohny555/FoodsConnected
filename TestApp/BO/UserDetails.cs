using System.ComponentModel.DataAnnotations;

namespace TestApp.BO
{
    public class UserDetails
    {
        [Key]
        public int UserId { get; set; }

        public string UserName { get; set; }
    }
}