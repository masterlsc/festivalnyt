using UnityEngine;

namespace UIStyles
{
	[System.Serializable]
	public class ButtonValues 
	{
		// Values
		[HideInInspector] public bool interactable = true;
		[HideInInspector] public TransitionValues transitionValues = new TransitionValues();
		
		// enabled
		[HideInInspector]public bool interactableEnabled;
		
		[HideInInspector]public string targetGraphicReference;
		
		public ButtonValues CloneValues ()
		{
			ButtonValues values = new ButtonValues();
			
			values.interactable 			= this.interactable;
			values.transitionValues 		= this.transitionValues.CloneValues();
			values.interactableEnabled		= this.interactableEnabled;
			values.targetGraphicReference	= this.targetGraphicReference;
				
			return values;
		}
	}
}





















