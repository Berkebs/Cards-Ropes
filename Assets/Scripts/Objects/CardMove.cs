using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardMove : MonoBehaviour
{

    void Update()
    {
        transform.Translate(0, 0, 5 * Time.deltaTime);
    }
}
