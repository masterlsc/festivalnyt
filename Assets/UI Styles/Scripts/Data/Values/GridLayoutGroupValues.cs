using UnityEngine;
using UnityEngine.UI;

namespace UIStyles
{
    [System.Serializable]
    public class GridLayoutGroupValues
    {
	    public bool paddingDropdown;
	    public RectOffset padding = new RectOffset();
        public bool paddingEnabled;
    
	    public Vector2 cellSize = new Vector2(100, 100);
        public bool cellSizeEnabled;
    
	    public Vector2 spacing = new Vector2(0, 0);
        public bool spacingEnabled;
    
	    public GridLayoutGroup.Corner startCorner = GridLayoutGroup.Corner.LowerLeft;
        public bool startCornerEnabled;
    
	    public GridLayoutGroup.Axis startAxis = GridLayoutGroup.Axis.Horizontal;
        public bool startAxisEnabled;
    
	    public TextAnchor childAlignment = TextAnchor.UpperLeft;
        public bool childAlignmentEnabled;
    
	    public GridLayoutGroup.Constraint constraint = GridLayoutGroup.Constraint.Flexible;
        public bool constraintEnabled;
    
        public int constraintCount;
        public bool constraintCountEnabled;
    
        
        public GridLayoutGroupValues CloneValues ()
        {
            GridLayoutGroupValues values = new GridLayoutGroupValues();
            
            values.padding = this.padding;
            values.paddingEnabled = this.paddingEnabled;
            
            values.cellSize = this.cellSize;
            values.cellSizeEnabled = this.cellSizeEnabled;
            
            values.spacing = this.spacing;
            values.spacingEnabled = this.spacingEnabled;
            
            values.startCorner = this.startCorner;
            values.startCornerEnabled = this.startCornerEnabled;
            
            values.startAxis = this.startAxis;
            values.startAxisEnabled = this.startAxisEnabled;
            
            values.childAlignment = this.childAlignment;
            values.childAlignmentEnabled = this.childAlignmentEnabled;
            
            values.constraint = this.constraint;
            values.constraintEnabled = this.constraintEnabled;
            
            values.constraintCount = this.constraintCount;
            values.constraintCountEnabled = this.constraintCountEnabled;
            
            return values;
        }
    }
}
