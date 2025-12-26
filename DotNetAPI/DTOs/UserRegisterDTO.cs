namespace DotNetAPI.DTOs
{
    public partial class UserRegisterDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }

        public UserRegisterDTO()
        {
            if (Email == null)
            {
                Email = "";
            }

            if (Password == null)
            {
                Password = "";
            }

            if (PasswordConfirm == null)
            {
                PasswordConfirm = "";
            }

             if (FirstName == null)
            {
                FirstName = "";
            }

            if (LastName == null)
            {
                LastName = "";
            }

            if (Gender == null)
            {
                Gender = "";
            }
        }
    }
}