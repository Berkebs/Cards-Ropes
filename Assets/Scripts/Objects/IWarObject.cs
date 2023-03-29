using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWarObject
{
    public bool isDie { get; set; }
    public void GoWar();
    public bool CheckDie();
}
