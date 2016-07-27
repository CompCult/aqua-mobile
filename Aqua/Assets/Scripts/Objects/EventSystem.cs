using UnityEngine;
using System.IO;
using System.Collections;

public class EventSystem : MonoBehaviour {

	private User GlobalUser;

	public void Awake() 
	{
        DontDestroyOnLoad(transform.gameObject);
    }

    public void UpdateGlobalUser(int id) 
    {
    	GlobalUser = new User(id);
    }

    public void UpdateGlobalUser(string json)
    {
        User aux = new User(0);
        aux = aux.CreateUserByJson(json);

        UpdateGlobalUser(aux);
    }

    public void UpdateGlobalUser(User GlobalUser) 
    { 
        this.GlobalUser = GlobalUser; 
    }

    public void UpdateGlobalUserAddress(string json)
    {
        Address aux = new Address();
        aux = aux.CreateAddressByJSON(json);

        UpdateGlobalUserAddress(aux);
    }

    public void UpdateGlobalUserAddress(Address Address) 
    { 
        GlobalUser.SetAddress(Address); 
    }

    public User GetUser() { return GlobalUser; }
}
