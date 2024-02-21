using System;
using UnityEngine;
using UnityEngine.UI;

public class HeaderPrefab : MonoBehaviour
{
    public int mHeigtProcentage;
    public Image ImageBack;
    public Image ImageMenuDrawer;
    public Text Title;

    public Action ActionBackClicked = delegate () { };
    public Action ActionDrawerClicked = delegate { };

    public void BackClicked()
    {
        ActionBackClicked();
    }

    public void DrawerClicked()
    {
        ActionDrawerClicked();
    }

    public void ShowBackButton(bool show)
    {
        ImageBack.gameObject.SetActive(show);
    }

    public void ShowDrawerButton(bool show)
    {
        ImageMenuDrawer.gameObject.SetActive(show);
    }

    public void SetTitle(string title)
    {
        Title.text = title;
    }

}
