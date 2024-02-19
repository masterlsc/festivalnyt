using UnityEngine;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Text;
using System;
using System.Linq;

namespace UIStyles
{
	public class StringFormats : MonoBehaviour 
	{
		/// <summary>
		/// Indicates whether a specified string is null, empty, or consists only of white-space characters.
		/// </summary>
		public static bool IsNullOrWhiteSpace(string value)
		{
			if (value == null)
			{
				return true;
			}
			
			var index = 0;
			while (index < value.Length)
			{
				if (char.IsWhiteSpace(value[index]))
				{
					index++;
				}
				else
				{
					return false;
				}
			}
			
			return true;
		}
		
		/// <summary>
		/// remove all non-BMP characters, i.e. anything with a Unicode code point of U+10000 and higher (Good for removing emojis)
		/// </summary>
		public static string RemoveNonBMP (string value)
		{
			string result = Regex.Replace(value, @"\p{Cs}", "");
			
			return result;
		}
		
		
		/// <summary>
		/// Returns array of urls specified in a string
		/// </summary>
		public static string[] GetUrls (string value)
		{
			Regex linkParser = new Regex(@"\b(?:https?://|www\.)\S+\b", RegexOptions.IgnoreCase);
			
			string[] urls = new string[linkParser.Matches(value).Count];
			
			for (int i = 0; i < linkParser.Matches(value).Count; i++) 
				urls[i] = linkParser.Matches(value)[i].Value;
			
			return urls;
		}
		
		/// <summary>
		/// Returns a string converted for HyperText
		/// </summary>
		public static string ConvertUrls (string value)
		{
			string[] urls = GetUrls (value);
			
			string n = string.Empty;
			foreach (string url in urls)
			{
				n = value.Replace (url, "<a name= \"" + url + "\" class=\"url\">" + url + "</a>");
				value = n;
			}
			return string.IsNullOrEmpty (n) ? value : n;
		}
		
		/// <summary>
		/// Indicates whether a specified string contains urls
		/// </summary>
		public static bool IsUrl (string text)
		{
			Regex linkParser = new Regex(@"\b(?:https?://|www\.)\S+\b", RegexOptions.IgnoreCase);
			
			string[] urls = new string[linkParser.Matches(text).Count];
			return urls.Length != 0;
		}
		
		
		
		
		
		/// <summary>
		/// Returns array of emails specified in a string
		/// </summary>
		public static string[] GetEmails (string value)
		{
			Regex linkParser = new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", RegexOptions.IgnoreCase);
			
			string[] emails = new string[linkParser.Matches(value).Count];
			
			for (int i = 0; i < linkParser.Matches(value).Count; i++) 
				emails[i] = linkParser.Matches(value)[i].Value;
			
			return emails;
		}
		
		/// <summary>
		/// Returns a string converted for HyperText
		/// </summary>
		public static string ConvertEmails (string value)
		{
			string[] emails = GetEmails (value);
			
			string n = string.Empty;
			foreach (string email in emails)
			{
				n = value.Replace (email, "<a name= \"" + email + "\" class=\"email\">" + email + "</a>");
				value = n;
			}
			return string.IsNullOrEmpty (n) ? value : n;
		}
		
		/// <summary>
		/// Indicates whether a specified string contains an email address
		/// </summary>
		public static bool IsEmail (string value)
		{
			Regex linkParser = new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", RegexOptions.IgnoreCase);
			
			string[] urls = new string[linkParser.Matches(value).Count];
			return urls.Length != 0;
		}
		
		
		public static void OpenLink (string url)
		{
			// find out if the link is url or email
			if (StringFormats.IsUrl (url)) // its a url
			{
				if (!url.Contains ("http://"))
					url = "http://" + url;
				
				#if UNITY_STANDALONE || UNITY_EDITOR
				Application.OpenURL(url);
				//#elif UNITY_IOS || UNITY_ANDROID
				//EtceteraBinding.showWebPage(url, true);
				#else
				Debug.LogError ("Open web browser not set on this platfrom");
				#endif
			}
			else if (StringFormats.IsEmail (url))// its an email address
			{
				#if UNITY_STANDALONE || UNITY_EDITOR
				Application.OpenURL("mailto:" + url);
				//#elif UNITY_IOS || UNITY_ANDROID
				//EtceteraBinding.showMailComposer( url, "", "", false );
				#else
				Debug.LogError ("Open mail composer not set on this platfrom");
				#endif
			}
			else Debug.LogError ("Unknown link dose not seem to be an email or url");
		}
		
		
		
		
		/// <summary>
		/// converts a texture to a string
		/// </summary>
		public static string TextureToString (Texture2D value)
		{
			return System.Convert.ToBase64String (value.EncodeToJPG ());
		}
		
		/// <summary>
		/// converts a string back to a texture
		/// </summary>
		public static Texture2D StringToTexture (string value)
		{
			if (!string.IsNullOrEmpty (value))
			{
				byte[] Bytes = System.Convert.FromBase64String (value);
				Texture2D tex = new Texture2D (500, 700);
				tex.LoadImage (Bytes);
				
				return tex;
			}
			else return null;
		}
		
		
		/// <summary>
		/// converts colour to string
		/// </summary>
		public static string ColourToString (Color value)
		{
			string c = value.r + "|" + value.g + "|" + value.b + "|" + value.a;
			return c;
		}
		
		/// <summary>
		/// converts string to colour
		/// </summary>
		public static Color stringToColour (string value)
		{
			string[] s = value.Split ('|');
			
			float r = 0;
			if (s.Length > 0) float.TryParse (s[0], out r);
			
			float g = 0;
			if (s.Length > 1) float.TryParse (s[1], out g);
			
			float b = 0;
			if (s.Length > 2) float.TryParse (s[2], out b);
			
			float a = 1;
			if (s.Length > 3) float.TryParse (s[3], out a);
			
			Color c = new Color (r, g, b, a);
			return c;
		}
		
		
		/// <summary>
		/// Hex Color to String.
		/// </summary>
		public static string HexColorToString(Color aColor) 
		{
			return HexColorToString((Color32)aColor, false);
		}
		public static string HexColorToString(Color aColor, bool includeAlpha) 
		{
			return HexColorToString((Color32)aColor, includeAlpha);
		}
		public static string HexColorToString(Color32 aColor, bool includeAlpha) 
		{
			string rs = Convert.ToString(aColor.r,16).ToUpper();
			string gs = Convert.ToString(aColor.g,16).ToUpper();
			string bs = Convert.ToString(aColor.b,16).ToUpper();
			string a_s = Convert.ToString(aColor.a,16).ToUpper();
			while(rs.Length < 2) rs= "0" + rs;
			while(gs.Length < 2) gs= "0" + gs;
			while(bs.Length < 2) bs= "0" + bs;
			while(a_s.Length < 2) a_s= "0" + a_s;
			if(includeAlpha) return "#"+ rs + gs + bs + a_s;
			return "#"+ rs + gs + bs;
		}
		
		
		/// <summary>
		/// String to hex color.
		/// </summary>
		public static Color HexStringToColor(string value) 
		{
			Color col = new Color(0,0,0);
			if(value!=null && value.Length>0)
			{
				try 
				{
					string str = value.Substring(1, value.Length - 1);
					col.r = (float)System.Int32.Parse(str.Substring(0,2), NumberStyles.AllowHexSpecifier) / 255.0f;
					col.g = (float)System.Int32.Parse(str.Substring(2,2), NumberStyles.AllowHexSpecifier) / 255.0f;
					col.b = (float)System.Int32.Parse(str.Substring(4,2), NumberStyles.AllowHexSpecifier) / 255.0f;
					if(str.Length==8) col.a = System.Int32.Parse(str.Substring(6,2), NumberStyles.AllowHexSpecifier) / 255.0f;
					else col.a = 1.0f;
				}
					catch(Exception e) {
						Debug.Log("Could not convert " + value + " to Color. " +e);
						return new Color(0,0,0,0);
					}
			}
			return col;
		}
		
		
		/// <summary>
		/// converts string to lowercase
		/// </summary>
		public static string StringToLower (string value)
		{
			return value.ToLower();
		}
		
		/// <summary>
		/// converts string to uppercase
		/// </summary>
		public static string StringToUpper (string value)
		{
			return value.ToUpper();
		}
		
		/// <summary>
		/// converts string to first upper
		/// </summary>
		public static string StringToFirstUpper (string value)
		{
			if (!string.IsNullOrEmpty (value))
			{
				StringBuilder newString = new StringBuilder();
				for (int i = 0; i < value.Length; i++)
				{
					if (i == 0)
						newString.Append(value[0].ToString ().ToUpper ());
					else
						newString.Append(value[i].ToString ().ToLower ());
				}
				return newString.ToString ();
			}
			else return value;
		}
		
		/// <summary>
		/// converts string to first lower
		/// </summary>
		public static string StringToFirstLower (string value)
		{
			if (!string.IsNullOrEmpty (value))
			{
				StringBuilder newString = new StringBuilder();
				for (int i = 0; i < value.Length; i++)
				{
					if (i == 0)
						newString.Append(value[0].ToString ().ToLower ());
					else
						newString.Append(value[i]);
				}
				return newString.ToString ();
			}
			else return value;
		}
		
		/// <summary>
		/// converts string to title case
		/// </summary>
		public static string StringToTitleCase (string value)
		{
			if (!string.IsNullOrEmpty (value))
			{
				bool firstWord = true;
				string[] words = value.Split (' ');
				StringBuilder newString = new StringBuilder();
				foreach (string w in words)
				{
					StringBuilder newWord = new StringBuilder();
					for (int i = 0; i < w.Length; i++)
					{
						if (i == 0)
							newWord.Append(w[0].ToString ().ToUpper ());
						else
							newWord.Append(w[i].ToString ().ToLower ());
					}
					if (firstWord)
						newString.Append(newWord);
					else
						newString.Append(" " + newWord);
					
					firstWord = false;
				}
				return newString.ToString ();
			}
			else return value;
		}
		
		/// <summary>
		/// converts string to title case
		/// </summary>
		public static string StringToSpaceOutTitleCase (string value)
		{
			if (!string.IsNullOrEmpty (value))
			{
				string newString = string.Empty;
				
				for (int i = 0; i < value.Length; i++)
				{
					if (i == 0)
						newString = value[i].ToString().ToUpper();
					
					else if (IsAllUpperCase (value[i].ToString()))
					{
						newString += " " + value[i].ToString().ToUpper();
					}
					else newString += value[i].ToString();
				}
								
				return newString;
			}
			else return value;
		}
		
		/// <summary>
		/// Is the value Uppercase
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static bool IsAllUpperCase (string value)
		{
			for (int i = 0; i < value.Length; i++)
			{
				if (!char.IsUpper(value[i]))
				{
					return false;
				}
			}
			
			return true;
		}
		
		/// <summary>
		/// Randomly reorders the letters in a string
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static string RandomizeString (string value)
		{
			string newValue = string.Empty;
			
			List <char> c = value.ToList();
			List <int> index = new List<int> ();
			
			for (int i = 0; i < c.Count; i++)
				index.Add(i);
			
			for (int i = 0; i < c.Count; i++)
			{
				int ran = index[UnityEngine.Random.Range(0, index.Count)];
				newValue += c[ran].ToString();
				index.Remove(ran);
			}
			
			return newValue;
		}
		
		
		public static string GetGameObjectPath(GameObject obj)
		{
			string path = "/" + obj.name;
			while (obj.transform.parent != null)
			{
				obj = obj.transform.parent.gameObject;
				path = "/" + obj.name + path;
			}
			return path;
		}
		
	}
}














