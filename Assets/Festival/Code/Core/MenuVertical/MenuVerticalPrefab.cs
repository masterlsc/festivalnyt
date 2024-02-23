using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Linq;
using Core;
using System;

[RequireComponent(typeof(MyRectTransformExtended))]
[RequireComponent(typeof(Effects))]
public class MenuVerticalPrefab : MonoBehaviour
{
    private RectTransform mParent;
    private Effects mEffects;
    private float mYposition;

    public GameObject menuItemPrefab;
    public GameObject MenuBottomSeperatorPrefab;

    private GameObject mSelectedMenu = null;
    private ScreensMain.Id mSelectedScreen;

    private Dictionary<ScreensMain.Id, GameObject> ScreenMenu;
    private int counter = 0;
    private float menuWidth = 0;
    private float mMenuHeight = 0;

    public MenuVerticalOptions mMenuVerticalOptions;

    public Action<ScreensMain.Id> ActionMenuClicked = delegate { };

    void Start()
    {
        ScreenMenu = new Dictionary<ScreensMain.Id, GameObject>();
        mMenuVerticalOptions = MenuVerticalOptionsContainer.GetInstance().mMenuVerticalOptions;

    }

    public void StartMenu(float height)
    {


        mEffects = gameObject.GetComponent<Effects>();
        mParent = this.GetComponent<RectTransform>();

        mMenuHeight = height;
        mYposition = 0;// Screen.height - mMenuHeight;

        //----------------------MenuTypeStartPosition---------------------
        //if (mMenuVerticalOptions.mMenuTypeStart == MenuVerticalOptions.MenuTypeStart.showInstantly)
        //{

        //}
        //else if (mMenuVerticalOptions.mMenuTypeStart == MenuVerticalOptions.MenuTypeStart.showWithDelayFadingIn)
        //{

        //}
        StartCoroutine(showInstantly());
    }

    public IEnumerator showInstantly()
    {
        yield return new WaitForSeconds(0f);

        foreach (Transform item in mParent.transform)
        {
            Destroy(item.gameObject);
        }

        ScreenMenu.Clear();
        counter = 0;
        int MenuCount = ScreenDataManager.GetInstance().Screens.Where(x => x.Value.Value.IsFavorite || x.Value.Value.IsMandatory || x.Value.Value.AddToMenuDrawer).Count();


        foreach (var screenKeyValue in ScreenDataManager.GetInstance().Screens.Where(x => x.Value.Value.IsFavorite || x.Value.Value.IsMandatory || x.Value.Value.AddToMenuDrawer))
        {
            var screenData = screenKeyValue.Value.Value;
            var screenKey = screenKeyValue.Key;

            GameObject clone = Instantiate(menuItemPrefab, mParent, false);
            clone.name = "clone_menu:" + counter;

            ScreenMenu.Add(screenKey, clone);

            MyRectTransformExtended extendedRect = clone.GetComponent<MyRectTransformExtended>();
            RectTransform rect = clone.GetComponent<RectTransform>();
            RectTransform rectParent = rect.parent.GetComponent<RectTransform>();
            MenuVerticalItemPrefab menuDynamicSquare = clone.GetComponent<MenuVerticalItemPrefab>();

            menuWidth = rectParent.rect.width / MenuCount;
            float moveValue = menuWidth * counter;

            rect.SetPosition(new Vector2(moveValue, mYposition));
            extendedRect.GetComponent<RectTransform>().SetWidth(menuWidth);
            extendedRect.GetComponent<RectTransform>().SetHeight(mMenuHeight);

            Debug.Log(mMenuVerticalOptions.mMenuColorDeSelected);

            MenuVerticalItemPrefab prefab = clone.GetComponent<MenuVerticalItemPrefab>();
            prefab.Load(screenData, mMenuVerticalOptions.mShowText, mMenuVerticalOptions.mMenuColorDeSelected, screenKey);
            AddMenuClick(clone, screenKey, screenData);

            if (screenData.IsHome)
            {

                SelectMenu(clone, screenKey);
            }

            counter++;
        }

        //foreach (var sub in ScreenDataContainer.GetInstance().Screens)
        //{
        //    if ((sub.Value.IsMandatory || sub.Value.IsFavorite) && sub.Value.AddToMenuBottom)
        //    {
        //        GameObject clone = Instantiate(menuItemPrefab, mParent, false);
        //        clone.name = "clone_menu:" + counter;

        //        ScreenMenu.Add(sub.Key, clone);

        //        MyRectTransformExtended extendedRect = clone.GetComponent<MyRectTransformExtended>();
        //        RectTransform rect = clone.GetComponent<RectTransform>();
        //        RectTransform rectParent = rect.parent.GetComponent<RectTransform>();
        //        MenuVerticalItemPrefab menuDynamicSquare = clone.GetComponent<MenuVerticalItemPrefab>();

        //        menuWidth = rectParent.rect.width / MenuCount;
        //        float moveValue = menuWidth * counter;

        //        rect.SetPosition(new Vector2(moveValue, mYposition));
        //        extendedRect.GetComponent<RectTransform>().SetWidth(menuWidth);
        //        extendedRect.GetComponent<RectTransform>().SetHeight(mMenuHeight);

        //        Debug.Log(mMenuVerticalOptions.mMenuColorDeSelected);

        //        MenuVerticalItemPrefab prefab = clone.GetComponent<MenuVerticalItemPrefab>();
        //        prefab.Load(sub.Value, mMenuVerticalOptions.mShowText, mMenuVerticalOptions.mMenuColorDeSelected, sub.Key);
        //        AddMenuClick(clone, sub.Key, sub.Value);

        //        if (sub.Value.IsHome)
        //        {

        //            SelectMenu(clone, sub.Key);
        //        }

        //        counter++;
        //    }
        //}
        ////this.GetComponentInParent<MyLoad>().TransLateBottomMenu(this.gameObject);

        //if (mMenuVerticalOptions.mShowSeperators)
        //{
        //    AddSeperators();
        //}

    }

    private void AddSeperators()
    {
        float yPosition = 0;
        float xPosition = 0;

        for (int i = 0; i < counter - 1; i++)
        {
            GameObject clone = Instantiate(MenuBottomSeperatorPrefab, mParent, false);
            RectTransform rect = clone.GetComponent<RectTransform>();
            rect.SetHeight(mMenuHeight);
            rect.SetWidth(2);
            yPosition = (mMenuHeight / 2) - (rect.GetSize().y / 2);
            xPosition = menuWidth + (menuWidth * i) - (rect.GetSize().x / 2);
            rect.SetPosition(new Vector2(xPosition, yPosition));

        }
    }

    private void AddMenuClick(GameObject gameobject, ScreensMain.Id id, ScreenData data)
    {
        Button btn = gameobject.GetComponentInChildren<Button>();

        btn.onClick.AddListener(() =>
        {
            if (mSelectedMenu != gameobject)
            {
                RectTransform rect = gameobject.GetComponent<RectTransform>();

                if (mSelectedMenu != null && mSelectedMenu != gameobject)
                {
                    DeselectMenu(mSelectedMenu);
                }

                if (mSelectedMenu != gameobject)
                {
                    SelectMenu(gameobject, id);
                }

                ActionMenuClicked(id);

            }
        });
    }

    private void DeselectMenu(GameObject menu)
    {
        RectTransform rect = menu.GetComponent<RectTransform>();
        rect.GetComponent<Image>().color = MenuVerticalOptionsContainer.GetInstance().mMenuVerticalOptions.mMenuColorDeSelected;// new Color32(255, 255, 255, 255);
    }

    private void SelectMenu(GameObject menu, ScreensMain.Id myScreen)
    {
        mSelectedMenu = menu;
        mSelectedScreen = myScreen;
        RectTransform rect = menu.GetComponent<RectTransform>();
        rect.GetComponent<Image>().color = MenuVerticalOptionsContainer.GetInstance().mMenuVerticalOptions.mMenuColorSelected; ;// new Color32(59, 64, 175, 255);
    }

    public void BackClicked(ScreensMain.Id id)
    {
        DeselectMenu(mSelectedMenu);
        GameObject obj = ScreenMenu[id];
        SelectMenu(obj, id);

        ActionMenuClicked(id);
    }
}












//OLD--------------------------------------------------------------------


//mEffects.GetMoveToParentEdge(extendedRect, 0.5f, Effects.Edge.bottom).OnComplete(() =>
// {
//     mEffects.getMoveValue(extendedRect, 0.5f, moveValue, Effects.Edge.right);
// });




//Sequence s = DOTween.Sequence();
//s.Append(mEffects.GetMoveToParentEdge(extendedRect, 1f, Effects.Edge.bottom));
//            s.Append(mEffects.getMoveValue(extendedRect, 0.5f, moveValue, Effects.Edge.right));


//Sequence s = DOTween.Sequence();
//s.Append(mEffects.GetMoveToParentEdge(extendedRect, 1.5f, Effects.Edge.bottom));
//s.Append(gameObject.GetComponentInParent<Effects>().getMoveValue(clone.GetComponent<MyRectTransformExtended>(), 1.5f, 100f, Effects.Edge.right));


//foreach (IEnumerator item in jobs)
//{
//    clone.GetComponent<MyRectTransformExtended>().dom
//}


//gameObject.GetComponentInParent<Effects>().moveToParentEdge(clone.GetComponent<MyRectTransformExtended>(), 0, 0.5f, Effects.Edge.bottom);
//gameObject.GetComponentInParent<Effects>().moveToParentEdge(clone.GetComponent<MyRectTransformExtended>(), 0, 0.5f, Effects.Edge.bottom);

//MyRectTransformExtended mRectExtended = clone.GetComponent<MyRectTransformExtended>();
////Tweener tween = DOTween.To(() => mRectExtended.PositionIgnoringAnchorsAndPivot, value => mRectExtended.PositionIgnoringAnchorsAndPivot = value, new Vector2(0,0), 2).Pause();
//Sequence s = DOTween.Sequence();
//s.Append(DOTween.To(() => mRectExtended.PositionIgnoringAnchorsAndPivot, value => mRectExtended.PositionIgnoringAnchorsAndPivot = value, new Vector2(0, 0), 2).Pause());

// tween.
// Tweener tween = transform.do(target.position, 2).SetAutoKill(false);

//StartCoroutine(gameObject.GetComponentInParent<Effects>().moveToParentEdge(clone.GetComponent<MyRectTransformExtended>(),0,0.5f, Effects.Edge.bottom));
// yield return new WaitForSeconds(0.5f);
// StartCoroutine(gameObject.GetComponentInParent<Effects>().moveValue(clone.GetComponent<RectTransform>(), 0,0.5f, 226 * i, Effects.Edge.right));
// yield return new WaitForSeconds(0.5f);



//IEnumerator showWithDelayRollingIn()
//{
//    yield return new WaitForSeconds(1f);



//    int counter = 0;
//    Dictionary<MyScreen.Screens, MyScreen> filteredScreen = (from x in this.GetComponentInParent<MyController>().screensList
//                                                             where x.Value.AddToBottomMenu
//                                                             select x).ToDictionary(p => p.Key, p => p.Value);

//    foreach (KeyValuePair<MyScreen.Screens, MyScreen> screen in filteredScreen)
//    {

//        yield return new WaitForSeconds(1f);

//        GameObject clone = Instantiate(menuItemPrefab, mParent, false);
//        clone.name = "clone_menu:" + counter;

//        MyRectTransformExtended extendedRect = clone.GetComponent<MyRectTransformExtended>();
//        RectTransform rect = clone.GetComponent<RectTransform>();
//        RectTransform rectParent = rect.parent.GetComponent<RectTransform>();
//        MenuDynamicPrefab menuDynamicSquare = clone.GetComponent<MenuDynamicPrefab>();

//        float menuWidth = rectParent.rect.width / filteredScreen.Count;
//        float moveValue = menuWidth * counter;

//        rect.SetPosition(new Vector2(Screen.width, mYposition));

//        extendedRect.GetComponent<RectTransform>().SetWidth(menuWidth);
//        extendedRect.GetComponent<RectTransform>().SetHeight(mMenuHeight);

//        // rect.GetComponentInChildren<Text>().DOBlendableColor(Color.white, 0.4f);

//        Effects.Edge edge = Effects.Edge.bottom;
//        //if (mMenuTypeStartPosition == MenuTypeStartPosition.top)
//        //{
//        //    edge = Effects.Edge.top;
//        //}
//        //else if (mMenuTypeStartPosition == MenuTypeStartPosition.bottom)
//        //{
//        //    edge = Effects.Edge.bottom;
//        //}

//        mEffects.GetMoveToParentEdgeAndMoveValue(extendedRect, 0.5f, moveValue, edge).OnComplete(() =>
//        {
//            rect.GetComponent<Image>().DOBlendableColor(Color.blue, 0.4f);
//            //rect.GetComponentInChildren<Text>().DOBlendableColor(Color.white, 0.4f);
//        });

//        MenuDynamicPrefab prefab = clone.GetComponent<MenuDynamicPrefab>();
//        prefab.Load(screen.Value);
//        AddMenuClick(clone, screen.Value);

//        counter++;
//    }

//}





//if (mMenuTypeClickEffect == MenuTypeClickEffect.bubbleUp)
//{
//    rect.GetComponent<Image>().color = new Color32(59, 64, 175, 255);
//    //rect.GetComponentInChildren<Text>().DOBlendableColor(Color.white, 0.6f);
//    //rect.SetHeight(rect.rect.height + 30);

//    //DOTween.To(() => rect.SetHeight, value => rect.sizeDelta = value, new Vector2(rect.sizeDelta.x, rect.sizeDelta.y + 30), 0.3f).SetEase(Ease.InElastic);
//}
//else if (mMenuTypeClickEffect == MenuTypeClickEffect.crazy)
//{
//    mEffects.GetMoveToParentEdge(btn.GetComponentInParent<MyRectTransformExtended>(), 0.5f, Effects.Edge.top).OnComplete(() =>
//    {
//        mEffects.GetMoveToParentEdge(btn.GetComponentInParent<MyRectTransformExtended>(), 0.5f, Effects.Edge.bottom);
//    });
//}







//foreach (KeyValuePair<MyUIItem.UIItem, MyScreen> screen in filteredScreen)
//{
//    MenuDrawerData.MenuDrawerDataItem data = MenuDrawerData.GetInst().dictonary[screen.Key];

//    GameObject clone = Instantiate(menuItemPrefab, mParent, false);
//    clone.name = "clone_menu:" + counter;

//    ScreenMenu.Add(screen.Key, clone);

//    MyRectTransformExtended extendedRect = clone.GetComponent<MyRectTransformExtended>();
//    RectTransform rect = clone.GetComponent<RectTransform>();
//    RectTransform rectParent = rect.parent.GetComponent<RectTransform>();
//    MenuDynamicPrefab menuDynamicSquare = clone.GetComponent<MenuDynamicPrefab>();

//    menuWidth = rectParent.rect.width / filteredScreen.Count;
//    float moveValue = menuWidth * counter;

//    rect.SetPosition(new Vector2(moveValue, mYposition));
//    extendedRect.GetComponent<RectTransform>().SetWidth(menuWidth);
//    extendedRect.GetComponent<RectTransform>().SetHeight(mMenuHeight);


//    MenuDynamicPrefab prefab = clone.GetComponent<MenuDynamicPrefab>();
//    prefab.Load(screen.Value, mShowText, mMenuColorDeSelected, screen.Key);
//    AddMenuClick(clone, screen.Value);

//    if (screen.Value.MenuDefaultSelected)
//    {

//        SelectMenu(clone,screen.Key);
//    }

//    counter++;
//}

//foreach (var sub in ScreenDataContainer.GetInstance().Screens)
//{
//    if (sub.Value.IsMandatory || sub.Value.AddToMenuDrawer)
//        if ((sub.Value.IsMandatory || sub.Value.IsFavorite))
//        {
//            MenuCount++;
//        }
//}