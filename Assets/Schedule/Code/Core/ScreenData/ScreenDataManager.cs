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
        else if (id == ScreensMain.Id.ScreensHome)
        {
            screen = new ScreenData("Home", "icon8/icon_home") { IncludeHeader = true, IncludeMenuBottom = true, IncludeMenuDrawer = true, AddToMenuBottom = true, AddToMenuDrawer = true, ShowBackButton = false, IsHome = true };
        }
        else if (id == ScreensMain.Id.ScreensEvents)
        {
            screen = new ScreenData("Events", "icon8/icon_calendar") { IncludeHeader = true, IncludeMenuBottom = true, IncludeMenuDrawer = true, AddToMenuBottom = true, AddToMenuDrawer = true, ShowBackButton = false };
        }
        else if (id == ScreensMain.Id.ScreensArtists)
        {
            screen = new ScreenData("Artists", "icon8/icon_artists") { IncludeHeader = true, IncludeMenuBottom = true, IncludeMenuDrawer = true, AddToMenuBottom = false, AddToMenuDrawer = false, ShowBackButton = false };
        }
        else if (id == ScreensMain.Id.ScreensMap)
        {
            screen = new ScreenData("Map", "icon8/icons8-alarm1") { IncludeHeader = true, IncludeMenuBottom = true, IncludeMenuDrawer = true, AddToMenuBottom = true, AddToMenuDrawer = true, ShowBackButton = false };
        }
        else if (id == ScreensMain.Id.ScreensSettings)
        {
            screen = new ScreenData("Settings", "icon8/icon_settings") { IncludeHeader = true, IncludeMenuBottom = true, IncludeMenuDrawer = true, AddToMenuBottom = true, AddToMenuDrawer = true, ShowBackButton = false };
        }

        return screen;
    }

}


