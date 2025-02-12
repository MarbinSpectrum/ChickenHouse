using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyMgr : Mgr
{
    public static KeyCode GetKeyCode(KeyBoardValue pKey)
    {
        switch(pKey)
        {
            case KeyBoardValue.RIGHT:
                return KeyCode.D;
            case KeyBoardValue.LEFT:
                return KeyCode.A;
        }

        return KeyCode.None;
    }
}
