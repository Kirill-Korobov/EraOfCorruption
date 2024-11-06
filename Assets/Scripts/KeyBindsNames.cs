using System;
using UnityEngine;

[Serializable]
public struct KeyBindsNames
{
    public KeyCode bind;
    public string name;

    public KeyBindsNames(KeyCode keyCode, string namekeyCode)
    {
        bind = keyCode;
        name = namekeyCode;
    }
    public void Save(KeyCode keyCode, string namekeyCode)
    {
        bind = keyCode;
        name = namekeyCode;
    }

    public KeyCode ReturnKeyCode()
    {
        return bind;
    }
    public string ReturnString()
    {
        return name;
    }

    public void SaveKeyCode(KeyCode keyCode)
    {
        bind = keyCode;
    }
    public void SaveTexts(string keyCode)
    {
        name = keyCode;
    }
}
