using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(order = 1, fileName = "NewGunPart", menuName = "GunPart")]
public class GunPartSO : ScriptableObject
{
    public GunPartEnum partEnum;
    public string partName;
    public string partDescription;
    public Sprite partIcon;
    public GameObject partPrefab;
    public GunPart partScript;
}
