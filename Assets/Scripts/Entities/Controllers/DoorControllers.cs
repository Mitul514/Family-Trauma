using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControllers : MonoBehaviour
{
    [SerializeField] private EnititySO playerEntity;
    [SerializeField] private SceneTransition sceneTransition;
    [SerializeField] private string sceneToGo;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out EntityType enitity))
        {
            if (enitity.EnititySO == playerEntity)
            {
                sceneTransition.StartSceneTransition(sceneToGo);
            }
        }
    }
}
