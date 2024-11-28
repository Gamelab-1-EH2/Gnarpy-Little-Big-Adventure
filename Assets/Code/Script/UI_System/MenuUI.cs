using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    public static Action OnNewGameRequest;

    [SerializeField] private Button _newGameButton;

    private void Awake()
    {
        _newGameButton.onClick.AddListener(RequestNewGame);
    }

    public void RequestNewGame() => OnNewGameRequest?.Invoke();
}
