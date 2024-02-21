using UnityEngine;

namespace UIStyles
{
    [System.Serializable]
    public class DropdownValues
    {
#if !PRE_UNITY_5
        // Values
        [HideInInspector] public bool interactable = true;

        [HideInInspector] public TransitionValues transitionValues = new TransitionValues();

        [HideInInspector] public string targetGraphicReference;
        [HideInInspector] public string templateReference;
        [HideInInspector] public string captionTextReference;
        [HideInInspector] public string captionImageReference;
        [HideInInspector] public string itemTextReference;
        [HideInInspector] public string itemImageReference;

        // enabled
        [HideInInspector] public bool interactableEnabled;

        public DropdownValues CloneValues()
        {
            DropdownValues values = new DropdownValues();

            values.interactable = this.interactable;
            values.transitionValues = this.transitionValues.CloneValues();
            values.interactableEnabled = this.interactableEnabled;

            values.targetGraphicReference = this.targetGraphicReference;
            values.templateReference = this.templateReference;
            values.captionTextReference = this.captionTextReference;
            values.captionImageReference = this.captionImageReference;
            values.interactableEnabled = this.interactableEnabled;
            values.itemTextReference = this.itemImageReference;

            return values;
        }

#endif
    }
}



















