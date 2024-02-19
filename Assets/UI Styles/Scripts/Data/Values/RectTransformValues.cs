using UnityEngine;

namespace UIStyles
{
	[System.Serializable]
	public class RectTransformValues 
	{
		// Foldout
		[System.NonSerialized]public bool foldout = false;
		
		// Values
		[HideInInspector]public Vector3 position	= Vector3.zero;
		[HideInInspector]public Vector2 sizeDelta	= new Vector2(100, 100);
		[HideInInspector]public Vector2 anchorMin	= new Vector2(0.5f, 0.5f);
		[HideInInspector]public Vector2 anchorMax	= new Vector2(0.5f, 0.5f);
		[HideInInspector]public Vector2 pivot		= new Vector2(0.5f, 0.5f);
		[HideInInspector]public Vector3 rotation	= Vector3.zero;
		[HideInInspector]public Vector3 scale		= Vector3.one;
		
		// enabled
		[HideInInspector]public bool positionEnabled	= false;
		[HideInInspector]public bool sizeDeltaEnabled	= false;
		[HideInInspector]public bool anchorMinEnabled	= false;
		[HideInInspector]public bool anchorMaxEnabled	= false;
		[HideInInspector]public bool pivotEnabled		= false;
		[HideInInspector]public bool rotationEnabled	= false;
		[HideInInspector]public bool scaleEnabled		= false;
		
		
		public RectTransformValues CloneValues ()
		{	
			RectTransformValues values = new RectTransformValues();
			
			values.foldout		= this.foldout;
			
			values.position		= this.position;
			values.sizeDelta	= this.sizeDelta;
			values.anchorMin	= this.anchorMin;
			values.anchorMax	= this.anchorMax;
			values.pivot		= this.pivot;
			values.rotation		= this.rotation;
			values.scale		= this.scale;
			
			values.positionEnabled	= this.positionEnabled;
			values.sizeDeltaEnabled	= this.sizeDeltaEnabled;
			values.anchorMinEnabled	= this.anchorMinEnabled;
			values.anchorMaxEnabled	= this.anchorMaxEnabled;
			values.pivotEnabled		= this.pivotEnabled;
			values.rotationEnabled	= this.rotationEnabled;
			values.scaleEnabled		= this.scaleEnabled;
			
			return values;
		}
	}
}



















