using UnityEngine;
using UnityEngine.UI;

namespace UIStyles
{
    [System.Serializable]
    public class CanvasGroupHelper
    {
        /// <summary>
        /// Create Style, pass in an object to match the styles values with the objects values
        /// </summary>
        public static void CreateStyle (StyleDataFile data, string category, string styleName, string findByName, GameObject obj)
        {
            Style style = new Style(data, category, styleName, findByName);
            style.rename = !Application.isPlaying;
            
            StyleComponent v = new StyleComponent(data, style, StyleComponentType.CanvasGroup, "CanvasGroup", "");
            
            if (obj != null)
                v.canvasGroup = SetValuesFromComponent ( obj );
            
            UIStylesDatabase.Save ();
        }
        
        /// <summary>
        /// Sets the Values from a component
        /// </summary>
        public static CanvasGroupValues SetValuesFromComponent ( Component com )
        {
            if (com is CanvasGroup)
                return SetValuesFromComponent ( (CanvasGroup)com, null );
            
             else Debug.LogError ("SetValuesFromComponent: Type is: " + com.GetType() + " but should be CanvasGroup");
             
             return null;
        }
        public static CanvasGroupValues SetValuesFromComponent ( GameObject obj )
        {
            if (obj.GetComponent<CanvasGroup>())
                return SetValuesFromComponent ( obj.GetComponent<CanvasGroup>(), obj );
            
            else Debug.LogError ("SetValuesFromComponent: Component not found!");
            
            return null;
        }
        
        public static CanvasGroupValues SetValuesFromComponent ( CanvasGroup value, GameObject obj )
        {
            CanvasGroupValues values = new CanvasGroupValues ();
             
                values.alpha = value.alpha;
                values.alphaEnabled = true;
                
                values.interactable = value.interactable;
                values.interactableEnabled = true;
                
                values.blocksRaycasts = value.blocksRaycasts;
                values.blocksRaycastsEnabled = true;
                
                values.ignoreParentGroups = value.ignoreParentGroups;
                values.ignoreParentGroupsEnabled = true;
                
             return values;
        }
        
        /// <summary>
        /// Apply
        /// </summary>
        public static void Apply ( CanvasGroupValues values, GameObject obj )
        {
            if ( obj.GetComponent<CanvasGroup> () )
            {
                CanvasGroup component = obj.GetComponent<CanvasGroup> ();
            
                if ( values.alphaEnabled )
                    component.alpha = values.alpha;
        
                if ( values.interactableEnabled )
                    component.interactable = values.interactable;
        
                if ( values.blocksRaycastsEnabled )
                    component.blocksRaycasts = values.blocksRaycasts;
        
                if ( values.ignoreParentGroupsEnabled )
                    component.ignoreParentGroups = values.ignoreParentGroups;
        
            }
        }
    }
}
