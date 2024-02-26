using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonUIManager : MonoBehaviour
{
    public CanvasGroup LeaderboardPanel;
    public CanvasGroup SavedBuildsPanel;

    public ScoreManager scoreManager;
    public LeaderBoard leaderboard;
    public AudioSource Sound;
    public AudioSource WinSound;
    public Image DisableChallengeImage;

    public Image SoundButton;
    public Sprite MutedSprite;
    public Sprite UnmutedSprite;


    public void SwitchChallengeMode()
    {
        if (scoreManager.ChallengeIsActive)
        {
            scoreManager.ChallengeIsActive = false;
            DisableChallengeImage.enabled = true;
        }
        else
        {
            scoreManager.ChallengeIsActive = true;
            DisableChallengeImage.enabled = false;
        }
    }
    public void MuteSound()
    {
        if (Sound.mute == true)
        {
            Sound.mute = false;
            WinSound.mute = false;
            SoundButton.sprite = UnmutedSprite;
        }
        else
        {
            Sound.mute = true;
            WinSound.mute = true;
            SoundButton.sprite = MutedSprite;
        }
    }

    public void ChangeLeaderBoardType()
    {
        if (leaderboard.currentOpenBoard == 0)
        {
            leaderboard.UpdateLeaderboard();
            leaderboard.currentOpenBoard = 1;
        }
        else if(leaderboard.currentOpenBoard == 1)
        {
            leaderboard.UpdateLeaderboardWins();
            leaderboard.currentOpenBoard = 2;    
        }
        else
        {
            leaderboard.UpdateLeaderboardTotalScore();
            leaderboard.currentOpenBoard = 0;
        }
    }

    public void OpenLeaderboard()
    {
        if (LeaderboardPanel.alpha == 0)
        {
            SavedBuildsPanel.alpha = 0f;
            SavedBuildsPanel.blocksRaycasts = false;
            SavedBuildsPanel.interactable = false;

            LeaderboardPanel.alpha = 1.0f;
            LeaderboardPanel.blocksRaycasts = true;
            LeaderboardPanel.interactable = true;
        }
        else
        {
            CloseLeaderboard();
        }
    }

    public void CloseLeaderboard()
    {
        LeaderboardPanel.alpha = 0f;
        LeaderboardPanel.blocksRaycasts = false;
        LeaderboardPanel.interactable = false;
    }

    public void OpenSavedBuild()
    {
        if (SavedBuildsPanel.alpha == 0)
        {
            LeaderboardPanel.alpha = 0f;
            LeaderboardPanel.blocksRaycasts = false;
            LeaderboardPanel.interactable = false;

            SavedBuildsPanel.alpha = 1.0f;
            SavedBuildsPanel.blocksRaycasts = true;
            SavedBuildsPanel.interactable = true;
        }
        else
        {
            CloseSavedBuild();
        }
    }

    public void CloseSavedBuild()
    {
        SavedBuildsPanel.alpha = 0f;
        SavedBuildsPanel.blocksRaycasts = false;
        SavedBuildsPanel.interactable = false;
    }


    public void CloseGame()
    {
        Application.Quit();
    }

    
}
