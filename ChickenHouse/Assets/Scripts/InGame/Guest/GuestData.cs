using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "GiestData", menuName = "ScriptableObjects/Guest", order = 1)]
public class GuestData : ScriptableObject
{
    /** ¼Õ´Ô ¼ºÇâ **/
    public List<GuestType> guestTypes;
}
