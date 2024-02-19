using UnityEngine;
using UnityEngine.UI;

namespace UIStyles
{
    [System.Serializable]
    public class ContentSizeFitterValues
    {
	    public ContentSizeFitter.FitMode horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
        public bool horizontalFitEnabled;
    
	    public ContentSizeFitter.FitMode verticalFit = ContentSizeFitter.FitMode.Unconstrained;
        public bool verticalFitEnabled;
    
        
        public ContentSizeFitterValues CloneValues ()
        {
            ContentSizeFitterValues values = new ContentSizeFitterValues();
            
            values.horizontalFit = this.horizontalFit;
            values.horizontalFitEnabled = this.horizontalFitEnabled;
            
            values.verticalFit = this.verticalFit;
            values.verticalFitEnabled = this.verticalFitEnabled;
            
            return values;
        }
    }
}
