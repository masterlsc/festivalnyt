using UnityEngine;
using UnityEngine.UI;

namespace UIStyles
{
	public class TransitionHelper 
	{
		/// <summary>
		/// Applies the color block.
		/// </summary>
		public static ColorBlock ApplyColorBlock (Color normalColor, Color highlightedColor,  Color pressedColor, Color disabledColor, float colorMultiplier, float fadeDuration)
		{
			ColorBlock c = new ColorBlock();
			c.normalColor = normalColor;
			c.highlightedColor = highlightedColor;
			c.pressedColor = pressedColor;
			c.disabledColor = disabledColor;
			c.colorMultiplier = colorMultiplier;
			c.fadeDuration = fadeDuration;				
			return c;
		}
		
		/// <summary>
		/// Applies the state sprite.
		/// </summary>
		public static SpriteState ApplySpriteState (Sprite highlightedGraphic, Sprite pressedGraphic,  Sprite disabledGraphic)
		{
			SpriteState s = new SpriteState();
			s.highlightedSprite = highlightedGraphic;
			s.pressedSprite = pressedGraphic;
			s.disabledSprite = disabledGraphic;
			return s;
		}
		
		/// <summary>
		/// Applies the animation triggers.
		/// </summary>
		public static AnimationTriggers ApplyAnimationTriggers(string normalTrigger, string highlightedTrigger,  string pressedTrigger, string disabledTrigger)
		{
			AnimationTriggers t = new AnimationTriggers();
			t.normalTrigger = normalTrigger;
			t.highlightedTrigger = highlightedTrigger;
			t.pressedTrigger = pressedTrigger;
			t.disabledTrigger = disabledTrigger;
			return t;
		}
	}
}




















