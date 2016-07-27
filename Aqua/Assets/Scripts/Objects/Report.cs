using UnityEngine;
using System.Collections;

public class Report {

	public string id,
				  latitude,
				  longitude,
				  status,
				  type;

	private string query;

	public Report CreateReportByJSON(string json)
	{
		return JsonUtility.FromJson<Report>(json);
	}

	public string GetQueryToAddress()
	{
		if (query == null)
		{
			query = ("http://maps.googleapis.com/maps/api/staticmap?center=" 
					+ latitude + "," + longitude
					+ "&zoom=17&size=640x640&scale=1&maptype=roadmap&key=AIzaSyCZB4-V1JgCoxMZWI5hutfPf-_7dgnNdCI&format=png&markers=color:blue%7Clabel:" 
					+ status[0] + "%7C" 
					+ latitude + "," + longitude);
		}

		return query;
	}

	public string GetID() { return id; }
	public void SetID(string id) { this.id = id; }
	public string GetLongitude() { return longitude; }
	public void SetLongitude(string longitude) { this.longitude = longitude; }
	public string GetLatitude() { return latitude; }
	public void SetLatitude(string latitude) { this.latitude = latitude; }
	public string GetStatus() { return status; }
	public void SetStatus(string status) { this.status = status; }
	public string GetReportType() { return type; }
	public void SetReportType(string type) { this.type = type; }
}
