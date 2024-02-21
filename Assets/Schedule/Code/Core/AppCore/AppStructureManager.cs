using BayatGames.SaveGamePro;
using Unity.Plastic.Newtonsoft.Json;

public class AppStructureManager
{

    private static AppStructureManager Instance;
    private string SettingName = "AppStructureManager";
    public AppStructureData AppStructureData;

    public static AppStructureManager GetInstance()
    {
        if (Instance == null)
        {
            Instance = new AppStructureManager();
        }
        return Instance;
    }

    private AppStructureManager()
    {
        Load();
    }

    private void Load()
    {
        string value = SaveGame.Load<string>(SettingName);
        if (string.IsNullOrEmpty(value))
        {
            AppStructureData = new AppStructureData() {ShowBottomMenu=true, ShowDrawer=true, ShowHeader=true };
        }
        else
        {
            AppStructureData = JsonConvert.DeserializeObject<AppStructureData>(value);
        }
    }

    public void Save()
    {
        string json = JsonConvert.SerializeObject(AppStructureData);
        SaveGame.Save<string>(SettingName, json);
    }

}
