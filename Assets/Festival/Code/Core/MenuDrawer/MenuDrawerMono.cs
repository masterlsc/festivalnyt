using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuDrawerMono : MonoBehaviour
{
    public GameObject MenuDrawerPrefabOriginal;

    [HideInInspector]
    public GameObject MenuDrawerPrefab;
    [HideInInspector]
    public MenuDrawerPrefab MenuDrawerPrefabScript;
    [HideInInspector]
    public RectTransform MenuDrawerPrefabInner;

    public void AddDrawerMenu()
    {
        MenuDrawerPrefab = Instantiate(MenuDrawerPrefabOriginal, this.gameObject.transform);
        MenuDrawerPrefabScript = MenuDrawerPrefab.GetComponent<MenuDrawerPrefab>();
        MenuDrawerPrefabInner = MenuDrawerPrefabScript.Panel_menu_drawer_inner.GetComponent<RectTransform>();

        MenuDrawerPrefabInner.SetSize(new Vector2(GetDrawerWidth(), GetMonoUtil().GetScaledScreenHeight()));
        MenuDrawerPrefabInner.SetPosition(new Vector2(-GetDrawerWidth(), 0));

        ToggleRayCastTarget(false);
    }

    public AppUtilMono GetMonoUtil()
    {
        return this.GetComponentInParent<AppUtilMono>();
    }

    public float GetDrawerWidth()
    {
        if (GetMonoUtil().Islandscape())
        {
            return (GetMonoUtil().GetScaledScreenWidth() / 4);
        }
        else
        {
            return (GetMonoUtil().GetScaledScreenWidth() / 2);
        }
    }

    public void ToggleRayCastTarget(bool enable)
    {
        MenuDrawerPrefab.GetComponent<Image>().raycastTarget = enable;
    }

    public void ShowDrawer()
    {
       
        DOTween.To(() => MenuDrawerPrefabInner.offsetMin, x => MenuDrawerPrefabInner.offsetMin = x, new Vector2(0, 0), 0.3f).SetEase(Ease.InCubic);
        DOTween.To(() => MenuDrawerPrefabInner.offsetMax, x => MenuDrawerPrefabInner.offsetMax = x, new Vector2(GetDrawerWidth() - Screen.width, 0), 0.3f).SetEase(Ease.InCubic);

        ToggleRayCastTarget(true);
    }

    public void HideDrawer()
    {

        DOTween.To(() => MenuDrawerPrefabInner.offsetMin, x => MenuDrawerPrefabInner.offsetMin = x, new Vector2(-GetMonoUtil().GetScaledScreenWidth() / 2, 0), 0.3f).SetEase(Ease.InCubic);
        DOTween.To(() => MenuDrawerPrefabInner.offsetMax, x => MenuDrawerPrefabInner.offsetMax = x, new Vector2(-GetMonoUtil().GetScaledScreenWidth(), 0), 0.3f).SetEase(Ease.InCubic);

        ToggleRayCastTarget(false);
    }

}
