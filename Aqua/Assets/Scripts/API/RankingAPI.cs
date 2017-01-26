using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RankingAPI 
{
	public static WWW RequestRanking()
	{
		WebFunctions.apiPlace = "/user/rank/";
		WebFunctions.pvtKey = "6b2b7f9bc0";

		return WebFunctions.Get();
	}
}
