using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

namespace UIStyles
{
    public class UIStylesGUIScrollRect : EditorWindow
    {
        private static UnityEngine.Object[] draggedObjects;

        private static StyleComponent tempStyleComponent;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="style"></param>
        private static void ContentReferenceContext(Style style)
        {
            GenericMenu menu = new GenericMenu();

            menu.AddItem(new GUIContent("Null"), tempStyleComponent.scrollRect.contentReference == "Null", OnGotContentReference, "Null");

            menu.AddSeparator("");

            foreach (StyleComponent styleComponent in style.styleComponents)
	            if (styleComponent.styleComponentType == StyleComponentType.RectTransform || styleComponent.styleComponentType == StyleComponentType.Text || styleComponent.styleComponentType == StyleComponentType.Image)
                    menu.AddItem(new GUIContent(styleComponent.name), tempStyleComponent.scrollRect.contentReference == styleComponent.name, OnGotContentReference, styleComponent.name);

            menu.ShowAsContext();
        }

        private static void OnGotContentReference(object obj)
        {
            tempStyleComponent.scrollRect.contentReference = (string)obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="style"></param>
        private static void ViewpointReferenceContext(Style style)
        {
            GenericMenu menu = new GenericMenu();

            menu.AddItem(new GUIContent("Null"), tempStyleComponent.scrollRect.viewportReference == "Null", OnGotViewpointReference, "Null");

            menu.AddSeparator("");

            foreach (StyleComponent styleComponent in style.styleComponents)
	            if (styleComponent.styleComponentType == StyleComponentType.RectTransform || styleComponent.styleComponentType == StyleComponentType.Text || styleComponent.styleComponentType == StyleComponentType.Image)
                    menu.AddItem(new GUIContent(styleComponent.name), tempStyleComponent.scrollRect.viewportReference == styleComponent.name, OnGotViewpointReference, styleComponent.name);

            menu.ShowAsContext();
        }

        private static void OnGotViewpointReference(object obj)
        {
            tempStyleComponent.scrollRect.viewportReference = (string)obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="style"></param>
        private static void HorizonalScrollbarReferenceContext(Style style)
        {
            GenericMenu menu = new GenericMenu();

            menu.AddItem(new GUIContent("Null"), tempStyleComponent.scrollRect.horizonalScrollbarReference == "Null", OnGotHorizonalScrollbarReference, "Null");

            menu.AddSeparator("");

            foreach (StyleComponent styleComponent in style.styleComponents)
                if (styleComponent.styleComponentType == StyleComponentType.Scrollbar)
                    menu.AddItem(new GUIContent(styleComponent.name), tempStyleComponent.scrollRect.horizonalScrollbarReference == styleComponent.name, OnGotHorizonalScrollbarReference, styleComponent.name);

            menu.ShowAsContext();
        }

        private static void OnGotHorizonalScrollbarReference(object obj)
        {
            tempStyleComponent.scrollRect.horizonalScrollbarReference = (string)obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="style"></param>
        private static void VerticalScrollbarReferenceContext(Style style)
        {
            GenericMenu menu = new GenericMenu();

            menu.AddItem(new GUIContent("Null"), tempStyleComponent.scrollRect.verticalScrollbarReference == "Null", OnGotVerticalScrollbarReference, "Null");

            menu.AddSeparator("");

            foreach (StyleComponent styleComponent in style.styleComponents)
                if (styleComponent.styleComponentType == StyleComponentType.Scrollbar)
                    menu.AddItem(new GUIContent(styleComponent.name), tempStyleComponent.scrollRect.verticalScrollbarReference == styleComponent.name, OnGotVerticalScrollbarReference, styleComponent.name);

            menu.ShowAsContext();
        }

        private static void OnGotVerticalScrollbarReference(object obj)
        {
            tempStyleComponent.scrollRect.verticalScrollbarReference = (string)obj;
        }



        /// <summary>
        /// Draw the values
        /// </summary>
        public static void DrawValues(Style style, StyleComponent componentValues, ref bool checkPath, string findByName)
        {
            ScrollRectValues values = componentValues.scrollRect;

            GUILayout.Space(-8);
            EditorGUI.indentLevel = 0;

            GUILayout.BeginVertical(EditorHelper.StandardPanel(10));
            {
                // -------------------------------------------------- //
                // Draw Component Path
                // -------------------------------------------------- //
                UIStylesGUIPath.DrawPath(ref componentValues.path, ref componentValues.renamePath, componentValues.hasPathError, ref checkPath, findByName);
                GUILayout.Space(5);

                GUILayout.BeginVertical(EditorHelper.StandardPanel(10));
                {
                    // -------------------------------------------------- //
                    // Scroll Rect Values
                    // -------------------------------------------------- //
                    GUILayout.BeginHorizontal();
                    {
                        values.horizontalEnabled = (bool)EditorGUILayout.Toggle(values.horizontalEnabled, GUILayout.MaxWidth(26));

                        EditorGUI.BeginDisabledGroup(!values.horizontalEnabled);
                        {
                            values.horizontal = (bool)EditorGUILayout.Toggle("Cam Move Horizontal:", values.horizontal);
                        }
                        EditorGUI.EndDisabledGroup();
                    }
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();
                    {
                        values.verticalEnabled = (bool)EditorGUILayout.Toggle(values.verticalEnabled, GUILayout.MaxWidth(26));

                        EditorGUI.BeginDisabledGroup(!values.verticalEnabled);
                        {
                            values.vertical = (bool)EditorGUILayout.Toggle("Cam Move Vertical:", values.vertical);
                        }
                        EditorGUI.EndDisabledGroup();
                    }
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();
                    {
                        values.movementTypeEnabled = (bool)EditorGUILayout.Toggle(values.movementTypeEnabled, GUILayout.MaxWidth(26));

                        EditorGUI.BeginDisabledGroup(!values.movementTypeEnabled);
                        {
                            values.movementType = (ScrollRect.MovementType)EditorGUILayout.EnumPopup("Movement Type:", values.movementType);
                        }
                        EditorGUI.EndDisabledGroup();
                    }
                    GUILayout.EndHorizontal();

                    if (values.movementType == ScrollRect.MovementType.Elastic)
                    {
                        GUILayout.BeginHorizontal();
                        {
                            values.elasticityEnabled = (bool)EditorGUILayout.Toggle(values.elasticityEnabled, GUILayout.MaxWidth(26));

                            EditorGUI.BeginDisabledGroup(!values.elasticityEnabled);
                            {
                                values.elasticity = (float)EditorGUILayout.FloatField("Elasticity:", values.elasticity);
                            }
                            EditorGUI.EndDisabledGroup();
                        }
                        GUILayout.EndHorizontal();
                    }

                    GUILayout.BeginHorizontal();
                    {
                        values.scrollSensitivityEnabled = (bool)EditorGUILayout.Toggle(values.scrollSensitivityEnabled, GUILayout.MaxWidth(26));

                        EditorGUI.BeginDisabledGroup(!values.scrollSensitivityEnabled);
                        {
                            values.scrollSensitivity = (float)EditorGUILayout.FloatField("Scroll Sensitivity:", values.scrollSensitivity);
                        }
                        EditorGUI.EndDisabledGroup();
                    }
                    GUILayout.EndHorizontal();

                    GUILayout.Space(10);
                    EditorGUI.indentLevel = 0;
                    GUILayout.Label("Horizontal Scrollbar");
                    EditorGUI.indentLevel = 1;

                    GUILayout.BeginHorizontal();
                    {
                        values.showHorizontalScrollbarEnabled = (bool)EditorGUILayout.Toggle(values.showHorizontalScrollbarEnabled, GUILayout.MaxWidth(26));

                        EditorGUI.BeginDisabledGroup(!values.showHorizontalScrollbarEnabled);
                        {
                            values.showHorizontalScrollbar = (bool)EditorGUILayout.Toggle("Show Scrollbar:", values.showHorizontalScrollbar);
                        }
                        EditorGUI.EndDisabledGroup();
                    }
                    GUILayout.EndHorizontal();

                    if (values.showHorizontalScrollbar)
                    {
                        GUILayout.BeginHorizontal();
                        {
                            values.horizontalScrollbarVisibilityEnabled = (bool)EditorGUILayout.Toggle(values.horizontalScrollbarVisibilityEnabled, GUILayout.MaxWidth(26));

                            EditorGUI.BeginDisabledGroup(!values.horizontalScrollbarVisibilityEnabled);
                            {
                                values.horizontalScrollbarVisibility = (ScrollRect.ScrollbarVisibility)EditorGUILayout.EnumPopup("Visibility:", values.horizontalScrollbarVisibility);
                            }
                            EditorGUI.EndDisabledGroup();
                        }
                        GUILayout.EndHorizontal();

                        if (values.horizontalScrollbarVisibility == ScrollRect.ScrollbarVisibility.AutoHideAndExpandViewport)
                        {
                            GUILayout.BeginHorizontal();
                            {
                                values.horizontalScrollbarSpacingEnabled = (bool)EditorGUILayout.Toggle(values.horizontalScrollbarSpacingEnabled, GUILayout.MaxWidth(26));

                                EditorGUI.BeginDisabledGroup(!values.horizontalScrollbarSpacingEnabled);
                                {
                                    values.horizontalScrollbarSpacing = (float)EditorGUILayout.FloatField("Spacing:", values.horizontalScrollbarSpacing);
                                }
                                EditorGUI.EndDisabledGroup();
                            }
                            GUILayout.EndHorizontal();
                        }
                    }

                    GUILayout.Space(10);
                    EditorGUI.indentLevel = 0;
                    GUILayout.Label("Vertical Scrollbar");
                    EditorGUI.indentLevel = 1;

                    GUILayout.BeginHorizontal();
                    {
                        values.showVerticalScrollbarEnabled = (bool)EditorGUILayout.Toggle(values.showVerticalScrollbarEnabled, GUILayout.MaxWidth(26));

                        EditorGUI.BeginDisabledGroup(!values.showVerticalScrollbarEnabled);
                        {
                            values.showVerticalScrollbar = (bool)EditorGUILayout.Toggle("Show Scrollbar:", values.showVerticalScrollbar);
                        }
                        EditorGUI.EndDisabledGroup();
                    }
                    GUILayout.EndHorizontal();

                    if (values.showVerticalScrollbar)
                    {
                        GUILayout.BeginHorizontal();
                        {
                            values.verticalScrollbarVisibilityEnabled = (bool)EditorGUILayout.Toggle(values.verticalScrollbarVisibilityEnabled, GUILayout.MaxWidth(26));

                            EditorGUI.BeginDisabledGroup(!values.verticalScrollbarVisibilityEnabled);
                            {
                                values.verticalScrollbarVisibility = (ScrollRect.ScrollbarVisibility)EditorGUILayout.EnumPopup("Visibility:", values.verticalScrollbarVisibility);
                            }
                            EditorGUI.EndDisabledGroup();
                        }
                        GUILayout.EndHorizontal();

                        if (values.verticalScrollbarVisibility == ScrollRect.ScrollbarVisibility.AutoHideAndExpandViewport)
                        {
                            GUILayout.BeginHorizontal();
                            {
                                values.verticalScrollbarSpacingEnabled = (bool)EditorGUILayout.Toggle(values.verticalScrollbarSpacingEnabled, GUILayout.MaxWidth(26));

                                EditorGUI.BeginDisabledGroup(!values.verticalScrollbarSpacingEnabled);
                                {
                                    values.verticalScrollbarSpacing = (float)EditorGUILayout.FloatField("Spacing:", values.verticalScrollbarSpacing);
                                }
                                EditorGUI.EndDisabledGroup();
                            }
                            GUILayout.EndHorizontal();
                        }
                    }


					// -------------------------------------------------- //
					// References
					// -------------------------------------------------- //
	                GUILayout.Space ( 10 );
	                EditorGUI.indentLevel = 0;
	                EditorGUILayout.LabelField("References");
	                EditorGUI.indentLevel = 1;
	                
	                GUILayout.BeginHorizontal ();
	                {
		                EditorGUILayout.LabelField("Content: ", GUILayout.Width(140));
		                if (GUILayout.Button((string.IsNullOrEmpty(componentValues.scrollRect.contentReference) ? "Null" : componentValues.scrollRect.contentReference), EditorHelper.dropdownSkinNormal, GUILayout.MinWidth(40)))
		                {
			                tempStyleComponent = componentValues;
			                ContentReferenceContext(style);
		                }
	                }
	                GUILayout.EndHorizontal ();
	                
	                GUILayout.BeginHorizontal ();
	                {
		                EditorGUILayout.LabelField("Viewpoint: ", GUILayout.Width(140));
		                if (GUILayout.Button((string.IsNullOrEmpty(componentValues.scrollRect.viewportReference) ? "Null" : componentValues.scrollRect.viewportReference), EditorHelper.dropdownSkinNormal, GUILayout.MinWidth(40)))
		                {
			                tempStyleComponent = componentValues;
			                ViewpointReferenceContext(style);
		                }
	                }
	                GUILayout.EndHorizontal ();
	                
	                GUILayout.BeginHorizontal ();
	                {
		                EditorGUILayout.LabelField("Horizonal Scrollbar: ", GUILayout.Width(140));
		                if (GUILayout.Button((string.IsNullOrEmpty(componentValues.scrollRect.horizonalScrollbarReference) ? "Null" : componentValues.scrollRect.horizonalScrollbarReference), EditorHelper.dropdownSkinNormal, GUILayout.MinWidth(40)))
		                {
			                tempStyleComponent = componentValues;
			                HorizonalScrollbarReferenceContext(style);
		                }
	                }
	                GUILayout.EndHorizontal ();
	                
	                GUILayout.BeginHorizontal ();
	                {
		                EditorGUILayout.LabelField("Vertical Scrollbar: ", GUILayout.Width(140));
		                if (GUILayout.Button((string.IsNullOrEmpty(componentValues.scrollRect.verticalScrollbarReference) ? "Null" : componentValues.scrollRect.verticalScrollbarReference), EditorHelper.dropdownSkinNormal, GUILayout.MinWidth(40)))
		                {
			                tempStyleComponent = componentValues;
			                VerticalScrollbarReferenceContext(style);
		                }
	                }
	                GUILayout.EndHorizontal ();

                }
                GUILayout.EndVertical();

                // -------------------------------------------------- //
                // Drop Area
                // -------------------------------------------------- //
                draggedObjects = new UnityEngine.Object[0];
                EditorHelper.DropArea(ref draggedObjects);

                if (draggedObjects.Length > 0)
                {
                    foreach (Object draggedObj in draggedObjects)
                    {
                        if (draggedObj is ScrollRect)
                        {
                            componentValues.scrollRect = ScrollRectHelper.SetValuesFromComponent((ScrollRect)draggedObj);
                        }
                        if (draggedObj is GameObject)
                        {
                            GameObject obj = (GameObject)draggedObj;

                            if (obj.GetComponent<ScrollRect>())
                                componentValues.scrollRect = ScrollRectHelper.SetValuesFromComponent(obj.GetComponent<ScrollRect>());
                        }
                    }
                }
            }
            GUILayout.EndHorizontal();
        }
    }
}





















