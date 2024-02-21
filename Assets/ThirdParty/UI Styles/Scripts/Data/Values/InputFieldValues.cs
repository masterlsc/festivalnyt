using UnityEngine;
using UnityEngine.UI;

namespace UIStyles
{
	[System.Serializable]
	public class InputFieldValues 
	{
		[HideInInspector] public string name = string.Empty;
		
		// Values
		[HideInInspector] public bool	interactable = true;
		[HideInInspector] public int	characterLimit = 0;
		
		[HideInInspector] public InputField.ContentType contentType;
		[HideInInspector] public InputField.LineType	lineType;
		
		[HideInInspector] public InputField.InputType			inputType;
		[HideInInspector] public TouchScreenKeyboardType		keyboardType;
		[HideInInspector] public InputField.CharacterValidation characterValidation;
		
		[HideInInspector] public float	caretBlinkRate		= 0.85f;
		[HideInInspector] public int	caretWidth			= 1;
		[HideInInspector] public bool	customCaretColor	= false;
		[HideInInspector] public Color	caretColor			= new Color (0.196f, 0.196f, 0.196f, 1f);
		[HideInInspector] public string caretColorID;
		[HideInInspector] public Color	selectionColor		= new Color (0.659f, 0.808f, 1.000f, 0.753f);
		[HideInInspector] public string selectionColorID;
		[HideInInspector] public bool	hideMobileInput 	= false;
		[HideInInspector] public bool	readOnly			= false;
		
		[HideInInspector] public TransitionValues transitionValues = new TransitionValues();
		
		[HideInInspector]public string targetGraphicReference;
		[HideInInspector]public string textReference;
		[HideInInspector]public string placeholderReference;
		
		// enabled
		[HideInInspector]public bool interactableEnabled;
		[HideInInspector]public bool characterLimitEnabled;
		[HideInInspector]public bool contentTypeEnabled;
		[HideInInspector]public bool caretBlinkRateEnabled;
		[HideInInspector]public bool caretWidthEnabled;
		[HideInInspector]public bool customCaretColorEnabled;
		[HideInInspector]public bool caretColorEnabled;
		[HideInInspector]public bool selectionColorEnabled;
		[HideInInspector]public bool hideMobileInputEnabled;
		[HideInInspector]public bool readOnlyEnabled;
		
		
		
		public InputFieldValues CloneValues ()
		{
			InputFieldValues values = new InputFieldValues();
			
			values.interactable 			= this.interactable;
			values.characterLimit			= this.characterLimit;
			
			values.contentType				= this.contentType;
			values.lineType 				= this.lineType;
			
			values.inputType				= this.inputType;
			values.keyboardType 			= this.keyboardType;
			values.characterValidation		= this.characterValidation;
			
			values.caretBlinkRate			= this.caretBlinkRate;
			values.caretWidth				= this.caretWidth;
			values.customCaretColor			= this.customCaretColor;
			values.caretColor				= this.caretColor;
			values.caretColorID 			= this.caretColorID;
			values.selectionColor			= this.selectionColor;
			values.selectionColorID 		= this.selectionColorID;
			values.hideMobileInput 			= this.hideMobileInput;
			values.readOnly					= this.readOnly;
			
			values.transitionValues			= this.transitionValues.CloneValues();
			
			values.interactableEnabled		= this.interactableEnabled;
			values.characterLimitEnabled	= this.characterLimitEnabled;
			values.contentTypeEnabled		= this.contentTypeEnabled;
			values.caretBlinkRateEnabled	= this.caretBlinkRateEnabled;
			values.caretWidthEnabled		= this.caretWidthEnabled;
			values.customCaretColorEnabled	= this.customCaretColorEnabled;
			values.caretColorEnabled		= this.caretColorEnabled;
			values.selectionColorEnabled	= this.selectionColorEnabled;
			values.hideMobileInputEnabled	= this.hideMobileInputEnabled;
			values.readOnlyEnabled			= this.readOnlyEnabled;
			
			values.targetGraphicReference	= this.targetGraphicReference;
			values.textReference			= this.textReference;
			values.placeholderReference		= this.placeholderReference;
			
			return values;
		}
	}
}





















