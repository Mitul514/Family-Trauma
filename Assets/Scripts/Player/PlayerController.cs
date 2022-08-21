using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Range(0, 100)]
    [SerializeField] private float speed;
    [SerializeField] private EnititySO singleEntity, sceneChangeEntity;
    [SerializeField] private SceneTransition sceneTransition;
    [SerializeField] private string sceneToGo = "01_GameplayScene";
    private float x, y;

    void Update()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        transform.position += new Vector3(x * Time.deltaTime * speed, y * Time.deltaTime * speed, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out EntityType enitity))
        {
            if (enitity.EnititySO == sceneChangeEntity)
            {
                sceneTransition.StartSceneTransition(sceneToGo);
            }
            else if (enitity.EnititySO == sceneChangeEntity)
            {
                sceneTransition.StartSceneTransition(sceneToGo);
            }
        }

        if (collision.gameObject.TryGetComponent(out ShopController shopController))
        {
            shopController.InitShopping();
        }
    }
}
