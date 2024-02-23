//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Core;
//using System.Linq;
//using DG.Tweening;
//using UnityEngine.UI;
//using System;

//public class RootMaster : MonoBehaviour
//{
//    public GameObject MenuDrawerPrefab;
//    private GameObject MenuDrawerPrefabClone;



//    public GameObject prefab_header;
//    private GameObject cloneHeader;

//    public GameObject prefab_bottom_menu;
//    private GameObject cloneMenuBottom;

//    private float headerHeight;
//    private int headerHeightProcentage;
//    private float bottomMenuHeight;
//    private int bottomMenuHeightProcentafe;
//    private float Delay = 0.4f;

//    private IdsData.Ids ActiveScreen = IdsData.Ids.screen_splash_root;
//    private IdsData.Ids PreviouslyScreen = IdsData.Ids.none;

//    private Dictionary<IdsData.Ids, GameObject> idToGameobject;

//    void Start()
//    {


//        List<ScreenMono> screens = this.GetComponentsInChildren<ScreenMono>(true).ToList();
//        foreach (ScreenMono item in screens)
//        {
//            IdsMono id = item.gameObject.GetComponent<IdsMono>();
//            if (id != null)
//            {
//                ScreenDataContainer.GetInstance().RegisterScreens(item, id, item.gameObject);
//            }
//        }


//        SetIds();

//        AddHeaderOutside();
//        AddBottomMenuOutside();
//        AddDrawerMenu();

//    }

//    public void SetIds()
//    {
//        idToGameobject = new Dictionary<IdsData.Ids, GameObject>();

//        foreach (var item in this.GetComponentsInChildren<IdsMono>(true))
//        {
//            if (!idToGameobject.ContainsKey(item.mIdsData.mIds))
//            {
//                idToGameobject.Add(item.mIdsData.mIds, item.gameObject);
//            }
//        }
//    }

//    public enum Location
//    {
//        afterSibling,
//        top,
//        bottom,
//    }

//    public void EnableScreen(IdsData.Ids id)
//    {
//        Debug.Log(id);
//        GameObject screenShowGameObject = idToGameobject[id];
//        RectTransform screenShowRect = screenShowGameObject.GetComponent<RectTransform>();
//        ScreenData screenShow = ScreenDataContainer.GetInstance().Screens[id];

//        GameObject screenHideGameObject = idToGameobject[ActiveScreen];
//        RectTransform screenHideRect = screenHideGameObject.GetComponent<RectTransform>();
//        ScreenData screenHide = ScreenDataContainer.GetInstance().Screens[ActiveScreen];

//        //set screenShow outside of view so we can slide it in. Then activate it
//        float spaceFromTop = 0;
//        float height = GetScaledScreenHeight();

//        PreviouslyScreen = ActiveScreen;
//        ActiveScreen = id;

//        if (screenShow.IncludeHeader)
//        {
//            spaceFromTop = headerHeight;
//            height = height - headerHeight;
//        }

//        //if (screenShow.IncludeMenuBottom)
//        //{
//        //    height = height - bottomMenuHeight;
//        //}

//        if (ShowMenuBottom(screenShow))
//        {
//            Debug.Log("WHY");
//            height = height - bottomMenuHeight;
//        }

//        //Set height
//        SetHeight(screenShowGameObject.GetComponent<RectTransform>(), height);

//        //set show screen position outside view
//        if (ShowMenuBottom(screenShow))
//        {
//            screenShowRect.offsetMin = new Vector2(GetScaledScreenWidth(), bottomMenuHeight);
//            screenShowRect.offsetMax = new Vector2(GetScaledScreenWidth(), -headerHeight);
//        }
//        else
//        {
//            screenShowRect.offsetMin = new Vector2(GetScaledScreenWidth(), 0);
//            screenShowRect.offsetMax = new Vector2(GetScaledScreenWidth(), -headerHeight);
//        }


//        screenShowGameObject.SetActive(true);

//        //Set header
//        if (screenShow.IncludeHeader && !screenHide.IncludeHeader)
//        {
//            //header must be positioned outside, enabled
//            SetPositionOutside(cloneHeader.GetComponent<RectTransform>(), Location.top);
//            cloneHeader.SetActive(true);
//        }
//        else if (!screenShow.IncludeHeader && screenHide.IncludeHeader)
//        {
//            //disable header
//            cloneHeader.SetActive(false);
//        }

//        //Set menu
//        if (ShowMenuBottom(screenShow) && !screenHide.IncludeMenuBottom)
//        {
//            //menu must be positioned outside, enabled
//            SetPositionOutside(cloneMenuBottom.GetComponent<RectTransform>(), Location.bottom);
//            cloneMenuBottom.SetActive(true);
//        }
//        else if (!ShowMenuBottom(screenShow))
//        {
//            //disable menu
//            cloneMenuBottom.SetActive(false);
//        }

//        //-------------------------------------------transition header, menu and screen----------------------------------------------
//        //show screen
//        screenShowRect.DOLocalMoveX(0, Delay, true);

//        //hide screen
//        screenHideRect.DOLocalMoveX(-GetScaledScreenWidth(), Delay, true).OnComplete(() => HideShowScreen(screenHideGameObject, false));

//        //show header
//        if (screenShow.IncludeHeader && !screenHide.IncludeHeader)
//        {
//            cloneHeader.GetComponent<RectTransform>().DOLocalMoveX(0, Delay, true);
//        }

//        //show bottom menu
//        if (ShowMenuBottom(screenShow) && !screenHide.IncludeMenuBottom)
//        {
//            cloneMenuBottom.GetComponent<RectTransform>().DOLocalMoveX(0, Delay, true);
//        }


//        //else if ((screenShow.IncludeHeader && screenHide.IncludeHeader) || (!screenShow.IncludeHeader && !screenHide.IncludeHeader))
//        //{
//        //    //everything is good
//        //}

//        ////Set menu
//        //if (screenShow.IncludeMenuBottom && !screenHide.IncludeMenuBottom)
//        //{
//        //    //menu must be positioned outside, enabled
//        //    SetPositionOutside(cloneMenuBottom.GetComponent<RectTransform>(), Location.bottom);
//        //    cloneMenuBottom.SetActive(true);
//        //}
//        //else if ((screenShow.IncludeMenuBottom && screenHide.IncludeMenuBottom) || (!screenShow.IncludeMenuBottom && !screenHide.IncludeMenuBottom))
//        //{
//        //    //everything is good
//        //}
//        //else if (!screenShow.IncludeMenuBottom && screenHide.IncludeMenuBottom)
//        //{
//        //    //disable menu
//        //    cloneMenuBottom.SetActive(false);
//        //}
//    }

//    public void BackClicked()
//    {
//        IdsData.Ids screen = idToGameobject[ActiveScreen].GetComponent<ScreenMono>().Parent.GetComponent<IdsMono>().mIdsData.mIds;

//        EnableScreen(screen);
//        this.GetComponentInChildren<MenuVerticalPrefab>(true).BackClicked(screen);

//    }

//    private void HideShowScreen(GameObject screenHide, bool show)
//    {
//        screenHide.SetActive(show);
//    }

//    public float SetHeightProcentage(RectTransform rect, int HeigtProcentage)
//    {
//        float mHeight = (HeigtProcentage * GetScaledScreenHeight()) / 100;
//        rect.SetHeight(mHeight);
//        return mHeight;
//    }

//    public void SetHeight(RectTransform rect, float height)
//    {
//        rect.SetHeight(height);
//    }

//    public float GetHeight(int HeigtProcentage)
//    {
//        float mHeight = (HeigtProcentage * GetScaledScreenHeight()) / 100;
//        return mHeight;
//    }

//    public void SetPosition(RectTransform rect, Location location)
//    {
//        if (location == Location.bottom)
//        {
//            rect.SetPosition(new Vector2(0, 0));
//        }
//        else if (location == Location.top)
//        {
//            rect.SetPosition(new Vector2(0, GetScaledScreenHeight() - rect.rect.height));
//        }

//    }

//    public void SetPositionOutside(RectTransform rect, Location location)
//    {
//        if (location == Location.bottom)
//        {
//            rect.SetPosition(new Vector2(GetScaledScreenWidth(), 0));
//        }
//        else if (location == Location.top)
//        {
//            rect.SetPosition(new Vector2(GetScaledScreenWidth(), GetScaledScreenHeight() - rect.rect.height));
//        }

//    }

//    public float GetScaledScreenHeight()
//    {
//        return Screen.height / this.GetComponentInParent<RectTransform>().lossyScale.y;
//    }

//    public float GetScaledScreenWidth()
//    {
//        return Screen.width / this.GetComponentInParent<RectTransform>().lossyScale.x;
//    }

//    public void AddHeaderOutside()
//    {
//        //create header and add to screen
//        cloneHeader = Instantiate(prefab_header, this.gameObject.transform);
//        MyHeader headerScript = cloneHeader.GetComponent<MyHeader>();
//        RectTransform rect = headerScript.gameObject.GetComponent<RectTransform>();

//        headerHeightProcentage = headerScript.mHeigtProcentage;
//        headerHeight = GetHeight(headerScript.mHeigtProcentage);

//        //Debug.Log("Height header:" + headerHeight);

//        SetHeight(rect, headerHeight);
//        SetPositionOutside(rect, Location.top);

//        //cloneHeader.SetActive(false);
//    }

//    public void AddBottomMenuOutside()
//    {
//        //create bottom menu and add to screen
//        cloneMenuBottom = Instantiate(prefab_bottom_menu, this.gameObject.transform);
//        //MyBottomMenu bottomMenuScript = cloneMenuBottom.GetComponent<MyBottomMenu>();

//        MenuVerticalPrefab menuDynamicFactory = cloneMenuBottom.GetComponent<MenuVerticalPrefab>();
//        RectTransform rect = cloneMenuBottom.GetComponent<RectTransform>();

//        bottomMenuHeightProcentafe = MenuVerticalOptionsContainer.GetInstance().mMenuVerticalOptions.mMenuHeightProcentage;
//        bottomMenuHeight = GetHeight(MenuVerticalOptionsContainer.GetInstance().mMenuVerticalOptions.mMenuHeightProcentage);
//        //Debug.Log("Height bottom menu:" + bottomMenuHeight);
//        SetHeight(rect, bottomMenuHeight);
//        SetPositionOutside(rect, Location.bottom);
//        menuDynamicFactory.StartMenu(bottomMenuHeight);
//    }

//    public void AddDrawerMenu()
//    {
//        MenuDrawerPrefabClone = Instantiate(MenuDrawerPrefab, this.gameObject.transform);
//        MenuDrawer drawer = MenuDrawerPrefabClone.GetComponent<MenuDrawer>();
//        RectTransform rect = drawer.Panel_menu_drawer_inner.GetComponent<RectTransform>();

//        rect.SetSize(new Vector2(GetDrawerWidth(), GetScaledScreenHeight()));
//        rect.SetPosition(new Vector2(-GetDrawerWidth(), 0));

//        ToggleRayCastTarget(false);
//    }

//    public void ShowDrawer()
//    {
//        MenuDrawer drawer = MenuDrawerPrefabClone.GetComponent<MenuDrawer>();
//        RectTransform rect = drawer.Panel_menu_drawer_inner.GetComponent<RectTransform>();

//        DOTween.To(() => rect.offsetMin, x => rect.offsetMin = x, new Vector2(0, 0), 0.3f).SetEase(Ease.InCubic);
//        DOTween.To(() => rect.offsetMax, x => rect.offsetMax = x, new Vector2(GetDrawerWidth() - Screen.width, 0), 0.3f).SetEase(Ease.InCubic);

//        ToggleRayCastTarget(true);

//    }

//    public float GetDrawerWidth()
//    {
//        if (Islandscape())
//        {
//            return (GetScaledScreenWidth() / 4);
//        }
//        else
//        {
//            return (GetScaledScreenWidth() / 2);
//        }
//    }

//    public void ToggleRayCastTarget(bool enable)
//    {
//        MenuDrawerPrefabClone.GetComponent<Image>().raycastTarget = enable;
//    }

//    public void HideDrawer()
//    {
//        MenuDrawer drawer = MenuDrawerPrefabClone.GetComponent<MenuDrawer>();
//        RectTransform rect = drawer.Panel_menu_drawer_inner.GetComponent<RectTransform>();

//        DOTween.To(() => rect.offsetMin, x => rect.offsetMin = x, new Vector2(-GetScaledScreenWidth() / 2, 0), 0.3f).SetEase(Ease.InCubic);
//        DOTween.To(() => rect.offsetMax, x => rect.offsetMax = x, new Vector2(-GetScaledScreenWidth(), 0), 0.3f).SetEase(Ease.InCubic);

//        ToggleRayCastTarget(false);
//    }

//    internal void ClickMenuDrawer(IdsData.Ids id)
//    {
//        this.GetComponentInParent<RootMaster>().HideDrawer();
//        this.GetComponentInParent<RootMaster>().EnableScreen(id);
//        this.GetComponentInChildren<MenuVerticalPrefab>(true).BackClicked(id);
//    }

//    public int GetFavoriteCount()
//    {
//        int count = 0;
//        foreach (var sub in ScreenDataContainer.GetInstance().Screens)
//        {
//            if (sub.Value.IsFavorite)
//            {
//                count++;
//            }
//        }
//        return count;
//    }

//    public bool ShowMenuBottom(ScreenData screenShow)
//    {
//        bool optionsShowMenu = MenuVerticalOptionsContainer.GetInstance().mMenuVerticalOptions.mShow;
//        return optionsShowMenu && screenShow.IncludeMenuBottom;
//    }

//    public bool Islandscape()
//    {
//        return Screen.width > Screen.height;
//    }

//}
