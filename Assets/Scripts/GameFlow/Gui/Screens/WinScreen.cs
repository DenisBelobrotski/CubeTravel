using System;
using UnityEngine;
using UnityEngine.UI;

public class WinScreen : GuiScreen
{
    public static event Action OnContinueButtonClicked;

    [SerializeField] Button continueButton;


    void OnEnable()
    {
        continueButton.onClick.AddListener(ContinueButton_OnClick);
    }


    void OnDisable()
    {
        continueButton.onClick.RemoveListener(ContinueButton_OnClick);
    }


    void ContinueButton_OnClick()
    {
        OnContinueButtonClicked?.Invoke();
    }
}
