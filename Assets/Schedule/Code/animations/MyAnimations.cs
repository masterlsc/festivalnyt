using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Core;

public class MyAnimations : MonoBehaviour
{
    private Image image;
    public RectTransform rect;

    public bool isCollapsed = true;

    private Vector2 offsetMaxStart;
    private Vector2 offsetMinStart;

    private Color32 colorStart;

    // Start is called before the first frame update
    void Start()
    {
        image = rect.gameObject.GetComponent<Image>();
        offsetMaxStart = rect.offsetMax;
        offsetMinStart = rect.offsetMin;
        colorStart = image.color;
    }

    public IEnumerator StartAnimation()
    {
        yield return new WaitForSeconds(0f);

        if (isCollapsed)
        {
            Vector2 cuttentOffsetMax = rect.offsetMax;
            DOTween.To(() => rect.offsetMax, x => rect.offsetMax = x, new Vector2(-10, -10), 1).SetEase(Ease.OutExpo);
            DOTween.To(() => rect.offsetMin, x => rect.offsetMin = x, new Vector2(10, 10), 1).SetEase(Ease.OutExpo);
            image.DOBlendableColor(new Color32(255, 255, 255, 255), 1).OnComplete(() => Complete());
        }
        else
        {

            DOTween.To(() => rect.offsetMax, x => rect.offsetMax = x, offsetMaxStart, 1).SetEase(Ease.OutExpo);
            DOTween.To(() => rect.offsetMin, x => rect.offsetMin = x, offsetMinStart, 1).SetEase(Ease.OutExpo);
            image.DOBlendableColor(colorStart, 1).OnComplete(() => Complete());
        }
        isCollapsed = !isCollapsed;

    }

    public void Complete()  
    {
        To(() => rect.offsetMin, x => rect.offsetMin = x, offsetMinStart, 1);

        //To(x => rect.offsetMin = x, offsetMinStart, 1);

        //To(()x => rect.offsetMin = x, x => rect.offsetMin = x, offsetMinStart, 1);
    }

    public void clickImage()
    {
        //StartCoroutine(StartAnimation());
        //To(() => rect.offsetMin, x => rect.offsetMin = x, offsetMinStart, 1);
        //To(x => rect.offsetMin = x, offsetMinStart, 1);
        To(x => rect.offsetMin = x);
        //To(new DOSetter<Vector2>(new Vector2(0,0)));
    }


    public delegate T DOGetter<out T>();
    public delegate void DOSetter<in T>(T pNewValue);

    public static void To(DOGetter<Vector2> getter,DOSetter<Vector2> setter, Vector2 endValue, float duration)
    {
        setter.Invoke(new Vector2(0, 0));

       // getter.Invoke();
    }

    public static void To(DOSetter<Vector2> setter, Vector2 endValue, float duration)
    {
        setter.Invoke(new Vector2(0, 0));
    }

    public static void To(DOSetter<Vector2> setter)
    {
        setter.Invoke(new Vector2(0, 0));
    }



}
