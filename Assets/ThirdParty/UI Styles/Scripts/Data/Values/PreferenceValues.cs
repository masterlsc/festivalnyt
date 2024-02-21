using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace UIStyles
{
	[System.Serializable]
	public class PreferenceValues
	{
		public enum FindSceneMode {ScenesInBuild, Project, Folder}
		[HideInInspector] public FindSceneMode findSceneMode;
		
		[HideInInspector] public string sceneFolder = "/Scenes";
		[HideInInspector] public bool oneStyleOpenAtOnce = true;
		[HideInInspector] public bool oneComponentOpenAtOnce = true;
		
		[HideInInspector] public bool disableFindByNameWarning = false;
		[HideInInspector] public bool allowMultipleFindByNameInSeparateCategories = false;
		
		[HideInInspector] public RenderMode defaultCanvasRenderMode;
		[HideInInspector] public bool defaultCanvasPixelPerfect;
		[HideInInspector] public int defaultCanvasSortOrder;
		[HideInInspector] public int defaultCanvasTargetDisplay;
		
		[HideInInspector] public CanvasScaler.ScaleMode defaultCanvasScalerScaleMode;
		[HideInInspector] public float defaultCanvasScalerScaleFactor;
		[HideInInspector] public float defaultCanvasScalerPixelsPerUnit;
		[HideInInspector] public float defaultCanvasScalerDynamicPixelsPerUnit;
		[HideInInspector] public Vector2 defaultCanvasResolution;
		[HideInInspector] public CanvasScaler.ScreenMatchMode defaultCanvasScreenMatchMode;
		[HideInInspector] public float defaultCanvasScalerMatch;
		
		
		[HideInInspector] public CanvasScaler.Unit defaultCanvasScalerPhysicalUnit;
		[HideInInspector] public float defaultCanvasScalerFallbackScreenDPI;
		[HideInInspector] public float defaultCanvasScalerDefaultSpriteDPI;
	
		
		[HideInInspector] public bool defaultGraphicRaycasterIgnoreReversedGraphics;
		[HideInInspector] public GraphicRaycaster.BlockingObjects defaultGraphicRaycasterBlockingObjects;
		
		
		
		[HideInInspector] public List<string> excludedProperties;
		[HideInInspector] public List<string> excludedFields;
		[HideInInspector] public List<string> excludedTypes;
		
		private List<string> DefaultExcludedProperties ()
		{
			List<string> list = new List<string>
			{
				"name", 
				"useGUILayout", 
				"enabled", 
				"isActiveAndEnabled", 
				"transform", 
				"gameObject", 
				"tag", 
				"rigidbody",
				"rigidbody2D",
				"camera",
				"light",
				"animation",
				"constantForce",
				"renderer",
				"audio",
				"guiText",
				"networkView",
				"guiElement",
				"guiTexture",
				"collider",
				"collider2D",
				"hingeJoint",
				"particleEmitter",
				"particleSystem",
				"hideFlags"
			};
			
			list.Sort();
			return list;
		}
		
		private List<string> DefaultExcludedTypes  ()
		{
			List<string> list = new List<string>
			{
				"UnityEngine.Events.UnityEvent"
			};
			
			list.Sort();
			return list;
		}
		
		private List<string> DefaultExcludedFields  ()
		{
			List<string> list = new List<string>
			{
				
			};
			
			list.Sort();
			return list;
		}
		
		public PreferenceValues ()
		{
			excludedProperties	= DefaultExcludedProperties ();
			excludedFields		= DefaultExcludedFields  ();
			excludedTypes		= DefaultExcludedTypes  ();
			
			defaultCanvasRenderMode = RenderMode.ScreenSpaceOverlay;
			defaultCanvasPixelPerfect = false;
			defaultCanvasSortOrder = 0;
			defaultCanvasTargetDisplay = 0;
			defaultCanvasScalerScaleMode = UnityEngine.UI.CanvasScaler.ScaleMode.ConstantPixelSize;
			defaultCanvasScalerScaleFactor = 1;
			defaultCanvasScalerPixelsPerUnit = 100;
			defaultCanvasScalerDynamicPixelsPerUnit = 1;
			defaultGraphicRaycasterIgnoreReversedGraphics = true;
			defaultGraphicRaycasterBlockingObjects = UnityEngine.UI.GraphicRaycaster.BlockingObjects.None;
			defaultCanvasResolution = new Vector2(800, 600);
			defaultCanvasScalerMatch = .5f;
			defaultCanvasScreenMatchMode = UnityEngine.UI.CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
			defaultCanvasScalerPhysicalUnit = UnityEngine.UI.CanvasScaler.Unit.Points;
			defaultCanvasScalerFallbackScreenDPI = 96;
			defaultCanvasScalerDefaultSpriteDPI = 96;
		}
		
		
		
		public PreferenceValues Clone ()
		{
			PreferenceValues values = new PreferenceValues();
			
			values.findSceneMode									= this.findSceneMode;
			values.sceneFolder										= this.sceneFolder;
			values.oneStyleOpenAtOnce								= this.oneStyleOpenAtOnce;
			values.oneComponentOpenAtOnce							= this.oneComponentOpenAtOnce;
			values.disableFindByNameWarning 						= this.disableFindByNameWarning;
			values.allowMultipleFindByNameInSeparateCategories		= this.allowMultipleFindByNameInSeparateCategories;
			
			values.excludedProperties								= this.excludedProperties;
			values.excludedFields									= this.excludedFields;
			values.excludedTypes									= this.excludedTypes;
			
			values.defaultCanvasRenderMode							= this.defaultCanvasRenderMode;
			values.defaultCanvasPixelPerfect						= this.defaultCanvasPixelPerfect;
			values.defaultCanvasSortOrder							= this.defaultCanvasSortOrder;
			values.defaultCanvasTargetDisplay						= this.defaultCanvasTargetDisplay;
			values.defaultCanvasScalerScaleMode						= this.defaultCanvasScalerScaleMode;
			values.defaultCanvasScalerScaleFactor					= this.defaultCanvasScalerScaleFactor;
			values.defaultCanvasScalerPixelsPerUnit					= this.defaultCanvasScalerPixelsPerUnit;
			values.defaultGraphicRaycasterIgnoreReversedGraphics	= this.defaultGraphicRaycasterIgnoreReversedGraphics;
			values.defaultGraphicRaycasterBlockingObjects			= this.defaultGraphicRaycasterBlockingObjects;
			
			values.defaultCanvasScalerDynamicPixelsPerUnit			= this.defaultCanvasScalerDynamicPixelsPerUnit;
			values.defaultCanvasResolution							= this.defaultCanvasResolution;
			values.defaultCanvasScalerMatch							= this.defaultCanvasScalerMatch;
			values.defaultCanvasScreenMatchMode						= this.defaultCanvasScreenMatchMode;
			values.defaultCanvasScalerPhysicalUnit					= this.defaultCanvasScalerPhysicalUnit;
			values.defaultCanvasScalerFallbackScreenDPI				= this.defaultCanvasScalerFallbackScreenDPI;
			values.defaultCanvasScalerDefaultSpriteDPI				= this.defaultCanvasScalerDefaultSpriteDPI;
			
			return values;
		}
	}
}




















