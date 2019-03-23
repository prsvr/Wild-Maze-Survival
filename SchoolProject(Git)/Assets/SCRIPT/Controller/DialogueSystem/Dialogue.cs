using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueState
{
    public enum State
    {
        None,
        Opening,
        Middle,
        Hint,
        End
    }

}


[System.Serializable]
public class Dialogue
{
    public string name;
    [TextArea(3, 10)]
    public string[] sentences;
}
