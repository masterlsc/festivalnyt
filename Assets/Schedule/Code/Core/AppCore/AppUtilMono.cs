using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppUtilMono : MonoBehaviour
{

    public enum Location
    {
        afterSibling,
        top,
        bottom,
    }

    public float GetScaledScreenHeight()
    {
        return Screen.height / this.GetComponentInParent<RectTransform>().lossyScale.y;
    }

    public float GetScaledScreenWidth()
    {
        return Screen.width / this.GetComponentInParent<RectTransform>().lossyScale.x;
    }

    public bool Islandscape()
    {
        return Screen.width > Screen.height;
    }

    public float GetHeight(int HeigtProcentage)
    {
        float mHeight = (HeigtProcentage * GetScaledScreenHeight()) / 100;
        return mHeight;
    }

    public void SetHeight(RectTransform rect, float height)
    {
        rect.SetHeight(height);
    }

    public void SetPositionOutside(RectTransform rect, Location location)
    {
        if (location == Location.bottom)
        {
            rect.SetPosition(new Vector2(GetScaledScreenWidth(), 0));
        }
        else if (location == Location.top)
        {
            rect.SetPosition(new Vector2(GetScaledScreenWidth(), GetScaledScreenHeight() - rect.rect.height));
        }

    }


}
