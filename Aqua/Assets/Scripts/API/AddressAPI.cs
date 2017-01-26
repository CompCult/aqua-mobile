using UnityEngine;

public static class AddressAPI
{
	public static WWW RequestAddress(int id)
	{
		WebFunctions.apiPlace = "/address/" + id + "/";
		WebFunctions.pvtKey = "fc64ec6244";

		return WebFunctions.Get();
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

		WebFunctions.apiPlace = "/address/" + AddressManager.address.id + "/";
		WebFunctions.pvtKey = "fc64ec6244";

		return WebFunctions.Post(updateForm);
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

		WebFunctions.apiPlace = "/address/";
		WebFunctions.pvtKey = "fc64ec6244";

		return WebFunctions.Post(updateForm);
	}
}
