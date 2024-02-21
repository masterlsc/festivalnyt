using UnityEngine;
using UnityEngine.UI;

namespace UIStyles
{
    [System.Serializable]
    public class HorizontalLayoutGroupValues
	{
		public bool paddingDropdown;
		public RectOffset padding = new RectOffset();
        public bool paddingEnabled;
    
        public float spacing;
        public bool spacingEnabled;
    
		public TextAnchor childAlignment = TextAnchor.UpperLeft;
        public bool childAlignmentEnabled;
    
        public bool childControlWidth;
        public bool childControlWidthEnabled;
    
        public bool childControlHeight;
        public bool childControlHeightEnabled;
    
        public bool childForceExpandWidth;
        public bool childForceExpandWidthEnabled;
    
        public bool childForceExpandHeight;
        public bool childForceExpandHeightEnabled;
    
        
        public HorizontalLayoutGroupValues CloneValues ()
        {
            HorizontalLayoutGroupValues values = new HorizontalLayoutGroupValues();
            
            values.padding = this.padding;
            values.paddingEnabled = this.paddingEnabled;
            
            values.spacing = this.spacing;
            values.spacingEnabled = this.spacingEnabled;
            
            values.childAlignment = this.childAlignment;
            values.childAlignmentEnabled = this.childAlignmentEnabled;
            
            values.childControlWidth = this.childControlWidth;
            values.childControlWidthEnabled = this.childControlWidthEnabled;
            
            values.childControlHeight = this.childControlHeight;
            values.childControlHeightEnabled = this.childControlHeightEnabled;
            
            values.childForceExpandWidth = this.childForceExpandWidth;
            values.childForceExpandWidthEnabled = this.childForceExpandWidthEnabled;
            
            values.childForceExpandHeight = this.childForceExpandHeight;
            values.childForceExpandHeightEnabled = this.childForceExpandHeightEnabled;
            
            return values;
        }
    }
}
