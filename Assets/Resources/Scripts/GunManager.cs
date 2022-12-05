using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Gun Manager")]

public class GunManager : ScriptableObject
{
    public bool[] modesEnabled = new bool[4];
}
