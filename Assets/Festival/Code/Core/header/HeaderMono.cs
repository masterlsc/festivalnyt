using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeaderMono : MonoBehaviour
{

    public GameObject HeaderPrefab;

    [HideInInspector]
    public GameObject PrefabHeaderClone;
    [HideInInspector]
    public HeaderPrefab headerScript;
    [HideInInspector]
    public float Height;
    [HideInInspector]
    private int HeightProcentage;

    public void AddHeaderOutside()
    {
        //create header and add to screen
        PrefabHeaderClone = Instantiate(HeaderPrefab, this.gameObject.transform);
        headerScript = PrefabHeaderClone.GetComponent<HeaderPrefab>();
        RectTransform rect = headerScript.gameObject.GetComponent<RectTransform>();

        HeightProcentage = headerScript.mHeigtProcentage;
        Height = GetMonoUtil().GetHeight(headerScript.mHeigtProcentage);

        GetMonoUtil().SetHeight(rect, Height);
        GetMonoUtil().SetPositionOutside(rect, AppUtilMono.Location.top);
    }

    public AppUtilMono GetMonoUtil()
    {
        return this.GetComponentInParent<AppUtilMono>();
    }

    public void ScreenChanged(ScreenData dataShow, ScreenData dataHide, float slideInSpeed)
    {
        var screenWith = GetMonoUtil().GetScaledScreenWidth();

        if (dataShow.IncludeHeader && !dataHide.IncludeHeader)
            PrefabHeaderClone.GetComponent<RectTransform>().DOLocalMoveX(0, slideInSpeed, true);

        if (!dataShow.IncludeHeader && dataHide.IncludeHeader)
            PrefabHeaderClone.GetComponent<RectTransform>().DOLocalMoveX(-screenWith, slideInSpeed, true).OnComplete(() => PrefabHeaderClone.GetComponent<RectTransform>().DOLocalMoveX(screenWith, 0, true));

        headerScript.SetTitle(dataShow.MenuText);

        headerScript.ShowBackButton(dataShow.ShowBackButton);
       // headerScript.ShowDrawerButton(dataShow.IsHome);
    }

}
