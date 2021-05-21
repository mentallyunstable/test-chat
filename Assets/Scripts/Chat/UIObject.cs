using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Utility struct for enabling and disabling the UI component in the scene.
/// </summary>
[System.Serializable]
public struct UIObject
{
    /// <summary>
    /// Link to the <see cref="Canvas"/> component in the scene.
    /// </summary>
    public Canvas canvas;
    /// <summary>
    /// Link to the <see cref="GraphicRaycaster"/> component in the scene.
    /// </summary>
    public GraphicRaycaster raycaster;

    /// <summary>
    /// Enabling the <see cref="canvas"/> and <see cref="raycaster"/> components.
    /// </summary>
    public void Enable()
    {
        canvas.enabled = true;
        raycaster.enabled = true;
    }

    /// <summary>
    /// Disabling the <see cref="canvas"/> and <see cref="raycaster"/> components.
    /// </summary>
    public void Disable()
    {
        canvas.enabled = false;
        raycaster.enabled = false;
    }
}