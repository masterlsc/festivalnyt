using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace UIStyles
{
	[AddComponentMenu("UI Styles/Button Component")]
	public class UIStylesButtonComponent : MonoBehaviour 
	{
		public enum ButtonType {ApplyStyle, ApplyCategory, ApplyCurrentCategory, ApplyAllCategories}
		public ButtonType buttonType;
		
		private Button button;
		public int dataIndex;
		public string category;
		public string styleName;
				
		private void Start ()
		{
			if (GetComponent<Button>())
				button = GetComponent<Button>();
			
			else Debug.LogError("No Button Found!");
			
			if (button != null && UIStylesManager.instance != null && UIStylesManager.instance.dataList.Count > 0)
			{
				if (buttonType == ButtonType.ApplyStyle)
				{
					button.onClick.AddListener (delegate {
						if (UIStylesManager.instance.data != null)
							StyleHelper.ApplyStyle(UIStylesManager.instance.dataList[dataIndex], category, styleName, UIStylesManager.instance.cachedObjs.ToArray());
					} );
				}
				else if (buttonType == ButtonType.ApplyCategory)
				{
					button.onClick.AddListener (delegate {
						if (UIStylesManager.instance.data != null)
							StyleHelper.ApplyCategory(UIStylesManager.instance.dataList[dataIndex], category, UIStylesManager.instance.cachedObjs.ToArray());
					} );
				}
				else if (buttonType == ButtonType.ApplyAllCategories)
				{
					button.onClick.AddListener (delegate {
						if (UIStylesManager.instance.data != null)
						{
							UIStylesManager.instance.SetDataFile(dataIndex);
							StyleHelper.ApplyAllCategories(UIStylesManager.instance.data, UIStylesManager.instance.cachedObjs.ToArray());
						}
					} );
				}
				else if (buttonType == ButtonType.ApplyCurrentCategory)
				{
					button.onClick.AddListener (delegate {
						if (UIStylesManager.instance.data != null)
							StyleHelper.ApplyCurrentCategory(UIStylesManager.instance.data, UIStylesManager.instance.cachedObjs.ToArray());
					} );
				}
			}
		}
	}
}


















