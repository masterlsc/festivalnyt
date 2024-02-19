using UnityEngine;
using UnityEngine.UI;

namespace UIStyles
{
    [System.Serializable]
    public class AspectRatioFitterValues
    {
        public AspectRatioFitter.AspectMode aspectMode;
        public bool aspectModeEnabled;
    
        public float aspectRatio;
        public bool aspectRatioEnabled;
    
        
        public AspectRatioFitterValues CloneValues ()
        {
            AspectRatioFitterValues values = new AspectRatioFitterValues();
            
            values.aspectMode = this.aspectMode;
            values.aspectModeEnabled = this.aspectModeEnabled;
            
            values.aspectRatio = this.aspectRatio;
            values.aspectRatioEnabled = this.aspectRatioEnabled;
            
            return values;
        }
    }
}
