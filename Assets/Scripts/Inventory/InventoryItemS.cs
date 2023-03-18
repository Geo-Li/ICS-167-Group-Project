using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Photon.Pun;


// Geo Li, Leyna Ho
public class InventoryItemS : MonoBehaviourPun, IPunObservable
{
    private int count;
    private const float FULL_ALPHA = 255f;
    private float initAlpha;
    private Image image;

    [Header("UI")]
    [SerializeField] private Text countText;

    private void Start() 
    {
        count = 0;
        image = GetComponent<Image>();
        initAlpha = image.color.a;

        photonView.ObservedComponents.Add(this);
    }

    private void Update()
    {
        float currentAlpha = image.color.a;

        if (count <= 0 && currentAlpha != initAlpha)
            ChangeImageAlpha(initAlpha);
        else if (count > 0 && currentAlpha != FULL_ALPHA)
            ChangeImageAlpha(FULL_ALPHA);
    }

    // Set the alpha of the image to be initAlpha
    // and set the conut to be zero
    public void ResetAlphaWhenZero() 
    {
        count = 0;
        RefreshCount();
    }

    /*
    // Initialize the item when player collects it
    public void InitializeItem(InventoryItemSO newItem)
    {
        IncreaseCount();
        // make the item show full alpha value when stored into inventory
        RefreshCount();
    }
    */

    // Returns the sprite of the inventory item
    public Sprite GetItemSprite()
    {
        return image.sprite;
    }

    // update the count text when new item is collected
    public void RefreshCount() 
    {
        countText.text = count.ToString();
    }

    // Returns the count of the inventory item
    public int GetCount() 
    {
        return count;
    }

    // Increments the count of the inventory item
    public void IncreaseCount() 
    {
        count++;
        RefreshCount();
    }

    // Decrements the count of the inventory item
    public void DecreaseCount() 
    {
        if (count > 0)
        {
            count--;
            RefreshCount();
        }
    }

    private void ChangeImageAlpha(float newAlpha)
    {
        var tempColor = image.color;
        tempColor.a = newAlpha;
        image.color = tempColor;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        Debug.Log("called");

        if (stream.IsWriting)
        {
            stream.SendNext(count);
        }
        else if (stream.IsReading)
        {
            Debug.Log(count);

            count = (int)stream.ReceiveNext();

            Debug.Log(count);
            RefreshCount();
        }
    }
}
