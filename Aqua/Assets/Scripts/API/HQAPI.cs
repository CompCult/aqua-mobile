using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HQAPI 
{
	public static WWW RequestHQ ()
	{
		WebFunctions.apiPlace = "/hq/random/";
		WebFunctions.pvtKey = "6b2b7f9bc0";

		return WebFunctions.Get();
	}

	public static WWW SendHQ (HQResponse hqResponse)
	{
		WWWForm hqForm = new WWWForm ();
		hqForm.AddField ("user_id", hqResponse.user_id);
		hqForm.AddBinaryData("photo", hqResponse.photo, "Photo.png", "image/png");

		WebFunctions.apiPlace = "/hq/";
		WebFunctions.pvtKey = "6b2b7f9bc0";

		return WebFunctions.Post(hqForm);
	}

	public static WWW SendHQRate (HQ currentHQ, int hqRate)
	{
		WWWForm rateForm = new WWWForm ();
		rateForm.AddField ("user_id", currentHQ.user_id);
		rateForm.AddField ("hq_id", currentHQ.photo.hq_id);
		rateForm.AddField ("value", hqRate);

		WebFunctions.apiPlace = "/rating/";
		WebFunctions.pvtKey = "6b2b7f9bc0";

		return WebFunctions.Post(rateForm);
	}
}
