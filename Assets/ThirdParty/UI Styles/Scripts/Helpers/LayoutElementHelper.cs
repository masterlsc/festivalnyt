using UnityEngine;
using UnityEngine.UI;

namespace UIStyles
{
    [System.Serializable]
    public class LayoutElementHelper
    {
        /// <summary>
        /// Create Style, pass in an object to match the styles values with the objects values
        /// </summary>
        public static void CreateStyle (StyleDataFile data, string category, string styleName, string findByName, GameObject obj)
        {
            Style style = new Style(data, category, styleName, findByName);
            style.rename = !Application.isPlaying;
            
            StyleComponent v = new StyleComponent(data, style, StyleComponentType.LayoutElement, "LayoutElement", "");
            
            if (obj != null)
                v.layoutElement = SetValuesFromComponent ( obj );
            
            UIStylesDatabase.Save ();
        }
        
        /// <summary>
        /// Sets the Values from a component
        /// </summary>
        public static LayoutElementValues SetValuesFromComponent ( Component com )
        {
            if (com is LayoutElement)
                return SetValuesFromComponent ( (LayoutElement)com, null );
            
             else Debug.LogError ("SetValuesFromComponent: Type is: " + com.GetType() + " but should be LayoutElement");
             
             return null;
        }
        public static LayoutElementValues SetValuesFromComponent ( GameObject obj )
        {
            if (obj.GetComponent<LayoutElement>())
                return SetValuesFromComponent ( obj.GetComponent<LayoutElement>(), obj );
            
            else Debug.LogError ("SetValuesFromComponent: Component not found!");
            
            return null;
        }
        
        public static LayoutElementValues SetValuesFromComponent ( LayoutElement value, GameObject obj )
        {
            LayoutElementValues values = new LayoutElementValues ();
             
                values.ignoreLayout = value.ignoreLayout;
                values.ignoreLayoutEnabled = true;
                
                values.minWidth = value.minWidth;
                values.minWidthEnabled = true;
                
                values.minHeight = value.minHeight;
                values.minHeightEnabled = true;
                
                values.preferredWidth = value.preferredWidth;
                values.preferredWidthEnabled = true;
                
                values.preferredHeight = value.preferredHeight;
                values.preferredHeightEnabled = true;
                
                values.flexibleWidth = value.flexibleWidth;
                values.flexibleWidthEnabled = true;
                
                values.flexibleHeight = value.flexibleHeight;
                values.flexibleHeightEnabled = true;
                
             return values;
        }
        
        /// <summary>
        /// Apply
        /// </summary>
        public static void Apply ( LayoutElementValues values, GameObject obj )
        {
            if ( obj.GetComponent<LayoutElement> () )
            {
                LayoutElement component = obj.GetComponent<LayoutElement> ();
            
                if ( values.ignoreLayoutEnabled )
                    component.ignoreLayout = values.ignoreLayout;
        
                if ( values.minWidthEnabled )
                    component.minWidth = values.minWidth;
        
                if ( values.minHeightEnabled )
                    component.minHeight = values.minHeight;
        
                if ( values.preferredWidthEnabled )
                    component.preferredWidth = values.preferredWidth;
        
                if ( values.preferredHeightEnabled )
                    component.preferredHeight = values.preferredHeight;
        
                if ( values.flexibleWidthEnabled )
                    component.flexibleWidth = values.flexibleWidth;
        
                if ( values.flexibleHeightEnabled )
                    component.flexibleHeight = values.flexibleHeight;
        
            }
        }
    }
}
