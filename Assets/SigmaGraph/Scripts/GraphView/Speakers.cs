using System.Collections.Generic;
using UnityEngine;
using System; 

[CreateAssetMenu(fileName = "Speakers", menuName = "Scriptable Objects/Speakers")]
public class Speakers : ScriptableObject
{
    public List<SpeakerInfo> speakers;
}

[Serializable]
public enum Espeaker
{
    Toi,
    Gaelle
}
