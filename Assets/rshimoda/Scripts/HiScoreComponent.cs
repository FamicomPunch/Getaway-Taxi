using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.UI;

public class HiScoreComponent:MonoBehaviour
{
    private Text nameLbl;
    private Text scoreLbl;
    private Text timeLbl;
    private Text roundsLbl;
    private Text attemptsLbl;
    void Start(){
        nameLbl = this.gameObject.transform.Find("NameLbl").gameObject.GetComponent<Text>();
        scoreLbl = this.gameObject.transform.Find("ScoreLbl").gameObject.GetComponent<Text>();
        timeLbl = this.gameObject.transform.Find("TimeLbl").gameObject.GetComponent<Text>();
        roundsLbl = this.gameObject.transform.Find("RoundsLbl").gameObject.GetComponent<Text>();
        attemptsLbl = this.gameObject.transform.Find("AttemptsLbl").gameObject.GetComponent<Text>();
    }
    public void Disable(){
        this.gameObject.SetActive(false);
    }
    public void DisplayValue(UserData score){
        nameLbl.text = score.user_id.ToString();
        scoreLbl.text =score.top_score.ToString(); 
        timeLbl.text = score.top_time.ToString();
        roundsLbl.text = score.top_rounds.ToString();
        attemptsLbl.text = score.total_matches.ToString();
    }
}