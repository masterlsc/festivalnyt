using UnityEngine;
using UnityEngine.UI;

namespace UIStyles
{
    [System.Serializable]
    public class CanvasHelper
    {
        /// <summary>
        /// Create Style, pass in an object to match the styles values with the objects values
        /// </summary>
        public static void CreateStyle (StyleDataFile data, string category, string styleName, string findByName, GameObject obj)
        {
            Style style = new Style(data, category, styleName, findByName);
            style.rename = !Application.isPlaying;
            
            StyleComponent v = new StyleComponent(data, style, StyleComponentType.Canvas, "Canvas", "");
            
            if (obj != null)
                v.canvas = SetValuesFromComponent ( obj );
            
            UIStylesDatabase.Save ();
        }
        
        /// <summary>
        /// Sets the Values from a component
        /// </summary>
        public static CanvasValues SetValuesFromComponent ( Component com )
        {
            if (com is Canvas)
                return SetValuesFromComponent ( (Canvas)com, null );
            
             else Debug.LogError ("SetValuesFromComponent: Type is: " + com.GetType() + " but should be Canvas");
             
             return null;
        }
        public static CanvasValues SetValuesFromComponent ( GameObject obj )
        {
            if (obj.GetComponent<Canvas>())
                return SetValuesFromComponent ( obj.GetComponent<Canvas>(), obj );
            
            else Debug.LogError ("SetValuesFromComponent: Component not found!");
            
            return null;
        }
        
        public static CanvasValues SetValuesFromComponent ( Canvas value, GameObject obj )
        {
            CanvasValues values = new CanvasValues ();
             
            values.renderMode = value.renderMode;
            values.renderModeEnabled = true;
            
            values.pixelPerfect = value.pixelPerfect;
            values.pixelPerfectEnabled = true;
            
            values.sortingOrder = value.sortingOrder;
            values.sortingOrderEnabled = true;
            
            values.targetDisplay = value.targetDisplay;
            values.targetDisplayEnabled = true;
            
        	values.rendererCamera = value.worldCamera;
	        values.rendererCameraEnabled = true;
	        
	        values.planeDistance = value.planeDistance;
	        values.planeDistanceEnabled = true;
                
             return values;
        }
        
        /// <summary>
        /// Apply
        /// </summary>
        public static void Apply ( CanvasValues values, GameObject obj )
        {
            if ( obj.GetComponent<Canvas> () )
            {
                Canvas component = obj.GetComponent<Canvas> ();
            
                if ( values.renderModeEnabled )
                    component.renderMode = values.renderMode;
        
                if ( values.pixelPerfectEnabled )
                    component.pixelPerfect = values.pixelPerfect;
        
                if ( values.sortingOrderEnabled )
                    component.sortingOrder = values.sortingOrder;
        
                if ( values.targetDisplayEnabled )
                    component.targetDisplay = values.targetDisplay;
        
                if ( values.rendererCameraEnabled )
	                component.worldCamera = values.rendererCamera;
	            
	            if ( values.planeDistanceEnabled )
		            component.planeDistance = values.planeDistance;
            }
        }
    }
}




















