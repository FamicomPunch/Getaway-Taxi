using System;
[Serializable]
public class ScoreUpdateRequest{
    public string Username;
    public string SessionToken;
    public float top_score;
    public float top_rounds;
    public float top_time;
    public float total_matches;
    /*
     * Start for the new table
     */
    public string user_id_match;
    public float score;
    public float rounds;
    public float time;
    public float bonus;
}