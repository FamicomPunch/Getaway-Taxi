using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class HiScoreManager : MonoBehaviour
{
    public HiScoreComponent[] components;
    private string urlBase;
    void Start(){
        urlBase = ScoringManager.Instance.urlBase;
        StartCoroutine("GetScores");

    }
    public IEnumerator GetScores(){
        using (UnityWebRequest www = UnityWebRequest.Get(urlBase + "BTGetTop"))
        {
            www.method = UnityWebRequest.kHttpVerbGET;
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", "application/json");
 
            yield return www.SendWebRequest();
            if (www.isNetworkError)
            {
                Debug.Log(www.error);
            }
            else
            {
                MountHiscoreScreen(www.downloadHandler.text);
            }
        }
    }

    private void MountHiscoreScreen(string returnString){
        Debug.Log("list: " + returnString);
        if(returnString.IndexOf("error") >=0){
            var srvError = JsonUtility.FromJson<ServerError>(returnString);
            Debug.Log(srvError.error);
        }
        else{
            HiScores EveryHiScore = JsonUtility.FromJson<HiScores>(returnString);
            if(EveryHiScore != null)
            {
                var TopTen = EveryHiScore.scores.OrderBy(score => score.top_score).Take(10).ToList<UserData>();
                for(int countPlayer = 0; countPlayer < TopTen.Count; countPlayer++)
                {
                    components[countPlayer+1].DisplayValue(TopTen[countPlayer]);
                }
                for(int countEmpty = TopTen.Count; countEmpty < 10; countEmpty++){
                    components[countEmpty+1].Disable();
                }

            }
        }
    }

}
