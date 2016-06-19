using UnityEngine;
using System.IO;
using System.Collections;

public class EventSystem : MonoBehaviour {

	User GlobalUser;
    double[] Location;

    public void Start()
    {
        Location = new double[2];
        UpdateUserLocation();
    }

	public void Awake() 
	{
        DontDestroyOnLoad(transform.gameObject);
    }

    public void CreateUser(int id) 
    {
    	GlobalUser = new User(id);
    }

    public void CreateUser(string json)
    {
        User aux = new User(0);
        aux = aux.CreateUserByJson(json);

        aux.SetLoaded(true);

        GlobalUser = aux;
    }

    public void CreateUser(User GlobalUser)
    {
        this.GlobalUser = GlobalUser;
    }

    public User GetUser() 
    {
    	return GlobalUser;
    }

    public double[] GetUserLocation()
    {
        UpdateUserLocation();
        return Location;
    }

        // Updates the user location
    public void UpdateUserLocation()
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

}
