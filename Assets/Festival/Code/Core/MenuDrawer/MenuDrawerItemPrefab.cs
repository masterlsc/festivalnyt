using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuDrawerItemPrefab : MonoBehaviour
{

    public Image Image_icon;
    public Text text_name;
    public Image Image_favorite;
    private ScreensMain.Id UIItem;
    private ScreenData screenData;

    public void LoadMenuItem(ScreenData screen, ScreensMain.Id id)
    {
        text_name.text = screen.MenuText;
        UIItem = id;
        screenData = screen;
        if (!string.IsNullOrEmpty(screen.MenuIconPath))
        {
            Image_icon.sprite = Resources.Load<Sprite>(screen.MenuIconPath);
        }

        //data= MenuDrawerData.GetInst().dictonary[key];
        if (screenData.IsMandatory)
        {
            Image_favorite.gameObject.SetActive(false);
        }
        else
        {
            //if (this.GetComponentInParent<RootMaster>().GetComponentInChildren<MenuDrawer>().MaxFavoritesReached())
            //{
            //    Image_favorite.GetComponent<Button>().interactable = false;
            //}
            //else
            //{
            //    Image_favorite.GetComponent<Button>().interactable = true;
            //}


            if (screenData.IsFavorite)
            {
                Image_favorite.color = new Color32(198, 255, 0, 255);
                Image_favorite.GetComponent<Button>().interactable = true;
            }
            else
            {
                Image_favorite.color = new Color32(255, 255, 255, 255);
            }

        }

    }

    public void UpdateFavoritesMax()
    {
        //data = MenuDrawerData.GetInst().dictonary[UIItem];
        if (screenData.IsMandatory)
        {
            Image_favorite.gameObject.SetActive(false);
        }
        else
        {
            //if (this.GetComponentInParent<RootMaster>().GetComponentInChildren<MenuDrawer>().MaxFavoritesReached())
            //{
            //    Image_favorite.GetComponent<Button>().interactable = false;
            //}
            //else
            //{
            //    Image_favorite.GetComponent<Button>().interactable = true;
            //}


            if (screenData.IsFavorite)
            {
                Image_favorite.color = new Color32(198, 255, 0, 255);
                Image_favorite.GetComponent<Button>().interactable = true;
            }
            else
            {
                Image_favorite.color = new Color32(255, 255, 255, 255);
            }

        }
    }

    public void FavoriteClicked()
    {
        //if (!screenData.IsFavorite)
        //{
        //    screenData.IsFavorite = true;
        //    Image_favorite.color = new Color32(198, 255, 0, 255);
        //}
        //else
        //{
        //    screenData.IsFavorite = false;
        //    Image_favorite.color = new Color32(255, 255, 255, 255);
        //}

        //ScreenDataContainer.GetInstance().Save();
        ////MenuDrawerData.GetInst().Save();

        //StartCoroutine(this.GetComponentInParent<RootMaster>().GetComponentInChildren<MenuVerticalPrefab>(true).showInstantly());
        //this.GetComponentInParent<RootMaster>().GetComponentInChildren<MenuDrawer>().favoritesClicked();
    }

    public void MenuClicked()
    {
        this.GetComponentInParent<MenuDrawerPrefab>().ClickMenu(UIItem);

    }



}
