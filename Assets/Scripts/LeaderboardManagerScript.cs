using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using TMPro;

public class LeaderboardManager : MonoBehaviour
{
    private string leaderboardFile = "leaderboard.json";
    public GameObject leaderboardEntryPrefab;
    public Transform leaderboardContent;
    public GameObject leaderboardPanel;

    [Serializable]
    public class LeaderboardEntry
    {
        public string playerName;
        public string formattedTime;
        public int diceRolls;
    }

    [Serializable]
    public class LeaderboardData
    {
        public List<LeaderboardEntry> entries = new List<LeaderboardEntry>();
    }

    private LeaderboardData leaderboard = new LeaderboardData();

    private void Awake()
    {
        LoadLeaderboard();
    }

    public void AddNewScore(string playerName, float time, int rolls)
    {
        if (!gameObject.CompareTag("Player")) return;

        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        string formattedTime = $"{minutes:00}:{seconds:00}";

        LeaderboardEntry newEntry = new LeaderboardEntry
        {
            playerName = playerName,
            formattedTime = formattedTime,
            diceRolls = rolls
        };

        leaderboard.entries.Add(newEntry);
        leaderboard.entries.Sort((a, b) => string.Compare(a.formattedTime, b.formattedTime));
        SaveLeaderboard();
    }

    private void SaveLeaderboard()
    {
        string path = Application.persistentDataPath + "/" + leaderboardFile;
        string json = JsonUtility.ToJson(leaderboard, true);
        File.WriteAllText(path, json);
        Debug.Log($"Leaderboard saved to: {path}");

        if (File.Exists(path))
        {
            Debug.Log("Leaderboard file exists and was successfully updated.");
        }
        else
        {
            Debug.LogError("Failed to save leaderboard file!");
        }
    }

    private void LoadLeaderboard()
    {
        string path = Application.persistentDataPath + "/" + leaderboardFile;
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            leaderboard = JsonUtility.FromJson<LeaderboardData>(json);
        }
    }

    public void ShowLeaderboard()
    {
        leaderboardPanel.SetActive(true);
        PopulateLeaderboardUI();
    }

    public void HideLeaderboard()
    {
        leaderboardPanel.SetActive(false);
    }

    private void PopulateLeaderboardUI()
    {
        foreach (Transform child in leaderboardContent)
        {
            Destroy(child.gameObject);
        }

        foreach (var entry in leaderboard.entries)
        {
            GameObject newEntry = Instantiate(leaderboardEntryPrefab, leaderboardContent);
            newEntry.GetComponent<TMP_Text>().text = $"{entry.playerName} - Time: {entry.formattedTime} - Rolls: {entry.diceRolls}";
        }
    }
}
