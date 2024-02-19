using UnityEngine;
using UnityEngine.UI;

namespace UIStyles
{
    [System.Serializable]
    public class GraphicRaycasterValues
    {
        public bool ignoreReversedGraphics;
        public bool ignoreReversedGraphicsEnabled;
    
	    public GraphicRaycaster.BlockingObjects blockingObjects = GraphicRaycaster.BlockingObjects.None;
        public bool blockingObjectsEnabled;
    
        
        public GraphicRaycasterValues CloneValues ()
        {
            GraphicRaycasterValues values = new GraphicRaycasterValues();
            
            values.ignoreReversedGraphics = this.ignoreReversedGraphics;
            values.ignoreReversedGraphicsEnabled = this.ignoreReversedGraphicsEnabled;
            
            values.blockingObjects = this.blockingObjects;
            values.blockingObjectsEnabled = this.blockingObjectsEnabled;
            
            return values;
        }
    }
}
