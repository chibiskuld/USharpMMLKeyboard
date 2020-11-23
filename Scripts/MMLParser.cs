
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using System;

public class MMLParser : UdonSharpBehaviour
{
    public bool autoplay;
    public KeyBoard keyboard;
    public float startDelay;
    public int octaveShift = 1; //Because Mabi is fucking weird. 
    public bool debug = false;

    MMLParser[] others;

    //flow control
    int cursor = 0;
    //load flow control
    bool loading = false;
    int defaultLength = 4;
    bool extend = false;
    bool localExtend = false;
    int octave = 4; //octave is calculated into the final note.
    //playback flow control
    bool playing = false;
    int tempo = 120;
    private DateTime resumeTime;
    private DateTime currentTime;
    float volume = .5f;
    bool suppressNext = false;

    //unparsed data
    public string mml;
    private char[] mmlc = null;
    //loaded data
    char[] commands;
    int[] notes;
    int[] values;
    bool[] extended;
    int size;
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
            Load(mml);
            Play();
        }
    }

    private void Update()
    {
        if (loading)
        {
            int i = 0;
            while ( i < 25 && loading)
            {
                ParseMML();
                ++i;
            }
            if (!loading)
            {
                //reset the cursor position for playback.
                cursor = 0;
                if (debug) Debug.Log("Done loading " + size + ":" + mml.Length + " commands");
            }
        }
        else if (playing)
        {
            currentTime = System.DateTime.Now;
            while (currentTime.CompareTo(resumeTime) > 0)
            {
                PlayMML();
            }
        }
    }

    /********************************************************************************************
     * Play Functions:
     *    These look similar to the parse functions, but act very different.
     *    
     ********************************************************************************************/
    void PlayMML()
    {
        if (cursor > size-1)
        {
            Stop();
            return;
        }
        if (debug) Debug.Log(cursor.ToString() + ":" + commands[cursor].ToString() + ":" + notes[cursor].ToString() + ":" + values[cursor].ToString() + ":" + extended[cursor].ToString() + " -- " + octave.ToString() + ":" + volume.ToString() + ":" + tempo.ToString());
        switch (commands[cursor])
        {
            case 'N':
                PlayNote();
                break;
            case '&':
                suppressNext = true;
                break;
            case 'T':
                PlayTempo();
                break;
            case 'R':
                PlayRest();
                break;
            case 'V':
                PlayVolume();
                break;
            default:
                Debug.LogError("This should not happen.");
                //Do nothing, although this should never happen.
                break;
        }
        ++cursor;
    }

    void PlayRest()
    {
        float t = tempo;
        if (values[cursor] > 0 && values[cursor] < 65)
        {
            float n = (60.0f / t);
            n *= 4.0f;
            n /= (float)values[cursor];

            if (extended[cursor])
            {
                n *= 1.5f;
            }

            if (debug) Debug.Log("rest:" + volume + ":" + n);
            resumeTime = resumeTime.AddSeconds(n);
        }
    }

    void PlayNote()
    {
        if (notes[cursor] > -25 && notes[cursor] < 72)//todo: should be mabi note range
        {
            if (values[cursor] > 0 && values[cursor] < 65)//check valid length
            {
                //play note first, math later.
                if (!suppressNext)
                {
                    int note = notes[cursor] + (octaveShift * 12);
                    keyboard.PlayNoteVolume(note, volume);
                    keyboard.HighLightNote(notes[cursor]);
                }
                suppressNext = false;

                float t = tempo;
                float n = (60.0f / t);
                n *= 4.0f;
                n /= (float)values[cursor];

                if (extended[cursor]) n *= 1.5f;

                if (debug) Debug.Log(notes[cursor] + ":" + suppressNext + ":" + volume + ":" + n);

                resumeTime = resumeTime.AddSeconds(n);
            }
        }
    }

    void PlayTempo()
    {
        if (values[cursor] >= 35 || values[cursor] <= 255)
        {
            if (others != null)
            {
                //keep tempo in sync on all tracks.
                for (int i = 0; i < others.Length; i++)
                {
                    others[i].SetTempo(values[cursor]);
                }
            }
            else
            {
                //if it's null, just set it locally.
                tempo = values[cursor];
            }
        }
    }

    void PlayVolume()
    {
        if (values[cursor] > -1 && values[cursor] < 15)
        {
            volume = (float)values[cursor] / 15.0f;
        }
    }

    /********************************************************************************************
    * Parse Functions:
    *    These are used to prepare the data for playback.
    *    
    ********************************************************************************************/
    void ParseMML()
    {
        if (cursor > mmlc.Length - 1)
        {
            loading = false;
            return;
        }
        char command = mmlc[cursor];

        switch (command)
        {
            case '>':
                octave++;
                break;
            case '<':
                octave--;
                break;
            case '&':
                AddCommand('&', 0, 0, false);
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
            case '@':
                ParseTempo();
                break;
            case 'V':
                ParseVolume();
                break;
            case 'L':
                ParseDefaultLength();
                break;
            case 'N':
                ParseDirectNote();
                break;
            default:
                break;
        }
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
        AddCommand('T', 0, t, false);
    }

    void ParseOctave()
    {
        int o = GetNumber(4, false);
        octave = o;
    }

    void ParseVolume()
    {
        int v = GetNumber(8, false);
        AddCommand('V', 0, v, false);
    }
    void ParseDirectNote()
    {
        int n = GetNumber(0, false);
        n -= 24;//My notes start at 0 for C2, MML 0 = C0
        AddCommand('N', n, defaultLength, extend);
    }
    void ParseNote()
    {
        string note = mmlc[cursor] + octave.ToString();
        if (cursor + 1 < mmlc.Length - 1)
        {
            if (mmlc[cursor + 1] == '#')
            {
                note += "s";
                cursor++;
            }
            if (mmlc[cursor + 1] == '+')
            {
                note += "s";
                cursor++;
            }
            if (mmlc[cursor + 1] == '-')
            {
                note += "b";
                cursor++;
            }
        }

        int value = GetNumber(defaultLength, extend);
        if (value == 0) value = defaultLength;
        AddCommand('N', keyboard.GetNoteIndex(note), value, localExtend);
    }

    void ParseRest()
    {
        int value = GetNumber(defaultLength, extend); //length
        AddCommand('R', 0, value, localExtend);
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

    public void Load(string inMML)
    {
        Stop();

        if (inMML.Length < 0)
        {
            return;
        } 

        mml = inMML.ToUpper();
        mmlc = mml.ToCharArray();

        commands = new char[mml.Length];
        notes = new int[mml.Length];
        values = new int[mml.Length];
        extended = new bool[mml.Length];

        loading = true;
        size = 0;
    }

    //resumeTime = System.DateTime.Now.AddSeconds(5);
    public void Play()
    {
        playing = true;
        resumeTime = DateTime.Now.AddSeconds(startDelay);
    }

    public void PlaySync( DateTime syncTime)
    {
        playing = true;
        resumeTime = syncTime.AddSeconds(startDelay);
    }

    public void Pause()
    {
        playing = false;
    }
 
    //be careful with stop, as it will also unload.
    public void Stop()
    {
        size = 0;
        commands = null;
        notes = null;
        values = null;
        extended = null;
        cursor = 0;
        playing = false;
        defaultLength = 4;
        octave = 4;
        tempo = 120;
        resumeTime = System.DateTime.Now;
        localExtend = false;
        extend = false;
        mml = "";
        mmlc = mml.ToCharArray();
    }

    void AddCommand(char command, int note, int value, bool extend)
    {
        commands[size] = command;
        notes[size] = note;
        values[size] = value;
        extended[size] = extend;
        ++size;
    }

    public void SetTempo(int t)
    {
        tempo = t;
    }

    public string GetStatus()
    {

        if ( loading )
        {
            return "Loading " + cursor + "/" + mmlc.Length;
        } else if (playing)
        {
            if (cursor < size)
            {
                return cursor.ToString() + ":" + commands[cursor].ToString() + ":" + notes[cursor].ToString() + ":" + values[cursor].ToString() + ":" + extended[cursor].ToString() + " -- " + octave.ToString() + ":" + volume.ToString() + ":" + tempo.ToString();
            } else
            {
                return "Stopped.";
            }
        } else
        {
            if (size < 1)
            {
                return "Stopped.";
            }else
            {
                return "Ready (" + size + ")";
            }
        }
    }

    public void SetOctaveShift(int o)
    {
        octaveShift = o;
    }
}
