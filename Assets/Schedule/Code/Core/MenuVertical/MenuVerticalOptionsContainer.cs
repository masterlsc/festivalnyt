using BayatGames.SaveGamePro;
using System.Collections;
using System.Collections.Generic;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;

public class MenuVerticalOptionsContainer 
{
    private string SettingName= "MenuVerticalOptions";

    private static MenuVerticalOptionsContainer mInstance;

    public MenuVerticalOptions mMenuVerticalOptions;

    public static MenuVerticalOptionsContainer GetInstance()
    {
        if (mInstance == null)
        {
            mInstance = new MenuVerticalOptionsContainer();
        }
        return mInstance;
    }

    private MenuVerticalOptionsContainer()
    {
        Load();
    }

    private void Load()
    {
        string optionValue = "";// SaveGame.Load<string>(SettingName);
        if (string.IsNullOrEmpty(optionValue))
        {
            mMenuVerticalOptions = new MenuVerticalOptions(true, true,6, new Color32(141, 152,142,255), new Color32(82,134,183,255), MenuVerticalOptions.MenuTypeStart.showInstantly, MenuVerticalOptions.MenuTypeClickEffect.bubbleUp, true);
            Save();
        }
        else
        {
            mMenuVerticalOptions = JsonConvert.DeserializeObject<MenuVerticalOptions>(optionValue);
        }
    }

    public void Save()
    {
        string json = JsonConvert.SerializeObject(mMenuVerticalOptions);
        SaveGame.Save<string>(SettingName, json);
    }

}
