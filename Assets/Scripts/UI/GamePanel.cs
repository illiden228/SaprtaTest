using TMPro;
using UnityEngine;

public class GamePanel : Panel
{
    [SerializeField] private TMP_Text _healthText;
    [SerializeField] private TMP_Text _fruitsText;
    private PlayerModel _model;

    private void Start()
    {
        Initialization();
        Close();
    }

    public void OnGameStart()
    {
        Open();
    }

    public void OnDeath()
    {
        Close();
    }

    public void OnHealthChanged(int health)
    {
        SetNumberToText(_healthText, health);
    }

    public void OnFruitsChanged(int fruits)
    {
        SetNumberToText(_fruitsText, fruits);
    }
}
