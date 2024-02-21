using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTranslateItem : MonoBehaviour
{
    public TextId mTextId= TextId.not_set;
    public bool IgoneTranslation = false;

    public enum TextId
    {
        not_set,
        home_text_test,
        screen_profile,
        screen_terms,
        screen_news,
        screen_splash,
        screen_home,
        screen_settings,
        screen_page1,
        screen_page2,
        screen_page3,
        screen_child_page_3,
        screen_child_page_3_child,

    }
}
