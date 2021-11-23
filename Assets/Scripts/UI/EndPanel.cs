using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndPanel : Panel
{
    [SerializeField] private TMP_Text _currentScoreText;
    [SerializeField] private TMP_Text _maxScoreText;
    [SerializeField] private Button _restartButton;
    private const string HighScore = "HighScore";

    public event Action RestartClick;

    private void Start()
    {
        Initialization();
        Close();
    }

    protected override void Open()
    {
        base.Open();
        _restartButton.onClick.AddListener(OnStartGame);
    }

    protected override void Close()
    {
        base.Close();
        _restartButton.onClick.RemoveListener(OnStartGame);
    }

    public void OnDeath(int score)
    {
        Open();
        SetNumberToText(_currentScoreText, score);

        if(PlayerPrefs.HasKey(HighScore))
        {
            if (score < PlayerPrefs.GetInt(HighScore))
            {
                SetNumberToText(_maxScoreText, PlayerPrefs.GetInt(HighScore));
                return;
            }
            else
            {
                SetNumberToText(_maxScoreText, score);
            }
        }
        PlayerPrefs.SetInt(HighScore, score);
    }

    public void OnStartGame()
    {
        RestartClick?.Invoke();
        Close();
    }
}
