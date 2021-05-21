using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that loading chat members avatars.
/// </summary>
public class AvatarLoader : MonoBehaviour
{
    /// <summary>
    /// All avatars data.
    /// </summary>
    public List<AvatarData> avatars = new List<AvatarData>();

    /// <summary>
    /// Returning chat member avatar by id.
    /// </summary>
    /// <param name="sender">Chat member id.</param>
    /// <returns></returns>
    public Sprite GetAvatar(string sender) => avatars.Find(e => e.sender == sender).avatar;
}
