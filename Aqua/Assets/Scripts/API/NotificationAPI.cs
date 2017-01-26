using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NotificationAPI 
{
	public static WWW RequestNotifications(int id)
	{
		WebAPI.apiPlace = "/notification/user/" + id + "/";
		WebAPI.pvtKey = "d86c362f4b";

		return WebAPI.Get();
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

		WebAPI.apiPlace = "/notification/";
		WebAPI.pvtKey = "d86c362f4b";

		return WebAPI.Post(photoForm);
	}
}
