using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    // pozvace se na kraju animacije da skloni ono sto je ostalo 
    // u ExplosionGO - mora se podesi da bi se ovo pozvalo na kraju animacije - Animations tab
    // Dodamo Animation event nakon svega
    void DestroyGameObject()
    {
        Destroy(gameObject);
    }
}
