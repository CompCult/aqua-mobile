using UnityEngine;

public static class AddressAPI
{
	public static WWW RequestAddress(int id)
	{
		WebAPI.apiPlace = "/address/" + id + "/";
		WebAPI.pvtKey = "fc64ec6244";

		return WebAPI.Get();
	}

	public static WWW UpdateAddress (string zipcode, string street, string number, string district, string city, string state, string complement)
	{
		WWWForm updateForm = new WWWForm ();
		updateForm.AddField ("zipcode", zipcode);
		updateForm.AddField ("street", street);
		updateForm.AddField ("number", number);
		updateForm.AddField ("district", district);
		updateForm.AddField ("city", city);
		updateForm.AddField ("state", state);
		updateForm.AddField ("complement", complement);

		WebAPI.apiPlace = "/address/" + AddressManager.address.id + "/";
		WebAPI.pvtKey = "fc64ec6244";

		return WebAPI.Post(updateForm);
	}

	public static WWW CreateAddress (string zipcode, string street, string number, string district, string city, string state, string complement)
	{
		WWWForm updateForm = new WWWForm ();
		updateForm.AddField ("zipcode", zipcode);
		updateForm.AddField ("street", street);
		updateForm.AddField ("number", number);
		updateForm.AddField ("district", district);
		updateForm.AddField ("city", city);
		updateForm.AddField ("state", state);
		updateForm.AddField ("complement", complement);

		WebAPI.apiPlace = "/address/";
		WebAPI.pvtKey = "fc64ec6244";

		return WebAPI.Post(updateForm);
	}
}
