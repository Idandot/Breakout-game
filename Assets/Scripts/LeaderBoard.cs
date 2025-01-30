using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderBoard : MonoBehaviour
{
    [SerializeField] List<Text> leaders = new List<Text>();
    public static List<Text> s_Leaders = new List<Text>();

    private void Awake()
    {
        s_Leaders = leaders;
    }

    public void ClearLeaderboard()
    {
        
        BetweenScenesData.instance.ClearData();
    }
}
