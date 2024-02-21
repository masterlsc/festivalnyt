using UnityEngine;
using UnityEngine.UI;

namespace UIStyles
{
    [System.Serializable]
    public class CanvasGroupValues
    {
        public float alpha;
        public bool alphaEnabled;
    
        public bool interactable;
        public bool interactableEnabled;
    
        public bool blocksRaycasts;
        public bool blocksRaycastsEnabled;
    
        public bool ignoreParentGroups;
        public bool ignoreParentGroupsEnabled;
    
        
        public CanvasGroupValues CloneValues ()
        {
            CanvasGroupValues values = new CanvasGroupValues();
            
            values.alpha = this.alpha;
            values.alphaEnabled = this.alphaEnabled;
            
            values.interactable = this.interactable;
            values.interactableEnabled = this.interactableEnabled;
            
            values.blocksRaycasts = this.blocksRaycasts;
            values.blocksRaycastsEnabled = this.blocksRaycastsEnabled;
            
            values.ignoreParentGroups = this.ignoreParentGroups;
            values.ignoreParentGroupsEnabled = this.ignoreParentGroupsEnabled;
            
            return values;
        }
    }
}
