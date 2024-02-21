using Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuVerticalItemPrefab : MonoBehaviour {

    public Text mText;
    public Image mImage;
    public ScreensMain.Id mUIItem;
    [HideInInspector]
    public MyTranslateItem.TextId TextId;

    public void Load(ScreenData screen, bool showText, Color32 color, ScreensMain.Id uiItem)
    {
        
        mUIItem = uiItem;
        if (showText)
        {
            mText.text = screen.MenuText;
        }
        else
        {
            mText.text = "";
        }

        if (!string.IsNullOrEmpty(screen.MenuIconPath))
        {
            mImage.sprite = Resources.Load<Sprite>(screen.MenuIconPath);
        }

        this.GetComponent<Image>().color = color;

       // setTranslateIdFromScreen(uiItem);
    }

    ///// <summary>
    ///// Add the translate component to the text field, from screen enum
    ///// </summary>
    ///// <param name="uiItem"></param>
    //private void setTranslateIdFromScreen(MyUIItem.UIItem uiItem)
    //{
    //    MyTranslateItem.TextId id =(MyTranslateItem.TextId)Enum.Parse(typeof(MyTranslateItem.TextId), uiItem.ToString());

    //    mText.gameObject.AddComponent<MyTranslateItem>();
    //    mText.gameObject.GetComponent<MyTranslateItem>().mTextId = id;
    //}
}
