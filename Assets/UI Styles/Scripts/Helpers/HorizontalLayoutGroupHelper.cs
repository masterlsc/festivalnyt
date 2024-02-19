using UnityEngine;
using UnityEngine.UI;

namespace UIStyles
{
    [System.Serializable]
    public class HorizontalLayoutGroupHelper
    {
        /// <summary>
        /// Create Style, pass in an object to match the styles values with the objects values
        /// </summary>
        public static void CreateStyle (StyleDataFile data, string category, string styleName, string findByName, GameObject obj)
        {
            Style style = new Style(data, category, styleName, findByName);
            style.rename = !Application.isPlaying;
            
            StyleComponent v = new StyleComponent(data, style, StyleComponentType.HorizontalLayoutGroup, "HorizontalLayoutGroup", "");
            
            if (obj != null)
                v.horizontalLayoutGroup = SetValuesFromComponent ( obj );
            
            UIStylesDatabase.Save ();
        }
        
        /// <summary>
        /// Sets the Values from a component
        /// </summary>
        public static HorizontalLayoutGroupValues SetValuesFromComponent ( Component com )
        {
            if (com is HorizontalLayoutGroup)
                return SetValuesFromComponent ( (HorizontalLayoutGroup)com, null );
            
             else Debug.LogError ("SetValuesFromComponent: Type is: " + com.GetType() + " but should be HorizontalLayoutGroup");
             
             return null;
        }
        public static HorizontalLayoutGroupValues SetValuesFromComponent ( GameObject obj )
        {
            if (obj.GetComponent<HorizontalLayoutGroup>())
                return SetValuesFromComponent ( obj.GetComponent<HorizontalLayoutGroup>(), obj );
            
            else Debug.LogError ("SetValuesFromComponent: Component not found!");
            
            return null;
        }
        
        public static HorizontalLayoutGroupValues SetValuesFromComponent ( HorizontalLayoutGroup value, GameObject obj )
        {
            HorizontalLayoutGroupValues values = new HorizontalLayoutGroupValues ();
             
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
        public static void Apply ( HorizontalLayoutGroupValues values, GameObject obj )
        {
            if ( obj.GetComponent<HorizontalLayoutGroup> () )
            {
                HorizontalLayoutGroup component = obj.GetComponent<HorizontalLayoutGroup> ();
            
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
