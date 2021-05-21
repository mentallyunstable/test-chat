using UnityEngine;

/// <summary>
/// Storing chat member avatar data.
/// </summary>
[System.Serializable]
public struct AvatarData
{
    /// <summary>
    /// Chat member id.
    /// </summary>
    public string sender;
    /// <summary>
    /// Chat member avatar.
    /// </summary>
    public Sprite avatar;
}