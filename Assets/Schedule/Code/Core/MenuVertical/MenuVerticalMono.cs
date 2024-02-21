using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AppUtilMono;

public class MenuVerticalMono : MonoBehaviour
{
    [HideInInspector]
    public GameObject MenuVerticalPrefabClone;
    public GameObject MenuVerticalPrefab;
    [HideInInspector]
    public MenuVerticalPrefab MenuVerticalPrefabScript;
    [HideInInspector]
    public float Height;
    [HideInInspector]
    private int HeightProcentage;

    public void AddMenu()
    {
        MenuVerticalPrefabClone = Instantiate(MenuVerticalPrefab, this.gameObject.transform);
        MenuVerticalPrefabScript = MenuVerticalPrefabClone.GetComponent<MenuVerticalPrefab>();

        GetMonoUtil().SetPositionOutside(MenuVerticalPrefabClone.GetComponent<RectTransform>(), Location.bottom);
        MenuVerticalPrefabClone.SetActive(true);

        Height = MenuVerticalPrefabClone.gameObject.GetComponent<RectTransform>().rect.height;

        MenuVerticalPrefabScript.StartMenu(Height);
    }

    public void ScreenChanged(ScreenData dataShow, ScreenData dataHide, float slideInSpeed)
    {
        var screenWith = GetMonoUtil().GetScaledScreenWidth();

        if (dataShow.IncludeMenuBottom && !dataHide.IncludeMenuBottom)
            MenuVerticalPrefabClone.GetComponent<RectTransform>().DOLocalMoveX(0, slideInSpeed, true);

        if (!dataShow.IncludeMenuBottom && dataHide.IncludeMenuBottom)
            MenuVerticalPrefabClone.GetComponent<RectTransform>().DOLocalMoveX(-screenWith, slideInSpeed, true).OnComplete(() => MenuVerticalPrefabClone.GetComponent<RectTransform>().DOLocalMoveX(screenWith, 0, true));
    }

    public void RefreshMenuItems()
    {
        foreach (var screen in ScreenDataManager.GetInstance().Screens)
        {
            if (screen.Value.Value.AddToMenuBottom)
            {

            }
        }
      
    }

    public AppUtilMono GetMonoUtil()
    {
        return this.GetComponentInParent<AppUtilMono>();
    }

}
