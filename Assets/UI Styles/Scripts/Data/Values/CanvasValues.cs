using UnityEngine;
using UnityEngine.UI;

namespace UIStyles
{
    [System.Serializable]
    public class CanvasValues
    {
	    public RenderMode renderMode = RenderMode.ScreenSpaceOverlay;
        public bool renderModeEnabled;
    
        public bool pixelPerfect;
        public bool pixelPerfectEnabled;
    
        public int sortingOrder;
        public bool sortingOrderEnabled;
    
        public int targetDisplay;
        public bool targetDisplayEnabled;
    
        public Camera rendererCamera;
        public bool rendererCameraEnabled;
	    
	    public float planeDistance;
	    public bool planeDistanceEnabled;
    
        
        public CanvasValues CloneValues ()
        {
            CanvasValues values = new CanvasValues();
            
            values.renderMode = this.renderMode;
            values.renderModeEnabled = this.renderModeEnabled;
            
            values.pixelPerfect = this.pixelPerfect;
            values.pixelPerfectEnabled = this.pixelPerfectEnabled;
            
            values.sortingOrder = this.sortingOrder;
            values.sortingOrderEnabled = this.sortingOrderEnabled;
            
            values.targetDisplay = this.targetDisplay;
            values.targetDisplayEnabled = this.targetDisplayEnabled;
            
            values.rendererCamera = this.rendererCamera;
            values.rendererCameraEnabled = this.rendererCameraEnabled;
            
	        values.planeDistance = this.planeDistance;
	        values.planeDistanceEnabled = this.planeDistanceEnabled;
            
            return values;
        }
    }
}
