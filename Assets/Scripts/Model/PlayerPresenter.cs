using UnityEngine;

public class PlayerPresenter : Presenter<PlayerView, PlayerModel>
{
    private GamePanel _playerUI;

    public PlayerPresenter(PlayerView view, PlayerModel model, GamePanel plauerUI) : base(view, model)
    {
        _playerUI = plauerUI;
    }

    public override void Enable()
    {
        Model.PositionChanged += OnPositionChanged;
        Model.HealthChanged += OnHealthChanged;
        Model.FruitsChanged += OnFruitsChanged;
        Model.Death += OnPlayerDeath;
        View.Click += OnClick;
        View.FrameUpdated += OnFrameUpdated;
    }

    public override void Disable()
    {
        Model.PositionChanged -= OnPositionChanged;
        Model.HealthChanged -= OnHealthChanged;
        Model.FruitsChanged -= OnFruitsChanged;
        Model.Death -= OnPlayerDeath;
        View.Click -= OnClick;
        View.FrameUpdated -= OnFrameUpdated;
    }

    private void OnPositionChanged(Vector3 position)
    {
        View.SetPosition(position);
    }

    private void OnHealthChanged(int health)
    {
        _playerUI.OnHealthChanged(health);
    }

    private void OnFruitsChanged(int fruits)
    {
        _playerUI.OnFruitsChanged(fruits);
    }

    private void OnPlayerDeath(int score)
    {

    }

    private void OnClick(Vector3 position)
    {
        if(Model.IsAlive())
            Model.SetTarget(position);

        View.LookAt(position);
    }

    private void OnFrameUpdated()
    {
        Model.UpdatePosition();
    }
}
