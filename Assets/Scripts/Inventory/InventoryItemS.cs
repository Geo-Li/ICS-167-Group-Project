using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryItemS : MonoBehaviour
{

    // this one is initially for drag and drop
    [HideInInspector] public InventoryItemSO item;
    [HideInInspector] public int count = 0;
    [HideInInspector] public float fullAlpha = 255f;
    [HideInInspector] public float alpha;

    [Header("UI")]
    public Image image;
    public string name;
    public Text countText;


    public void Start() {
        alpha = image.color.a;
    }

    public void ResetAlphaWhenZero() {
        count = 0;
        var tempColor = image.color;
        tempColor.a = alpha;
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
        image.sprite = newItem.image;
        RefreshCount();
    }

    // update the count text when new item is collected
    public void RefreshCount() {
        countText.text = count.ToString();
    }
    
}
