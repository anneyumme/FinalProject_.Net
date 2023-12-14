namespace FinalProject_.Net.Service;

public class PasswordService
{
	public  string HashPassword(string password)
	{
		return BCrypt.Net.BCrypt.HashPassword(password);
	}
	public  bool ValidatePassword(string password, string hashedPassword)
	{
		return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
	}
}