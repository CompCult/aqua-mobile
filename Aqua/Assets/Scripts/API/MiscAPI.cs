using UnityEngine;
using System;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Collections;
using System.Security.Cryptography;
using System.Text;

public static class MiscAPI
{
	public static WWW CheckVersion ()
	{
		WebFunctions.apiPlace = "/sysinfo/mobile-version";
		WebFunctions.pvtKey = "";

		return WebFunctions.Get();
	}
}
