using UnityEngine;
using System.IO;
using System.Collections;

public class EventSystem : MonoBehaviour {

	User GlobalUser;

	void Awake() 
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

    public int printUser() 
    {
    	return GlobalUser.GetID();
    }
}
