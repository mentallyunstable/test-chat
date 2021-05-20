using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BottomPanelView : MonoBehaviour
{
    public UIObject commonPanel;
    public UIObject removePanel;

    public MessageRemover remover;

    private void Awake()
    {
        remover.OnMessageDeleteInitiated += Remover_OnMessageDeleteInitiated;
        remover.OnMessageDeleteAccepted += Remover_OnMessageDeleteAccepted;
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
