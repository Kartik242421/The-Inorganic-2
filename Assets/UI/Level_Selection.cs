using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Level_Selection : MonoBehaviour
{
    [SerializeField]
    RectTransform page1, page2;
    private RectTransform currPage, nextPage;

    private Vector2 shifvector = new Vector2(1, 0); 
    // Start is called before the first frame update
    void Start()
    {
        currPage = page1;
        nextPage = page2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GUI_Next()
    {
        currPage.DOAnchorMin(currPage.anchorMin - shifvector, 1);
        currPage.DOAnchorMax(currPage.anchorMax - shifvector, 1);
        nextPage.DOAnchorMin(nextPage.anchorMin - shifvector, 1);
        nextPage.DOAnchorMax(nextPage.anchorMax - shifvector, 1);
    }
    public void GUI_Prev()
    {
        currPage.DOAnchorMin(currPage.anchorMin + shifvector, 1);
        currPage.DOAnchorMax(currPage.anchorMax + shifvector, 1);
        nextPage.DOAnchorMin(nextPage.anchorMin + shifvector, 1);
        nextPage.DOAnchorMax(nextPage.anchorMax + shifvector, 1);
    }

    void OnCompleteNext()
    {

    }

    void OnCompletePrev()
    {

    }
}
