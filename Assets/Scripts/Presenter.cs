using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Presenter<TView, TModel>
{
    public TView View { get; private set; }
    public TModel Model { get; private set; }

    protected Presenter(TView view, TModel model)
    {
        View = view;
        Model = model;
    }

    public virtual void Enable()
    {

    }

    public virtual void Disable()
    {

    }
}
