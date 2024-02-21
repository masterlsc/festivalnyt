using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreensMain : MonoBehaviour
{

    [HideInInspector]
    public Id EnumId = Id.NotSet;
     
    public enum Id
    {
        NotSet,
        ScreensSplash,
        ScreensHome,
        ScreensEvents,
        ScreensArtists,
        ScreensMap,
        ScreensSettings,
    }
}
