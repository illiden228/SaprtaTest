using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPresenter : Presenter<EnemyView, EnemyModel>
{
    public EnemyPresenter(EnemyView view, EnemyModel model) : base (view, model)
    {

    }

    public override void Enable()
    {
        Model.PositionChanged += OnPositionChanged;
        Model.EnableChanged += OnEnableChanged;
        Model.DirectionChanged += OnDirecitionChanged;
    }

    public override void Disable()
    {
        Model.PositionChanged -= OnPositionChanged;
        Model.EnableChanged -= OnEnableChanged;
        Model.DirectionChanged -= OnDirecitionChanged;
    }

    private void OnPositionChanged(Vector3 position)
    {
        View.SetPosition(position);
    }

    private void OnEnableChanged(bool enabled)
    {
        View.OnEnableChanged(enabled);
    }

    private void OnDirecitionChanged(Vector3 direction)
    {
        View.LookToDirection(direction);
    }
}
