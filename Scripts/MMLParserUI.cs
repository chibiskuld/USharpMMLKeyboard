using System;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using UnityEngine.UI;

public class MMLParserUI : UdonSharpBehaviour
{
    [UdonSynced]
    int state = 0;
    [UdonSynced]
    int preset = 0;
    //0=ready
    //1=loading
    //2=playing
    //3=stop
    //4+=load preset 

    //Merlin about UdonSync and Strings: nope it'll just work, it has an upper limit of around 70-80 characters before it explodes though
    //synced play system.
    string track1mml;
    string track2mml;
    string track3mml;
    string track4mml;
    string track5mml;

    int pState = 0;
    int uI = 0;
    int octaveShift = 0;

    public Text status;
    public Text octave;
    public Text debug;

    public MMLParser track1;
    public InputField track1Text;
    public MMLParser track2;
    public InputField track2Text;
    public MMLParser track3;
    public InputField track3Text;
    public MMLParser track4;
    public MMLParser track5;
    public PresetLoader[] presets;

    public void Start()
    {
        octave.text = octaveShift.ToString();
        track1mml = "";
        track2mml = "";
        track3mml = "";
        track4mml = "";
        track5mml = "";
    }

    public void Update()
    {
        debug.text = state.ToString() + ":" + 
                    track1mml.Length + ":" + 
                    track2mml.Length + ":" + 
                    track3mml.Length;
        if (state != pState)
        {
            switch (state)
            {
                default://Nothing/Invalid
                    break;
                case 0://Idle
                    break;
                case 1:
                    Load();
                    break;
                case 2:
                    Play();
                    break;
                case 3:
                    Stop();
                    break;
            }
            if ( state > 3)
            {
                LoadPreset();
            }
            pState = state;
        }

        if (uI == 0)
        {
            string stat = "Status:\n";
            stat += "1: " + track1.GetStatus() + "\n";
            stat += "2: " + track2.GetStatus() + "\n";
            stat += "3: " + track3.GetStatus() + "\n";
            status.text = stat;
        }

        if (uI > 14)
        {
            uI = 0;
        } else
        {
            uI++;
        }
    }

    public void Load()
    {
        track1.Load(track1mml);
        track2.Load(track2mml);
        track3.Load(track3mml);
        track4.Load(track4mml);
        track5.Load(track5mml);
    }

    public void Play()
    {
        DateTime sync = DateTime.Now;
        track1.PlaySync(sync);
        track2.PlaySync(sync);
        track3.PlaySync(sync);
        track4.PlaySync(sync);
        track5.PlaySync(sync);
    }

    

    public void Stop()
    {
        track1.Stop();
        track2.Stop();
        track3.Stop();
        track4.Stop();
        track5.Stop();
    }

    public void OnRaiseShift()
    {
        octaveShift++;
        SetOctaveShift();
    }
    public void OnLowerShift()
    {
        octaveShift--;
        SetOctaveShift();
    }

    public void OnStopPressed()
    {
        Networking.SetOwner(Networking.LocalPlayer, gameObject);
        state = 3;
    }

    public void OnLoadPressed()
    {
        state = 0;
        Networking.SetOwner(Networking.LocalPlayer, gameObject);
        //todo: Split it into chunks of 64, enough to cover 512 characters.
        track1mml = track1Text.text;
        track2mml = track2Text.text;
        track3mml = track3Text.text;
        track4mml = "";
        track5mml = "";
        state = 1;
    }

    public void OnPlayPressed()
    {
        Networking.SetOwner(Networking.LocalPlayer, gameObject);
        state = 2;
    }

    public void OnPresetPressed(int i)
    {
        Networking.SetOwner(Networking.LocalPlayer, gameObject);
        preset = i;
        state = 4+i;
    }

    void SetOctaveShift()
    {
        track1.SetOctaveShift(octaveShift);
        track2.SetOctaveShift(octaveShift);
        track3.SetOctaveShift(octaveShift);
        track4.SetOctaveShift(octaveShift);
        track5.SetOctaveShift(octaveShift);
        octave.text = octaveShift.ToString();
    }

    void LoadPreset()
    {
        if ( presets.Length > preset)
        {
            track1mml = presets[preset].track1;
            track2mml = presets[preset].track2;
            track3mml = presets[preset].track3;
            track4mml = presets[preset].track4;
            track5mml = presets[preset].track5;

            track1Text.text = track1mml;
            track2Text.text = track2mml;
            track3Text.text = track3mml;
            Load();
        }
    }
}