using UnityEngine;
using UnityEngine.UI;

namespace UIStyles
{
    [System.Serializable]
    public class ContentSizeFitterHelper
    {
        /// <summary>
        /// Create Style, pass in an object to match the styles values with the objects values
        /// </summary>
        public static void CreateStyle (StyleDataFile data, string category, string styleName, string findByName, GameObject obj)
        {
            Style style = new Style(data, category, styleName, findByName);
            style.rename = !Application.isPlaying;
            
            StyleComponent v = new StyleComponent(data, style, StyleComponentType.ContentSizeFitter, "ContentSizeFitter", "");
            
            if (obj != null)
                v.contentSizeFitter = SetValuesFromComponent ( obj );
            
            UIStylesDatabase.Save ();
        }
        
        /// <summary>
        /// Sets the Values from a component
        /// </summary>
        public static ContentSizeFitterValues SetValuesFromComponent ( Component com )
        {
            if (com is ContentSizeFitter)
                return SetValuesFromComponent ( (ContentSizeFitter)com, null );
            
             else Debug.LogError ("SetValuesFromComponent: Type is: " + com.GetType() + " but should be ContentSizeFitter");
             
             return null;
        }
        public static ContentSizeFitterValues SetValuesFromComponent ( GameObject obj )
        {
            if (obj.GetComponent<ContentSizeFitter>())
                return SetValuesFromComponent ( obj.GetComponent<ContentSizeFitter>(), obj );
            
            else Debug.LogError ("SetValuesFromComponent: Component not found!");
            
            return null;
        }
        
        public static ContentSizeFitterValues SetValuesFromComponent ( ContentSizeFitter value, GameObject obj )
        {
            ContentSizeFitterValues values = new ContentSizeFitterValues ();
             
                values.horizontalFit = value.horizontalFit;
                values.horizontalFitEnabled = true;
                
                values.verticalFit = value.verticalFit;
                values.verticalFitEnabled = true;
                
             return values;
        }
        
        /// <summary>
        /// Apply
        /// </summary>
        public static void Apply ( ContentSizeFitterValues values, GameObject obj )
        {
            if ( obj.GetComponent<ContentSizeFitter> () )
            {
                ContentSizeFitter component = obj.GetComponent<ContentSizeFitter> ();
            
                if ( values.horizontalFitEnabled )
                    component.horizontalFit = values.horizontalFit;
        
                if ( values.verticalFitEnabled )
                    component.verticalFit = values.verticalFit;
        
            }
        }
    }
}
