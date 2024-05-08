using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField]
    private string keyName;

    [SerializeField]
    private Sprite sprite;

    [TextArea]
    [SerializeField]
    private string keyDescription;

    private InventoryManager inventoryManager;

    private bool destroyed = false; 
    // Start is called before the first frame update
    void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && destroyed == false)
        {
            Destroy(gameObject);
            destroyed = true;
            inventoryManager.AddKey(keyName, sprite, keyDescription);

        }
    }
}
