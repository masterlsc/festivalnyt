using UnityEngine;
using System.Collections;
using System.Linq;
using System.Reflection;

namespace UIStyles
{
	public class ReflectionHelper 
	{
		
		/// <summary>
		/// Is the given value excluded
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static bool IsPropertyExcluded (StyleDataFile data, string value)
		{
			return data.preferenceData.excludedProperties.Contains(value);
		}
		
		/// <summary>
		/// Is the given value excluded
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>

		public static bool IsFieldExcluded (StyleDataFile data, string value)
		{

			return data.preferenceData.excludedFields.Contains(value);
		}
		
		/// <summary>
		/// Is the given value excluded
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static bool IsTypeExcluded (StyleDataFile data, string value)
		{
			return data.preferenceData.excludedTypes.Contains(value);
		}
	}
}











