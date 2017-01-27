using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;

public static class LocalizationManager 
{
	private static TextAsset languageFile;
	private static Dictionary<string, Dictionary<string, string>> languages;
	
	private static XmlDocument xmlDoc = new XmlDocument();
	private static XmlReader reader;
	
	private static string[] tags;
	private static string lang;
	private static string Lang {
		get 
		{
			return lang;
		}
		set 
		{
			PlayerPrefs.SetString("Language", value);
			lang = value;
		}
	}

	public static string GetLang () 
	{
		return lang;
	}

	public static void SetLang (string lan) 
	{
		PlayerPrefs.SetString("Language", lan);
	}

	public static void Start () 
	{
		tags = new string[] {"NULL", "PT", "EN"};
		languageFile = (TextAsset) Resources.Load ("Lang/Translations", typeof(TextAsset));

		if (!PlayerPrefs.HasKey("Language")) 
		{
			Lang = tags[0];
			return;
		}
		else 
		{
			Lang = PlayerPrefs.GetString("Language");
		}

		languages = new Dictionary<string, Dictionary<string, string>>();
		reader = XmlReader.Create(new StringReader(languageFile.text));
		xmlDoc.Load(reader);

		for (int i = 0; i < tags.Length; i++) 
		{
			languages.Add(tags[i], new Dictionary<string, string>());
			XmlNodeList langs = xmlDoc["Data"].GetElementsByTagName(tags[i]);
			
			for (int j = 0; j < langs.Count; j++) 
			{
				languages[tags[i]].Add(langs[j].Attributes["Key"].Value, langs[j].Attributes["Word"].Value);
			}
		}
	}

	public static string GetText(string lan, string key) 
	{
		return languages[lan][key];
	}

	public static string GetText(string key) 
	{
		return languages[lang][key];
	}
}
