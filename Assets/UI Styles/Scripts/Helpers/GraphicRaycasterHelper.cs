using UnityEngine;
using UnityEngine.UI;

namespace UIStyles
{
    [System.Serializable]
    public class GraphicRaycasterHelper
    {
        /// <summary>
        /// Create Style, pass in an object to match the styles values with the objects values
        /// </summary>
        public static void CreateStyle (StyleDataFile data, string category, string styleName, string findByName, GameObject obj)
        {
            Style style = new Style(data, category, styleName, findByName);
            style.rename = !Application.isPlaying;
            
            StyleComponent v = new StyleComponent(data, style, StyleComponentType.GraphicRaycaster, "GraphicRaycaster", "");
            
            if (obj != null)
                v.graphicRaycaster = SetValuesFromComponent ( obj );
            
            UIStylesDatabase.Save ();
        }
        
        /// <summary>
        /// Sets the Values from a component
        /// </summary>
        public static GraphicRaycasterValues SetValuesFromComponent ( Component com )
        {
            if (com is GraphicRaycaster)
                return SetValuesFromComponent ( (GraphicRaycaster)com, null );
            
             else Debug.LogError ("SetValuesFromComponent: Type is: " + com.GetType() + " but should be GraphicRaycaster");
             
             return null;
        }
        public static GraphicRaycasterValues SetValuesFromComponent ( GameObject obj )
        {
            if (obj.GetComponent<GraphicRaycaster>())
                return SetValuesFromComponent ( obj.GetComponent<GraphicRaycaster>(), obj );
            
            else Debug.LogError ("SetValuesFromComponent: Component not found!");
            
            return null;
        }
        
        public static GraphicRaycasterValues SetValuesFromComponent ( GraphicRaycaster value, GameObject obj )
        {
            GraphicRaycasterValues values = new GraphicRaycasterValues ();
             
                values.ignoreReversedGraphics = value.ignoreReversedGraphics;
                values.ignoreReversedGraphicsEnabled = true;
                
                values.blockingObjects = value.blockingObjects;
                values.blockingObjectsEnabled = true;
                
             return values;
        }
        
        /// <summary>
        /// Apply
        /// </summary>
        public static void Apply ( GraphicRaycasterValues values, GameObject obj )
        {
            if ( obj.GetComponent<GraphicRaycaster> () )
            {
                GraphicRaycaster component = obj.GetComponent<GraphicRaycaster> ();
            
                if ( values.ignoreReversedGraphicsEnabled )
                    component.ignoreReversedGraphics = values.ignoreReversedGraphics;
        
                if ( values.blockingObjectsEnabled )
                    component.blockingObjects = values.blockingObjects;
        
            }
        }
    }
}
