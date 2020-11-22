using System;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using UnityEngine.UI;

public class MMLParserUI : UdonSharpBehaviour
{
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

    public void OnPlayPressed()
    {
        DateTime sync = DateTime.Now;
        track1.PlaySync(track1Text.text, sync);
        track2.PlaySync(track2Text.text, sync);
        track3.PlaySync(track3Text.text, sync);
        track4.PlaySync(track4Text.text, sync);
        track5.PlaySync(track5Text.text, sync);
    }

    public void OnStopPressed()
    {
        track1.Stop();
        track2.Stop();
        track3.Stop();
        track4.Stop();
        track5.Stop();
    }
}
