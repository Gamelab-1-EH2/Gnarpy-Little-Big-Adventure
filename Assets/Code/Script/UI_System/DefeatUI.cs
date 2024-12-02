using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using System;
using UI_System;

public class DefeatUI : MonoBehaviour
{
    [SerializeField] private float ShowDelay = 5f;
    [SerializeField] private Image _panel;
    [SerializeField] private Button _retryLevelButton;
    [SerializeField] private Button _quitToMainMenu;

    private Coroutine Coroutine;

    private void Awake()
    {
        _retryLevelButton.onClick.AddListener(RetryLevel);
        _quitToMainMenu.onClick.AddListener(GoToMainMenu);
    }

    private void OnDestroy()
    {
        _retryLevelButton.onClick.RemoveListener(RetryLevel);
        _quitToMainMenu.onClick.RemoveListener(GoToMainMenu);
    }

    private void OnDisable()
    {
        if(Coroutine != null)
            StopCoroutine(Coroutine);
        _panel.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _panel.gameObject.SetActive(false);
    }

    public void Show()
    {
        Coroutine = StartCoroutine(ShowUI());
    }

    private IEnumerator ShowUI()
    {
        yield return new WaitForSeconds(ShowDelay);
        _panel.gameObject.SetActive(true);
    }

    private void GoToMainMenu() => GameplayUI.OnMainMenuRequest?.Invoke();
    private void RetryLevel() => GameplayUI.OnReloadLevelRequest?.Invoke();

    
}
