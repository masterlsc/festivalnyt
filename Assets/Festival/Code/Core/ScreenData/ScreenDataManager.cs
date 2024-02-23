using BayatGames.SaveGamePro;
using System.Collections;
using System.Collections.Generic;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;

public class ScreenDataManager
{

    private static ScreenDataManager Instance;
    //public Dictionary<ScreensMain.Id, ScreenData> Screens;
    public Dictionary<ScreensMain.Id, KeyValuePair<GameObject, ScreenData>> Screens;

    private string SettingName = "ScreenDataManager";

    public static ScreenDataManager GetInstance()
    {
        if (Instance == null)
        {
            Instance = new ScreenDataManager();
        }
        return Instance;
    }

    private ScreenDataManager()
    {
        Load();
    }

    public void RegisterScreens(List<ScreensMain> scr)
    {
        foreach (var Screen in scr)
        {
            if (!Screens.ContainsKey(Screen.EnumId))
            {

                ScreenData screenData = CreateScreenData(Screen.EnumId);
                Screens.Add(Screen.EnumId, new KeyValuePair<GameObject, ScreenData>(Screen.gameObject, screenData));
            }
        }


    }

    public void RegisterScreens(ScreensMain Screen)
    {
        if (!Screens.ContainsKey(Screen.EnumId))
        {

            ScreenData screenData = CreateScreenData(Screen.EnumId);
            Screens.Add(Screen.EnumId, new KeyValuePair<GameObject, ScreenData>(Screen.gameObject, screenData));
        }

    }

    private void Load()
    {
        string value = "";// SaveGame.Load<string>(SettingName);
        if (string.IsNullOrEmpty(value))
        {
            Screens = new Dictionary<ScreensMain.Id, KeyValuePair<GameObject, ScreenData>>();
        }
        else
        {
            Screens = JsonConvert.DeserializeObject<Dictionary<ScreensMain.Id, KeyValuePair<GameObject, ScreenData>>>(value);
        }
    }

    public void Save()
    {
        string json = JsonConvert.SerializeObject(Screens);
        SaveGame.Save<string>(SettingName, json);
    }

    private ScreenData CreateScreenData(ScreensMain.Id id)
    {
        ScreenData screen = new ScreenData(id);

        if (id == ScreensMain.Id.ScreensSplash)
        {
            screen = new ScreenData(id);
        }
        else if (id == ScreensMain.Id.Bobler)
        {
            screen = new ScreenData("Bobler", "icon8/icon_calendar") { IncludeHeader = true, IncludeMenuBottom = true, IncludeMenuDrawer = true, AddToMenuBottom = true, AddToMenuDrawer = true, ShowBackButton = false, IsHome = true };
        }
        else if (id == ScreensMain.Id.Liste)
        {
            screen = new ScreenData("Liste", "icon8/icon_liste") { IncludeHeader = true, IncludeMenuBottom = true, IncludeMenuDrawer = true, AddToMenuBottom = true, AddToMenuDrawer = true, ShowBackButton = false };
        }
        else if (id == ScreensMain.Id.ScreensMap)
        {
            screen = new ScreenData("Kort", "icon8/icons8-alarm1") { IncludeHeader = true, IncludeMenuBottom = true, IncludeMenuDrawer = true, AddToMenuBottom = true, AddToMenuDrawer = true, ShowBackButton = false };
        }
        else if (id == ScreensMain.Id.Filter)
        {
            screen = new ScreenData("Filter", "icon8/icons8-filter") { IncludeHeader = true, IncludeMenuBottom = true, IncludeMenuDrawer = true, AddToMenuBottom = true, AddToMenuDrawer = true, ShowBackButton = false, IsPopUp = true };
        }

        return screen;
    }

}


