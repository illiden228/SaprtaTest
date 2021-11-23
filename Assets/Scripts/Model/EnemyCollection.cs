using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollection
{
    private List<EnemyModel> _enemyModels = new List<EnemyModel>();
    private List<EnemyPresenter> _enemyPresenters = new List<EnemyPresenter>();
    private List<EnemyView> _enemyViews = new List<EnemyView>();

    public void Add(EnemyModel model, EnemyView view, EnemyPresenter presenter)
    {
        _enemyModels.Add(model);
        _enemyPresenters.Add(presenter);
        _enemyViews.Add(view);
    }

    public void UpdatePosition()
    {
        for (int i = 0; i < _enemyModels.Count; i++)
        {
            if(_enemyModels[i].Enabled())
            {
                _enemyModels[i].UpdatePosition();
            }
        }
    }

    public void Enable()
    {
        foreach (var presenter in _enemyPresenters)
        {
            presenter.Enable();
        }
    }

    public void Disable()
    {
        foreach (var presenter in _enemyPresenters)
        {
            presenter.Disable();
        }
    }

    public void Die()
    {
        foreach (var model in _enemyModels)
        {
            model.Die();
        }
    }

    public void Start()
    {
        foreach (var model in _enemyModels)
        {
            model.Restart();
        }
    }
}
