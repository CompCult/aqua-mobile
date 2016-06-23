using UnityEngine;
using System.IO;
using System.Collections;

public class User {

	// These public variables needs declaration and lower case statement to use with JSON Converter

	public int id,
		invited_by,
		address,
		level,
		coins,
		xp;
	
	public string name = "",
			email = "",
			password = "",
			camera_password = "",
			cpf = "",
			birth = "",
			remember_token = "",
			items = "",
			accomplished_missions = "",
			type = "";

	private Address Address;
	private double[] Location;

	public User(int id)  
	{ 
		this.id = id; 
	}

	public User CreateUserByJson(string json)
	{
		ReceiveCurrentLocationFromGPS();

		return JsonUtility.FromJson<User>(json);
	}

    public IEnumerator ReceiveCurrentLocationFromGPS()
    {
        // First, check if user has location service enabled
        if (!Input.location.isEnabledByUser)
            yield break;

        // Start service before querying location
        Input.location.Start();

        // Wait until service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // Service didn't initialize in 20 seconds
        if (maxWait < 1)
        {
            Debug.Log("Timed out while trying to get device location");
            yield break;
        }

        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.Log("Unable to determine device location");
            yield break;
        }
        else
        {
        	Location = new double[2];
        	
            Location[0] = System.Convert.ToDouble(Input.location.lastData.latitude);
            Location[1] = System.Convert.ToDouble(Input.location.lastData.longitude);

            Debug.Log("Localization registered!");
        }

        // Stop service if there is no need to query location updates continuously
        Input.location.Stop();
    }

	public void SetEXP(int xp) { this.xp = xp; }
	public int GetEXP(){ return xp; }
	public void SetLevel(int level) { this.level = level; }
	public int GetLevel(){ return level; }
	public void SetCoins(int coins) { this.coins = coins; }
	public int GetCoins(){ return coins; }	
	public void SetAddressID(int address) { this.address = address; }
	public int GetAddressID(){ return address; }
	public void SetInvitedBy(int invited_by) { this.invited_by = invited_by; }
	public int GetInvitedBy(){ return invited_by; }
	public void SetID(int id) { this.id = id; }
	public int GetID(){ return id; }

	public void SetPlayerType(string type)  { this.type = type; }
	public string GetPlayerType() { return type; }
	public void SetAccomplishedMissions(string accomplished_missions)  { this.accomplished_missions = accomplished_missions; }
	public string GetAccomplishedMissions() { return accomplished_missions; }
	public void SetItems(string items)  { this.items = items; }
	public string GetItems() { return items; }
	public void SetRememberToken(string remember_token)  { this.remember_token = remember_token; }
	public string GetRememberToken() { return remember_token; }
	public void SetBirth(string birth)  { this.birth = birth; }
	public string GetBirth() { return birth; }
	public void SetCPF(string cpf)  { this.cpf = cpf; }
	public string GetCPF() { return cpf; }
	public void SetCameraPassword(string camera_password)  { this.camera_password = camera_password; }
	public string GetCameraPassword() { return camera_password; }
	public void SetPassword(string password)  { this.password = password; }
	public string GetPassword() { return password; }
	public void SetName(string name) { this.name = name; }
	public string GetName() { return name; }
	public void SetEmail(string email) { this.email = email; }
	public string GetEmail() { return email; }
	public void SetAddress(Address Address) { this.Address = Address; }
	public Address GetAddress() { return Address; }
	public void SetLocation(double[] Location) { this.Location = Location; }
	public double[] GetLocation() { return Location; }
}
