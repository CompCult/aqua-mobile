using UnityEngine;
using System.Collections;

public class Report {

	private string Address;
	private long Latitude, Longitude;

	public Report(string Address, long Latitude, long Longitude)
	{
		if (Latitude != null && Longitude != null && Address != null)
		{
			this.Latitude = Latitude;
			this.Longitude = Longitude;
			this.Address = Address;
		}
	}

	public Report(string Address)
	{
		if (Address != null)
			this.Address = Address;
	}
	
	public string GetAddress()
	{
		return Address;
	}

	public void SetAddress(string Address)
	{
		this.Address = Address;
	}

	public long GetLongitude()
	{
		return Longitude;
	}

	public void SetLongitude(long Longitude)
	{
		this.Longitude = Longitude;
	}

	public long GetLatitude()
	{
		return Latitude;
	}

	public void SetLatitude(long Latitude)
	{
		this.Latitude = Latitude;
	}
}
