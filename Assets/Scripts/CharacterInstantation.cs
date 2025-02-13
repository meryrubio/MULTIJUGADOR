using Com.MyCompany.MyGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInstantation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.InstancePlayer();

    }

  
}
