using DG.Tweening;
using DG.Tweening.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effects : MonoBehaviour
{
    public enum MoveType
    {
        third,
        quarter,
        halv
    }

    public enum Expansion
    {
        increase,
        decrease,
    }

    public enum Edge
    {
        top,
        bottom,
        right,
        left,
    }






    private int getDirectionValue(Edge moveEdge, Expansion moveDirection)
    {
        int directionValue = 1;
        if (moveEdge == Edge.top || moveEdge == Edge.right)
        {
            if (moveDirection == Expansion.increase)
            {
                directionValue = 1;
            }
            else if (moveDirection == Expansion.decrease)
            {
                directionValue = -1;
            }
        }
        else if (moveEdge == Edge.bottom || moveEdge == Edge.left)
        {
            if (moveDirection == Expansion.increase)
            {
                directionValue = -1;
            }
            else if (moveDirection == Expansion.decrease)
            {
                directionValue = 1;
            }
        }

        return directionValue;
    }

    public IEnumerator moveToParentEdge(MyRectTransformExtended mRectExtended, float delay, float duration,Edge moveEdge)
    {
        MyRectTransformExtended parent = mRectExtended.GetComponent<RectTransform>().parent.GetComponent<MyRectTransformExtended>();
        yield return new WaitForSeconds(delay);
        float x = 0;
        float y = 0;
        if (moveEdge == Edge.top)
        {
            x = mRectExtended.PositionIgnoringAnchorsAndPivot.x;
            y = parent.SizeAsCanvas.y - (mRectExtended.SizeAsCanvas.y);
        }
        else if (moveEdge == Edge.bottom)
        {
            x = mRectExtended.PositionIgnoringAnchorsAndPivot.x;
            y = 0;
        }
        else if (moveEdge == Edge.right)
        {
            x = parent.SizeAsCanvas.x - (mRectExtended.SizeAsCanvas.x);
            y = mRectExtended.PositionIgnoringAnchorsAndPivot.y;
        }
        else if (moveEdge == Edge.left)
        {
            x = 0;
            y = mRectExtended.PositionIgnoringAnchorsAndPivot.y;
        }


        DOTween.To(() => mRectExtended.PositionIgnoringAnchorsAndPivot, value => mRectExtended.PositionIgnoringAnchorsAndPivot = value, new Vector2(x, y), duration);
    }

    public IEnumerator moveValue(RectTransform Panel, float delay, float duration, float moveValue, Edge moveEdge)
    {
        MyRectTransformExtended mRectExtended = Panel.GetComponent<MyRectTransformExtended>();
        yield return new WaitForSeconds(delay);
        float x = 0;
        float y = 0;
        if (moveEdge == Edge.top)
        {
            x = mRectExtended.PositionIgnoringAnchorsAndPivot.x;
            y = mRectExtended.PositionIgnoringAnchorsAndPivot.y + moveValue;
        }
        else if (moveEdge == Edge.bottom)
        {
            x = mRectExtended.PositionIgnoringAnchorsAndPivot.x;
            y = mRectExtended.PositionIgnoringAnchorsAndPivot.y + moveValue *-1;
        }
        else if (moveEdge == Edge.right)
        {
            x = mRectExtended.PositionIgnoringAnchorsAndPivot.x + moveValue;
            y = mRectExtended.PositionIgnoringAnchorsAndPivot.y;
        }
        else if (moveEdge == Edge.left)
        {
            x = mRectExtended.PositionIgnoringAnchorsAndPivot.x + moveValue * -1;
            y = mRectExtended.PositionIgnoringAnchorsAndPivot.y;
        }
        DOTween.To(() => mRectExtended.PositionIgnoringAnchorsAndPivot, value => mRectExtended.PositionIgnoringAnchorsAndPivot = value, new Vector2(x,y), duration);
    }

    public IEnumerator resizeValue(RectTransform Panel, float delay, float duration, float moveValue, Expansion moveDirection, Edge moveEdge)
    {
        yield return new WaitForSeconds(delay);

        if (moveEdge == Edge.bottom)
        {
            DOTween.To(() => Panel.offsetMin, x => Panel.offsetMin = x, new Vector2(Panel.offsetMin.x, Panel.offsetMin.y + moveValue * getDirectionValue(moveEdge, moveDirection)), duration);
        }
        else if (moveEdge == Edge.top)
        {
            DOTween.To(() => Panel.offsetMax, x => Panel.offsetMax = x, new Vector2(Panel.offsetMax.x, Panel.offsetMax.y + moveValue * getDirectionValue(moveEdge, moveDirection)), duration);
        }
        else if (moveEdge == Edge.left)
        {
            DOTween.To(() => Panel.offsetMin, x => Panel.offsetMin = x, new Vector2(Panel.offsetMin.x + moveValue * getDirectionValue(moveEdge, moveDirection), Panel.offsetMin.y), duration);
        }
        else if (moveEdge == Edge.right)
        {
            DOTween.To(() => Panel.offsetMax, x => Panel.offsetMax = x, new Vector2(Panel.offsetMax.x + moveValue * getDirectionValue(moveEdge, moveDirection), Panel.offsetMax.y), duration);
        }
    }

    public IEnumerator resizeToParentEdge(MyRectTransformExtended myRectTransformExtended, float delay, float duration, Edge moveEdge)
    {
        yield return new WaitForSeconds(delay);
        RectTransform Panel = myRectTransformExtended.GetComponent<RectTransform>();
        MyRectTransformExtended parent = myRectTransformExtended.GetComponent<RectTransform>().parent.GetComponent<MyRectTransformExtended>();

        if (moveEdge == Edge.bottom)
        {
            DOTween.To(() => Panel.offsetMin, x => Panel.offsetMin = x, new Vector2(Panel.offsetMin.x, 0), duration);
        }
        else if (moveEdge == Edge.top)
        {
            DOTween.To(() => Panel.offsetMax, x => Panel.offsetMax = x, new Vector2(Panel.offsetMax.x, 0), duration);
        }
        else if (moveEdge == Edge.left)
        {
            DOTween.To(() => Panel.offsetMin, x => Panel.offsetMin = x, new Vector2(0, Panel.offsetMin.y), duration);
        }
        else if (moveEdge == Edge.right)
        {
            DOTween.To(() => Panel.offsetMax, x => Panel.offsetMax = x, new Vector2(0, Panel.offsetMax.y), duration);
        }
       
    }

    public IEnumerator resizeToPanelEdge(RectTransform panelExtendedFrom, Edge moveEdgeFrom, RectTransform panelExtendedTo, Edge moveEdgeTo, float delay, float duration)
    {
        RectTransform rectFrom = panelExtendedFrom;// panelExtendedFrom.gameObject.GetComponent<RectTransform>();
        RectTransform rectTo = panelExtendedTo;// panelExtendedTo.gameObject.GetComponent<RectTransform>();

        yield return new WaitForSeconds(delay);
        if(moveEdgeFrom == Edge.top && moveEdgeTo == Edge.top)
        {
            DOTween.To(() => rectFrom.offsetMax, x => rectFrom.offsetMax = x, new Vector2(rectFrom.offsetMax.x, rectTo.offsetMax.y), duration);
        }
        else if (moveEdgeFrom == Edge.top && moveEdgeTo == Edge.bottom)
        {
            DOTween.To(() => rectFrom.offsetMax, x => rectFrom.offsetMax = x, new Vector2(rectFrom.offsetMax.x, rectTo.offsetMax.y - rectTo.rect.height), duration);
        }
        else if (moveEdgeFrom == Edge.bottom && moveEdgeTo == Edge.bottom)
        {
            DOTween.To(() => rectFrom.offsetMin, x => rectFrom.offsetMin = x, new Vector2(rectFrom.offsetMin.x, rectTo.offsetMin.y), duration);
        }
        else if (moveEdgeFrom == Edge.bottom && moveEdgeTo == Edge.top)
        {
            DOTween.To(() => rectFrom.offsetMin, x => rectFrom.offsetMin = x, new Vector2(rectFrom.offsetMin.x, rectTo.offsetMin.y + rectTo.rect.height), duration);
        }

        if (moveEdgeFrom == Edge.left && moveEdgeTo == Edge.left)
        {
            DOTween.To(() => rectFrom.offsetMin, x => rectFrom.offsetMin = x, new Vector2(rectTo.offsetMin.x, rectFrom.offsetMin.y), duration);
        }
        else if (moveEdgeFrom == Edge.left && moveEdgeTo == Edge.right)
        {
            DOTween.To(() => rectFrom.offsetMin, x => rectFrom.offsetMin = x, new Vector2(rectTo.offsetMin.x + rectTo.rect.width, rectFrom.offsetMin.y), duration);
        }

        else if (moveEdgeFrom == Edge.right && moveEdgeTo == Edge.right)
        {
            DOTween.To(() => rectFrom.offsetMax, x => rectFrom.offsetMax = x, new Vector2(rectTo.offsetMax.x, rectFrom.offsetMax.y), duration);
        }
        else if (moveEdgeFrom == Edge.right && moveEdgeTo == Edge.left)
        {
            DOTween.To(() => rectFrom.offsetMax, x => rectFrom.offsetMax = x, new Vector2(rectTo.offsetMax.x - rectTo.rect.width, rectFrom.offsetMax.y), duration);
        }

        

    }

    public Tweener GetMoveToParentEdge(MyRectTransformExtended mRectExtended, float duration, Edge moveEdge)
    {
        MyRectTransformExtended parent = mRectExtended.gameObject.GetComponent<RectTransform>().parent.GetComponent<MyRectTransformExtended>();
     //   yield return new WaitForSeconds(delay);
        float x = 0;
        float y = 0;
        if (moveEdge == Edge.top)
        {
            x = mRectExtended.PositionIgnoringAnchorsAndPivot.x;
            y = parent.SizeAsCanvas.y - (mRectExtended.SizeAsCanvas.y);
        }
        else if (moveEdge == Edge.bottom)
        {
            x = mRectExtended.PositionIgnoringAnchorsAndPivot.x;
            y = 0;
        }
        else if (moveEdge == Edge.right)
        {
            x = parent.SizeAsCanvas.x - (mRectExtended.SizeAsCanvas.x);
            y = mRectExtended.PositionIgnoringAnchorsAndPivot.y;
        }
        else if (moveEdge == Edge.left)
        {
            x = 0;
            y = mRectExtended.PositionIgnoringAnchorsAndPivot.y;
        }
        return DOTween.To(() => mRectExtended.PositionIgnoringAnchorsAndPivot, value => mRectExtended.PositionIgnoringAnchorsAndPivot = value, new Vector2(x, y), duration);
    }

    public Tweener GetMoveToParentEdgeAndMoveValue(MyRectTransformExtended mRectExtended, float duration, float moveValueX, Edge moveEdge)
    {
        MyRectTransformExtended parent = mRectExtended.gameObject.GetComponent<RectTransform>().parent.GetComponent<MyRectTransformExtended>();
        //   yield return new WaitForSeconds(delay);
        float x = 0;
        float y = 0;
        if (moveEdge == Edge.top)
        {
            x = mRectExtended.PositionIgnoringAnchorsAndPivot.x + moveValueX;
            y = parent.SizeAsCanvas.y - (mRectExtended.SizeAsCanvas.y);
        }
        else if (moveEdge == Edge.bottom)
        {
            x = mRectExtended.PositionIgnoringAnchorsAndPivot.x + moveValueX;
            y = 0;
        }
        else if (moveEdge == Edge.right)
        {
            x = parent.SizeAsCanvas.x - (mRectExtended.SizeAsCanvas.x);
            y = mRectExtended.PositionIgnoringAnchorsAndPivot.y;
        }
        else if (moveEdge == Edge.left)
        {
            x = 0;
            y = mRectExtended.PositionIgnoringAnchorsAndPivot.y;
        }
        return DOTween.To(() => mRectExtended.PositionIgnoringAnchorsAndPivot, value => mRectExtended.PositionIgnoringAnchorsAndPivot = value, new Vector2(x, y), duration);
    }

    public Tweener getMoveValue(MyRectTransformExtended mRectExtended, float duration, float moveValue, Edge moveEdge)
    {
        //MyRectTransformExtended mRectExtended = Panel.GetComponent<MyRectTransformExtended>();
    
        float x = 0;
        float y = 0;
        if (moveEdge == Edge.top)
        {
            x = mRectExtended.PositionIgnoringAnchorsAndPivot.x;
            y = mRectExtended.PositionIgnoringAnchorsAndPivot.y + moveValue;
        }
        else if (moveEdge == Edge.bottom)
        {
            x = mRectExtended.PositionIgnoringAnchorsAndPivot.x;
            y = mRectExtended.PositionIgnoringAnchorsAndPivot.y + moveValue * -1;
        }
        else if (moveEdge == Edge.right)
        {
            x = mRectExtended.PositionIgnoringAnchorsAndPivot.x + moveValue;
            y = mRectExtended.PositionIgnoringAnchorsAndPivot.y;
        }
        else if (moveEdge == Edge.left)
        {
            x = mRectExtended.PositionIgnoringAnchorsAndPivot.x + moveValue * -1;
            y = mRectExtended.PositionIgnoringAnchorsAndPivot.y;
        }
       return DOTween.To(() => mRectExtended.PositionIgnoringAnchorsAndPivot, value => mRectExtended.PositionIgnoringAnchorsAndPivot = value, new Vector2(x, y), duration);
    }



    //public void startSequence(List<IEnumerator> list)
    //{
    //    Sequence s = DOTween.Sequence();
    //    foreach (var item in list)
    //    {
    //        s.Append(item);
    //    }
    //}



    //public void MoveResize(GameObject gameObject)
    //{
    //    MyRectTransformExtended mRectExtended = gameObject.GetComponent<MyRectTransformExtended>();
    //    RectTransform mRect = gameObject.GetComponent<RectTransform>();

    //    //Move
    //    DOTween.To(() => mRectExtended.PositionIgnoringAnchorsAndPivot, x => mRectExtended.PositionIgnoringAnchorsAndPivot = x, new Vector2(0, 0), 2f)
    //         .OnComplete(() =>
    //         {
    //             //resize right and left
    //             DOTween.To(() => mRect.offsetMin, x => mRect.offsetMin = x, new Vector2(mRect.offsetMin.x + 200, mRect.offsetMin.y), 2f);
    //             DOTween.To(() => mRect.offsetMax, x => mRect.offsetMax = x, new Vector2(mRect.offsetMax.x + 200, mRect.offsetMax.y), 2f);
    //         });
    //}

}
