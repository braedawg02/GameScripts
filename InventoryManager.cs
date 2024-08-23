using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static List<GameObject> inventory = new List<GameObject>();
    public GameObject inventoryPanel;
    public static ArrayList hotbar = new ArrayList();
    public int CurrentHotbarSlot = 1;
    public GameObject HotbarPanel1;
    public GameObject HotbarPanel2;
    public GameObject HotbarPanel3;
    public GameObject HotbarPanel4;
    public GameObject HotbarPanel5;
    public GameObject HotbarPanel6;
    public GameObject HotbarPanel7;
    public GameObject HotbarPanel8;
    public GameObject HotbarPanel9;
    public GameObject HotbarPanel0;

    public GameObject inventoryItem;

    public void Start()
    {
        hotbar.Add(HotbarPanel1);
        hotbar.Add(HotbarPanel2);
        hotbar.Add(HotbarPanel3);
        hotbar.Add(HotbarPanel4);
        hotbar.Add(HotbarPanel5);
        hotbar.Add(HotbarPanel6);
        hotbar.Add(HotbarPanel7);
        hotbar.Add(HotbarPanel8);
        hotbar.Add(HotbarPanel9);
        hotbar.Add(HotbarPanel0);


    }
public static void AddToInventory(GameObject item)
{
    bool itemAdded = false;

    foreach (GameObject slot in hotbar)
    {
        // Check if the slot is empty
        if (slot.transform.childCount == 0)
        {
            // Instantiate the item as a child of the slot
            GameObject newItem = Instantiate(item, slot.transform);
            newItem.transform.localPosition = Vector3.zero;
            itemAdded = true;
            break;
        }
    }

    if (!itemAdded)
    {
        Debug.Log("Hotbar is full");
    }
}
    // Start is called before the first frame update

    public void RemoveFromInventory(GameObject item)
    {
        item.AddComponent(typeof(Rigidbody));
        Instantiate(item, transform.position + transform.forward, Quaternion.identity);
        inventory.Remove(item);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
