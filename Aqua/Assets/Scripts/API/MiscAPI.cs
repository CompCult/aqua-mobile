﻿using UnityEngine;
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
		WebAPI.apiPlace = "/sysinfo/mobile-version";
		WebAPI.pvtKey = "";

		return WebAPI.Get();
	}
}
