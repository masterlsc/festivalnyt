using UnityEngine;
using System.Collections;

namespace UIStyles
{
	public class UIStylesDocumentation 
	{
		public static string findByName = 
			"Find By Name"
			
			+ "\n\n" +
			"To assign an object to a style the objects name needs to contain the styles find by name wrapped in parenthesis, for example if your styles find by name is \"Title\" (without quotation marks) then the objects name needs to contain \"(Title)\" (without quotation marks)."

			+ "\n\n" +
			"One of the best and easiest ways to add the find by name to an object or many objects at once is to drag the objects into the find by names field, this will add the find by name to the objects name for you."
		
			+ "\n\n" +
			"Another way is to right click the fine by name and choose copy, this will copy the find by name with the parenthesis already added, you can then paste that into the objects name."
			
			+ "\n\n" +
			"Alternatively you can just write the find by name with parenthesis in the objects name.\n";
		
		
		public static string applyStyle = 
			"Apply Style"
			
			+ "\n\n" +
			"First make sure the objects name contains the styles find by name."
			
			+ "\n\n" +
			"The apply option can also be found by right clicking the styles drop down and choosing “Apply."
		
			+ "\n\n" +
			"All Resources"
			+ "\n" +
			"Will find all styles in the scene and all objects in the project folder, meaning all prefabs as well."
		
			+ "\n\n" +
			"Active In Scene"
			+ "\n" +
			"Will only find the objects that are enabled in the scene, it will not find prefabs in the project. \n";
	}
}



















