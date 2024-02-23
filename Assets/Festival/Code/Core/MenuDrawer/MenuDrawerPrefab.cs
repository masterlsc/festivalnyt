using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Core;
using UnityEngine;
using UnityEngine.UI;

public class MenuDrawerPrefab : MonoBehaviour
{
    public GameObject PrefabDrawerMenuItem;
    public GameObject Panel_menu_drawer_inner;
    public GameObject Panel_menu_drawer_content;
    public GameObject Panel_menu_drawer_item_content;
    public int MaxFavorites = 3;

    public Action<ScreensMain.Id> ActionMenuClicked = delegate { };

    public static Action ActionHideDrawer = delegate { };

    void Start()
    {
        StartCoroutine(LoadMenu());
    }


    IEnumerator LoadMenu()
    {
        yield return new WaitForSeconds(0f);

        int counter = 0;

        foreach (var sub in ScreenDataManager.GetInstance().Screens)
        {
            if (sub.Value.Value.IsMandatory || sub.Value.Value.AddToMenuDrawer)
            {
                GameObject clone = Instantiate(PrefabDrawerMenuItem, Panel_menu_drawer_item_content.transform, false);
                clone.name = "clone_menu:" + sub.Key.ToString();

                MenuDrawerItemPrefab prefab = clone.GetComponent<MenuDrawerItemPrefab>();
                prefab.LoadMenuItem(sub.Value.Value, sub.Key);

                counter++;
            }
        }


        //string value = MySettings.GetInst().GetValue(MySettings.Settings.MenuDrawerData);
        //if (string.IsNullOrEmpty(value))
        //{
        //    foreach (KeyValuePair<MyUIItem.UIItem, MyScreen> screen in filteredScreen)
        //    {
        //        MenuDrawerData.GetInst().dictonary.Add(screen.Key, new MenuDrawerData.MenuDrawerDataItem(false));
        //    }
        //    MenuDrawerData.GetInst().Save();
        //}

        //foreach (KeyValuePair<MyUIItem.UIItem, MyScreen> screen in filteredScreen)
        //{
        //    GameObject clone = Instantiate(PrefabDrawerMenuItem, Panel_menu_drawer_item_content.transform, false);
        //    clone.name = "clone_menu:" + screen.Key.ToString();

        //    MenuDrawerPrefabItem prefab = clone.GetComponent<MenuDrawerPrefabItem>();
        //    prefab.LoadMenuItem(screen.Value, screen.Key);
        //    counter++;
        //}



        //this.GetComponentInParent<MyLoad>().TransLateBottomMenu(this.gameObject);
    }

    public void ClickRoot()
    {
        ActionHideDrawer();
       // this.GetComponentInParent<RootMaster>().HideDrawer();
    }

    public bool MaxFavoritesReached()
    {
        return true;// this.GetComponentInParent<RootMaster>().GetFavoriteCount() >= MaxFavorites;
    }

    internal void favoritesClicked()
    {
        //foreach (Transform item in Panel_menu_drawer_item_content.transform)
        //{
        //    MenuDrawerPrefabItem prefab = item.GetComponent<MenuDrawerPrefabItem>();
        //    prefab.UpdateFavoritesMax();
        //}
    }

    internal void ClickMenu(ScreensMain.Id id)
    {
        ActionMenuClicked(id);
    }
}
