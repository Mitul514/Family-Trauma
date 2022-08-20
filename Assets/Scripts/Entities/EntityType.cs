using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityType : MonoBehaviour
{
    [SerializeField] private EnititySO enititySO;

    public EnititySO EnititySO
    {
        get => enititySO;
    }
}
