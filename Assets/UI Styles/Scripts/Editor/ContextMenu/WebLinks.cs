using UnityEditor;
using UnityEngine;

namespace UIStyles
{
    public class WebLinks : Editor
    {
        // -------------------------------------------------- //
        // Documentation
        // -------------------------------------------------- //
        [MenuItem("Window/UI Styles/Web Links/Documentation", false, 1000)]
        private static void Documentation()
        {
            Application.OpenURL("http://uistyles.training");
        }

        // -------------------------------------------------- //
        // Changelog
        // -------------------------------------------------- //
        [MenuItem("Window/UI Styles/Web Links/Changelog", false, 2000)]
        private static void Changelog()
        {
            Application.OpenURL("http://uistyles.training/documentation/changelog/");
        }
	    
	     // -------------------------------------------------- //
        // Changelog
        // -------------------------------------------------- //
	    [MenuItem("Window/UI Styles/Web Links/Support", false, 3000)]
	    private static void Support()
	    {
		    Application.OpenURL("http://uistyles.training/support/");
	    }


        public static void RuntimeManager()
        {
            Application.OpenURL("http://messyentertainment.com/developer_tools/ui_styles_documentation/runtime_manager/");
        }
    }
}

















