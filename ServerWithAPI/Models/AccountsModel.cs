using Microsoft.EntityFrameworkCore;

namespace ServerWithAPI.Models
{
    public class AccountsModel
    {
        
        public string Email { get; set; }
        public string Pass { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
