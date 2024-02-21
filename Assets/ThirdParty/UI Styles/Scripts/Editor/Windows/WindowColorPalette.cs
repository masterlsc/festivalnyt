using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.IO;

namespace UIStyles
{
	public class WindowColorPalette : EditorWindow 
	{
		private bool debug = false;
		private bool debug_ShowColorIds;
		
		private static WindowColorPalette window;
		
		private static bool highlightRenameField = false;
		
		private bool needToSave = false;
		
		private static bool isWindowUtility;
		private static bool closeWhenGotColor = true;
		
		public enum ReturnToWindow {None, UIStyles, Inspector}
		private static ReturnToWindow returnToWindow;
			
		/// <summary>
		/// Scroll position
		/// </summary>
		private static Vector2 scrollPosition = Vector2.zero;
		
		/// <summary>
		/// Search string
		/// </summary>
		private string searchString = string.Empty;
		
		/// <summary>
		/// Reference to the data
		/// </summary>
		private static PaletteDataFile data
		{ 
			get 
			{ 
				if (UIStylesDatabase.paletteData == null)
					return null;
					
				return UIStylesDatabase.paletteData; 
			} 
		}
		
		/// <summary>
		/// Category values
		/// </summary>
		private bool addCategory = false;
		private string addCategoryName;
		private bool renameCurrentCategory = false;
		private string newCategoryName = string.Empty;
		
		/// <summary>
		/// Values stored used for the context menu
		/// </summary>
		private Palette paletteValues;
		private int currentIndex;
		
		/// <summary>
		/// Action for sending back the chosen color
		/// </summary>
		private static Action<Color, String> onGetColor;
		
		/// <summary>
		/// Shows the window
		/// </summary>
		[MenuItem ( "Window/UI Styles/Window/Color Palette", false, 1)]
		private static void OpenWindow ()
		{
			ShowWindow (true);
		}
		public static void ShowWindow (bool utility)
		{
			isWindowUtility = utility;
							
			window = (WindowColorPalette)EditorWindow.GetWindow ( typeof( WindowColorPalette ), utility, "Color Palette" );
			
			window.minSize = new Vector2 ( 411, 360f );
			#if !PRE_UNITY_5
			window.titleContent.text = "Color Palette";
			#endif
		}
		
		/// <summary>
		/// Shows the editor validator
		/// </summary>
		[MenuItem ( "Window/UI Styles/Color Palette Window", true )]
		public static bool ShowEditorValidator ()
		{
			return true;
		}
		
		/// <summary>
		/// Close the window
		/// </summary>
		public static void CloseWindow ()
		{
			window = (WindowColorPalette)EditorWindow.GetWindow ( typeof( WindowColorPalette ) );
			
			window.Close ();
			onGetColor = null;
		}
		
		/// <summary>
		/// Is the window doced
		/// </summary>
		/// <returns></returns>
		private static bool isWindowDocked ()
		{
			if (window == null)
				return false;
			
			BindingFlags fullBinding = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;
			MethodInfo isDockedMethod = typeof( EditorWindow ).GetProperty( "docked", fullBinding ).GetGetMethod( true );
			
			bool value = ( bool ) isDockedMethod.Invoke(window, null) == true;
			
			if (value)
				isWindowUtility = false;
			
			return value;
		}
		
		
		/// <summary>
		/// Enable
		/// </summary>
		private void OnEnable ()
		{			
			if (UIStylesDatabase.paletteData == null)
				UIStylesDatabase.Load();
		}
		
		private void OnDisable ()
		{
			
		}
		
		/// <summary>
		/// Use the window to get a coloe
		/// </summary>
		/// <param name="onComplete"></param>
		public static void GetColor (ReturnToWindow returnTo, Action <Color, string> onComplete)
		{
			returnToWindow = returnTo;
			onGetColor = onComplete;
			ShowWindow (true);
		}
		
		/// <summary>
		/// Close the window after picking a color if the window was open
		/// </summary>
		public static void GotColor (Color col, string id)
		{
			if (onGetColor != null)
			{
				onGetColor(col, id);
				
				if (closeWhenGotColor && isWindowUtility && !isWindowDocked())
					CloseWindow();
				
				if (returnToWindow == ReturnToWindow.Inspector)
				{
					EditorWindow w = EditorWindow.GetWindow(Type.GetType("UnityEditor.InspectorWindow,UnityEditor"));
					w.Show();
				}
				else if (returnToWindow == ReturnToWindow.UIStyles)
				{
					WindowStyles.ShowWindow(true);
				}
				
				onGetColor = null;
			}
		}
		
		
		/// <summary>
		/// Load new settings
		/// </summary>
		private void Load (object obj)
		{
			string guid = (string)obj;
			
			if (UIStylesDatabase.paletteDataGUID == guid)
			{
				string path = AssetDatabase.GUIDToAssetPath(guid);
				PaletteDataFile paletteData = (PaletteDataFile)AssetDatabase.LoadAssetAtPath(path, typeof(PaletteDataFile));
				EditorHelper.SelectObjectInProject ( paletteData, false, true );
			}
			
			else UIStylesDatabase.paletteDataGUID = guid;
			
			UIStylesDatabase.Load();
			UIStylesDatabase.Save();
		}
		
		/// <summary>
		/// Create data file
		/// </summary>
		private void Create (object path)
		{
			StyleHelper.CreatePaletteDataFile((string)path, (PaletteDataFile data) => {
				
				// Ping
				EditorHelper.SelectObjectInProject ( data, false, true );
				
				// Get guid
				string guid = AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(data));
				
				// Load
				Load (guid);
			} );
		}
		
		
		private void MenuContent ()
		{
			GUI.FocusControl ( null );
			GenericMenu menu = new GenericMenu ();
			
			// -------------------------------------------------- //
			// Preferences
			// -------------------------------------------------- //
			menu.AddItem ( new GUIContent ( "Preferences" ), false, delegate 
			{
				WindowPreferences.OpenPreferences();
			}, null );
			
			menu.AddSeparator ( "" );
			
			// -------------------------------------------------- //
			// Save
			// -------------------------------------------------- //
			if (UIStylesDatabase.paletteData != null)
				menu.AddItem ( new GUIContent ( "Save" ), false, delegate {
					UIStylesDatabase.Save();
				}, null );
			
			// -------------------------------------------------- //
			// Load
			// -------------------------------------------------- //
			string[] objs = AssetDatabase.FindAssets("t:PaletteDataFile");
			for (int i = 0; i < objs.Length; i++) 
			{								
				string guid = objs[i];
				string path = AssetDatabase.GUIDToAssetPath(guid);
				string pathFormat = path.Replace("/", "\\").Replace(".asset", "");
				
				object obj = AssetDatabase.LoadAssetAtPath(path, typeof(PaletteDataFile));
				
				if (obj != null)
					menu.AddItem ( new GUIContent ("Load/" + pathFormat), UIStylesDatabase.paletteDataGUID == guid, Load, guid );
			}
			
			// -------------------------------------------------- //
			// Create
			// -------------------------------------------------- //
			menu.AddItem ( new GUIContent ( "Create" ), false, Create, null );
			
			menu.AddSeparator ( "" );
			
			// -------------------------------------------------- //
			// Runtime
			// -------------------------------------------------- //
			if (UIStylesDatabase.paletteData != null)
			{
				menu.AddItem ( new GUIContent ( "Rintime/Create Class (Obsolete)" ), false, delegate {
					CreateRuntimeClass();
				}, null );
			}
			
			menu.AddSeparator ( "" );
			
			if (UIStylesDatabase.paletteData != null)
			{
				menu.AddSeparator ( "" );
				
				// -------------------------------------------------- //
				// Dockable
				// -------------------------------------------------- //
				menu.AddItem ( new GUIContent ( "Window/Dockable" ), !isWindowUtility, delegate {
					CloseWindow ();
					ShowWindow (!isWindowUtility);
				}, null );
								
				// -------------------------------------------------- //
				// Close Window
				// -------------------------------------------------- //
				menu.AddItem ( new GUIContent ( "Window/Close Window" ), false, delegate {
					CloseWindow ();
				}, null );
			}
			
			
			// -------------------------------------------------- //
			// debug
			// -------------------------------------------------- //
			if (debug)
			{
				menu.AddItem ( new GUIContent ( "Debug/Create new color ids" ), false, delegate {
					
					foreach (Palette v in data.palettes)
					{
						foreach (PaletteColor c in v.colors)
						{
							c.guid = System.Guid.NewGuid().ToString();
						}
					}
				}, null );
				
				menu.AddItem ( new GUIContent ( "Debug/Show Color Ids" ), debug_ShowColorIds, delegate {
					debug_ShowColorIds = !debug_ShowColorIds;
				}, null );
			}
			
			menu.ShowAsContext ();
		}
		
		/// <summary>
		/// Create the category dropdown
		/// </summary>
		private void CategoryDropdown ()
		{
			GenericMenu menu = new GenericMenu();
			
			// -------------------------------------------------- //
			// Categories
			// -------------------------------------------------- //
			data.categories.Sort();
			foreach ( string category in data.categories )
			{
				menu.AddItem ( new GUIContent ( category ), data.currentCategory == category, NewCategory, category );
			}

			menu.ShowAsContext();
		}
		
		/// <summary>
		/// Set to a new category
		/// </summary>
		private void NewCategory (object obj)
		{
			data.currentCategory = (string)obj;
			UIStylesDatabase.Save ();
		}
		

		/// <summary>
		/// Categories context menu
		/// </summary>
		private void CategoryContext ()
		{
			GUI.FocusControl(null);
			GenericMenu menu = new GenericMenu();
			
			// -------------------------------------------------- //
			// Rename
			// -------------------------------------------------- //
			menu.AddItem(new GUIContent("Rename"), false, delegate {
				newCategoryName = data.currentCategory;
				renameCurrentCategory = true;
				highlightRenameField = true;
			}, null);
			
			// -------------------------------------------------- //
			// Delete
			// -------------------------------------------------- //
			menu.AddItem(new GUIContent("Delete"), false, delegate {
				
				if (EditorUtility.DisplayDialog("Warning", "Are you sure you want to delete this category?", "Yes Delete", "No Cancel"))
				{
					List<Palette> removes = new List<Palette>();
					for (int i = 0; i < data.palettes.Count; i++) 
						if (data.palettes[i].category == data.currentCategory)
							removes.Add(data.palettes[i]);
					
					for ( int i = 0; i < removes.Count; i++ )
						data.palettes.Remove ( removes[i] );
					
					data.categories.Remove(data.currentCategory);
					
					if (data.categories.Count > 0)
						data.currentCategory = data.categories[0];
					
					else 
					{
						data.currentCategory = string.Empty;
						UIStylesDatabase.Save();
					}
				}
			}, null);
			menu.ShowAsContext();
		}
		
		/// <summary>
		/// Palettes context menu
		/// </summary>
		private void PaletteContext (int i)
		{
			currentIndex = i;
			GUI.FocusControl(null);
			GenericMenu menu = new GenericMenu();
			
			// -------------------------------------------------- //
			// Move up
			// -------------------------------------------------- //
			bool canMoveUp = false;
			int loop = i;
			while (loop > 0)
			{
				loop --;
				
				if (data.palettes [ loop ].category == data.currentCategory)
				{
					canMoveUp = true;
					break;
				}
			}
			
			if (canMoveUp)
				menu.AddItem(new GUIContent("Move Up"), false, delegate {
					if (i > 0)
					{
						int p = i -1;
						while (p > 0 && data.palettes [ p ].category != data.currentCategory)
							p --;
						
						Palette a = data.palettes [ i ];
						Palette b = data.palettes [ p ];
						data.palettes [ i ] = b;
						data.palettes [ p ] = a;
						Repaint();
						UIStylesDatabase.Save(); 
					}
				}, i);
			
			else menu.AddDisabledItem(new GUIContent("Move Up"));
			
			// -------------------------------------------------- //
			// Move down
			// -------------------------------------------------- //
			bool canMoveDown = false;
			loop = i;
			while (loop < data.palettes.Count -1)
			{
				loop ++;
				
				if (data.palettes [ loop ].category == data.currentCategory)
				{
					canMoveDown = true;
					break;
				}
			}
			
			if (canMoveDown)
				menu.AddItem(new GUIContent("Move Down"), false, delegate {
					if (i < data.palettes.Count -1)
					{
						int p = i +1;
						while (p < data.palettes.Count -1 && data.palettes [ p ].category != data.currentCategory)
							p ++;
						
						Palette a = data.palettes [ i ];
						Palette b = data.palettes [ p ];
						data.palettes [ i ] = b;
						data.palettes [ p ] = a;
						Repaint();
						UIStylesDatabase.Save();
					}
				}, i);
			
			else menu.AddDisabledItem(new GUIContent("Move Down"));
			
			menu.AddSeparator("");
			
			// -------------------------------------------------- //
			// Move to Category
			// -------------------------------------------------- //
			data.categories.Sort();
			foreach (string cat in data.categories)
			{
				if (data.currentCategory != cat)
					menu.AddItem(new GUIContent("Move to Category/" + cat), false, MoveCategory, cat);
				
				else menu.AddDisabledItem(new GUIContent("Move to Category/" + cat));
			}
			
			menu.AddSeparator("");
			
			// -------------------------------------------------- //
			// Rename
			// -------------------------------------------------- //
			menu.AddItem(new GUIContent("Rename"), false, delegate {
				data.palettes[i].rename = true;
				UIStylesDatabase.Save();
				highlightRenameField = true;
			}, i);
			
			// -------------------------------------------------- //
			// Duplicate
			// -------------------------------------------------- //
			menu.AddItem(new GUIContent("Duplicate"), false, delegate {
				Palette p = new Palette(UIStylesDatabase.paletteData, data.currentCategory, data.palettes[currentIndex].name + " Copy");
				foreach (PaletteColor copyValues in paletteValues.colors)
					p.AddColor(UIStylesDatabase.paletteData, copyValues.name, copyValues.color);
				
				p.rename = true;
				UIStylesDatabase.Save();
				highlightRenameField = true;
			}, i);
			
			// -------------------------------------------------- //
			// Delete
			// -------------------------------------------------- //
			menu.AddItem(new GUIContent("Delete"), false, delegate {
				data.palettes.RemoveAt(i);
				UIStylesDatabase.Save();
			}, i);
			
			menu.ShowAsContext();
		}
		
		/// <summary>
		/// Move to Category
		/// </summary>
		private void MoveCategory (object obj)
		{
			data.palettes[currentIndex].category = (string)obj;
		}
		

		
		
		/// <summary>
		/// Palette values context menu
		/// </summary>
		private void PaletteValuesContext (PaletteColor values, bool onColor)
		{
			GUI.FocusControl(null);
			GenericMenu menu = new GenericMenu();
			
			// -------------------------------------------------- //
			// Move Left / Move Up
			// -------------------------------------------------- //
			if (currentIndex > 0)
				menu.AddItem(new GUIContent(onColor ? "Move Left" : "Move Up"), false, delegate {
					if (currentIndex > 0)
					{
						PaletteColor a = paletteValues.colors [ currentIndex ];
						PaletteColor b = paletteValues.colors [ currentIndex - 1 ];
						paletteValues.colors [ currentIndex ] = b;
						paletteValues.colors [ currentIndex - 1 ] = a;
						paletteValues.colors[ currentIndex ].moveUp = false;
						Repaint();
						UIStylesDatabase.Save(); 
					}
				}, values);
			
			else menu.AddDisabledItem(new GUIContent(onColor ? "Move Left": "Move Up"));
			
			// -------------------------------------------------- //
			// Move Right / Move Down
			// -------------------------------------------------- //
			if (currentIndex < paletteValues.colors.Count -1)
				menu.AddItem(new GUIContent(onColor ? "Move Right" : "Move Down"), false, delegate {
					if (currentIndex < paletteValues.colors.Count -1)
					{
						PaletteColor a = paletteValues.colors [ currentIndex ];
						PaletteColor b = paletteValues.colors [ currentIndex + 1 ];
						paletteValues.colors [ currentIndex ] = b;
						paletteValues.colors [ currentIndex + 1 ] = a;
						paletteValues.colors[ currentIndex ].moveDown = false;
						Repaint();
						UIStylesDatabase.Save();
					}
				}, values);
			
			else menu.AddDisabledItem(new GUIContent(onColor ? "Move Right" : "Move Down"));
			
			menu.AddSeparator("");
			
			// -------------------------------------------------- //
			// Rename
			// -------------------------------------------------- //
			if (!onColor)
				menu.AddItem(new GUIContent("Rename"), false, delegate {
					values.rename = true;
					highlightRenameField = true;
				}, null);
			
			// -------------------------------------------------- //
			// Delete
			// -------------------------------------------------- //
			menu.AddItem(new GUIContent("Delete"), false, delegate {
				paletteValues.colors.RemoveAt(currentIndex);
				UIStylesDatabase.Save();
			}, null);
			
			menu.ShowAsContext();
		}
		
		
		/// <summary>
		/// New Palette Context menu
		/// </summary>
		private void NewPaletteContext ()
		{
			GUI.FocusControl(null);
			GenericMenu menu = new GenericMenu();
			
			// -------------------------------------------------- //
			// Add New
			// -------------------------------------------------- //
			menu.AddItem(new GUIContent("Add New"), false, delegate {
				Palette v = new Palette(UIStylesDatabase.paletteData, data.currentCategory, "New Palette");
				v.rename = true;
				highlightRenameField = false;
				UIStylesDatabase.Save();
			}, null);
			
			menu.ShowAsContext();
		}
		
		
		
		/// <summary>
		/// Creates the runtime class.
		/// </summary>
		private static void CreateRuntimeClass ()
		{
			string path = EditorUtility.OpenFolderPanel ( "Save To", "Assets", "NewClass" );
			
			if ( string.IsNullOrEmpty ( path ) )
				return;
			
			path += "/ColorPalette.cs";
			
			StreamWriter file = new StreamWriter ( path );
			file.WriteLine ( "using UnityEngine;" );
			file.WriteLine ( "" );
			file.WriteLine ( "public class ColorPalette" );
			file.WriteLine ( "{" );
			
			foreach ( string category in data.categories )
			{
				string className = StringFormats.StringToTitleCase ( category ).Replace ( " ", "" );
				string varName = StringFormats.StringToFirstLower ( className );
				
				file.WriteLine ( "" );
				file.WriteLine ( "    // -------------------------------------------------- //" );
				file.WriteLine ( "    // " + className);
				file.WriteLine ( "    // -------------------------------------------------- //" );
				file.WriteLine ( "" );
				file.WriteLine ( "    public "  + className + " " + varName + ";");
				file.WriteLine ( "" );
				file.WriteLine ( "    [System.Serializable]" );
				file.WriteLine ( "    public class " + className );
				file.WriteLine ( "    {" );
				
				foreach ( Palette value in data.palettes )
				{
					if (value.category == category)
					{
						className = StringFormats.StringToTitleCase ( value.name ).Replace ( " ", "" );
						varName = StringFormats.StringToFirstLower ( className );
						
						//file.WriteLine ( "" );
						//file.WriteLine ( "        // ------------------------------ //" );
						file.WriteLine ( "        // " + className);
						//file.WriteLine ( "        // ------------------------------ //" );
						//file.WriteLine ( "" );
						file.WriteLine ( "        public "  + className + " " + varName + ";");
						file.WriteLine ( "" );
						file.WriteLine ( "        [System.Serializable]" );
						file.WriteLine ( "        public class " + className );
						file.WriteLine ( "        {" );
						
						foreach ( PaletteColor pValue in value.colors )
						{
							file.WriteLine ( "	        public Color " + ( char.IsNumber ( pValue.name [ 0 ] ) ? "_" : "" ) + StringFormats.StringToFirstLower ( StringFormats.StringToTitleCase ( pValue.name ) ).Replace ( " ", "" ) + " = " + "new Color(" + pValue.color.r + "f," + pValue.color.g + "f," + pValue.color.b + "f," + pValue.color.a + "f);" );
						}
						
						file.WriteLine ( "        }" );
					}
				}
				
				file.WriteLine ( "    }" );
			}
			
			file.WriteLine ( "}" );
			file.Close ();
			AssetDatabase.Refresh ();
		}
		
		
		private void CloseAllPaletts ()
		{
			for (int i = 0; i < data.palettes.Count; i++) 
				if (data.palettes[i].category == data.currentCategory)
					data.palettes[i].open = false;
		}
		
		
		
 
		/// <summary>
		/// Draw the window
		/// </summary>
		private void OnGUI ()
		{
			DrawHeader ();
			
			if (UIStylesDatabase.paletteData != null)
			{
				DrawPalettes ();
				
				if (isWindowUtility && !isWindowDocked())
					DrawFooter ();
			}
		}
		
		
		private void DrawFooter ()
		{
			GUILayout.BeginVertical(EditorHelper.toolbarSkin);
			{
				GUILayout.Space(-0.0f);
				GUILayout.BeginHorizontal();
				{
					closeWhenGotColor = EditorGUILayout.Toggle(closeWhenGotColor, EditorHelper.ToggleSkin, GUILayout.Width(16));
					EditorGUILayout.LabelField("Close");
				}
				GUILayout.EndHorizontal();
			}
			GUILayout.EndVertical();
		}
		
		
		/// <summary>
		/// Draw Header
		/// </summary>
		private void DrawHeader ()
		{
			GUILayout.BeginHorizontal(EditorHelper.toolbarSkin);
			{
				if (UIStylesDatabase.paletteData != null && GUILayout.Button("+", EditorHelper.buttonSkin, GUILayout.Width(30)))
				{
					GUI.FocusControl(null);
					addCategoryName = "New Category";
					addCategory = true;
				}
				
				if (UIStylesDatabase.paletteData != null)
				{
					if (UIStylesDatabase.paletteData != null && renameCurrentCategory)
					{
						GUI.SetNextControlName("Rename Category Field");
						
						newCategoryName = EditorGUILayout.TextField(newCategoryName);
						
						if (highlightRenameField)
						{
							GUI.FocusControl("Rename Category Field");
							highlightRenameField = false;
						}
						
						if (GUILayout.Button("Done", EditorHelper.buttonSkin, GUILayout.Width(80)))
						{
							for ( int i = 0; i < data.palettes.Count; i++ )
							{
								if ( data.palettes[i].category == data.currentCategory )
								{
									data.palettes[i].category = newCategoryName;
								}
							}
							
							// Find and rename the currentCategory
							for ( int i = 0; i < data.categories.Count; i++ )
							{
								if ( data.categories[i] == data.currentCategory )
								{
									data.categories[i] = newCategoryName;
									break;
								}
							}
							
							data.currentCategory = newCategoryName;
							
							renameCurrentCategory = false;
							
							UIStylesDatabase.Save();
						}
					}
					else if ( GUILayout.Button(string.IsNullOrEmpty(data.currentCategory) ? "No Categories!" : data.currentCategory, EditorHelper.popUpSkin))
					{
						if (Event.current.button == 0)
						{
							CategoryDropdown ();
						}
						else if (Event.current.button == 1)
						{
							CategoryContext ();
						}
					}
				}
				
				GUILayout.Space(5);
				
				int w = GUI.GetNameOfFocusedControl() == "SeachField" ? 180 : 30;
				GUI.SetNextControlName("SeachField");
				searchString = GUILayout.TextField ( searchString, EditorHelper.SeachFieldSkin, GUILayout.MinWidth ( w ) );
				
				if ( GUILayout.Button ( "", EditorHelper.SeachFieldCancelSkin ) )
				{
					// Remove focus if cleared
					searchString = string.Empty;
					GUI.FocusControl ( null );
				}
				
				GUILayout.Space ( 5 );
				
				if ( GUILayout.Button ("Menu", EditorHelper.dropdownSkin, GUILayout.Width ( 50 ) ) )
				{
					MenuContent ();
				}
			}
			GUILayout.EndHorizontal();
			
			if (addCategory)
			{
				GUILayout.BeginHorizontal(EditorHelper.toolbarSkin);
				{
					addCategoryName = EditorGUILayout.TextField(addCategoryName);
					
					if (GUILayout.Button("Add", EditorHelper.buttonSkin, GUILayout.Width(100)))
					{
						if (data.categories.Contains(addCategoryName))
						{
							EditorUtility.DisplayDialog("Warning", "Category name already exists please choose another", "OK");
						}
						else
						{
							data.categories.Add(addCategoryName);
							data.currentCategory = addCategoryName;
							addCategory = false;
							UIStylesDatabase.Save();
						}
					}
					if (GUILayout.Button("X", EditorHelper.buttonSkin, GUILayout.Width(30)))
					{
						addCategory = false;
					}
				}
				GUILayout.EndHorizontal();
				
				EditorGUILayout.HelpBox("To add a new category enter its name above and click add", MessageType.Info);
				
				GUILayout.Space(10);
			}
			
			if (UIStylesDatabase.paletteData == null)
			{
				EditorGUILayout.HelpBox ( "Create some settings from the top right \"Menu\" button", MessageType.Info );
			}
		}
				
		/// <summary>
		/// Draw Palettes
		/// </summary>
		private void DrawPalettes ()
		{
			GUILayout.Space(5);
			
			if (data.categories.Count > 0)
			{
				bool addButton = false;
				EditorHelper.StandardSeparatorCentreTitle (10, "Add Palette", "", ref addButton);
				
				if (addButton)
				{
					NewPaletteContext ();
				}
			}
			else
			{
				if (!addCategory)
					EditorGUILayout.HelpBox ( "No Categories! Create Categories from the top left \"+\" button", MessageType.Info );
			}
			
			GUILayout.Space ( -10 );
			
			scrollPosition = GUILayout.BeginScrollView ( scrollPosition, false, false ); 
			{
				// -------------------------------------------------- //
				// Loop palettes
				// -------------------------------------------------- //
				for ( int p = 0; p < data.palettes.Count; p++ )
				{
					// Get the name as lower case, this id better for search
					string nameToLower = data.palettes[p].name.ToLower();
					if (data.palettes[p].category == data.currentCategory && (string.IsNullOrEmpty(searchString) || nameToLower.Contains(searchString.ToLower())))
					{
						GUI.color = data.palettes[p].open ? EditorHelper.highlightedBackgroundColor : Color.white;
						GUILayout.BeginVertical(EditorHelper.StandardPanel(6));
						{
							GUI.color = Color.white;
							
							GUILayout.BeginHorizontal ();
							{
								if (data.palettes[p].rename)
								{
									GUI.SetNextControlName("Rename Palette Field");
									
									data.palettes[p].name = EditorGUILayout.TextField(data.palettes[p].name);
									
									if (highlightRenameField)
									{
										GUI.FocusControl("Rename Palette Field");
										highlightRenameField = false;
									}
									
									if (GUILayout.Button("Done", EditorHelper.buttonSkin, GUILayout.Width(100)))
									{
										data.palettes[p].rename = false;
									}
									GUILayout.Space(5);
								}
								else
								{
									if (GUILayout.Button(" ", EditorHelper.buttonSkin))
									{
										if (Event.current.button == 0)
										{
											// Open/Close
											if (!data.palettes[p].open)
												CloseAllPaletts ();
											data.palettes[p].open = !data.palettes[p].open;
										}
										else if (Event.current.button == 1)
										{
											paletteValues = data.palettes[p];
											PaletteContext(p);
										}
									}
									
									Rect lastRect = GUILayoutUtility.GetLastRect();
									
									GUI.backgroundColor = Color.clear;
									GUI.Label(lastRect, data.palettes[p].name, EditorHelper.dropdownSkin);
									GUI.backgroundColor = Color.white;
									
									Vector2 textSize = GUI.skin.label.CalcSize(new GUIContent("ID: " + data.palettes[p].id));
									
									Rect rect = new Rect(lastRect.x + (lastRect.width - (textSize.x + 5)), lastRect.y, lastRect.width, lastRect.height );

									GUI.backgroundColor = Color.clear;
									GUI.Label(rect, "ID: " + data.palettes[p].id, EditorHelper.dropdownSkin);
									GUI.backgroundColor = Color.white;
								}
							}
							GUILayout.EndHorizontal ();
							
							GUILayout.Space(5);
							
							// -------------------------------------------------- //
							// Draw colours
							// -------------------------------------------------- //
							GUILayout.BeginHorizontal ();
							{
								for ( int c = 0; c < data.palettes[p].colors.Count; c++ )
								{
									Event currentEvent = Event.current;
									Rect contextRect;
									
									GUILayout.BeginVertical ();
									{
										GUILayout.BeginHorizontal ();
										{
											contextRect = GUILayoutUtility.GetRect ( 20, 20 );
											EditorGUI.DrawRect ( contextRect, data.palettes[p].colors [ c ].color );
											GUILayout.Space ( -1 ); // Move back to hide gap
										}
										GUILayout.EndHorizontal ();
										
										float w = GUILayoutUtility.GetLastRect ().width;
										EditorGUILayout.LabelField ( data.palettes[p].colors [ c ].name, EditorHelper.Caption ( TextAnchor.MiddleCenter, Color.gray ), GUILayout.MinWidth ( w ) );
										
									}
									GUILayout.EndVertical ();
									
									Vector2 mousePos = currentEvent.mousePosition;
									
									if (contextRect.Contains(mousePos) && Event.current.button == 0 && Event.current.type == EventType.MouseUp )
									{
										GotColor (data.palettes[p].colors [ c ].color, data.palettes[p].colors [ c ].guid);
									}
									else if (contextRect.Contains(mousePos) && Event.current.button == 1 && Event.current.type == EventType.MouseUp )
									{
										
										paletteValues = data.palettes[p];
										currentIndex = c;
										
										PaletteValuesContext (data.palettes[p].colors[c], true);
									}
								}
							}
							GUILayout.EndHorizontal ();
							
							// -------------------------------------------------- //
							// Draw options
							// -------------------------------------------------- //
							
							if (data.palettes[p].open)
							{
								if (Event.current.type == EventType.Repaint)
									Repaint();
								
								GUILayout.BeginVertical (EditorHelper.StandardPanel(6));
								{
									GUILayout.BeginHorizontal ();
									{
										EditorGUILayout.LabelField ( "ID", GUILayout.MinWidth ( 10 ) );
										
										EditorGUILayout.LabelField ( "Name", GUILayout.MinWidth ( 10 ) );
										
										EditorGUILayout.LabelField ( "Hex", GUILayout.Width ( 80 ), GUILayout.MaxHeight ( 20 ) );
										
										EditorGUILayout.LabelField ( "Color", GUILayout.MinWidth ( 50 ) );
									}
									GUILayout.EndHorizontal ();	
									
									EditorHelper.StandardSeparator(0, "", "");
									
									GUILayout.Space(5);
									
									for ( int c = 0; c < data.palettes[p].colors.Count; c++ )
									{											
										GUILayout.BeginHorizontal ();
										{
											EditorGUILayout.LabelField ( data.palettes[p].colors[ c ].id.ToString(), GUILayout.MinWidth ( 10 ) );
											
											if (data.palettes[p].colors[ c ].rename)
											{
												GUI.SetNextControlName("Rename Color Field");
												
												data.palettes[p].colors[ c ].name = EditorGUILayout.TextField ( data.palettes[p].colors[ c ].name, GUILayout.MinWidth ( 10 ) );
												
												if (highlightRenameField)
												{
													GUI.FocusControl("Rename Color Field");
													highlightRenameField = false;
												}
											}
											
											else 
											{
												EditorGUILayout.LabelField ( data.palettes[p].colors[ c ].name, GUILayout.MinWidth ( 10 ) );												
											}
											
											if (debug && debug_ShowColorIds)
												EditorGUILayout.LabelField ( data.palettes[p].colors[ c ].guid, GUILayout.MinWidth ( 10 ) );
											
											if (data.palettes[p].colors[ c ].rename)
											{
												if (GUILayout.Button("Done", EditorHelper.buttonSkin, GUILayout.Width ( 80 )))
													data.palettes[p].colors[ c ].rename = false;
											}
											else EditorGUILayout.SelectableLabel ( StringFormats.HexColorToString ( data.palettes[p].colors [ c ].color, true ), GUILayout.Width ( 80 ), GUILayout.MaxHeight ( 20 ) );
											
											data.palettes[p].colors [ c ].color = (Color)EditorGUILayout.ColorField ( data.palettes[p].colors [ c ].color, GUILayout.MinWidth ( 50 ) );
										}
										GUILayout.EndHorizontal ();	
										
										Event currentEvent = Event.current;
										Rect contextRect;
										
										contextRect =  GUILayoutUtility.GetLastRect ( );
										EditorGUI.DrawRect ( contextRect, Color.clear );
										
										Vector2 mousePos = currentEvent.mousePosition;
										
										if (contextRect.Contains(mousePos))
										{
											EditorHelper.HighlightRect(new Rect(contextRect.x, contextRect.y -2, contextRect.width, contextRect.height));
											
											if (Event.current.button == 1 && Event.current.type == EventType.MouseUp)
											{
												paletteValues = data.palettes[p];
												currentIndex = c;
												
												PaletteValuesContext (data.palettes[p].colors[c], false);
											}
										}
									}
									
									GUILayout.Space(10);
									if (GUILayout.Button("Add", EditorHelper.buttonSkin))
									{
										data.palettes[p].AddColor(UIStylesDatabase.paletteData, "New Color", Color.black);
									}
								}
								GUILayout.EndVertical ();
							}
						}
						GUILayout.EndVertical ();
					}
				}
			}
			GUILayout.EndScrollView ();
			
			// Save
			if (GUI.changed)
				needToSave = true;
			
			if (needToSave && Event.current.type == EventType.MouseUp)
			{
				needToSave = false;
				UIStylesDatabase.Save ();
			}
		}
		
		
		
		
		
		
		
		public static void DrawColorPicker (ref string colorID, ref Color colorField, string colorFieldName)
		{
			if (string.IsNullOrEmpty(colorID))
			{
				colorField = (Color)EditorGUILayout.ColorField ( colorFieldName + ":", colorField );
			}
			else
			{
				bool hasLink = UIStylesDatabase.paletteData.GetColorByGUID(colorID) != null;
				
				if (!hasLink)
				{
					GUI.color = Color.red;
					EditorGUILayout.LabelField(new GUIContent("Link Not Found", "  "), GUILayout.Width(150));
					GUI.color = Color.white;
					BreakLink (ref colorID);
					
					GUILayout.Box ( "", EditorHelper.ErrorSymbol, GUILayout.Height(16));
					BreakLink (ref colorID);
				}
				else
				{
					colorField = UIStylesDatabase.paletteData.GetColorByGUID(colorID).color;
					
					EditorGUILayout.LabelField(new GUIContent("Linked To Palette", "Category:\n" + UIStylesDatabase.paletteData.GetColorByGUID(colorID).category + "   \n\nPalette:\n" + UIStylesDatabase.paletteData.GetColorByGUID(colorID).paletteName + "   \n\nColor:\n" + UIStylesDatabase.paletteData.GetColorByGUID(colorID).name + "   "), GUILayout.Width(150));
					EditorGUI.BeginDisabledGroup ( true );
					{
						GUILayout.Space ( -152 );
						colorField = (Color)EditorGUILayout.ColorField ( " ", colorField );
					}
					EditorGUI.EndDisabledGroup ();
					
					BreakLink (ref colorID);
				}
			}
		}
		
		public static void BreakLink (ref string colorID)
		{
			Event currentEvent = Event.current;
			Rect contextRect;
			
			contextRect =  GUILayoutUtility.GetLastRect ( );
			EditorGUI.DrawRect ( contextRect, Color.clear );
			
			Vector2 mousePos = currentEvent.mousePosition;
			
			if (contextRect.Contains(mousePos) && Event.current.button == 0 && Event.current.type == EventType.MouseUp )
			{
				if (EditorUtility.DisplayDialog("Linked to Palette", "This color is linked to a color from the color palette", "Break link", "Keep Link"))
				{
					colorID = string.Empty;
				}
			}
		}
	}
}



















