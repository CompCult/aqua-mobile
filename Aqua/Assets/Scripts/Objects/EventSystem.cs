using UnityEngine;
using System.IO;
using System.Collections;

public class EventSystem : MonoBehaviour {

	User GlobalUser;
    double[] Location;

    public void Start()
    {
        Location = new double[2];
        UpdateGlobalUserLocation();
    }

	public void Awake() 
	{
        DontDestroyOnLoad(transform.gameObject);
    }

    public void UpdateGlobalUser(int id) 
    {
    	GlobalUser = new User(id);
    }

    public void UpdateGlobalUser(User GlobalUser) 
    { 
        this.GlobalUser = GlobalUser; 
    }

    public void UpdateGlobalUser(string json)
    {
        User aux = new User(0);
        aux = aux.CreateUserByJson(json);

        UpdateGlobalUser(aux);
    }

    public void UpdateGlobalUserAddress(Address Address) 
    { 
        GlobalUser.SetAddress(Address); 
    }

    public void UpdateGlobalUserAddress(string json)
    {
        Address aux = new Address();
        aux = aux.CreateAddressByJSON(json);

        UpdateGlobalUserAddress(aux);
    }

    // Updates the user location
    public void UpdateGlobalUserLocation()
    {
        StartCoroutine(RetriveLocationFromGPS());
    }

    IEnumerator RetriveLocationFromGPS()
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
            print("Timed out");
            yield break;
        }

        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            print("Unable to determine device location");
            yield break;
        }
        else
        {
            Location = new double[2];

            Location[0] = System.Convert.ToDouble(Input.location.lastData.latitude);
            Location[1] = System.Convert.ToDouble(Input.location.lastData.longitude);
        }

        // Stop service if there is no need to query location updates continuously
        Input.location.Stop();
    }
    
    public User GetUser() { return GlobalUser; }
    public double[] GetUserLocation() { UpdateGlobalUserLocation(); return Location; }
}
