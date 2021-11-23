using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public abstract class Panel : MonoBehaviour
{
    protected CanvasGroup CanvasGroup;

    protected void Initialization()
    {
        CanvasGroup = GetComponent<CanvasGroup>();
    }

    protected virtual void Close()
    {
        CanvasGroup.alpha = 0;
        CanvasGroup.interactable = false;
        CanvasGroup.blocksRaycasts = false;
    }

    protected virtual void Open()
    {
        CanvasGroup.alpha = 1;
        CanvasGroup.interactable = true;
        CanvasGroup.blocksRaycasts = true;
    }

    protected void SetNumberToText(TMP_Text textField, int number)
    {
        textField.text = number.ToString();
    }
}
