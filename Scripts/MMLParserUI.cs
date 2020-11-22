using System;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using UnityEngine.UI;

public class MMLParserUI : UdonSharpBehaviour
{
    [UdonSynced]
    bool loading = false;
    [UdonSynced]
    bool playing = false;
    //synced play system.
    [UdonSynced]
    string track1mml;
    [UdonSynced]
    string track2mml;
    [UdonSynced]
    string track3mml;
    [UdonSynced]
    string track4mml;
    [UdonSynced]
    string track5mml;
    [UdonSynced]
    int seq = 0;//if it is > pseq, we need to do stuff
    int pseq = 0;
    int uI = 0;

    public Text status;

    public MMLParser track1;
    public InputField track1Text;
    public MMLParser track2;
    public InputField track2Text;
    public MMLParser track3;
    public InputField track3Text;
    public MMLParser track4;
    public InputField track4Text;
    public MMLParser track5;
    public InputField track5Text;


    public void OnLoadPressed()
    {
        track1mml = track1Text.text;
        track2mml = track2Text.text;
        track3mml = track3Text.text;
        track4mml = track4Text.text;
        track5mml = track5Text.text;
        loading = true;
        playing = false;
        seq++;
    }

    public void OnPlayPressed()
    {
        loading = false;
        playing = true;
        seq++;
    }

    public void Update()
    {
        if (seq != pseq)
        {
            if (loading)
            {
                Load();
            }
            else if (playing)
            {
                Play();
            }
            else
            {
                Stop();
            }
            pseq = seq;
        }

        if (uI == 0)
        {
            string stat = "Status:\n";
            stat += "1: " + track1.GetStatus() + "\n";
            stat += "2: " + track2.GetStatus() + "\n";
            stat += "3: " + track3.GetStatus() + "\n";
            stat += "4: " + track4.GetStatus() + "\n";
            stat += "5: " + track5.GetStatus() + "\n";
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

    public void OnStopPressed()
    {
        Stop();
    }

    public void Stop()
    {
        track1.Stop();
        track2.Stop();
        track3.Stop();
        track4.Stop();
        track5.Stop();
    }
}