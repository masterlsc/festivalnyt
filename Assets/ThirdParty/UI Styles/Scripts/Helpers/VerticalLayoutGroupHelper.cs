using UnityEngine;
using UnityEngine.UI;

namespace UIStyles
{
    [System.Serializable]
    public class VerticalLayoutGroupHelper
    {
        /// <summary>
        /// Create Style, pass in an object to match the styles values with the objects values
        /// </summary>
        public static void CreateStyle (StyleDataFile data, string category, string styleName, string findByName, GameObject obj)
        {
            Style style = new Style(data, category, styleName, findByName);
            style.rename = !Application.isPlaying;
            
            StyleComponent v = new StyleComponent(data, style, StyleComponentType.VerticalLayoutGroup, "VerticalLayoutGroup", "");
            
            if (obj != null)
                v.verticalLayoutGroup = SetValuesFromComponent ( obj );
            
            UIStylesDatabase.Save ();
        }
        
        /// <summary>
        /// Sets the Values from a component
        /// </summary>
        public static VerticalLayoutGroupValues SetValuesFromComponent ( Component com )
        {
            if (com is VerticalLayoutGroup)
                return SetValuesFromComponent ( (VerticalLayoutGroup)com, null );
            
             else Debug.LogError ("SetValuesFromComponent: Type is: " + com.GetType() + " but should be VerticalLayoutGroup");
             
             return null;
        }
        public static VerticalLayoutGroupValues SetValuesFromComponent ( GameObject obj )
        {
            if (obj.GetComponent<VerticalLayoutGroup>())
                return SetValuesFromComponent ( obj.GetComponent<VerticalLayoutGroup>(), obj );
            
            else Debug.LogError ("SetValuesFromComponent: Component not found!");
            
            return null;
        }
        
        public static VerticalLayoutGroupValues SetValuesFromComponent ( VerticalLayoutGroup value, GameObject obj )
        {
            VerticalLayoutGroupValues values = new VerticalLayoutGroupValues ();
             
                values.padding = value.padding;
                values.paddingEnabled = true;
                
                values.spacing = value.spacing;
                values.spacingEnabled = true;
                
                values.childAlignment = value.childAlignment;
                values.childAlignmentEnabled = true;
                
			#if !PRE_UNITY_5
                values.childControlWidth = value.childControlWidth;
                values.childControlWidthEnabled = true;
                
                values.childControlHeight = value.childControlHeight;
                values.childControlHeightEnabled = true;
			#endif 

                values.childForceExpandWidth = value.childForceExpandWidth;
                values.childForceExpandWidthEnabled = true;
                
                values.childForceExpandHeight = value.childForceExpandHeight;
                values.childForceExpandHeightEnabled = true;
                
             return values;
        }
        
        /// <summary>
        /// Apply
        /// </summary>
        public static void Apply ( VerticalLayoutGroupValues values, GameObject obj )
        {
            if ( obj.GetComponent<VerticalLayoutGroup> () )
            {
                VerticalLayoutGroup component = obj.GetComponent<VerticalLayoutGroup> ();
            
                if ( values.paddingEnabled )
                    component.padding = values.padding;
        
                if ( values.spacingEnabled )
                    component.spacing = values.spacing;
        
                if ( values.childAlignmentEnabled )
                    component.childAlignment = values.childAlignment;
        
			#if !PRE_UNITY_5
                if ( values.childControlWidthEnabled )
                    component.childControlWidth = values.childControlWidth;
        
                if ( values.childControlHeightEnabled )
                    component.childControlHeight = values.childControlHeight;
			#endif
        
                if ( values.childForceExpandWidthEnabled )
                    component.childForceExpandWidth = values.childForceExpandWidth;
        
                if ( values.childForceExpandHeightEnabled )
                    component.childForceExpandHeight = values.childForceExpandHeight;
        
            }
        }
    }
}
