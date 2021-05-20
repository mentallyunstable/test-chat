using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarLoader : MonoBehaviour
{
    public List<AvatarData> avatars = new List<AvatarData>();

    public Sprite GetAvatar(string sender) => avatars.Find(e => e.sender == sender).avatar;
}
