using UnityEngine;
using System.Collections;

public class Report {

	private string Address;
	private double Latitude, Longitude;
	private string Status = "Pendente";
	private string Querry;

	public Report(string Address, double Latitude, double Longitude)
	{
		if (Address != null)
		{
			this.Latitude = Latitude;
			this.Longitude = Longitude;
			this.Address = Address;
		}
	}

	public Report(string Address, double Latitude, double Longitude, string Status) 
		: this (Address, Latitude, Longitude)
	{
		if (Status != null)
			this.Status = Status;
	}

	public string GetQuerryToAddress()
	{
		if (Querry == null)
		{
			Querry = ("http://maps.googleapis.com/maps/api/staticmap?center=" + Latitude + "," + Longitude
					  + "&zoom=17&size=640x640&scale=1&maptype=roadmap&key=AIzaSyCZB4-V1JgCoxMZWI5hutfPf-_7dgnNdCI&format=png&markers=color:blue%7Clabel:" 
					  + Status[0] + "%7C" + Latitude + "," + Longitude);
		}

		return Querry;
	}
	
	public string GetAddress()
	{
		return Address;
	}

	public void SetAddress(string Address)
	{
		this.Address = Address;
	}

	public double GetLongitude()
	{
		return Longitude;
	}

	public void SetLongitude(double Longitude)
	{
		this.Longitude = Longitude;
	}

	public double GetLatitude()
	{
		return Latitude;
	}

	public void SetLatitude(double Latitude)
	{
		this.Latitude = Latitude;
	}

	public string GetStatus()
	{
		return Status;
	}

	public void SetStatus(string Status)
	{
		this.Status = Status;
	}
}
