using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPanelRestart : MonoBehaviour, IMenu
{
    [SerializeField] private Button btnRestart;
    [SerializeField] private Button btnClose;

    private UIMainManager m_mngr;

    private void Awake()
    {
        btnRestart.onClick.AddListener(OnClickRestartGame);
        btnClose.onClick.AddListener(OnClickClose);
    }

    private void OnDestroy()
    {
        if (btnRestart) btnRestart.onClick.RemoveAllListeners();
        if (btnClose) btnClose.onClick.RemoveAllListeners();
    }

    public void Setup(UIMainManager mngr)
    {
        m_mngr = mngr;
    }

    private void OnClickRestartGame()
    {
        m_mngr.ShowGameMenu();
        
        // Restart the game
        
        
        throw new NotImplementedException();
    }

    private void OnClickClose()
    {
        m_mngr.ShowGameMenu();
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
