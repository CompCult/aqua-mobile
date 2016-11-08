using UnityEngine;
using System.Collections;

public static class HQManager
{
	private static HQ _hq;
	private static HQResponse _hqResponse;

	public static HQ hq { get { return _hq; } set { _hq = value; } }
	public static HQResponse hqResponse { get { return _hqResponse; } set { _hqResponse = value; } }

	public static void UpdateHQ (string JSON)
	{
		Debug.Log("HQ Updated");

		_hq = CreateHQ(JSON);
		_hqResponse = new HQResponse ();
	}

	public static void UpdateHQ (HQ hq)
	{
		Debug.Log("HQ Updated");

		_hq = hq;
		_hqResponse = new HQResponse ();
	}

	public static HQ CreateHQ (string json)
	{
		return JsonUtility.FromJson<HQ>(json);
	}
}
