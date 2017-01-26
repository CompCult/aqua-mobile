using UnityEngine;
using System.Text;
using System.Security.Cryptography;

public static class UserAPI 
{
	public static WWW UpdateUser (string name, string email, string birth, string cpf, string address, string phone, string pass)
	{
		WWWForm updateForm = new WWWForm();
		updateForm.AddField ("name", name);
		updateForm.AddField ("email", email);
		updateForm.AddField ("password", CalculateSHA1(pass));
		updateForm.AddField ("birth", birth);
		updateForm.AddField ("cpf", cpf);
		if (address != "0") // Indicates that the user have an address
			updateForm.AddField ("address", address);
		updateForm.AddField ("phone", phone);

		WebAPI.apiPlace = "/user/" + UsrManager.user.id + "/";
		WebAPI.pvtKey = "6b2b7f9bc0";

		return WebAPI.Post(updateForm);
	}

	private static string CalculateSHA1 (string input)
	{
		using (SHA1Managed sha1 = new SHA1Managed())
		{
			var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
			var sb = new StringBuilder(hash.Length * 2);

			foreach (byte b in hash)
			{
				// X2 to uppercase and x2 to lowercase
				sb.Append(b.ToString("x2"));
			}

			return sb.ToString();
		}
	}
}
