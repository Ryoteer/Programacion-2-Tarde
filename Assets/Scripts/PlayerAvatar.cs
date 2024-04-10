using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAvatar : MonoBehaviour
{
    private Player _parent;

    private void Start()
    {
        _parent = GetComponentInParent<Player>();
    }

    public void Attack(int dmgAvg)
    {
        _parent.Attack(Random.Range(dmgAvg / 2, dmgAvg * 2));
    }

    public void Jump()
    {
        _parent.Jump();
    }
}
