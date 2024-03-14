using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BackgroundDatabase : ScriptableObject
{
    public Background[] background;
    public int BackgroundCount
    {
        get
        {
            return background.Length;
        }
    }
    public Background GetBackground(int index)
    {
        return background[index];
    }

}
