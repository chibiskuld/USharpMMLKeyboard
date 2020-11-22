
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using System;

public class MMLParser : UdonSharpBehaviour
{
    [UdonSynced]
    public string mml;
    [UdonSynced]
    public bool playing = false;
    [UdonSynced]
    int cursor = 0;
    [UdonSynced]
    int seq = 0;//if it is > pseq, we need to net init.
    int pseq = 0;

    private char[] mmlc = null;

    public bool autoplay;
    public KeyBoard keyboard;
    public float startDelay;
    public int octaveShift = 1; //Because Mabi is fucking weird. 
    public bool debug = false;
    MMLParser[] others;

    //flow control
    private DateTime resumeTime;
    private DateTime currentTime;
    [UdonSynced]
    public int tempo = 120;
    [UdonSynced]
    public int octave = 4;
    [UdonSynced]
    public int defaultLength = 4;
    [UdonSynced]
    public bool extend = false;
    bool localExtend = false;
    int lastNoteLength = 4;
    bool suppressNext = false;
    [UdonSynced]
    public int volume = 8;
    /*
    Reference:
    T = tempo
    O = octave
    V = volume (not implemented, yet?)
    (MML@) = ignore
    a,b,c,d,e,f,g Notes
        #/+ = sharp
        - = flat (Not yet implemented)
    l = defaultLength
    r Rest
    & Extend (extend note)
    . (half extend a note)
    N = raw note from octave 0 and up. 0 = C0, 1 = C1
    1/2/4,etc - Note Length to ignore for now, but do a wait lock.
    no number = 1
    */

    void Start()
    {
        others = transform.parent.GetComponentsInChildren<MMLParser>();
        if (autoplay)
        {
            Play(mml);
        }
    }

    private void Update()
    {
        if (playing)
        {
            if ( seq > pseq)
            {
                NetInit();
            }
            currentTime = System.DateTime.Now;
            while (currentTime.CompareTo(resumeTime) > 0)
            {
                ParseMML();
            }
        }
    }

    void ParseMML()
    {
        if (cursor > mmlc.Length - 1)
        {
            Stop();
            return;
        }
        char command = mmlc[cursor];
        if (debug) Debug.Log("(" + cursor.ToString() + ") " + command.ToString());
        switch (command)
        {
            case '>':
                octave++;
                break;
            case '<':
                octave--;
                break;
            case '&':
                suppressNext = true;
                break;
            case 'O':
                ParseOctave();
                break;
            case 'T':
                ParseTempo();
                break;
            case 'R':
                ParseRest();
                break;
            case 'A':
                ParseNote();
                break;
            case 'B':
                ParseNote();
                break;
            case 'C':
                ParseNote();
                break;
            case 'D':
                ParseNote();
                break;
            case 'E':
                ParseNote();
                break;
            case 'F':
                ParseNote();
                break;
            case 'G':
                ParseNote();
                break;
            case 'L':
                ParseDefaultLength();
                break;
            case '@':
                ParseTempo();
                break;
            case 'V':
                ParseVolume();
                break;
            default:
                //Do nothing
                break;
        }
        if (octave < 1) octave = 1;
        if (octave > 7) octave = 7;
        ++cursor;
    }

    void ParseDefaultLength()
    {
        defaultLength = GetNumber(4,extend);
        extend = localExtend;
    }

    void ParseTempo()
    {
        int t = GetNumber(120,false);
        if (t >= 35 || t <= 255)
        {
            if (others != null)
            {
                //keep tempo in sync on all tracks.
                for (int i = 0; i < others.Length; i++)
                {
                    others[i].SetTempo(t);
                }
            } else
            {
                //if it's null, just set it locally.
                tempo = t;
            }
        }
    }

    void ParseOctave()
    {
        octave = GetNumber(4, false);
    }

    void ParseRest()
    {
        //it's just adding time to wait.
        float t = tempo;
        int l = GetNumber(defaultLength,extend); //length
        if (l > 0 && l < 65)
        {
            if (l == 0) l = defaultLength;

            lastNoteLength = l;
            float n = (60.0f / t);
            n *= 4.0f;
            n /= (float)l;

            if (localExtend)
            {
                n *= 1.5f;
            }

            if (debug) Debug.Log("rest:" + volume + ":" + n);
            resumeTime = resumeTime.AddSeconds(n);
        }
    }

    void ParseVolume()
    {
        int v = GetNumber(8, false);
        if ( v > -1 && v < 16)
        {
            volume = v;
        }
    }
    void ParseNote()
    {
        string note = FixNote(mmlc[cursor] + ( octave + octaveShift).ToString());
        if (cursor + 1 < mmlc.Length - 1)
        {
            if (mmlc[cursor + 1] == '#')
            {
                note += "s";
                note = FixNote(note);
                cursor++;
            }
            if (mmlc[cursor + 1] == '+')
            {
                note += "s";
                note = FixNote(note);
                cursor++;
            }
        }
        
        float t = tempo;
        int l = GetNumber(defaultLength, extend); //length
        //if it's not valid, apparently MML just throws it away.
        if (l > 0 && l < 65)
        {
            if (l == 0) l = defaultLength;
            lastNoteLength = l;

            float n = (60.0f / t);
            n *= 4.0f;
            n /= (float)l;

            if (localExtend)
            {
                n *= 1.5f;
            }

            if (!suppressNext)
            {
                keyboard.PlayNoteVolume(note, ((float)volume) / 16.0f);
                keyboard.HighLightNote(note);
                if (debug) Debug.Log(note + "-O:" + volume + ":" + n);
            } else
            {
                if (debug) Debug.Log(note + "-X:" + volume + ":" + n);
            }
            suppressNext = false;

            resumeTime = resumeTime.AddSeconds(n);
        }
    }

    string FixNote(string note)
    {
        string output = note;
        switch (note)
        {
            case "B1s":
                output = "C2";
                break;
            case "B2s":
                output = "C3";
                break;
            case "B3s":
                output = "C4";
                break;
            case "B4s":
                output = "C5";
                break;
            case "B5s":
                output = "C6";
                break;
            case "B6s":
                output = "C7";
                break;
            case "B7s":
                output = "C8";
                break;

            case "E2s":
                output = "F2";
                break;
            case "E3s":
                output = "F3";
                break;
            case "E4s":
                output = "F4";
                break;
            case "E5s":
                output = "F5";
                break;
            case "E6s":
                output = "F6";
                break;
            case "E7s":
                output = "F7";
                break;
            case "E8s":
                output = "F8";
                break;

            default:
                output = note;
                break;
        }
        return output;
    }

    int GetNumber( int def, bool defExtend )
    {
        string outstr = "";
        char c;
        bool parsing = true;
        localExtend = false;
        int output = def;

        while (parsing)
        {
            if (cursor + 1 > mmlc.Length - 1)
            {
                parsing = false;
            } else
            {
                c = mmlc[cursor + 1];

                if (!Char.IsNumber(c))
                {
                    parsing = false;
                } else
                {
                    outstr += c.ToString();
                    cursor++;
                }
            }
        }

        if (debug) Debug.Log(outstr);

        if ( outstr.Length > 0 && outstr.Length < 9)
        {
            output = int.Parse(outstr);
            if (cursor + 1 < mmlc.Length)
            {
                if (mmlc[cursor + 1] == '.')
                {
                    cursor++;
                    if (debug) Debug.Log("ext");
                    localExtend = true;
                }
            }
        }
        else
        {
            localExtend = defExtend;
        }
        if (debug) Debug.Log(output);
        return output;
    }

    //resumeTime = System.DateTime.Now.AddSeconds(5);
    public void Play(string inMml)
    {
        Stop();
        mml = inMml.ToUpper();
        mmlc = mml.ToCharArray();
        playing = true;
        seq++;
        pseq = seq;
        resumeTime = DateTime.Now.AddSeconds(startDelay);
    }

    public void PlaySync(string inMml, DateTime syncTime)
    {
        if (inMml.Length > 0)
        {
            mml = inMml.ToUpper();
            mmlc = mml.ToCharArray();
            cursor = 0;
            playing = true;
            resumeTime = syncTime.AddSeconds(startDelay);
        }
        else
        {
            Stop();
        }
    }
 
    public void Stop()
    {
        cursor = 0;
        playing = false;
        defaultLength = 4;
        octave = 4;
        lastNoteLength = 4;
        tempo = 120;
        resumeTime = System.DateTime.Now;
        localExtend = false;
        extend = false;
        mml = "";
        mmlc = mml.ToCharArray();
    }

    public void SetTempo(int t)
    {
        tempo = t;
    }

    void NetInit()
    {
        mmlc = mml.ToCharArray();
        pseq = seq;
        resumeTime = DateTime.Now.AddSeconds(startDelay);
    }
}
