using UnityEngine;
using UnityEngine.UI;

namespace UIStyles
{
	[System.Serializable]
	public class SliderValues 
	{
		[HideInInspector] public string name = string.Empty;
		[HideInInspector] public string path = string.Empty;
		[HideInInspector] public bool show = true;
		[HideInInspector] public bool isCustom = false;
		
		// Values
		[HideInInspector] public bool interactable = true;
		[HideInInspector] public Slider.Direction direction = Slider.Direction.LeftToRight;
		[HideInInspector] public float minValue = 0;
		[HideInInspector] public float maxValue = 1;
		[HideInInspector] public bool wholeNumbers = false;
		[HideInInspector] public float value = 0;
		
		[HideInInspector] public TransitionValues transitionValues = new TransitionValues();
		
		[HideInInspector]public string targetGraphicReference;
		[HideInInspector]public string fillRectReference;
		[HideInInspector]public string handleRectReference;
		
		// enabled
		[HideInInspector]public bool interactableEnabled;
		[HideInInspector]public bool directionEnabled;
		[HideInInspector]public bool minValueEnabled;
		[HideInInspector]public bool maxValueEnabled;
		[HideInInspector]public bool wholeNumbersEnabled;
		[HideInInspector]public bool valueEnabled;
		
		
		public SliderValues CloneValues ()
		{
			SliderValues values = new SliderValues();
			
			values.name = this.name;
			values.path = this.path;
			values.show = this.show;
			values.isCustom = this.isCustom;
			
			values.interactable 			= this.interactable;
			values.direction				= this.direction;
			values.minValue 				= this.minValue;
			values.maxValue 				= this.maxValue;
			values.wholeNumbers 			= this.wholeNumbers;
			values.value					= this.value;
			
			values.transitionValues 		= this.transitionValues.CloneValues();
			
			values.interactableEnabled		= this.interactableEnabled;
			values.directionEnabled 		= this.directionEnabled;
			values.minValueEnabled			= this.minValueEnabled;
			values.maxValueEnabled			= this.maxValueEnabled;
			values.wholeNumbersEnabled		= this.wholeNumbersEnabled;
			values.valueEnabled 			= this.valueEnabled;
			
			values.targetGraphicReference	= this.targetGraphicReference;
			values.fillRectReference		= this.fillRectReference;
			values.handleRectReference 		= this.handleRectReference;
			
			return values;
		}
	}
}



















