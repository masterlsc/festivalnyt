using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{

    public class Utility
    {
        public static GameObject GetSiblingBefore(GameObject obj)
        {
            int siblingId = obj.GetComponent<RectTransform>().GetSiblingIndex();
            if (siblingId == 0)
            {
                return null;
            }
            else
            {
                return obj.transform.parent.GetChild(siblingId - 1).gameObject;
            }
        }

        public static float GetSiblingBeforeHeight(GameObject obj)
        {
            float space = 0;
            GameObject sibling = GetSiblingBefore(obj);
            if (sibling != null)
            {
                space = sibling.GetComponent<RectTransform>().rect.height;
            }

            return space;
        }

    }

}