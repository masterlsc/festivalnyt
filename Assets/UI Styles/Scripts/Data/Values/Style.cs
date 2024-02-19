using UnityEngine;
using System.Collections.Generic;

namespace UIStyles
{
	public enum StyleState {Enable, Disable}
	
	[System.Serializable]
	public class Style
	{
		#if UNITY_EDITOR
		[HideInInspector] public bool open = false;
		
		[HideInInspector] [System.NonSerialized]public bool renameFindBy = false;
		[HideInInspector] public bool findByNameExists = false;
		#endif
		
		[HideInInspector] public StyleState styleState;
		
		[HideInInspector] [System.NonSerialized]public bool rename = false;
		
		// Use this id to find this style
		[HideInInspector] public int id;
		
		// Name of the style
		[HideInInspector] public string name = string.Empty;
		
		// Styles find by name
		[HideInInspector] public string findByName = string.Empty;
		
		// Category the style is in
		[HideInInspector] public string category = string.Empty;
		
		// list of style components
		[HideInInspector] public List <StyleComponent> styleComponents = new List<StyleComponent>();
		
		/// <summary>
		/// Ids for grouping components
		/// </summary>
		[HideInInspector] private int groupIDCount;
		public int GetNewGroupID ()
		{
			groupIDCount ++;
			return groupIDCount;
		}
		
		
		/// <summary>
		/// Using this override will not initialise the style, it will not automatically be given an id, neither will it be added to the list of styles.
		/// </summary>
		/// <returns></returns>
		public Style () {}
		
		/// <summary>
		/// Using this override will initialise the style to give it an id and add it to the list of styles.
		/// </summary>
		/// param: data = The data file to use
		public Style (StyleDataFile data)
		{
			Initialise (data, "", "", "");
		}
		
		/// <summary>
		/// Using this override will initialise the style to give it an id and add it to the list of styles, also give it a category, name and find by name.
		/// </summary>
		/// param: data 	  = The data file to use.
		/// param: category	  = The category of the new style.
		/// param: name		  = The name of the new style.
		/// param: findByName = The new styles find by name.
		public Style (StyleDataFile data, string category, string styleName, string findByName)
		{
			Initialise (data, category, styleName, findByName);
		}
		
		private void Initialise (StyleDataFile data, string category, string styleName, string findByName)
		{
			// Check the data file.
			if (data == null)
			{
				Debug.LogError ("No data file!, without a data file, the style will have no id and can not be added to the list within the data file");
			}
			else
			{
				this.id = data.GetNewStyleID();
				data.styleIDs.Add(id, this);
				
				// Check the category name, if its empty change it to "Default".
				if (string.IsNullOrEmpty(category))
					category = "Default";
				
				// Check the style name, if its empty, change it to "New Style".
				if (string.IsNullOrEmpty(styleName))
					styleName = "New Style";
				
				// Check the find by name, if its empty, change it to match the styleName.
				if (Application.isPlaying && string.IsNullOrEmpty(findByName))
					findByName = styleName;
				
				// Add names
				this.name		= styleName;
				this.category	= category;
				this.findByName = findByName;
				
				// Add to list
				data.AddStyle(this);
			}
		}
		
		
		
				
		public void AddComponent (StyleComponent values)
		{
			styleComponents.Add ( values );
		}
		public void InsertComponent (int index, StyleComponent values)
		{
			styleComponents.Insert ( index, values );
		}
		
		public void DuplicateComponent (StyleDataFile data, StyleComponent copy, string newName = "")
		{
			StyleComponent newValues = copy.Clone(data, this);
						
			if (string.IsNullOrEmpty(newName))
				newValues.name += " Copy";
			
			else newValues.name = newName;
			
			styleComponents.Add(newValues);
		}
		
		
		
				
		/// <summary>
		/// Check if matching components have the same path, if they do its an error
		/// </summary>
		public void CheckForPathError ()
		{
			for (int i1 = 0; i1 < styleComponents.Count; i1++) 
			{
				styleComponents[i1].hasPathError = false;
				for (int i2 = 0; i2 < styleComponents.Count; i2++) 
				{
					if (i1 != i2 && styleComponents[i1].styleComponentType != StyleComponentType.Custom && styleComponents[i1].styleComponentType == styleComponents[i2].styleComponentType && styleComponents[i1].path == styleComponents[i2].path)
					{
						styleComponents[i1].hasPathError = true;
						break;
					}
				}
			}
		}
		
		public Style Clone (StyleDataFile data)
		{
			Style values = new Style(data);
			
			values.name					= this.name;
			values.findByName			= this.findByName;
			values.category				= this.category;
			values.groupIDCount			= this.groupIDCount;
			
			
			#if UNITY_EDITOR
			values.findByNameExists 	= this.findByNameExists;
			values.open 				= this.open;
			values.rename				= this.rename;
			values.renameFindBy 		= this.renameFindBy;
			#endif
			
			
			values.styleComponents = new List<StyleComponent>();
			foreach (StyleComponent v in this.styleComponents)
				values.styleComponents.Add(v.Clone(data, values));
			
			StyleHelper.AddComponentsToGroups (values);
			
			return values;
		}
	}	
}




















