using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct UIObject
{
    public Canvas canvas;
    public GraphicRaycaster raycaster;

    public void Enable()
    {
        canvas.enabled = true;
        raycaster.enabled = true;
    }

    public void Disable()
    {
        canvas.enabled = false;
        raycaster.enabled = false;
    }
}