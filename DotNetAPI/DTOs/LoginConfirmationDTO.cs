namespace DotNetAPI.DTOs
{
    partial class LoginConfirmationDTO
    {
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        LoginConfirmationDTO()
        {
            if (PasswordHash == null)
            {
                PasswordHash = new byte[0];
            }

            if (PasswordSalt == null)
            {
                PasswordSalt = new byte[0];
            }
        }
    }
}