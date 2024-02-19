using UnityEngine;

namespace UIStyles
{
	public class RectTransformHelper
	{		
		/// <summary>
		/// Create Style, pass in an object to match the styles values with the objects values
		/// </summary>
		public static void CreateStyle (StyleDataFile data, string category, string styleName, string findByName, GameObject obj)
		{
			Style style = new Style(data, category, styleName, findByName);
			style.rename = !Application.isPlaying;
			
			StyleComponent v = new StyleComponent(data, style, StyleComponentType.RectTransform, "Rect Transform", "");
			
			if (obj != null)
				v.rectTransform = SetValuesFromComponent ( obj );
			
			UIStylesDatabase.Save ();
		}		
		
		/// <summary>
		/// Sets the Values from a component
		/// </summary>
		public static RectTransformValues SetValuesFromComponent (Component com, bool forceOpen = false)
		{
			if (com is RectTransform)
				return SetValuesFromComponent ( (RectTransform)com, forceOpen );
			
			else Debug.LogError ("SetValuesFromComponent: Type is: " + com.GetType() + " but should be RectTransform");
			
			return null;
		}
		public static RectTransformValues SetValuesFromComponent ( GameObject obj, bool forceOpen = false )
		{	
			if (obj.GetComponent<RectTransform>())
				return SetValuesFromComponent ( obj.GetComponent<RectTransform>(), forceOpen);
			
			else Debug.LogError ("SetValuesFromComponent: Component not found!");
			
			return null;
		}
		public static RectTransformValues SetValuesFromComponent (RectTransform rt, bool forceOpen)
		{	
			RectTransformValues values = new RectTransformValues();
			
			values.anchorMin = rt.anchorMin;
			values.anchorMax = rt.anchorMax;
			values.pivot	 = rt.pivot;
			values.rotation	 = rt.eulerAngles;
			values.scale	 = rt.localScale;
			
			values.position.x	= values.anchorMin.x == values.anchorMax.x ? rt.anchoredPosition.x	: rt.offsetMin.x;
			values.position.y	= values.anchorMin.y == values.anchorMax.y ? rt.anchoredPosition.y	: -rt.offsetMax.y;
			values.sizeDelta.x	= values.anchorMin.x == values.anchorMax.x ? rt.sizeDelta.x 		: -rt.offsetMax.x;
			values.sizeDelta.y	= values.anchorMin.y == values.anchorMax.y ? rt.sizeDelta.y 		: rt.offsetMin.y;
			
			// enabled
			values.positionEnabled	= true;
			values.sizeDeltaEnabled = true;
			values.anchorMinEnabled = true;
			values.anchorMaxEnabled = true;
			values.pivotEnabled 	= true;
			values.rotationEnabled	= true;
			values.scaleEnabled 	= true;
			
			if (forceOpen)
				values.foldout = true;
			
			return values;
		}		
		
		/*
			RectTransformHelper.SetRect 
			( 
				rt, 		// RectTransform
				0, 0, 0, 	// position
				0, 0, 		// sizeDelta
				0, 0, 		// anchorMin
				0, 0, 		// anchorMax
				0, 0 		// pivot
			);
		*/
		public static void SetRect ( RectTransform rt, float positionX, float positionY, float positionZ, float sizeDeltaX, float sizeDeltaY, float anchorMinX, float anchorMinY, float anchorMaxX, float anchorMaxY, float pivotX, float pivotY )
		{
			SetRect ( rt, new Vector3(positionX, positionY, positionZ), new Vector2 (sizeDeltaX, sizeDeltaY), new Vector2 (anchorMinX, anchorMinY), new Vector2 (anchorMaxX, anchorMaxY), new Vector2 (pivotX, pivotY) );
		}
		
		/*
		 	RectTransformHelper.SetRect 
			( 
				rt, 						// RectTransform
				new Vector3 ( 0, 0, 0 ),	// position
				new Vector2 ( 0, 0 ), 		// sizeDelta
				new Vector2 ( 0, 0 ), 		// anchorMin
				new Vector2 ( 0, 0 ), 		// anchorMax
				new Vector2 ( 0, 0 ) 		// pivot
			);
		*/
		public static void SetRect ( RectTransform rt, Vector3 position, Vector2 sizeDelta, Vector2 anchorMin, Vector2 anchorMax, Vector2 pivot )
		{
			// Anchors
			rt.anchorMin = new Vector2 ( anchorMin.x, anchorMin.y );
			rt.anchorMax = new Vector2 ( anchorMax.x, anchorMax.y );
			
			// Pivot
			rt.pivot = new Vector2 ( pivot.x, pivot.y );
						
			// X Position
			if ( anchorMin.x == anchorMax.x )
				// anchoredPosition
				rt.anchoredPosition = new Vector2 ( position.x, rt.anchoredPosition.y );
			else
				// offsetMin
				rt.offsetMin = new Vector2 ( position.x, rt.offsetMin.y );
			
			// Y Position
			if ( anchorMin.y == anchorMax.y )
				// anchoredPosition
				rt.anchoredPosition = new Vector2 ( rt.anchoredPosition.x, position.y );
			else
				// offsetMin
				rt.offsetMax = new Vector2 ( rt.offsetMax.x, -position.y );
			
			// Z anchoredPosition3D
			rt.anchoredPosition3D = new Vector3 ( rt.anchoredPosition3D.x, rt.anchoredPosition3D.y, position.z );
						
			// X Size Delta
			if ( anchorMin.x == anchorMax.x )
				// sizeDelta
				rt.sizeDelta = new Vector2 ( sizeDelta.x, rt.sizeDelta.y );
			else
				// offsetMax
				rt.offsetMax = new Vector2 ( -sizeDelta.x, rt.offsetMax.y );
			
			// Y Size Delta
			if ( anchorMin.y == anchorMax.y )
				// sizeDelta
				rt.sizeDelta = new Vector2 ( rt.sizeDelta.x, sizeDelta.y );
			else
				// offsetMax
				rt.offsetMin = new Vector2 ( rt.offsetMin.x, sizeDelta.y );
		}
		
		/// <summary>
		/// Apply the specified values and rectTransform.
		/// </summary>
		public static void Apply (RectTransformValues values, GameObject obj)
		{
			Apply (values, obj.GetComponent<RectTransform>());
		}
		public static void Apply (RectTransformValues values, RectTransform rectTransform)
		{
			if (rectTransform != null)
			{				
				if (values.positionEnabled)
				{
					// X Position
					if (values.anchorMin.x == values.anchorMax.x)
						// anchoredPosition
						rectTransform.anchoredPosition = new Vector2(values.position.x, rectTransform.anchoredPosition.y);
					else
						// offsetMin
						rectTransform.offsetMin = new Vector2(values.position.x, rectTransform.offsetMin.y);
					
					// Y Position
					if (values.anchorMin.y == values.anchorMax.y)
						// anchoredPosition3D
						rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, values.position.y);
					else
						// offsetMin
						rectTransform.offsetMax = new Vector2(rectTransform.offsetMax.x, -values.position.y);
					
					// Z anchoredPosition3D
					rectTransform.anchoredPosition3D = new Vector3(rectTransform.anchoredPosition3D.x, rectTransform.anchoredPosition3D.y, values.position.z );
				}
				
				if (values.sizeDeltaEnabled)
				{
					// X Size Delta
					if (values.anchorMin.x == values.anchorMax.x)
						// sizeDelta
						rectTransform.sizeDelta = new Vector2(values.sizeDelta.x, rectTransform.sizeDelta.y);
					else
						// offsetMax
						rectTransform.offsetMax = new Vector2(-values.sizeDelta.x, rectTransform.offsetMax.y);
					
					// Y Size Delta
					if (values.anchorMin.y == values.anchorMax.y)
						// sizeDelta
						rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, values.sizeDelta.y);
					else
						// offsetMax
						rectTransform.offsetMin = new Vector2(rectTransform.offsetMin.x, values.sizeDelta.y);
				}
				
				if (values.anchorMinEnabled)
					rectTransform.anchorMin = values.anchorMin;
				
				if (values.anchorMaxEnabled)
					rectTransform.anchorMax = values.anchorMax;
				
				if (values.pivotEnabled)
					rectTransform.pivot = values.pivot;
				
				if (values.rotationEnabled)
					rectTransform.eulerAngles = values.rotation;
				
				if (values.scaleEnabled)
					rectTransform.localScale = values.scale;
			}
		}		
	}
}



















