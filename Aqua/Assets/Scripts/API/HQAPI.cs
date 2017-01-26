using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HQAPI 
{
	public static WWW RequestHQ ()
	{
		WebAPI.apiPlace = "/hq/random/";
		WebAPI.pvtKey = "6b2b7f9bc0";

		return WebAPI.Get();
	}

	public static WWW SendHQ (HQResponse hqResponse)
	{
		WWWForm hqForm = new WWWForm ();
		hqForm.AddField ("user_id", hqResponse.user_id);
		hqForm.AddBinaryData("photo", hqResponse.photo, "Photo.png", "image/png");

		WebAPI.apiPlace = "/hq/";
		WebAPI.pvtKey = "6b2b7f9bc0";

		return WebAPI.Post(hqForm);
	}

	public static WWW SendHQRate (HQ currentHQ, int hqRate)
	{
		WWWForm rateForm = new WWWForm ();
		rateForm.AddField ("user_id", currentHQ.user_id);
		rateForm.AddField ("hq_id", currentHQ.photo.hq_id);
		rateForm.AddField ("value", hqRate);

		WebAPI.apiPlace = "/rating/";
		WebAPI.pvtKey = "6b2b7f9bc0";

		return WebAPI.Post(rateForm);
	}
}
