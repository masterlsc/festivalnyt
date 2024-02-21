using UnityEngine;
using UnityEngine.UI;

namespace UIStyles
{
    [System.Serializable]
    public class CanvasScalerValues
    {
	    public CanvasScaler.ScaleMode uiScaleMode = CanvasScaler.ScaleMode.ConstantPixelSize;
        public bool uiScaleModeEnabled;
    
        public float scaleFactor;
        public bool scaleFactorEnabled;
    
        public float referencePixelsPerUnit;
        public bool referencePixelsPerUnitEnabled;
    
	    public Vector2 referenceResolution = new Vector2(800, 600);
        public bool referenceResolutionEnabled;
    
	    public CanvasScaler.ScreenMatchMode screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
        public bool screenMatchModeEnabled;
    
	    public float matchWidthOrHeight = 0.5f;
        public bool matchWidthOrHeightEnabled;
    
	    public CanvasScaler.Unit physicalUnit = CanvasScaler.Unit.Points;
        public bool physicalUnitEnabled;
    
        public float fallbackScreenDPI;
        public bool fallbackScreenDPIEnabled;
    
        public float defaultSpriteDPI;
        public bool defaultSpriteDPIEnabled;
    
        
        public CanvasScalerValues CloneValues ()
        {
            CanvasScalerValues values = new CanvasScalerValues();
            
            values.uiScaleMode = this.uiScaleMode;
            values.uiScaleModeEnabled = this.uiScaleModeEnabled;
            
            values.scaleFactor = this.scaleFactor;
            values.scaleFactorEnabled = this.scaleFactorEnabled;
            
            values.referencePixelsPerUnit = this.referencePixelsPerUnit;
            values.referencePixelsPerUnitEnabled = this.referencePixelsPerUnitEnabled;
            
            values.referenceResolution = this.referenceResolution;
            values.referenceResolutionEnabled = this.referenceResolutionEnabled;
            
            values.screenMatchMode = this.screenMatchMode;
            values.screenMatchModeEnabled = this.screenMatchModeEnabled;
            
            values.matchWidthOrHeight = this.matchWidthOrHeight;
            values.matchWidthOrHeightEnabled = this.matchWidthOrHeightEnabled;
            
            values.physicalUnit = this.physicalUnit;
            values.physicalUnitEnabled = this.physicalUnitEnabled;
            
            values.fallbackScreenDPI = this.fallbackScreenDPI;
            values.fallbackScreenDPIEnabled = this.fallbackScreenDPIEnabled;
            
            values.defaultSpriteDPI = this.defaultSpriteDPI;
            values.defaultSpriteDPIEnabled = this.defaultSpriteDPIEnabled;
            
            return values;
        }
    }
}
