using UnityEngine;
using UnityEngine.UI;

namespace UIStyles
{
    [System.Serializable]
    public class CanvasScalerHelper
    {
        /// <summary>
        /// Create Style, pass in an object to match the styles values with the objects values
        /// </summary>
        public static void CreateStyle (StyleDataFile data, string category, string styleName, string findByName, GameObject obj)
        {
            Style style = new Style(data, category, styleName, findByName);
            style.rename = !Application.isPlaying;
            
            StyleComponent v = new StyleComponent(data, style, StyleComponentType.CanvasScaler, "CanvasScaler", "");
            
            if (obj != null)
                v.canvasScaler = SetValuesFromComponent ( obj );
            
            UIStylesDatabase.Save ();
        }
        
        /// <summary>
        /// Sets the Values from a component
        /// </summary>
        public static CanvasScalerValues SetValuesFromComponent ( Component com )
        {
            if (com is CanvasScaler)
                return SetValuesFromComponent ( (CanvasScaler)com, null );
            
             else Debug.LogError ("SetValuesFromComponent: Type is: " + com.GetType() + " but should be CanvasScaler");
             
             return null;
        }
        public static CanvasScalerValues SetValuesFromComponent ( GameObject obj )
        {
            if (obj.GetComponent<CanvasScaler>())
                return SetValuesFromComponent ( obj.GetComponent<CanvasScaler>(), obj );
            
            else Debug.LogError ("SetValuesFromComponent: Component not found!");
            
            return null;
        }
        
        public static CanvasScalerValues SetValuesFromComponent ( CanvasScaler value, GameObject obj )
        {
            CanvasScalerValues values = new CanvasScalerValues ();
             
                values.uiScaleMode = value.uiScaleMode;
                values.uiScaleModeEnabled = true;
                
                values.scaleFactor = value.scaleFactor;
                values.scaleFactorEnabled = true;
                
                values.referencePixelsPerUnit = value.referencePixelsPerUnit;
                values.referencePixelsPerUnitEnabled = true;
                
                values.referenceResolution = value.referenceResolution;
                values.referenceResolutionEnabled = true;
                
                values.screenMatchMode = value.screenMatchMode;
                values.screenMatchModeEnabled = true;
                
                values.matchWidthOrHeight = value.matchWidthOrHeight;
                values.matchWidthOrHeightEnabled = true;
                
                values.physicalUnit = value.physicalUnit;
                values.physicalUnitEnabled = true;
                
                values.fallbackScreenDPI = value.fallbackScreenDPI;
                values.fallbackScreenDPIEnabled = true;
                
                values.defaultSpriteDPI = value.defaultSpriteDPI;
                values.defaultSpriteDPIEnabled = true;
                
             return values;
        }
        
        /// <summary>
        /// Apply
        /// </summary>
        public static void Apply ( CanvasScalerValues values, GameObject obj )
        {
            if ( obj.GetComponent<CanvasScaler> () )
            {
                CanvasScaler component = obj.GetComponent<CanvasScaler> ();
            
                if ( values.uiScaleModeEnabled )
                    component.uiScaleMode = values.uiScaleMode;
        
                if ( values.scaleFactorEnabled )
                    component.scaleFactor = values.scaleFactor;
        
                if ( values.referencePixelsPerUnitEnabled )
                    component.referencePixelsPerUnit = values.referencePixelsPerUnit;
        
                if ( values.referenceResolutionEnabled )
                    component.referenceResolution = values.referenceResolution;
        
                if ( values.screenMatchModeEnabled )
                    component.screenMatchMode = values.screenMatchMode;
        
                if ( values.matchWidthOrHeightEnabled )
                    component.matchWidthOrHeight = values.matchWidthOrHeight;
        
                if ( values.physicalUnitEnabled )
                    component.physicalUnit = values.physicalUnit;
        
                if ( values.fallbackScreenDPIEnabled )
                    component.fallbackScreenDPI = values.fallbackScreenDPI;
        
                if ( values.defaultSpriteDPIEnabled )
                    component.defaultSpriteDPI = values.defaultSpriteDPI;
        
            }
        }
    }
}
