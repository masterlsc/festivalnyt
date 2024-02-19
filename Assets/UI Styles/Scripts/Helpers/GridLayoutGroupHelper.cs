using UnityEngine;
using UnityEngine.UI;

namespace UIStyles
{
    [System.Serializable]
    public class GridLayoutGroupHelper
    {
        /// <summary>
        /// Create Style, pass in an object to match the styles values with the objects values
        /// </summary>
        public static void CreateStyle (StyleDataFile data, string category, string styleName, string findByName, GameObject obj)
        {
            Style style = new Style(data, category, styleName, findByName);
            style.rename = !Application.isPlaying;
            
            StyleComponent v = new StyleComponent(data, style, StyleComponentType.GridLayoutGroup, "GridLayoutGroup", "");
            
            if (obj != null)
                v.gridLayoutGroup = SetValuesFromComponent ( obj );
            
            UIStylesDatabase.Save ();
        }
        
        /// <summary>
        /// Sets the Values from a component
        /// </summary>
        public static GridLayoutGroupValues SetValuesFromComponent ( Component com )
        {
            if (com is GridLayoutGroup)
                return SetValuesFromComponent ( (GridLayoutGroup)com, null );
            
             else Debug.LogError ("SetValuesFromComponent: Type is: " + com.GetType() + " but should be GridLayoutGroup");
             
             return null;
        }
        public static GridLayoutGroupValues SetValuesFromComponent ( GameObject obj )
        {
            if (obj.GetComponent<GridLayoutGroup>())
                return SetValuesFromComponent ( obj.GetComponent<GridLayoutGroup>(), obj );
            
            else Debug.LogError ("SetValuesFromComponent: Component not found!");
            
            return null;
        }
        
        public static GridLayoutGroupValues SetValuesFromComponent ( GridLayoutGroup value, GameObject obj )
        {
            GridLayoutGroupValues values = new GridLayoutGroupValues ();
             
                values.padding = value.padding;
                values.paddingEnabled = true;
                
                values.cellSize = value.cellSize;
                values.cellSizeEnabled = true;
                
                values.spacing = value.spacing;
                values.spacingEnabled = true;
                
                values.startCorner = value.startCorner;
                values.startCornerEnabled = true;
                
                values.startAxis = value.startAxis;
                values.startAxisEnabled = true;
                
                values.childAlignment = value.childAlignment;
                values.childAlignmentEnabled = true;
                
                values.constraint = value.constraint;
                values.constraintEnabled = true;
                
                values.constraintCount = value.constraintCount;
                values.constraintCountEnabled = true;
                
             return values;
        }
        
        /// <summary>
        /// Apply
        /// </summary>
        public static void Apply ( GridLayoutGroupValues values, GameObject obj )
        {
            if ( obj.GetComponent<GridLayoutGroup> () )
            {
                GridLayoutGroup component = obj.GetComponent<GridLayoutGroup> ();
            
                if ( values.paddingEnabled )
                    component.padding = values.padding;
        
                if ( values.cellSizeEnabled )
                    component.cellSize = values.cellSize;
        
                if ( values.spacingEnabled )
                    component.spacing = values.spacing;
        
                if ( values.startCornerEnabled )
                    component.startCorner = values.startCorner;
        
                if ( values.startAxisEnabled )
                    component.startAxis = values.startAxis;
        
                if ( values.childAlignmentEnabled )
                    component.childAlignment = values.childAlignment;
        
                if ( values.constraintEnabled )
                    component.constraint = values.constraint;
        
                if ( values.constraintCountEnabled )
                    component.constraintCount = values.constraintCount;
        
            }
        }
    }
}
