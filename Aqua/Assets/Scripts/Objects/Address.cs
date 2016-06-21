using UnityEngine;
using System.Collections;

public class Address {

	private int id;

	private string zipcode = "", 
				   street = "", 
				   number = "", 
				   district = "", 
				   city = "", 
				   state = "", 
				   complement = "";
	
	public Address(){}

	public Address CreateAddressByJSON(string json)
	{
		return JsonUtility.FromJson<Address>(json);
	}

	public void SetZIP(string zipcode) { this.zipcode = zipcode; }
	public string GetZIP() { return zipcode; }
	public void SetStreet(string street) { this.street = street; }
	public string GetStreet() { return street; }
	public void SetNumber(string number) { this.number = number; }
	public string GetNumber() { return number; }
	public void SetDistrict(string district) { this.district = district; }
	public string GetDistrict() { return district; }
	public void SetCity(string city) { this.city = city; }
	public string GetCity() { return city; }
	public void SetState(string state) { this.state = state; }
	public string GetState() { return state; }
	public void SetComplement(string complement) { this.complement = complement; }
	public string GetComplement() { return complement; }
	public void SetID(int id) { this.id = id; }
	public int GetID() { return id; }
}
