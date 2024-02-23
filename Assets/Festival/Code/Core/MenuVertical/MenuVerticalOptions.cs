using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuVerticalOptions
{
    public bool mShowText;
    public bool mShowSeperators = true;

    public int mMenuHeightProcentage;

    public Color32 mMenuColorSelected;
    public Color32 mMenuColorDeSelected;

    public MenuTypeStart mMenuTypeStart;
    public MenuTypeClickEffect mMenuTypeClickEffect;
    public bool mShow;

    public enum MenuTypeStart
    {
        showInstantly,
        showWithDelayFadingIn,
    }

    public enum MenuTypeClickEffect
    {
        bubbleUp,
        crazy,
    }

    public MenuVerticalOptions(bool mShowText, bool mShowSeperators, int mMenuHeightProcentage, Color32 mMenuColorSelected, Color32 mMenuColorDeSelected, MenuTypeStart mMenuTypeStart, MenuTypeClickEffect mMenuTypeClickEffect, bool mShow)
    {
        this.mShowText = mShowText;
        this.mShowSeperators = mShowSeperators;
        this.mMenuHeightProcentage = mMenuHeightProcentage;
        this.mMenuColorSelected = mMenuColorSelected;
        this.mMenuColorDeSelected = mMenuColorDeSelected;
        this.mMenuTypeStart = mMenuTypeStart;
        this.mMenuTypeClickEffect = mMenuTypeClickEffect;
        this.mShow = mShow;
    }

}
