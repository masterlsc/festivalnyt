using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace UIStyles
{
	public class Gradient : 
	#if UNITY_5
	BaseMeshEffect
	#else
	MonoBehaviour
	#endif
	{
		public Color topColor = Color.white;
		public Color bottomColor = Color.black;
		
		#if UNITY_5
		public override void ModifyMesh(VertexHelper vHelper)
		{
			if (!IsActive() || vHelper.currentVertCount == 0)
				return;
			
			List<UIVertex> verts = new List<UIVertex>();
			vHelper.GetUIVertexStream(verts);
			
			float top = verts[0].position.y;
			float bottom = verts[0].position.y;
			
			for (int i = 1; i < verts.Count; i++)
			{
				float y = verts[i].position.y;
				if (y > top)
				{
					top = y;
				}
				else if (y < bottom)
				{
					bottom = y;
				}
			}
			
			float height = top - bottom;
			UIVertex v = new UIVertex();
			
			for (int i = 0; i < vHelper.currentVertCount; i++)
			{
				vHelper.PopulateUIVertex(ref v, i);
				v.color = Color32.Lerp(bottomColor, topColor, (v.position.y - bottom) / height);
				vHelper.SetUIVertex(v, i);
			}
		}
		#endif
	}
}