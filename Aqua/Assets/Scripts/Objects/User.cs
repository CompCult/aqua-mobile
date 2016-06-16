using UnityEngine;
using System.Collections;

public class User {

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
			created_at = "",
			updated_at = "",
			items = "",
			accomplished_missions = "",
			type = "";

	bool loaded = false;

	public User CreateUserByJson(string json)
	{
		return JsonUtility.FromJson<User>(json);
	}

	// Set and Get methods above
	// Integer

	public void SetEXP(int xp) 
	{
		this.xp = xp;
	}

	public int GetEXP()
	{
		return xp;
	}

	public void SetLevel(int level) 
	{
		this.level = level;
	}

	public int GetLevel()
	{
		return level;
	}

	public void SetCoins(int coins) 
	{
		this.coins = coins;
	}

	public int GetCoins()
	{
		return coins;
	}	

	public void SetAddress(int address) 
	{
		this.address = address;
	}

	public int GetAddress()
	{
		return address;
	}

	public void SetInvitedBy(int invited_by) 
	{
		this.invited_by = invited_by;
	}

	public int GetInvitedBy()
	{
		return invited_by;
	}

	public void SetID(int id) 
	{
		this.id = id;
	}

	public int GetID()
	{
		return id;
	}

	// Strings

	public void SetPlayerType(string type) 
	{
		this.type = type;
	}

	public string GetPlayerType()
	{
		return type;
	}

	public void SetAccomplishedMissions(string accomplished_missions) 
	{
		this.accomplished_missions = accomplished_missions;
	}

	public string GetAccomplishedMissions()
	{
		return accomplished_missions;
	}

	public void SetItems(string items) 
	{
		this.items = items;
	}

	public string GetItems()
	{
		return items;
	}

	public void SetUpdatedAt(string updated_at) 
	{
		this.updated_at = updated_at;
	}

	public string GetUpdatedAt()
	{
		return updated_at;
	}

	public void SetCreatedAt(string created_at) 
	{
		this.created_at = created_at;
	}

	public string GetCreatedAt()
	{
		return created_at;
	}

	public void SetRememberToken(string remember_token) 
	{
		this.remember_token = remember_token;
	}

	public string GetRememberToken()
	{
		return remember_token;
	}

	public void SetBirth(string birth) 
	{
		this.birth = birth;
	}

	public string GetBirth()
	{
		return birth;
	}

	public void SetCPF(string cpf) 
	{
		this.cpf = cpf;
	}

	public string GetCPF()
	{
		return cpf;
	}

	public void SetCameraPassword(string camera_password) 
	{
		this.camera_password = camera_password;
	}

	public string GetCameraPassword()
	{
		return camera_password;
	}

	public void SetPassword(string password) 
	{
		this.password = password;
	}

	public string GetPassword()
	{
		return password;
	}

	public User(int id) 
	{
		this.id = id;
	}

	public void SetName(string name)
	{
		this.name = name;
	}

	public string GetName()
	{
		return name;
	}

	public void SetEmail(string email)
	{
		this.email = email;
	}

	public string GetEmail()
	{
		return email;
	}

	public void SetLoaded(bool loaded)
	{
		this.loaded = loaded;
	}

	public bool GetLoaded()
	{
		return loaded;
	}
}
