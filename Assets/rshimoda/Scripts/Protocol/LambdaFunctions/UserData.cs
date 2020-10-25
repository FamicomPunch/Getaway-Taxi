using System;

[Serializable]
public class UserData {
    public string user_id;
    public string password;
    public float top_score;
    public float top_rounds;
    public float total_matches;
    public string session_tk;
    public string login_date;
    public UserPlay[] plays;
}

[Serializable]
public class UserPlay{
    public int levels;
    public float score;
    public float time;
}
