using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoringManager : Singleton<ScoringManager>
{
    public UserData currentUser;
    private static int state;
    // Start is called before the first frame update
    public string gameTitle = "Getaway Taxi";
    public string gameScene = "Game";
    public string urlBase = "https://rdifbvxni2.execute-api.us-east-1.amazonaws.com/default/";
    
}
