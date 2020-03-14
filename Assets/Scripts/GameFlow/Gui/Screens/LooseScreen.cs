using System;
using UnityEngine;
using UnityEngine.UI;

public class LooseScreen : GuiScreen
{
    public static event Action OnRetryButtonClicked;

    [SerializeField] Button retryButton;


    void OnEnable()
    {
        retryButton.onClick.AddListener(RetryButton_OnClick);
    }


    void OnDisable()
    {
        retryButton.onClick.RemoveListener(RetryButton_OnClick);
    }


    void RetryButton_OnClick()
    {
        OnRetryButtonClicked?.Invoke();
    }
}
