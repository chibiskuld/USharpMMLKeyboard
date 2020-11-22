#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioPlayTest : MonoBehaviour
{
    KeyBoard keyboard;
    public string note;

    public void Play()
    {
        if (keyboard == null)
        {
            keyboard = GetComponent<KeyBoard>();
        }
        if (keyboard != null)
        {
            keyboard.PlayNote(note);
        }
    }
}
#endif