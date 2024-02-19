using UnityEngine;
using UnityEngine.UI;

namespace UIStyles
{
    [System.Serializable]
    public class LayoutElementValues
    {
        public bool ignoreLayout;
	    public bool ignoreLayoutEnabled;
    
        public float minWidth;
	    public bool minWidthEnabled;
	    public bool minWidthAllow;
    
        public float minHeight;
	    public bool minHeightEnabled;
	    public bool minHeightAllow;
    
        public float preferredWidth;
	    public bool preferredWidthEnabled;
	    public bool preferredWidthAllow;
    
        public float preferredHeight;
	    public bool preferredHeightEnabled;
	    public bool preferredHeightAllow;
    
        public float flexibleWidth;
	    public bool flexibleWidthEnabled;
	    public bool flexibleWidthAllow;
    
        public float flexibleHeight;
	    public bool flexibleHeightEnabled;
	    public bool flexibleHeightAllow;
    
        
        public LayoutElementValues CloneValues ()
        {
            LayoutElementValues values = new LayoutElementValues();
            
            values.ignoreLayout = this.ignoreLayout;
            values.ignoreLayoutEnabled = this.ignoreLayoutEnabled;
            
            values.minWidth = this.minWidth;
            values.minWidthEnabled = this.minWidthEnabled;
            
            values.minHeight = this.minHeight;
            values.minHeightEnabled = this.minHeightEnabled;
            
            values.preferredWidth = this.preferredWidth;
            values.preferredWidthEnabled = this.preferredWidthEnabled;
            
            values.preferredHeight = this.preferredHeight;
            values.preferredHeightEnabled = this.preferredHeightEnabled;
            
            values.flexibleWidth = this.flexibleWidth;
            values.flexibleWidthEnabled = this.flexibleWidthEnabled;
            
            values.flexibleHeight = this.flexibleHeight;
            values.flexibleHeightEnabled = this.flexibleHeightEnabled;
            
            return values;
        }
    }
}
