using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGui : MonoBehaviour
{
    public EPanel guiName;

    virtual public void Awake()
    {
        Register();
    }

    public void Register()
    {
        GuiManager.Get().Register(this);
    }
}
