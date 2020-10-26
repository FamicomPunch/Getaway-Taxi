using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text timerTxt;
    public Text turnTxt;
    public Text scoreTxt;
    public Text bonusTxt;
    public Image directionImg;
    public Image speedoNeedleImg;
    public Image wheelImg;
    public Sprite upArrow;
    public Sprite leftArrow;
    public Sprite rightArrow;
    private GameManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if(manager.rightTurnEvent != null) manager.rightTurnEvent.AddListener(ChangeImageRight);
        if(manager.leftTurnEvent != null) manager.leftTurnEvent.AddListener(ChangeImageLeft);
        if(manager.backToNormalEvent!= null) manager.backToNormalEvent.AddListener(ChangeImageUp);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void ChangeImageLeft(){
        directionImg.sprite = leftArrow;
    }
    void ChangeImageRight(){
        directionImg.sprite = rightArrow;
    }
    void ChangeImageUp(){
        directionImg.sprite = upArrow;
    }
}
