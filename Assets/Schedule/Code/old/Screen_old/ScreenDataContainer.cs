//using BayatGames.SaveGamePro;
//using Core;
//using Newtonsoft.Json;
//using System.Collections.Generic;
//using UnityEngine;

//public class ScreenDataContainer
//{
//    private static ScreenDataContainer Instance;
//    public Dictionary<IdsData.Ids, ScreenData> Screens;

//    private string SettingName = "MenuData";

//    public static ScreenDataContainer GetInstance()
//    {
//        if (Instance == null)
//        {
//            Instance = new ScreenDataContainer();
//        }
//        return Instance;
//    }

//    private ScreenDataContainer()
//    {
//        Load();
//    }

//    public void RegisterScreens(ScreenMono screenMono, IdsMono idsMono, GameObject gameobject)
//    {
//        if (!Screens.ContainsKey(idsMono.mIdsData.mIds))
//        {
//            ScreenData screen = CreateScreenData(idsMono, idsMono.mIdsData);
//            Screens.Add(idsMono.mIdsData.mIds, screen);
//        }     

//    }

//    private void Load()
//    {
//        string value = SaveGame.Load<string>(SettingName);
//        if (string.IsNullOrEmpty(value))
//        {
//            Screens = new Dictionary<IdsData.Ids, ScreenData>();
//        }
//        else
//        {
//            Screens = JsonConvert.DeserializeObject<Dictionary<IdsData.Ids,ScreenData>>(value);
//        }
//    }

//    public void Save()
//    {
//        string json = JsonConvert.SerializeObject(Screens);
//        SaveGame.Save<string>(SettingName, json);
//    }

//    private ScreenData CreateScreenData(IdsMono idsMono, IdsData idsData)
//    {
//        ScreenData screen = new ScreenData();
//        if(idsMono.mIdsData.mType== IdsData.Type.screen)
//        {
//            if(idsMono.mIdsData.mIds== IdsData.Ids.screen_home_root)
//            {
//                screen.AddToMenuDrawer = true;
//                screen.AddToMenuBottom = true;

//                screen.IsHome = true;
//                screen.IsMandatory = true;
//                screen.IsFavorite = true;
//                screen.IncludeHeader = true;
//                screen.IncludeMenuBottom = true;
//                screen.IncludeMenuDrawer = true;
//                screen.ShowBackButton = false;
//                screen.MenuText = "Home";
//                screen.MenuIconPath = "icon8/icon_home";
//            }
//            else if (idsMono.mIdsData.mIds == IdsData.Ids.screen_settings_root)
//            {
//                screen.AddToMenuDrawer = true;
//                screen.AddToMenuBottom = true;

//                screen.IsHome = false;
//                screen.IsMandatory = true;
//                screen.IsFavorite = true;
//                screen.IncludeHeader = true;
//                screen.IncludeMenuBottom = true;
//                screen.IncludeMenuDrawer = true;
//                screen.ShowBackButton = true;
//                screen.MenuText = "Settings";
//                screen.MenuIconPath = "icon8/icon_settings";
//            }
//            else if (idsMono.mIdsData.mIds == IdsData.Ids.screen_profile_root)
//            {
//                screen.AddToMenuDrawer = true;
//                screen.AddToMenuBottom = true;

//                screen.IsHome = false;
//                screen.IsMandatory = false;
//                screen.IncludeHeader = true;
//                screen.IncludeMenuBottom = true;
//                screen.IncludeMenuDrawer = true;
//                screen.ShowBackButton = true;
//                screen.MenuText = "Profile";
//                screen.MenuIconPath = "icon8/icon_profile";
//            }
//            else if (idsMono.mIdsData.mIds == IdsData.Ids.screen_news_root)
//            {
//                screen.AddToMenuDrawer = true;
//                screen.AddToMenuBottom = true;

//                screen.IsHome = false;
//                screen.IsMandatory = false;
//                screen.IncludeHeader = true;
//                screen.IncludeMenuBottom = true;
//                screen.IncludeMenuDrawer = true;
//                screen.ShowBackButton = true;
//                screen.MenuText = "News";
//                screen.MenuIconPath = "icon8/icon_news";
//            }

//            else if (idsMono.mIdsData.mIds == IdsData.Ids.screen_feedback_root)
//            {
//                screen.AddToMenuDrawer = true;
//                screen.AddToMenuBottom = true;

//                screen.IsHome = false;
//                screen.IsMandatory = false;
//                screen.IncludeHeader = true;
//                screen.IncludeMenuBottom = true;
//                screen.IncludeMenuDrawer = true;
//                screen.ShowBackButton = true;
//                screen.MenuText = "Feedback";
//                screen.MenuIconPath = "icon8/icon_feedback";
//            }
//            else if (idsMono.mIdsData.mIds == IdsData.Ids.screen_webapi_root)
//            {
//                screen.AddToMenuDrawer = true;
//                screen.AddToMenuBottom = true;

//                screen.IsHome = false;
//                screen.IsMandatory = false;
//                screen.IncludeHeader = true;
//                screen.IncludeMenuBottom = true;
//                screen.IncludeMenuDrawer = true;
//                screen.ShowBackButton = true;
//                screen.MenuText = "WebApi";
//                screen.MenuIconPath = "icon8/icon_feedback";
//            }
//            else if (idsMono.mIdsData.mIds == IdsData.Ids.screen_login_root)
//            {
//                screen.AddToMenuDrawer = false;
//                screen.AddToMenuBottom = false;

//                screen.IsHome = false;
//                screen.IsMandatory = false;
//                screen.IncludeHeader = false;
//                screen.IncludeMenuBottom = false;
//                screen.IncludeMenuDrawer = false;
//                screen.ShowBackButton = false;
//                screen.MenuText = "";
//                screen.MenuIconPath = "";
//            }
//        }
//        return screen;
//    }
         
//}

