using UnityEngine;
using UnityEngine.UI;

namespace UIStyles
{
	[System.Serializable]
	public class TransitionValues 
	{
		[HideInInspector] public Selectable.Transition transition = Selectable.Transition.ColorTint;
		
		// Images
		[HideInInspector] public Sprite highlightedGraphic;
		[HideInInspector] public Sprite pressedGraphic;
		[HideInInspector] public Sprite disabledGraphic;
		
		// Colours		
		[HideInInspector] public Color normalColor		= new Color (1f, 1f, 1f, 1f);
		[HideInInspector] public string normalColorID;
		[HideInInspector] public Color highlightedColor = new Color (0.961f, 0.961f, 0.961f, 1f);
		[HideInInspector] public string highlightedColorID;
		[HideInInspector] public Color pressedColor 	= new Color (0.784f, 0.784f, 0.784f, 1f);
		[HideInInspector] public string pressedColorID;
		[HideInInspector] public Color disabledColor	= new Color (0.784f, 0.784f, 0.784f, 0.502f);
		[HideInInspector] public string disabledColorID;
		
		// Strings
		[HideInInspector] public string normalTrigger		= "Normal";
		[HideInInspector] public string highlightedTrigger	= "Highlighted";
		[HideInInspector] public string pressedTrigger		= "Pressed";
		[HideInInspector] public string disabledTrigger 	= "Disabled";
		
		// Floats
		[HideInInspector] public float colorMultiplier	= 1;
		[HideInInspector] public float fadeDuration 	= 0.1f;
		
		// Enabled
		[HideInInspector]public bool transitionEnabled = false;
		
		
		
		public TransitionValues CloneValues ()
		{
			TransitionValues values = new TransitionValues();
			
			values.transition = this.transition;
		
			// Images
			values.highlightedGraphic	= this.highlightedGraphic;
			values.pressedGraphic		= this.pressedGraphic;
			values.disabledGraphic		= this.disabledGraphic;
			
			// Colours		
			values.normalColor			= this.normalColor;
			values.normalColorID		= this.normalColorID;
			values.highlightedColor 	= this.highlightedColor;
			values.highlightedColorID	= this.highlightedColorID;
			values.pressedColor 		= this.pressedColor;
			values.pressedColorID		= this.pressedColorID;
			values.disabledColor		= this.disabledColor;
			values.disabledColorID		= this.disabledColorID;
			
			// Strings
			values.normalTrigger		= this.normalTrigger;
			values.highlightedTrigger	= this.highlightedTrigger;
			values.pressedTrigger		= this.pressedTrigger;
			values.disabledTrigger 		= this.disabledTrigger;
			
			// Floats
			values.colorMultiplier		= this.colorMultiplier;
			values.fadeDuration 		= this.fadeDuration;
			
			// Enabled
			values.transitionEnabled	= this.transitionEnabled;
			
			return values;	
		}
	}
}



















 