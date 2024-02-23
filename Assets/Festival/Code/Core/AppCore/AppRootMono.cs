using BayatGames.SaveGamePro;
using BestHTTP;
using DG.Tweening;
using SharedModel.User;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class AppRootMono : MonoBehaviour
{

    private ScreensMain.Id PreviousScreen;
    private ScreensMain.Id ActiveScreen = ScreensMain.Id.ScreensSplash;

    private float ScreenSlideSpeed = 0.1f;
    private float Height;

   


    void Start()
    {
        StartCoroutine(StartApp());
        //TODO: testing
        //TestUserListPrefab();
        //StartCoroutine(TestUserCreatePrefab());
    }

    private IEnumerator StartApp()
    {
        yield return new WaitForSeconds(1f);

        //TODO: remove
        SaveGame.Clear();
        ScreenDataManager.GetInstance().RegisterScreens(GetComponentsInChildren<ScreensMain>(true).ToList());

        GetComponent<HeaderMono>().AddHeaderOutside();
        GetComponent<HeaderMono>().headerScript.ActionBackClicked = BackClicked;
        GetComponent<HeaderMono>().headerScript.ActionDrawerClicked = DrawerShow;

        GetComponent<MenuVerticalMono>().AddMenu();
        GetComponent<MenuVerticalMono>().MenuVerticalPrefabScript.ActionMenuClicked = EnableScreen;

        //GetComponent<MenuDrawerMono>().AddDrawerMenu();
        //GetComponent<MenuDrawerMono>().MenuDrawerPrefabScript.ActionMenuClicked = DraweMenuClicked;
        //MenuDrawerPrefab.ActionHideDrawer = DrawerHide;

        EnableScreen(ScreensMain.Id.Bobler);
    }

    public AppUtilMono GetMonoUtil()
    {
        return this.GetComponent<AppUtilMono>();
    }

    public void EnableScreen(ScreensMain.Id screen)
    {
        PreviousScreen = ActiveScreen;
        ActiveScreen = screen;

        ActivateScreenSlideIn();
    }

    public IEnumerator EnableScreenC(ScreensMain.Id screen)
    {
        yield return new WaitForEndOfFrame();
        EnableScreen(screen);

    }

    public void ActivateScreenSlideIn()
    {
        GameObject screenShow = ScreenDataManager.GetInstance().Screens[ActiveScreen].Key.GetComponentInChildren<ScreensMainContent>(true).gameObject;
        var DataShow = ScreenDataManager.GetInstance().Screens[ActiveScreen].Value;

        GameObject screenHide = ScreenDataManager.GetInstance().Screens[PreviousScreen].Key.GetComponentInChildren<ScreensMainContent>(true).gameObject;
        var DataHide = ScreenDataManager.GetInstance().Screens[PreviousScreen].Value;

        PrepareActiveScreen(screenShow, DataShow);

        screenShow.GetComponent<RectTransform>().DOLocalMoveX(0, ScreenSlideSpeed, true);
        screenHide.GetComponent<RectTransform>().DOLocalMoveX(-GetMonoUtil().GetScaledScreenWidth(), ScreenSlideSpeed, true).OnComplete(() => screenHide.SetActive(false));

        GetComponentInParent<HeaderMono>().ScreenChanged(DataShow, DataHide, ScreenSlideSpeed);
        GetComponent<MenuVerticalMono>().ScreenChanged(DataShow, DataHide, ScreenSlideSpeed);
    }

    public void BackClicked()
    {
        GetComponent<MenuVerticalMono>().MenuVerticalPrefabScript.BackClicked(PreviousScreen);
    }

    public void PrepareActiveScreen(GameObject screen, ScreenData data)
    {
        Height = GetMonoUtil().GetScaledScreenHeight();

        if (!data.IncludeHeader)
            Height = Height - GetComponentInParent<HeaderMono>().Height;

        if (!data.IncludeMenuBottom)
            Height = Height - GetComponentInParent<MenuVerticalMono>().Height;

        screen.GetComponent<RectTransform>().SetHeight(Height);
        screen.SetActive(true);

        var rect = screen.GetComponent<RectTransform>();

        rect.offsetMax = new Vector2(GetMonoUtil().GetScaledScreenWidth(), data.IncludeHeader ? -GetComponentInParent<HeaderMono>().Height : 0);
        rect.offsetMin = new Vector2(GetMonoUtil().GetScaledScreenWidth(), data.IncludeMenuBottom ? GetComponentInParent<MenuVerticalMono>().Height : 0);

    }

    public void DrawerShow()
    {
        GetComponent<MenuDrawerMono>().ShowDrawer();
    }

    public void DrawerHide()
    {
        GetComponent<MenuDrawerMono>().HideDrawer();
    }

    private void DraweMenuClicked(ScreensMain.Id menuId)
    {

        if (ActiveScreen != menuId)
            EnableScreen(menuId);

        DrawerHide();
    }

    public void ActionUserReturned(Usermodel user)
    {
        Debug.Log(user);
    }

    public void ActionEmailExistReturned(bool exists)
    {
        Debug.Log(exists);
    }

}

