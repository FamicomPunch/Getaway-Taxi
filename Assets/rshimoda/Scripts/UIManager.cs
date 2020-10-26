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
    private float timer;
    private int displayTime;
    private float score;
    private float bonus;
    private int turns;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if(manager.rightTurnEvent != null) manager.rightTurnEvent.AddListener(ChangeImageRight);
        if(manager.leftTurnEvent != null) manager.leftTurnEvent.AddListener(ChangeImageLeft);
        if(manager.backToNormalEvent!= null) manager.backToNormalEvent.AddListener(ChangeImageUp);
        if(manager.turnSucceed != null) manager.turnSucceed.AddListener(PlusOneTurn);
        timer = 0;
        score = 0;
        bonus = 0;
        turns = 0;
        displayTime = 0;
        turnTxt.text = turns.ToString("0");
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > 1.0f){
            displayTime++;
            string minutes = Mathf.Floor(displayTime/60).ToString("00");
            string seconds = (displayTime%60).ToString("00");
            timerTxt.text = minutes + "." + seconds;
            timer = timer - 1;
        }

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
    void PlusOneTurn(){
        turns++;
        turnTxt.text = turns.ToString("0");
    }
}
