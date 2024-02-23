using BayatGames.SaveGamePro;
using System.Collections.Generic;
using Unity.Plastic.Newtonsoft.Json;

public class MenuDrawerManager
{
    private static MenuDrawerManager Instance;
    public Dictionary<ScreensMain.Id, MenuDrawerDataItem> Data;

    private string SettingName = "MenuDrawerManager";

    public static MenuDrawerManager GetInstance()
    {
        if (Instance == null)
        {
            Instance = new MenuDrawerManager();
        }
        return Instance;
    }

    private MenuDrawerManager()
    {
        Load();
    }

    private void Load()
    {
        string value = SaveGame.Load<string>(SettingName);
        if (string.IsNullOrEmpty(value))
        {
            LoadDefault();
        }
        else
        {
            Data = JsonConvert.DeserializeObject<Dictionary<ScreensMain.Id, MenuDrawerDataItem>>(value);
        }
    }

    public void LoadDefault()
    {
        Data = new Dictionary<ScreensMain.Id, MenuDrawerDataItem>();
        foreach (var screen in ScreenDataManager.GetInstance().Screens)
        {
            if (screen.Value.Value.IncludeMenuDrawer)
            {
                Data.Add(screen.Key, new MenuDrawerDataItem(true));
            }
        }
    }

    public void Save()
    {
        string json = JsonConvert.SerializeObject(Data);
        SaveGame.Save<string>(SettingName, json);
    }

    public class MenuDrawerDataItem
    {
        public bool IsFavorite;


        public MenuDrawerDataItem(bool isFavorite)
        {
            IsFavorite = isFavorite;
        }

    }





}
