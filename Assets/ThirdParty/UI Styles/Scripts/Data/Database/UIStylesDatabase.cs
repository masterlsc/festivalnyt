using UnityEngine;
using System.Collections.Generic;

using System;
using System.IO;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace UIStyles
{
    public class UIStylesDatabase
    {
        public static StyleDataFile styleData;
        public static string stylesDataGUID
        {
            get
            {
#if UNITY_EDITOR
                return EditorPrefs.GetString("stylesDataGUID");
#else
				return string.Empty;
#endif
            }
            set
            {
#if UNITY_EDITOR
                EditorPrefs.SetString("stylesDataGUID", value);
#else
#endif
            }
        }

        public static PaletteDataFile paletteData;
        public static string paletteDataGUID
        {
            get
            {
#if UNITY_EDITOR
                return EditorPrefs.GetString("paletteDataGUID");
#else
				return string.Empty;
#endif
            }
            set
            {
#if UNITY_EDITOR
                EditorPrefs.SetString("paletteDataGUID", value);
#else
#endif
            }
        }

        /// <summary>
        /// Load
        /// </summary>
        public static void Load()
        {
#if UNITY_EDITOR
            // Styles
            string path = AssetDatabase.GUIDToAssetPath(stylesDataGUID);
            styleData = (StyleDataFile)AssetDatabase.LoadAssetAtPath(path, typeof(StyleDataFile));

            // Palette
            path = AssetDatabase.GUIDToAssetPath(paletteDataGUID);
            paletteData = (PaletteDataFile)AssetDatabase.LoadAssetAtPath(path, typeof(PaletteDataFile));
#else
			
#endif


            // Cache Style IDs
            if (styleData != null)
                styleData.CacheIDs();

            // Cache Color Ids
            if (paletteData != null)
                paletteData.CacheColor();
        }









        /// <summary>
        /// Save
        /// </summary>
        public static void Save()
        {
            if (styleData != null)
            {
#if UNITY_EDITOR
                EditorUtility.SetDirty(styleData);
                AssetDatabase.SaveAssets();
#else
#endif
            }

            if (paletteData != null)
            {
#if UNITY_EDITOR
                EditorUtility.SetDirty(paletteData);
                AssetDatabase.SaveAssets();
#else
#endif
            }
        }
    }
}






















