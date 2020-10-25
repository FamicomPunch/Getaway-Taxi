using System;

[Serializable]
public class UserData {
    public string user_id;
    public float top_score;
    public float top_rounds;
    public float top_time;
    public float total_matches;
    public string session_tk;
    public string login_date;
    //public UserPlay[] plays;
}

[Serializable]
public class UserPlay{
    public string player_id_match;
    public float rounds;
    public float score;
    public float bonus;
    public float time;
    public string session_tk;
}
