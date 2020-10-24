using System;
[Serializable]
public class ScoreUpdateRequest{
    public string Username;
    public float Score;
    public float Time;
    public string SessionToken;
}