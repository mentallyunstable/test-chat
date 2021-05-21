using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Component, that controlling chat bottom panel.
/// </summary>
public class BottomPanelView : MonoBehaviour
{
    /// <summary>
    /// Common panel with input field.
    /// </summary>
    public UIObject commonPanel;
    /// <summary>
    /// Panel that enabling when remove initiated.
    /// </summary>
    public UIObject removePanel;

    /// <summary>
    /// Link to the <see cref="MessageRemover"/> instance in the scene.
    /// </summary>
    public MessageRemover remover;

    private void Awake()
    {
        Subscribe();
    }

    /// <summary>
    /// Method that subscribes to the <see cref="MessageRemover"/> events.
    /// </summary>
    private void Subscribe()
    {
        remover.OnMessageRemoveInitiated += Remover_OnMessageDeleteInitiated;
        remover.OnMessageRemoveAccepted += Remover_OnMessageDeleteAccepted;
    }

    private void Remover_OnMessageDeleteAccepted(List<ChatMessage> messages)
    {
        commonPanel.Enable();
        removePanel.Disable();
    }

    private void Remover_OnMessageDeleteInitiated()
    {
        commonPanel.Disable();
        removePanel.Enable();
    }
}
