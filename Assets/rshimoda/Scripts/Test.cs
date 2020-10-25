using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Test : MonoBehaviour
{
    public InputField ScoreTxt;
    public InputField BonusTxt;
    public InputField TimeTxt;
    public InputField RoundsTxt;
    public void OnBtnReturn(){
        SceneManager.LoadScene("Menu");
    }

    public void OnBtnSendScore(){
        if(ScoringManager.Instance == null)
        {
            Debug.Log("FUCK");
            return;
        }
        ScoringManager.Instance.UpdateScore(
            new UserPlay(){
                rounds = float.Parse(ScoreTxt.text.Trim()),
                score = float.Parse(BonusTxt.text.Trim()),
                bonus = float.Parse(TimeTxt.text.Trim()),
                time =float.Parse(RoundsTxt.text.Trim()),
            }
        );
    }
}
