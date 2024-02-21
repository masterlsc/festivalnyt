using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace UIStyles
{
	[AddComponentMenu("UI Styles/Dropdown Component")]
	public class UIStylesDropdownComponent : MonoBehaviour 
	{
		public enum DropdownType {Data, Categories}
		public DropdownType dropdownType;
		
		public bool autoApply = false;
		
		private Dropdown dropdown;
		
		private void OnDisable ()
		{
			UIStylesManager.onGotData		-= OnGotItems;
			UIStylesManager.onGotCategories -= OnGotItems;
		}
		
		private void Start ()
		{
			if (GetComponent<Dropdown>() && UIStylesManager.instance != null && UIStylesManager.instance.dataList.Count > 0)
			{
				dropdown = GetComponent<Dropdown>();
				
				if (dropdownType == DropdownType.Data)
				{
					UIStylesManager.onGotData += OnGotItems;
					
					OnGotItems (UIStylesManager.instance.dataOptionData);
					
					// Data dropdown
					dropdown.onValueChanged.AddListener (delegate {
						SetData ();
					} );
				}
				
				else if (dropdownType == DropdownType.Categories)
				{
					UIStylesManager.onGotCategories += OnGotItems;
					
					OnGotItems (UIStylesManager.instance.CategoryOptionData);
					
					// Category dropdown
					dropdown.onValueChanged.AddListener (delegate {
						SetCategory ();
					} );
				}
			}
		}
		
		private void SetData ()
		{
			UIStylesManager.instance.dataIndex = dropdown.value;
			UIStylesManager.instance.FillCategoryDropdown ();
			
			if (autoApply && UIStylesManager.instance.data != null)
				StyleHelper.ApplyAllCategories(UIStylesManager.instance.data, UIStylesManager.instance.cachedObjs.ToArray());
		}
		
		private void SetCategory ()
		{
			UIStylesManager.instance.data.currentCategory = UIStylesManager.instance.data.categories[dropdown.value];
			UIStylesManager.instance.categoryIndex = dropdown.value;
			
			if (autoApply && UIStylesManager.instance.data != null)
				StyleHelper.ApplyCurrentCategory(UIStylesManager.instance.data, UIStylesManager.instance.cachedObjs.ToArray());
		}
		
		private void OnGotItems (List<Dropdown.OptionData> optionData)
		{
			dropdown.options = optionData;
			
			if (dropdownType == DropdownType.Data)
				dropdown.value = UIStylesManager.instance.dataIndex;
			
			else if (dropdownType == DropdownType.Categories)
				dropdown.value = UIStylesManager.instance.categoryIndex;
		}
	}
}



















