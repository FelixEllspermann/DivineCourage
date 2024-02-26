using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Dan.Main;
using System.Security.Cryptography.X509Certificates;
using UnityEngine.SocialPlatforms.Impl;


public class LeaderBoard : MonoBehaviour
{
 

    public string UserName;
    public int Score;
    public int TotalWins;

    public int currentOpenBoard = 0;

    public ScoreManager sm;

    public TextMeshProUGUI Header;

    [SerializeField]
    private List<TextMeshProUGUI> names;
    [SerializeField]
    private List<TextMeshProUGUI> scores;

    public AudioSource WinSound;


    private string publicLeaderboardKey = "ad54221f8a3f875553a31e4c536208d6bd7e72e446273fcd2ba9206191588cd2";
    private string winsLeaderboardKey = "4dfcc2f3afa157f13d506af7120e440e30c62a8b39f35a6f06e9d82aa42cc521";
    private string TotalPointsKey = "8b6d07da891f8ba2526d0d1bc24e11816a825f1f75c26d1ff46d7cf2c2a57bfc";

    private void Start()
    {
        InvokeRepeating("UpdateLeaderboard", 60f, 60f);
    }


    public void SubmitScore()
    {
        UserName = SaveSystem.LoadName();
        Score = sm.RawScore;

        UpdateLeaderboardEntryIfHigher(UserName, Score, publicLeaderboardKey);
        SetTotalWins();
        SetTotalPoints();
        WinSound.Play();
    }

    public void SetTotalPoints()
    {
        string UserName = SaveSystem.LoadName(); // Annahme, dass dies den aktuellen Benutzernamen lädt

        // Abrufen des Leaderboards
        LeaderboardCreator.GetLeaderboard(TotalPointsKey, entries =>
        {
            Dan.Models.Entry? existingEntry = null;

            // Durchsuche die Einträge, um zu sehen, ob einer mit dem Benutzernamen bereits existiert
            foreach (var entry in entries)
            {
                if (entry.Username == UserName)
                {
                    existingEntry = entry;
                    break;
                }
            }

            if (existingEntry != null)
            {
                
                int newScore = existingEntry.Value.Score + sm.RawScore;
                LeaderboardCreator.UploadNewEntry(TotalPointsKey, UserName, newScore, success =>
                {
                    if (success)
                    {
                        UploadNewEntry(TotalPointsKey, UserName, Score);
                        
                    }
                }, error =>
                {
                    Debug.LogError("Fehler beim Aktualisieren des Eintrags: " + error);
                });
            }
            else
            {
                LeaderboardCreator.UploadNewEntry(TotalPointsKey, UserName, sm.RawScore, success =>
                {
                    if (success)
                    {
                        Debug.Log("Neuer Eintrag erfolgreich erstellt.");
                    }
                }, error =>
                {
                    Debug.LogError("Fehler beim Erstellen des neuen Eintrags: " + error);
                });
            }
        });
    }

    public void SetTotalWins()
    {
        string UserName = SaveSystem.LoadName(); // Annahme, dass dies den aktuellen Benutzernamen lädt

        // Abrufen des Leaderboards
        LeaderboardCreator.GetLeaderboard(winsLeaderboardKey, entries =>
        {
            Dan.Models.Entry? existingEntry = null;

            // Durchsuche die Einträge, um zu sehen, ob einer mit dem Benutzernamen bereits existiert
            foreach (var entry in entries)
            {
                if (entry.Username == UserName)
                {
                    existingEntry = entry;
                    break;
                }
            }

            if (existingEntry != null)
            {
                // Wenn der Eintrag existiert, erhöhe den Score um 1 und lade ihn hoch
                int newScore = existingEntry.Value.Score + 1;
                LeaderboardCreator.UploadNewEntry(winsLeaderboardKey, UserName, newScore, success =>
                {

                    if (success)
                    {
                        UploadNewEntry(winsLeaderboardKey, UserName, newScore);                        
                    }
                }, error =>
                {
                    Debug.LogError("Fehler beim Aktualisieren des Eintrags: " + error);
                });
            }
            else
            {
                // Wenn kein Eintrag existiert, erstelle einen neuen Eintrag mit einem Score von 1
                LeaderboardCreator.UploadNewEntry(winsLeaderboardKey, UserName, 1, success =>
                {
                    if (success)
                    {
                        Debug.Log("Neuer Eintrag erfolgreich erstellt.");
                    }
                }, error =>
                {
                    Debug.LogError("Fehler beim Erstellen des neuen Eintrags: " + error);
                });
            }
        });
    }


    public void UpdateLeaderboard()
    {
        LeaderboardCreator.GetLeaderboard(publicLeaderboardKey, ((msg) =>
        {
            Header.text = "Leaderboard";
            for (int i = 0; i < names.Count; i++)
            {
                names[i].text = "Name #" + i;
                scores[i].text = "0";
            }

            for (int i = 0; i < msg.Length; i++)
            {
                
                names[i].text = msg[i].Username;
                scores[i].text = msg[i].Score.ToString();
                
            }
        }));        
    }


    public void UpdateLeaderboardWins()
    {
        LeaderboardCreator.GetLeaderboard(winsLeaderboardKey, ((msg) =>
        {
            Header.text = "Total Wins";
            for (int i = 0; i < names.Count; i++)
            {
                names[i].text = "Name #" + i;
                scores[i].text = "0";
            }
            for (int i = 0; i < msg.Length; i++)
            {
                names[i].text = msg[i].Username;
                scores[i].text = msg[i].Score.ToString();
            }
        }));
    }

    public void UpdateLeaderboardTotalScore()
    {
        LeaderboardCreator.GetLeaderboard(TotalPointsKey, ((msg) =>
        {
            Header.text = "Total Points";
            for (int i = 0; i < names.Count; i++)
            {
                names[i].text = "Name #" + i;
                scores[i].text = "0";
            }
            for (int i = 0; i < msg.Length; i++)
            {
                names[i].text = msg[i].Username;
                scores[i].text = msg[i].Score.ToString();
            }
        }));
    }

    public void getLeaderboard(string Key)
    {
        LeaderboardCreator.GetLeaderboard(Key, ((msg) =>
        {
            for (int i = 0; i < msg.Length; i++) 
            {
                names[i].text = msg[i].Username;
                scores[i].text = msg[i].Score.ToString();
            }   
        }));
    }


    public void UpdateLeaderboardEntryIfHigher(string username, int newScore, string key)
    {
        // Erstelle eine Suchanfrage, um nur den Eintrag für den gegebenen Benutzernamen zu erhalten
        Dan.Models.LeaderboardSearchQuery query = new()
        {
            Username = username,
            Skip = 0, // Wir überspringen keine Einträge
            Take = 1, // Wir möchten nur einen Eintrag zurück
                      // Du musst möglicherweise TimePeriod setzen, falls erforderlich
        };

        // Rufe das Leaderboard mit der Suchanfrage auf
        LeaderboardCreator.GetLeaderboard(key, false, query, (entries) =>
        {
            if (entries != null && entries.Length > 0)
            {
                var existingEntry = entries[0];
                // Überprüfe, ob der neue Score höher ist als der vorhandene Score
                if (newScore > existingEntry.Score)
                {
                    UploadNewEntry(key, UserName, Score);
                    
                }
                else
                {
                    Debug.Log("Der neue Score ist nicht höher als der vorhandene. Kein Update notwendig.");
                }
            }
            else
            {
                // Kein vorhandener Eintrag gefunden, lade den neuen Eintrag hoch
                UploadNewEntry(key, username, newScore);
            }
        });
    }

    private void UploadNewEntry(string key, string username, int score)
    {
        LeaderboardCreator.UploadNewEntry(key, username, score, (success) =>
        {
            if (success)
            {
                Debug.Log("Score erfolgreich aktualisiert.");
                getLeaderboard(key); // Aktualisiere das Leaderboard im UI
            }
            else
            {
                Debug.LogError("Fehler beim Hochladen des neuen Scores.");
            }
        }, (error) =>
        {
            Debug.LogError("Fehler beim Hochladen des neuen Scores: " + error);
        });
    }

    public void SetLeaderboardEntry(string username, int score, string Key)
    {
        UpdateLeaderboardEntryIfHigher(UserName, Score, publicLeaderboardKey);
    }

    
}
