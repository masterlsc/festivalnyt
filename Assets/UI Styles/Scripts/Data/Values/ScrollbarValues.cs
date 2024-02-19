using UnityEngine;
using UnityEngine.UI;

namespace UIStyles
{
	[System.Serializable]
	public class ScrollbarValues 
	{
		[HideInInspector] public string name = string.Empty;
		[HideInInspector] public string path = string.Empty;
		[HideInInspector] public bool show = true;
		[HideInInspector] public bool isCustom = false;
		
		// Values
		[HideInInspector] public bool interactable = true;
		[HideInInspector] public Scrollbar.Direction direction = Scrollbar.Direction.LeftToRight;
		[HideInInspector] public float	value = 0;
		[HideInInspector] public float	size = 0;
		[HideInInspector] public int	numberOfSteps = 0;
		
		[HideInInspector] public TransitionValues transitionValues = new TransitionValues();
		
		[HideInInspector]public string targetGraphicReference;
		[HideInInspector]public string handleRectReference;
		
		// enabled
		[HideInInspector]public bool interactableEnabled;
		[HideInInspector]public bool directionEnabled;
		[HideInInspector]public bool valueEnabled;
		[HideInInspector]public bool sizeEnabled;
		[HideInInspector]public bool numberOfStepsEnabled;
		
		
		public ScrollbarValues CloneValues ()
		{
			ScrollbarValues values = new ScrollbarValues();
			
			values.name 					= this.name;
			values.path 					= this.path;
			values.show 					= this.show;
			values.isCustom 				= this.isCustom;
			
			values.interactable 			= this.interactable;
			values.direction				= this.direction;
			values.value					= this.value;
			values.size 					= this.size;
			values.numberOfSteps			= this.numberOfSteps;
			
			values.transitionValues 		= this.transitionValues;
			
			values.interactableEnabled		= this.interactableEnabled;
			values.directionEnabled 		= this.directionEnabled;
			values.valueEnabled 			= this.valueEnabled;
			values.sizeEnabled				= this.sizeEnabled;
			values.numberOfStepsEnabled 	= this.numberOfStepsEnabled;
			
			values.targetGraphicReference	= this.targetGraphicReference;
			values.handleRectReference 		= this.handleRectReference;
			
			return values;
		}
	}
}



















