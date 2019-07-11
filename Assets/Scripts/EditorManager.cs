using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EditorManager : MonoBehaviour
{



#if UNITY_EDITOR

    [MenuItem("Game Settings/Level Up")]
    public static void levelUp()
    {

    }

    [MenuItem("Game Settings/Level Down")]
    public static void levelDown()
    {

    }

    [MenuItem("Game Settings/Level Reset")]
    public static void levelReset()
    {

    }

    [MenuItem("Game Settings/Clear All PlayerPrefs")]
    public static void clearPlayerPrefs()
    {

    }

#endif
}
