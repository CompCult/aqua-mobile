﻿using UnityEngine;
using System.Collections.Generic;

public static class WebFunctions
{
	// http://aqua-guardians-dev.herokuapp.com/api
	// http://aquaguardians.com.br/api
	private static string _url = "http://aquaguardians.com.br/api",
	_pvtKey = "",
	_apiPlace = "/";

	public static string url { get { return _url; } }
	public static string pvtKey { get { return _pvtKey; } set { _pvtKey = value; } }
	public static string apiPlace { get { return _apiPlace; } set { _apiPlace = value; } }
		
	#pragma warning disable 0219
	public static WWW Get()
	{
		string apiLink = url + apiPlace + pvtKey;
		WWW www = new WWW (apiLink);

		Debug.Log("WebFunctions - Get url: " + apiLink);

		WaitForSeconds w;
		while (!www.isDone)
			w = new WaitForSeconds(0.1f);

		return www; 
	}

	#pragma warning disable 0219
	public static WWW Post(WWWForm form)
	{
		string apiLink = url + apiPlace + pvtKey;
		WWW www = new WWW(apiLink, form);

		Debug.Log("WebFunctions - Post url: " + apiLink);

		WaitForSeconds w;
		while (!www.isDone)
			w = new WaitForSeconds(0.1f);

		return www; 
	}
}