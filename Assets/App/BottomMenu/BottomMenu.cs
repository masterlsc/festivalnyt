using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomMenu : MonoBehaviour
{
    public GameObject BottomMenuItemHome;
    public GameObject BottomMenuItemEvents;
    public GameObject BottomMenuItemMap;
    public GameObject BottomMenuItemFavotites;
    public GameObject BottomMenuItemSettings;

    public GameObject BottomMenuItemmSelected;

    private void Start()
    {
        BottomMenuItemmSelected = BottomMenuItemHome;
        UpdateSelectedItem();
    }

    private void UpdateSelectedItem()
    {

    }

    private void UpdateSelected()
    {
        //deselect all execpt BottomMenuItemmSelected

        //disable 
    }

    #region Click events

    public void ClickBottomMenuItemHome()
    {
        BottomMenuItemmSelected = BottomMenuItemHome;
        UpdateSelected();
    }

    public void ClickBottomMenuItemEvents()
    {
        BottomMenuItemmSelected = BottomMenuItemEvents;
    }

    public void ClickBottomMenuItemMap()
    {
        BottomMenuItemmSelected = BottomMenuItemMap;
    }

    public void ClickBottomMenuItemFavotites()
    {
        BottomMenuItemmSelected = BottomMenuItemFavotites;
    }

    public void ClickBottomMenuItemSettings()
    {
        BottomMenuItemmSelected = BottomMenuItemSettings;
    }

    #endregion

}
