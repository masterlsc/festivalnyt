using UnityEngine;

namespace UIStyles
{
	/// <summary>
	/// This class is a quick fix for checking effects, when adding to a prefab that is in the scene it will add more then one effect, so this class reorders and removes if needed
	/// </summary>
	public class CheckEffects
	{
		/// <summary>
		/// Preference data
		/// </summary>
		private static PreferenceValues preferenceData
		{  get { return UIStylesDatabase.styleData.preferenceData; } }
		
		/// <summary>
		/// Checks all.
		/// </summary>
		public static void CheckAll ( GameObject effects, TextValues textValues, ImageValues imageValues )
		{
			CheckMultiple ( effects, textValues, imageValues );

			if ( DataHelper.canEditValues(effects) )
			{
				bool hasShadow = textValues != null ? textValues.useShadow : imageValues.useShadow;
				bool hasOutline = textValues != null ? textValues.useOutline : imageValues.useOutline;
				bool hasGradient = textValues != null ? textValues.overlay == Overlay.ColorOverlay : imageValues.overlay == Overlay.GradientOverlay;
				
				CheckOrder ( effects, hasShadow, hasOutline, hasGradient );
			}
		}

		/// <summary>
		/// Checks for multiple.
		/// </summary>
		public static void CheckMultiple ( GameObject effects, TextValues textValues, ImageValues imageValues )
		{
			// Check there is only the one gradient attached
			bool removedGradiend = false;
			UIStyles.Gradient[] gradient = effects.GetComponents<UIStyles.Gradient> ();
			for ( int l = 1; l < gradient.Length; l++ )
			{
				removedGradiend = true;
				
				#if UNITY_EDITOR
				UnityEditor.EditorWindow.DestroyImmediate ( gradient[l].GetComponent<UIStyles.Gradient> (), true );
				#else
				GameObject.Destroy ( gradient[l].GetComponent<UIStyles.Gradient> () );
				#endif
			}

			if ( removedGradiend && effects.GetComponent<UIStyles.Gradient> () )
			{
				effects.GetComponent<UIStyles.Gradient> ().topColor = textValues != null ? textValues.gradientTopColor : imageValues.gradientTopColor;
				effects.GetComponent<UIStyles.Gradient> ().bottomColor = textValues != null ? textValues.gradientBottomColor : imageValues.gradientBottomColor;
			}

			// Check there is only the one outline attached
			bool removedOutline = false;
			UIStyles.Outline[] outline = effects.GetComponents<UIStyles.Outline> ();
			for ( int l = 1; l < outline.Length; l++ )
			{
				removedOutline = true;
				
				#if UNITY_EDITOR
				UnityEditor.EditorWindow.DestroyImmediate ( outline[l].GetComponent<UIStyles.Outline> (), true );
				#else
				GameObject.Destroy ( outline[l].GetComponent<UIStyles.Outline> () );
				#endif
			}

			if ( removedOutline && effects.GetComponent<UIStyles.Outline> () )
			{
				effects.GetComponent<UIStyles.Outline> ().effectColor = textValues != null ? textValues.outlineColor : imageValues.outlineColor;
				effects.GetComponent<UIStyles.Outline> ().effectDistance = textValues != null ? textValues.outlineDistance : imageValues.outlineDistance;
				effects.GetComponent<UIStyles.Outline> ().useGraphicAlpha = textValues != null ? textValues.outlineUseAlpha : imageValues.outlineUseAlpha;
			}

			// Check there is only the one shadow attached
			bool removedShadow = false;
			UIStyles.Shadow[] shadow = effects.GetComponents<UIStyles.Shadow> ();
			for ( int l = 1; l < shadow.Length; l++ )
			{
				removedShadow = true;
				
				#if UNITY_EDITOR
				UnityEditor.EditorWindow.DestroyImmediate ( shadow[l].GetComponent<UIStyles.Shadow> (), true );
				#else
				GameObject.Destroy( shadow[l].GetComponent<UIStyles.Shadow> () );
				#endif
			}

			if ( removedShadow && effects.GetComponent<UIStyles.Shadow> () )
			{
				effects.GetComponent<UIStyles.Shadow> ().effectColor = textValues != null ? textValues.shadowColor : imageValues.shadowColor;
				effects.GetComponent<UIStyles.Shadow> ().effectDistance = textValues != null ? textValues.shadowDistance : imageValues.shadowDistance;
				effects.GetComponent<UIStyles.Shadow> ().useGraphicAlpha = textValues != null ? textValues.shadowUseAlpha : imageValues.shadowUseAlpha;
			}
		}

		/// <summary>
		/// Checks the order.
		/// </summary>
		public static void CheckOrder ( GameObject effects, bool needsShadow, bool needsOutline, bool needsGradient )
		{
			bool gotGradient = false;
			bool gotOutline = false;
			bool gotShadow = false;

			bool replaceUnityOutline = false;
			bool replaceUnityShadow = false;

			Component[] components = effects.GetComponents<Component> ();
			foreach ( Component c in components )
			{
				if ( c != null )
				{
					string component = c.GetType ().ToString ();

					if ( !gotGradient )
						gotGradient = component == "UIStyles.Gradient";

					if ( !gotOutline )
						gotOutline = component == "UIStyles.Outline";

					if ( !gotShadow )
						gotShadow = component == "UIStyles.Shadow";

					// Unity Effects
					if ( !replaceUnityOutline )
					{
						replaceUnityOutline = component == "UnityEngine.UI.Outline";
					}

					if ( !replaceUnityShadow )
					{
						replaceUnityShadow = component == "UnityEngine.UI.Shadow";
					}
				}
			}

			if ( replaceUnityOutline )
			{
				Color colour = new Color();
				Vector2 distance = Vector2.zero;
				bool alpha = true;
				
				while ( effects.GetComponent<UnityEngine.UI.Outline> () )
				{
					colour = effects.GetComponent<UnityEngine.UI.Outline> ().effectColor;
					distance = effects.GetComponent<UnityEngine.UI.Outline> ().effectDistance;
					alpha = effects.GetComponent<UnityEngine.UI.Outline> ().useGraphicAlpha;
					
					#if UNITY_EDITOR
					UnityEditor.EditorWindow.DestroyImmediate ( effects.GetComponent<UnityEngine.UI.Outline> (), true );
					#else
					GameObject.Destroy( effects.GetComponent<UnityEngine.UI.Outline> () );
					#endif
				}

				if (needsOutline)
				{
					effects.gameObject.AddComponent<UIStyles.Outline> ();
					effects.gameObject.GetComponent<UIStyles.Outline> ().effectColor = colour;
					effects.gameObject.GetComponent<UIStyles.Outline> ().effectDistance = distance;
					effects.gameObject.GetComponent<UIStyles.Outline> ().useGraphicAlpha = alpha;
					
					gotOutline = true;
				}
				else gotOutline = false;
			}

			if ( replaceUnityShadow )
			{
				Color colour = new Color();
				Vector2 distance = Vector2.zero;
				bool alpha = true;
				
				while(effects.GetComponent<UnityEngine.UI.Shadow> ())
				{
					colour = effects.GetComponent<UnityEngine.UI.Shadow> ().effectColor;
					distance = effects.GetComponent<UnityEngine.UI.Shadow> ().effectDistance;
					alpha = effects.GetComponent<UnityEngine.UI.Shadow> ().useGraphicAlpha;
					
					#if UNITY_EDITOR
					UnityEditor.EditorWindow.DestroyImmediate ( effects.GetComponent<UnityEngine.UI.Shadow> (), true );
					#else
					GameObject.Destroy ( effects.GetComponent<UnityEngine.UI.Shadow> () );
					#endif
				}
				
				if (needsShadow)
				{
					effects.gameObject.AddComponent<UIStyles.Shadow> ();
					effects.gameObject.GetComponent<UIStyles.Shadow> ().effectColor = colour;
					effects.gameObject.GetComponent<UIStyles.Shadow> ().effectDistance = distance;
					effects.gameObject.GetComponent<UIStyles.Shadow> ().useGraphicAlpha = alpha;
					
					gotShadow = true;
				}
				else gotShadow = false;
			}
			
			
			
			if ( gotGradient )
			{
				Color top = effects.GetComponent<UIStyles.Gradient> ().topColor;
				Color bottom = effects.GetComponent<UIStyles.Gradient> ().bottomColor;
				
				#if UNITY_EDITOR
				UnityEditor.EditorWindow.DestroyImmediate ( effects.GetComponent<UIStyles.Gradient> (), true );
				#else
				GameObject.Destroy ( effects.GetComponent<UIStyles.Gradient> () );
				#endif
				
				effects.gameObject.AddComponent<UIStyles.Gradient> ();
				effects.GetComponent<UIStyles.Gradient> ().topColor = top;
				effects.GetComponent<UIStyles.Gradient> ().bottomColor = bottom;
			}
			if ( gotOutline )
			{
				if (!effects.GetComponent<UIStyles.Outline> ())
					effects.gameObject.AddComponent<UIStyles.Outline> ();
				
				Color colour = effects.GetComponent<UIStyles.Outline> ().effectColor;
				Vector2 distance = effects.GetComponent<UIStyles.Outline> ().effectDistance;
				bool alpha = effects.GetComponent<UIStyles.Outline> ().useGraphicAlpha;
				
				#if UNITY_EDITOR
				UnityEditor.EditorWindow.DestroyImmediate ( effects.GetComponent<UIStyles.Outline> (), true );
				#else
				GameObject.Destroy ( effects.GetComponent<UIStyles.Outline> () );
				#endif

				effects.gameObject.AddComponent<UIStyles.Outline> ();
				effects.GetComponent<UIStyles.Outline> ().effectColor = colour;
				effects.GetComponent<UIStyles.Outline> ().effectDistance = distance;
				effects.GetComponent<UIStyles.Outline> ().useGraphicAlpha = alpha;
			}
			if ( gotShadow )
			{
				Color colour = effects.GetComponent<UIStyles.Shadow> ().effectColor;
				Vector2 distance = effects.GetComponent<UIStyles.Shadow> ().effectDistance;
				bool alpha = effects.GetComponent<UIStyles.Shadow> ().useGraphicAlpha;
				
				#if UNITY_EDITOR
				UnityEditor.EditorWindow.DestroyImmediate ( effects.GetComponent<UIStyles.Shadow> (), true );
				#else
				GameObject.Destroy ( effects.GetComponent<UIStyles.Shadow> () );
				#endif

				effects.gameObject.AddComponent<UIStyles.Shadow> ();
				effects.GetComponent<UIStyles.Shadow> ().effectColor = colour;
				effects.GetComponent<UIStyles.Shadow> ().effectDistance = distance;
				effects.GetComponent<UIStyles.Shadow> ().useGraphicAlpha = alpha;
			}
		}
	}
}




















