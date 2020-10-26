using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public float maxSpeed = 9.8f;
    public float minSpeed = 3.7f;
    public Text timerTxt;
    public Text turnTxt;
    public Text scoreTxt;
    public Text bonusTxt;
    public Image directionImg;
    public RectTransform speedoNeedleImg;
    public Image wheelImg;
    public Sprite upArrow;
    public Sprite leftArrow;
    public Sprite rightArrow;
    public GameObject deadPanel;
    private GameManager manager;
    private float timer;
    private int displayTime;
    private float score;
    private float bonus;
    private int turns;
    private bool died = false;

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

        turnTxt.text = "0";
        scoreTxt.text = "0";
        bonusTxt.text = "0";
        //deadPanel.setActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if(!manager.gameOverTrigger){
            timer += Time.deltaTime;
            if(timer > 1.0f){
                displayTime++;
                string minutes = Mathf.Floor(displayTime/60).ToString("00");
                string seconds = (displayTime%60).ToString("00");
                timerTxt.text = minutes + "." + seconds;
                timer = timer - 1;
            }
            //Debug.Log(manager.speed);
            score += manager.speed * Time.deltaTime;
            scoreTxt.text = score.ToString("0");
            float finalAngle = 20.0f;
            if(manager.speed > maxSpeed){
                finalAngle = -90;
            } else if(manager.speed < minSpeed){
                finalAngle = 20;
            } else{
                finalAngle = 20 - (110 * (manager.speed-minSpeed)/(maxSpeed-minSpeed));
            }
            speedoNeedleImg.rotation = Quaternion.Euler(0f,0f,finalAngle);
        }
        else{
            //deadPanel.setActive(true);
            speedoNeedleImg.rotation = Quaternion.Euler(0f,0f,90f);
            if(!died){
                //died;
                //ScoringManager.Ins
            }
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
