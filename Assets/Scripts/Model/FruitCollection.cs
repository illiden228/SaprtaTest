using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitCollection
{
    private List<FruitModel> _fruitModels = new List<FruitModel>();
    private List<FruitPresenter> _fruitPresenters = new List<FruitPresenter>();
    private List<FruitView> _fruitViews = new List<FruitView>();

    public void Add(FruitModel model, FruitView view, FruitPresenter presenter)
    {
        _fruitModels.Add(model);
        _fruitPresenters.Add(presenter);
        _fruitViews.Add(view);
    }

    public void Enable()
    {
        foreach (var presenter in _fruitPresenters)
        {
            presenter.Enable();
        }
    }

    public void Disable()
    {
        foreach (var presenter in _fruitPresenters)
        {
            presenter.Disable();
        }
    }

    public void Hide()
    {
        foreach (var model in _fruitModels)
        {
            model.Destroy();
        }
    }

    public void Show()
    {
        foreach (var model in _fruitModels)
        {
            model.Restart();
        }
    }
}
