using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartPanel : Panel
{
    [SerializeField] private Button _startButton;

    public event Action GameStarted;

    private void Start()
    {
        Initialization();
        Open();
    }

    protected override void Open()
    {
        base.Open();
        _startButton.onClick.AddListener(OnStartGame);
    }

    protected override void Close()
    {
        base.Close();
        _startButton.onClick.RemoveListener(OnStartGame);
    }

    private void OnStartGame()
    {
        GameStarted?.Invoke();
        Close();
    }
}
