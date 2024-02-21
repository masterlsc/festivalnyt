using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenData
{

    public ScreensMain.Id Id;

    public bool IsHome = false;

    public bool IncludeHeader = false;
    public bool IncludeMenuBottom = false;
    public bool IncludeMenuDrawer = false;

    public bool IsMandatory = false;//these will always be added to drawer and bottom menu, toggle mandatory not possible
    public bool AddToMenuBottom = false;
    public bool AddToMenuDrawer = false;

    public bool IsFavorite = false;

    public bool ShowBackButton = false;

    public string MenuText = "Home";
    public string MenuIconPath = "icon8/icon_home";

    public ScreenData(ScreensMain.Id id)
    {
        Id = id;
    }


    public ScreenData(bool isHome, bool includeHeader, bool includeMenuBottom, bool includeMenuDrawer, bool isMandatory, bool addToMenuBottom, bool addToMenuDrawer, bool isFavorite, bool showBackButton, string menuText, string menuIconPath)
    {
        IsHome = isHome;
        IncludeHeader = includeHeader;
        IncludeMenuBottom = includeMenuBottom;
        IncludeMenuDrawer = includeMenuDrawer;
        IsMandatory = isMandatory;
        AddToMenuBottom = addToMenuBottom;
        AddToMenuDrawer = addToMenuDrawer;
        IsFavorite = isFavorite;
        ShowBackButton = showBackButton;
        MenuText = menuText;
        MenuIconPath = menuIconPath;
    }

    public ScreenData(string menuText, string menuIconPath)
    {
        MenuText = menuText;
        MenuIconPath = menuIconPath;
    }

}
