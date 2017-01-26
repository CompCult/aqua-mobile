using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NotificationAPI 
{
	public static WWW RequestNotifications(int id)
	{
		WebFunctions.apiPlace = "/notification/user/" + id + "/";
		WebFunctions.pvtKey = "d86c362f4b";

		return WebFunctions.Get();
	}

	public static WWW SendNotification (int id, string latitude, string longitude, string type, byte[] bytes)
	{
		WWWForm photoForm = new WWWForm ();
		photoForm.AddField ("user_id", id);
		photoForm.AddField ("latitude", latitude);
		photoForm.AddField ("longitude", longitude);
		photoForm.AddField ("status", "pending");
		photoForm.AddField ("type", type);
		photoForm.AddBinaryData("photo", bytes, "Photo.png", "image/png");

		WebFunctions.apiPlace = "/notification/";
		WebFunctions.pvtKey = "d86c362f4b";

		return WebFunctions.Post(photoForm);
	}
}
