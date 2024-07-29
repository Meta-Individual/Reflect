using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetKey : MonoBehaviour
{
    public GameObject targetDoor;
    public GameObject player;
    private TransferMap _transferMap;
    private PlayerController _playerController;
    private bool isTrigger;

    void Start()
    {
        _transferMap = targetDoor.GetComponent<TransferMap>();
        _playerController = player.GetComponent<PlayerController>();
        isTrigger = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isTrigger)
        {
            _playerController.libraryKey = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isTrigger = false;
        }
    }
}
