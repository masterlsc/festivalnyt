using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

namespace UIStyles
{
	public class CustomComponentHelper 
	{		
		const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic;
		
		public static void Apply ( StyleDataFile data, CustomComponentValues values, GameObject obj )
		{
			if (values.customComponent != null)
			{				
				// Change values
				if ( obj.GetComponent ( values.customComponent.GetType ().ToString () ) )
				{
					string className = values.customComponent.GetType ().ToString ();
					
					Type type = obj.GetComponent(className).GetType ();
					
					Component copyTo = obj.GetComponent(className);
					Component copyFrom = values.customComponent;
					
					// ---------- //
					// Properties
					// ---------- //
					PropertyInfo[] properties = type.GetProperties(flags); 
					
					foreach (PropertyInfo prop in properties)
					{
						//Debug.Log (prop.Name + " + " + prop.GetValue(copyFrom, null) + "\n" + prop.PropertyType.ToString());
						
						bool isExcluded = 
							
							// Can write
							!prop.CanWrite ||
							
							// User excluded
							values.excludedList.Contains(prop.Name) ||
							
							// System excluded property
							ReflectionHelper.IsPropertyExcluded(data, prop.Name) ||
							
							// System excluded types
							ReflectionHelper.IsTypeExcluded(data, prop.PropertyType.ToString());
						
						if (!isExcluded)
						{
							prop.SetValue ( copyTo, prop.GetValue ( copyFrom, null ), null );
						}
					}
					
					// ---------- //
					// Field
					// ---------- //
					FieldInfo[] fields = type.GetFields(flags); 
					
					foreach (FieldInfo field in fields)
					{
						//Debug.Log (field.Name + " + " + field.GetValue(copyFrom) + "\n" + field.FieldType.ToString());
						
						bool isExcluded = 
							
							// User excluded
							values.excludedList.Contains(field.Name) ||
							
							// System excluded fields
							ReflectionHelper.IsFieldExcluded(data, field.Name) ||
							
							// System excluded types
							ReflectionHelper.IsTypeExcluded(data, field.FieldType.ToString());
							
						if (!isExcluded)
						{
							field.SetValue(copyTo, field.GetValue(copyFrom));
						}
					}					
				}
			}
		}
	}
}



















