using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class KeyBoard : UdonSharpBehaviour
{
    public AudioClip C2;
    public AudioClip C2s;
    public AudioClip D2;
    public AudioClip D2s;
    public AudioClip E2;
    public AudioClip F2;
    public AudioClip F2s;
    public AudioClip G2;
    public AudioClip G2s;
    public AudioClip A2;
    public AudioClip A2s;
    public AudioClip B2;

    public AudioClip C3;
    public AudioClip C3s;
    public AudioClip D3;
    public AudioClip D3s;
    public AudioClip E3;
    public AudioClip F3;
    public AudioClip F3s;
    public AudioClip G3;
    public AudioClip G3s;
    public AudioClip A3;
    public AudioClip A3s;
    public AudioClip B3;

    public AudioClip C4;
    public AudioClip C4s;
    public AudioClip D4;
    public AudioClip D4s;
    public AudioClip E4;
    public AudioClip F4;
    public AudioClip F4s;
    public AudioClip G4;
    public AudioClip G4s;
    public AudioClip A4;
    public AudioClip A4s;
    public AudioClip B4;

    public AudioClip C5;
    public AudioClip C5s;
    public AudioClip D5;
    public AudioClip D5s;
    public AudioClip E5;
    public AudioClip F5;
    public AudioClip F5s;
    public AudioClip G5;
    public AudioClip G5s;
    public AudioClip A5;
    public AudioClip A5s;
    public AudioClip B5;

    public AudioClip C6;
    public AudioClip C6s;
    public AudioClip D6;
    public AudioClip D6s;
    public AudioClip E6;
    public AudioClip F6;
    public AudioClip F6s;
    public AudioClip G6;
    public AudioClip G6s;
    public AudioClip A6;
    public AudioClip A6s;
    public AudioClip B6;

    public AudioClip C7;
    public AudioClip C7s;
    public AudioClip D7;
    public AudioClip D7s;
    public AudioClip E7;
    public AudioClip F7;
    public AudioClip F7s;
    public AudioClip G7;
    public AudioClip G7s;
    public AudioClip A7;
    public AudioClip A7s;
    public AudioClip B7;

    public AudioClip C8;
    public AudioClip C8s;
    public AudioClip D8;
    public AudioClip D8s;
    public AudioClip E8;
    public AudioClip F8;
    public AudioClip F8s;
    public AudioClip G8;
    public AudioClip G8s;
    public AudioClip A8;
    public AudioClip A8s;
    public AudioClip B8;

    private AudioSource audio;

    public Key[] keys;

    bool initialized = false;

    void Start()
    {
        Initialize();
    }

    public void PlayNote(string note)
    {
        if (!initialized) Initialize();

        switch (note)
        {
            case "C2":
                audio.PlayOneShot(C2);
                break;
            case "C2s":
                audio.PlayOneShot(C2s);
                break;
            case "D2":
                audio.PlayOneShot(D2);
                break;
            case "D2s":
                audio.PlayOneShot(D2s);
                break;
            case "E2":
                audio.PlayOneShot(E2);
                break;
            case "F2":
                audio.PlayOneShot(F2);
                break;
            case "F2s":
                audio.PlayOneShot(F2s);
                break;
            case "G2":
                audio.PlayOneShot(G2);
                break;
            case "G2s":
                audio.PlayOneShot(G2s);
                break;
            case "A2":
                audio.PlayOneShot(A2);
                break;
            case "A2s":
                audio.PlayOneShot(A2s);
                break;
            case "B2":
                audio.PlayOneShot(B2);
                break;

            case "C3":
                audio.PlayOneShot(C3);
                break;
            case "C3s":
                audio.PlayOneShot(C3s);
                break;
            case "D3":
                audio.PlayOneShot(D3);
                break;
            case "D3s":
                audio.PlayOneShot(D3s);
                break;
            case "E3":
                audio.PlayOneShot(E3);
                break;
            case "F3":
                audio.PlayOneShot(F3);
                break;
            case "F3s":
                audio.PlayOneShot(F3s);
                break;
            case "G3":
                audio.PlayOneShot(G3);
                break;
            case "G3s":
                audio.PlayOneShot(G3s);
                break;
            case "A3":
                audio.PlayOneShot(A3);
                break;
            case "A3s":
                audio.PlayOneShot(A3s);
                break;
            case "B3":
                audio.PlayOneShot(B3);
                break;

            case "C4":
                audio.PlayOneShot(C4);
                break;
            case "C4s":
                audio.PlayOneShot(C4s);
                break;
            case "D4":
                audio.PlayOneShot(D4);
                break;
            case "D4s":
                audio.PlayOneShot(D4s);
                break;
            case "E4":
                audio.PlayOneShot(E4);
                break;
            case "F4":
                audio.PlayOneShot(F4);
                break;
            case "F4s":
                audio.PlayOneShot(F4s);
                break;
            case "G4":
                audio.PlayOneShot(G4);
                break;
            case "G4s":
                audio.PlayOneShot(G4s);
                break;
            case "A4":
                audio.PlayOneShot(A4);
                break;
            case "A4s":
                audio.PlayOneShot(A4s);
                break;
            case "B4":
                audio.PlayOneShot(B4);
                break;

            case "C5":
                audio.PlayOneShot(C5);
                break;
            case "C5s":
                audio.PlayOneShot(C5s);
                break;
            case "D5":
                audio.PlayOneShot(D5);
                break;
            case "D5s":
                audio.PlayOneShot(D5s);
                break;
            case "E5":
                audio.PlayOneShot(E5);
                break;
            case "F5":
                audio.PlayOneShot(F5);
                break;
            case "F5s":
                audio.PlayOneShot(F5s);
                break;
            case "G5":
                audio.PlayOneShot(G5);
                break;
            case "G5s":
                audio.PlayOneShot(G5s);
                break;
            case "A5":
                audio.PlayOneShot(A5);
                break;
            case "A5s":
                audio.PlayOneShot(A5s);
                break;
            case "B5":
                audio.PlayOneShot(B5);
                break;

            case "C6":
                audio.PlayOneShot(C6);
                break;
            case "C6s":
                audio.PlayOneShot(C6s);
                break;
            case "D6":
                audio.PlayOneShot(D6);
                break;
            case "D6s":
                audio.PlayOneShot(D6s);
                break;
            case "E6":
                audio.PlayOneShot(E6);
                break;
            case "F6":
                audio.PlayOneShot(F6);
                break;
            case "F6s":
                audio.PlayOneShot(F6s);
                break;
            case "G6":
                audio.PlayOneShot(G6);
                break;
            case "G6s":
                audio.PlayOneShot(G6s);
                break;
            case "A6":
                audio.PlayOneShot(A6);
                break;
            case "A6s":
                audio.PlayOneShot(A6s);
                break;
            case "B6":
                audio.PlayOneShot(B6);
                break;

            case "C7":
                audio.PlayOneShot(C7);
                break;
            case "C7s":
                audio.PlayOneShot(C7s);
                break;
            case "D7":
                audio.PlayOneShot(D7);
                break;
            case "D7s":
                audio.PlayOneShot(D7s);
                break;
            case "E7":
                audio.PlayOneShot(E7);
                break;
            case "F7":
                audio.PlayOneShot(F7);
                break;
            case "F7s":
                audio.PlayOneShot(F7s);
                break;
            case "G7":
                audio.PlayOneShot(G7);
                break;
            case "G7s":
                audio.PlayOneShot(G7s);
                break;
            case "A7":
                audio.PlayOneShot(A7);
                break;
            case "A7s":
                audio.PlayOneShot(A7s);
                break;
            case "B7":
                audio.PlayOneShot(B7);
                break;

            case "C8":
                audio.PlayOneShot(C8);
                break;
            case "C8s":
                audio.PlayOneShot(C8s);
                break;
            case "D8":
                audio.PlayOneShot(D8);
                break;
            case "D8s":
                audio.PlayOneShot(D8s);
                break;
            case "E8":
                audio.PlayOneShot(E8);
                break;
            case "F8":
                audio.PlayOneShot(F8);
                break;
            case "F8s":
                audio.PlayOneShot(F8s);
                break;
            case "G8":
                audio.PlayOneShot(G8);
                break;
            case "G8s":
                audio.PlayOneShot(G8s);
                break;
            case "A8":
                audio.PlayOneShot(A8);
                break;
            case "A8s":
                audio.PlayOneShot(A8s);
                break;
            case "B8":
                audio.PlayOneShot(B8);
                break;

            default:
                audio.Play(); //play default note.
                break;
        }
    }

    public void PlayNoteVolume(string note, float volume)
    {
        if (!initialized) Initialize();
        switch (note)
        {
            case "C2":
                audio.PlayOneShot(C2,volume);
                break;
            case "C2s":
                audio.PlayOneShot(C2s,volume);
                break;
            case "D2":
                audio.PlayOneShot(D2,volume);
                break;
            case "D2s":
                audio.PlayOneShot(D2s,volume);
                break;
            case "E2":
                audio.PlayOneShot(E2,volume);
                break;
            case "F2":
                audio.PlayOneShot(F2,volume);
                break;
            case "F2s":
                audio.PlayOneShot(F2s,volume);
                break;
            case "G2":
                audio.PlayOneShot(G2,volume);
                break;
            case "G2s":
                audio.PlayOneShot(G2s,volume);
                break;
            case "A2":
                audio.PlayOneShot(A2,volume);
                break;
            case "A2s":
                audio.PlayOneShot(A2s,volume);
                break;
            case "B2":
                audio.PlayOneShot(B2,volume);
                break;

            case "C3":
                audio.PlayOneShot(C3,volume);
                break;
            case "C3s":
                audio.PlayOneShot(C3s,volume);
                break;
            case "D3":
                audio.PlayOneShot(D3,volume);
                break;
            case "D3s":
                audio.PlayOneShot(D3s,volume);
                break;
            case "E3":
                audio.PlayOneShot(E3,volume);
                break;
            case "F3":
                audio.PlayOneShot(F3,volume);
                break;
            case "F3s":
                audio.PlayOneShot(F3s,volume);
                break;
            case "G3":
                audio.PlayOneShot(G3,volume);
                break;
            case "G3s":
                audio.PlayOneShot(G3s,volume);
                break;
            case "A3":
                audio.PlayOneShot(A3,volume);
                break;
            case "A3s":
                audio.PlayOneShot(A3s,volume);
                break;
            case "B3":
                audio.PlayOneShot(B3,volume);
                break;

            case "C4":
                audio.PlayOneShot(C4,volume);
                break;
            case "C4s":
                audio.PlayOneShot(C4s,volume);
                break;
            case "D4":
                audio.PlayOneShot(D4,volume);
                break;
            case "D4s":
                audio.PlayOneShot(D4s,volume);
                break;
            case "E4":
                audio.PlayOneShot(E4,volume);
                break;
            case "F4":
                audio.PlayOneShot(F4,volume);
                break;
            case "F4s":
                audio.PlayOneShot(F4s,volume);
                break;
            case "G4":
                audio.PlayOneShot(G4,volume);
                break;
            case "G4s":
                audio.PlayOneShot(G4s,volume);
                break;
            case "A4":
                audio.PlayOneShot(A4,volume);
                break;
            case "A4s":
                audio.PlayOneShot(A4s,volume);
                break;
            case "B4":
                audio.PlayOneShot(B4,volume);
                break;

            case "C5":
                audio.PlayOneShot(C5,volume);
                break;
            case "C5s":
                audio.PlayOneShot(C5s,volume);
                break;
            case "D5":
                audio.PlayOneShot(D5,volume);
                break;
            case "D5s":
                audio.PlayOneShot(D5s,volume);
                break;
            case "E5":
                audio.PlayOneShot(E5,volume);
                break;
            case "F5":
                audio.PlayOneShot(F5,volume);
                break;
            case "F5s":
                audio.PlayOneShot(F5s,volume);
                break;
            case "G5":
                audio.PlayOneShot(G5,volume);
                break;
            case "G5s":
                audio.PlayOneShot(G5s,volume);
                break;
            case "A5":
                audio.PlayOneShot(A5,volume);
                break;
            case "A5s":
                audio.PlayOneShot(A5s,volume);
                break;
            case "B5":
                audio.PlayOneShot(B5,volume);
                break;

            case "C6":
                audio.PlayOneShot(C6,volume);
                break;
            case "C6s":
                audio.PlayOneShot(C6s,volume);
                break;
            case "D6":
                audio.PlayOneShot(D6,volume);
                break;
            case "D6s":
                audio.PlayOneShot(D6s,volume);
                break;
            case "E6":
                audio.PlayOneShot(E6,volume);
                break;
            case "F6":
                audio.PlayOneShot(F6,volume);
                break;
            case "F6s":
                audio.PlayOneShot(F6s,volume);
                break;
            case "G6":
                audio.PlayOneShot(G6,volume);
                break;
            case "G6s":
                audio.PlayOneShot(G6s,volume);
                break;
            case "A6":
                audio.PlayOneShot(A6,volume);
                break;
            case "A6s":
                audio.PlayOneShot(A6s,volume);
                break;
            case "B6":
                audio.PlayOneShot(B6,volume);
                break;

            case "C7":
                audio.PlayOneShot(C7,volume);
                break;
            case "C7s":
                audio.PlayOneShot(C7s,volume);
                break;
            case "D7":
                audio.PlayOneShot(D7,volume);
                break;
            case "D7s":
                audio.PlayOneShot(D7s,volume);
                break;
            case "E7":
                audio.PlayOneShot(E7,volume);
                break;
            case "F7":
                audio.PlayOneShot(F7,volume);
                break;
            case "F7s":
                audio.PlayOneShot(F7s,volume);
                break;
            case "G7":
                audio.PlayOneShot(G7,volume);
                break;
            case "G7s":
                audio.PlayOneShot(G7s,volume);
                break;
            case "A7":
                audio.PlayOneShot(A7,volume);
                break;
            case "A7s":
                audio.PlayOneShot(A7s,volume);
                break;
            case "B7":
                audio.PlayOneShot(B7,volume);
                break;

            case "C8":
                audio.PlayOneShot(C8,volume);
                break;
            case "C8s":
                audio.PlayOneShot(C8s,volume);
                break;
            case "D8":
                audio.PlayOneShot(D8,volume);
                break;
            case "D8s":
                audio.PlayOneShot(D8s,volume);
                break;
            case "E8":
                audio.PlayOneShot(E8,volume);
                break;
            case "F8":
                audio.PlayOneShot(F8,volume);
                break;
            case "F8s":
                audio.PlayOneShot(F8s,volume);
                break;
            case "G8":
                audio.PlayOneShot(G8,volume);
                break;
            case "G8s":
                audio.PlayOneShot(G8s,volume);
                break;
            case "A8":
                audio.PlayOneShot(A8,volume);
                break;
            case "A8s":
                audio.PlayOneShot(A8s,volume);
                break;
            case "B8":
                audio.PlayOneShot(B8,volume);
                break;

            default:
                audio.Play(); //play default note.
                break;
        }
    }

    void Initialize()
    {
        audio = GetComponent<AudioSource>();
        initialized = true;
        keys = transform.GetComponentsInChildren<Key>();
    }

    public void HighLightNote(string note)
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
