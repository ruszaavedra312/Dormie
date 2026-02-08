namespace Dormie.Models
{
    public class User
    {
        public int Id { get; set; }   // Primary Key

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public UserTypeEnum UserType { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public bool AcceptedTerms { get; set; }
    }
}
