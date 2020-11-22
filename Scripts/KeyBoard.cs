using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class KeyBoard : UdonSharpBehaviour
{
    public AudioClip[] notes;

    private AudioSource audio;

    public Key[] keys;

    bool initialized = false;

    void Start()
    {
        Initialize();
    }

    //left for compatibility.
    public void PlayNoteByName(string note)
    {
        if (!initialized) Initialize();
        int n = GetNoteIndex(name);
        if (n > -1 && n < 84)
        {
            audio.PlayOneShot(notes[n]);
        }
    }

    public void PlayNoteByNameWithVolume(string note, float volume)
    {
        if (!initialized) Initialize();
        int n = GetNoteIndex(name);
        if (n > -1 && n < 84)
        {
            audio.PlayOneShot(notes[n], volume);
        }
    }

    public void PlayNote(int i)
    {
        if (!initialized) Initialize();
        if (i > -1 && i < 84)
        {
            audio.PlayOneShot(notes[i]);
        }
    }

    public void PlayNoteVolume(int i, float v)
    {
        if (!initialized) Initialize();
        if (i > -1 && i < 84)
        {
            audio.PlayOneShot(notes[i], v);
        }
    }


    public int GetNoteIndex(string note)
    {
        char[] noteArr = note.ToCharArray();
        int output = -1;
        if (noteArr.Length > 0)
        {
            switch (noteArr[0])
            {
                case 'C':
                    output = 0;
                    break;
                case 'D':
                    output = 2;
                    break;
                case 'E':
                    output = 4;
                    break;
                case 'F':
                    output = 5;
                    break;
                case 'G':
                    output = 7;
                    break;
                case 'A':
                    output = 9;
                    break;
                case 'B':
                    output = 11;
                    break;
                default:
                    output = -1;
                    break;
            }
        }
        if (noteArr.Length > 1)
        {
            switch (noteArr[1])
            {
                case '0':
                    output -= 24;
                    break;
                case '1':
                    output -= 12;
                    break;
                case '2':
                    break;
                case '3':
                    output += 12;
                    break;
                case '4':
                    output += 24;
                    break;
                case '5':
                    output += 36;
                    break;
                case '6':
                    output += 48;
                    break;
                case '7':
                    output += 60;
                    break;
                case '8':
                    output += 72;
                    break;
                case '9':
                    output += 84;
                    break;
                default:
                    output = -1;
                    break;
            }
        }
        if (noteArr.Length > 2) {
            switch (noteArr[2])
            {
                case 's':
                    output++;
                    break;
                case 'b':
                    output--;
                    break;
            }
        }
        return output;
    }

    void Initialize()
    {
        audio = GetComponent<AudioSource>();
        initialized = true;
    }

    public void HighLightNote(int i)
    {
        i = i - 24;
        if (i >= 0 && i < keys.Length)
        {
            keys[i].Highlight();
        }
    }


    public void HighLightNoteByName(string note)
    {
        if (keys != null)
        {
            for (int i = 0; i < keys.Length; i++)
            {
                if (keys[i].name == note)
                {
                    keys[i].Highlight();
                    return;
                }
            }
        }
    }
}
