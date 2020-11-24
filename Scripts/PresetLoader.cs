
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class PresetLoader : UdonSharpBehaviour
{
    public int preset = 0;
    public MMLParserUI parserUI;
    public string track1;
    public string track2;
    public string track3;
    public string track4;
    public string track5;

    public void OnPressed()
    {
        parserUI.OnPresetPressed(preset);
    }
}
