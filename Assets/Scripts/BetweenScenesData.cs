using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BetweenScenesData : MonoBehaviour
{
    public static BetweenScenesData instance;
    public SavedData data;
    [SerializeField] List<Text> leaderBoards = new List<Text>();

    string playerName;
    string savePath;

    [System.Serializable]
    public class SavedData
    {
        public List<string> names = new List<string>();
        public List<int> scores = new List<int>();
    }

    public void AddNewScore(int score)
    {
        int scrollNameNum = 0;
        int foundNameNum = -1;
        foreach (string name in data.names)
        {
            if (name == playerName)
            {
                foundNameNum = scrollNameNum;
                break;
            }
            scrollNameNum++;
        }
        if (foundNameNum == -1)
        {
            data.names.Add(playerName);
            data.scores.Add(score);
        }
        else
        {
            data.scores[foundNameNum] = score;
        }
        SortMassive();
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        savePath = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            data = JsonUtility.FromJson<SavedData>(json);
            Debug.Log("Loaded!");
        }
        else
        {
            ClearData();
        }
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            UpdateLeaderboard();
        }
    }

    public void GetNewName(string newName)
    {
        playerName = newName;
    }

    public void SaveData()
    {
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(savePath, json);
        Debug.Log("Saved!");
    }

    public void ClearData()
    {
        data = new SavedData();
        for (int i = 0; i < 5; i++)
        {
            data.scores.Add(0);
            data.names.Add("none");
        }
        SaveData();
    }

    public void SortMassive()
    {
        string temporaryName;
        int temporaryScore;

        for (int i = 0; i < data.names.Count - 1; i++)
        {
            for (int j = i + 1; j < data.names.Count; j++)
            {
                if (data.scores[i] < data.scores[j])
                {
                    temporaryName = data.names[i];
                    data.names[i] = data.names[j];
                    data.names[j] = temporaryName;
                    temporaryScore = data.scores[i];
                    data.scores[i] = data.scores[j];
                    data.scores[j] = temporaryScore;
                }
            }
        }
        for (int i = 5; i < data.names.Count; i++)
        {
            data.names.Remove(data.names[i]);
            data.scores.Remove(data.scores[i]);
        }
        for (int i = 0; i < data.names.Count; i++)
        {
            Debug.Log(data.names[i] + data.scores[i]);
        }
        SaveData();
    }

    void UpdateLeaderboard()
    {
        for (int i = 0; i < 5; i++)
        {
            LeaderBoard.s_Leaders[i].text = (i + 1).ToString() + ". " + data.names[i] + " - " + data.scores[i];
        }
    }
}
