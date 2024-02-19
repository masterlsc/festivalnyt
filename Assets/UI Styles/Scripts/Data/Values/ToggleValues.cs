using UnityEngine;
using UnityEngine.UI;

namespace UIStyles
{
	[System.Serializable]
	public class ToggleValues 
	{
		[HideInInspector] public string name = string.Empty;
		[HideInInspector] public string path = string.Empty;
		[HideInInspector] public bool show = true;
		[HideInInspector] public bool isCustom = false;
		
		// Values
		[HideInInspector] public bool interactable = true;
		[HideInInspector] public bool isOn = true;
		[HideInInspector] public Toggle.ToggleTransition toggleTransition = Toggle.ToggleTransition.Fade;
		
		[HideInInspector] public TransitionValues transitionValues	= new TransitionValues();
		
		[HideInInspector]public string targetGraphicReference;
		[HideInInspector]public string checkmarkReference;
		
		// enabled
		[HideInInspector] public bool interactableEnabled;
		[HideInInspector] public bool isOnEnabled;
		[HideInInspector] public bool toggleTransitionEnabled;
		
		
		
		public ToggleValues CloneValues ()
		{
			ToggleValues values = new ToggleValues();
			
			values.name 					= this.name;
			values.path 					= this.path;
			values.show 					= this.show;
			values.isCustom 				= this.isCustom;
			
			values.isOn						= this.isOn;
			values.interactable 			= this.interactable;
			values.toggleTransition 		= this.toggleTransition;
			
			values.transitionValues 		= this.transitionValues.CloneValues();
			
			values.interactableEnabled		= this.interactableEnabled;
			values.isOnEnabled 				= this.isOnEnabled;
			values.toggleTransitionEnabled	= this.toggleTransitionEnabled;
			
			values.targetGraphicReference 	= this.targetGraphicReference;
			values.checkmarkReference		= this.checkmarkReference;
			
			return values;
		}
	}
}



















