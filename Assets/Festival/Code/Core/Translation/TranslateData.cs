using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class TranslateData
    {
        private TranslateData Instance;
        private Dictionary<ScreensMain.Id, string> Texts;

        public TranslateData GetInstance()
        {
            if (Instance == null)
            {
                Instance = new TranslateData();
            }
            return Instance;
        }

        private TranslateData()
        {
            LoadText();
        }

        private void LoadText()
        {
            if (Application.systemLanguage == SystemLanguage.Danish)
            {
            //    Texts.Add(IdsData.Ids.screen_home_root, "Hjem");
            //    Texts.Add(IdsData.Ids.screen_home_text_header, "Hjem");


            }
            else if (Application.systemLanguage == SystemLanguage.English)
            {

            }
        }

    }

}