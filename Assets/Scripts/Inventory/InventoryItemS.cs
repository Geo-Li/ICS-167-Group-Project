using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryItemS : MonoBehaviour
{
    [HideInInspector] private InventoryItemSO item;
    [HideInInspector] private int count;
    [HideInInspector] private float fullAlpha = 255f;
    [HideInInspector] private float initAlpha;

    [Header("UI")]
    [SerializeField] private Image image;
    [SerializeField] private string name;
    [SerializeField] private Text countText;


    public void Start() {
        count = 0;
        initAlpha = image.color.a;
    }

    // Set the alpha of the image to be initAlpha
    // and set the conut to be zero
    public void ResetAlphaWhenZero() {
        count = 0;
        var tempColor = image.color;
        tempColor.a = initAlpha;
        image.color = tempColor;
        RefreshCount();
    }

    // Initialize the item when player collects it
    public void InitializeItem(InventoryItemSO newItem)
    {
        item = newItem;
        count = 1;
        // make the item show full alpha value when stored into inventory
        var tempColor = image.color;
        tempColor.a = fullAlpha;
        image.color = tempColor;
        image.sprite = newItem.GetInventoryItemSOImage();
        RefreshCount();
    }

    // update the count text when new item is collected
    public void RefreshCount() {
        countText.text = count.ToString();
    }

    public string GetInventoryItemName() {
        return name;
    }

    public int GetCount() {
        return count;
    }
    
    public void IncreaseCount() {
        count++;
    }

    public void DecreaseCount() {
        count--;
    }
}
