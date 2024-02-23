using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionScreen : MonoBehaviour
{

    public GameObject OptionPrefab;
    public GameObject Container;

    // Start is called before the first frame update
    void Start()
    {
        CreateOptions();
    }

    public void CreateOptions()
    {
        //Load options for the bottom menu
        var bottomMenu = MenuVerticalOptionsContainer.GetInstance().mMenuVerticalOptions;

        //Load options for theme

        //load options for drawer menu
    }
}
