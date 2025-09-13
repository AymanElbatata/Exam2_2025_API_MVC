namespace Exam2025.API.DTO
{
    public class UserDTO
    {
        public string? UserId { get; set; }
        public string? Email { get; set; }
        public string? UserName { get; set; }
        public string? UserRole { get; set; }
        public string? Token { get; set; }
        //public DateTime? DateOfMaking { get; set;}
        //public DateTime DateOfExpiration { get; set; } 
        //public bool TokenIsExpired { get; set; } = false;
    }
}
