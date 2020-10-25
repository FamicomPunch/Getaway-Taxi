using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.Text;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class LoginManager : MonoBehaviour
{
    public InputField usernameTxt;
    public InputField passwordTxt;
    public Text gameTitleLbl;
    public Text errorLbl;
    private string urlBase;
    private string gameScene;

    void Start(){
        urlBase = ScoringManager.Instance.urlBase;
        gameScene = ScoringManager.Instance.gameScene;
        gameTitleLbl.text = ScoringManager.Instance.gameTitle;
    }
    public void OnRegisterBtn(){
        if(string.IsNullOrWhiteSpace(usernameTxt.text) || string.IsNullOrWhiteSpace(passwordTxt.text)){
            errorLbl.text = "Please fill in register information";
            return;
        }
        errorLbl.text = string.Empty;
        StartCoroutine("PostRegister");
    }
    public IEnumerator PostRegister(){
        var requestBody = new LoginRequest(){
            Username = usernameTxt.text.Trim(),
            Password = passwordTxt.text.Trim()
        };
        var postData = JsonUtility.ToJson(requestBody);
        using (UnityWebRequest www = UnityWebRequest.Put(urlBase + "BTUsrMgm", postData))
        {
            www.method = UnityWebRequest.kHttpVerbPUT;
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", "application/json");
 
            yield return www.SendWebRequest();
 
            if (www.isNetworkError)
            {
                errorLbl.text = www.error;
            }
            else
            {
                ReturnLoginRegister(www.downloadHandler.text);
            }
        }

    }
    public void OnLoginBtn(){
        if(string.IsNullOrWhiteSpace(usernameTxt.text) || string.IsNullOrWhiteSpace(passwordTxt.text)){
            errorLbl.text = "Please fill in login information";
            return;
        }
        errorLbl.text = string.Empty;
        StartCoroutine("PostLogin");
    }
    public IEnumerator PostLogin(){

        var requestBody = new LoginRequest(){
            Username = usernameTxt.text,
            Password = passwordTxt.text
        };
        var postData = JsonUtility.ToJson(requestBody);
        Debug.Log(urlBase + "BTLogin");
        using (UnityWebRequest www = UnityWebRequest.Put(urlBase + "BTLogin", postData))
        {
            www.method = UnityWebRequest.kHttpVerbPOST;
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", "application/json");
 
            yield return www.SendWebRequest();
 
            if (www.isNetworkError)
            {
                errorLbl.text = www.error;
            }
            else
            {
                ReturnLoginRegister(www.downloadHandler.text);
            }
        }
    }

    private void ReturnLoginRegister(string returnString){
        if(returnString.IndexOf("error") >=0){
            var srvError = JsonUtility.FromJson<ServerError>(returnString);
            errorLbl.text = srvError.error;
        }
        else{
            ScoringManager.Instance.currentUser = JsonUtility.FromJson<UserData>(returnString);
            /*
             * Goes to the game scene
             */
            SceneManager.LoadScene(gameScene);
        }
        usernameTxt.text = string.Empty;
        passwordTxt.text = string.Empty;
    }
}
