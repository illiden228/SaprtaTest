using UnityEngine;

public class FruitPresenter : Presenter<FruitView, FruitModel>
{
    public FruitPresenter(FruitView view, FruitModel model) : base(view, model) {}

    public override void Enable()
    {
        Model.PositionChanged += OnPositionChanged;
        Model.EnableChanged += OnEnableChanged;
    }

    public override void Disable()
    {
        Model.PositionChanged -= OnPositionChanged;
        Model.EnableChanged -= OnEnableChanged;
    }

    private void OnPositionChanged(Vector3 position)
    {
        View.SetPosition(position);
    }

    private void OnEnableChanged(bool enabled)
    {
        View.OnEnableChanged(enabled);
    }
}
