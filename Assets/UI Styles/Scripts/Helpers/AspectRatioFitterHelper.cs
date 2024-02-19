using UnityEngine;
using UnityEngine.UI;

namespace UIStyles
{
    [System.Serializable]
    public class AspectRatioFitterHelper
    {
        /// <summary>
        /// Create Style, pass in an object to match the styles values with the objects values
        /// </summary>
        public static void CreateStyle (StyleDataFile data, string category, string styleName, string findByName, GameObject obj)
        {
            Style style = new Style(data, category, styleName, findByName);
            style.rename = !Application.isPlaying;
            
            StyleComponent v = new StyleComponent(data, style, StyleComponentType.AspectRatioFitter, "AspectRatioFitter", "");
            
            if (obj != null)
                v.aspectRatioFitter = SetValuesFromComponent ( obj );
            
            UIStylesDatabase.Save ();
        }
        
        /// <summary>
        /// Sets the Values from a component
        /// </summary>
        public static AspectRatioFitterValues SetValuesFromComponent ( Component com )
        {
            if (com is AspectRatioFitter)
                return SetValuesFromComponent ( (AspectRatioFitter)com, null );
            
             else Debug.LogError ("SetValuesFromComponent: Type is: " + com.GetType() + " but should be AspectRatioFitter");
             
             return null;
        }
        public static AspectRatioFitterValues SetValuesFromComponent ( GameObject obj )
        {
            if (obj.GetComponent<AspectRatioFitter>())
                return SetValuesFromComponent ( obj.GetComponent<AspectRatioFitter>(), obj );
            
            else Debug.LogError ("SetValuesFromComponent: Component not found!");
            
            return null;
        }
        
        public static AspectRatioFitterValues SetValuesFromComponent ( AspectRatioFitter value, GameObject obj )
        {
            AspectRatioFitterValues values = new AspectRatioFitterValues ();
             
                values.aspectMode = value.aspectMode;
                values.aspectModeEnabled = true;
                
                values.aspectRatio = value.aspectRatio;
                values.aspectRatioEnabled = true;
                
             return values;
        }
        
        /// <summary>
        /// Apply
        /// </summary>
        public static void Apply ( AspectRatioFitterValues values, GameObject obj )
        {
            if ( obj.GetComponent<AspectRatioFitter> () )
            {
                AspectRatioFitter component = obj.GetComponent<AspectRatioFitter> ();
            
                if ( values.aspectModeEnabled )
                    component.aspectMode = values.aspectMode;
        
                if ( values.aspectRatioEnabled )
                    component.aspectRatio = values.aspectRatio;
        
            }
        }
    }
}
