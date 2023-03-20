using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Photon.Pun;


// Geo Li, Leyna Ho
public class InventoryItemS : MonoBehaviourPun
{
    private int count;
    private const float FULL_ALPHA = 1f;
    private const float INIT_ALPHA = .5f;
    [SerializeField] private string name;
    private Image image;

    private bool IsRemoteObject;
    ExitGames.Client.Photon.Hashtable CustomeValue;


    [Header("UI")]
    // Text that tracks count of items in inventory
    [SerializeField] private Text countText;
    [SerializeField] private PhotonView photonView;

    private void Start()
    {
        // if (IsRemoteObject)

        RefreshCount();
        image = GetComponent<Image>();
    }

    private void Update()
    {
        float currentAlpha = image.color.a;

        if (count <= 0 && currentAlpha != INIT_ALPHA)
            ChangeImageAlpha(INIT_ALPHA);
        else if (count > 0 && currentAlpha != FULL_ALPHA)
            ChangeImageAlpha(FULL_ALPHA);

        count = int.Parse(PhotonNetwork.CurrentRoom.CustomProperties[name].ToString());
        RefreshCount();
    }

    // Set the alpha of the image to be initAlpha
    // and set the conut to be zero
    public void ResetAlphaWhenZero()
    {
        count = 0;
    }

    // Returns the sprite of the inventory item
    public Sprite GetItemSprite()
    {
        return image.sprite;
    }

    // Sets the sprite of the inventory item
    public void SetItemSprite(Sprite newSprite)
    {
        image.sprite = newSprite;
    }

    // Initializes InventoryItem with image
    public void Initialize(Sprite newSprite)
    {
        RefreshCount();
        image = GetComponent<Image>();
        SetItemSprite(newSprite);
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
        CustomeValue = new ExitGames.Client.Photon.Hashtable();
        CustomeValue.Add(name, count);
        PhotonNetwork.CurrentRoom.SetCustomProperties(CustomeValue);
    }

    // Decrements the count of the inventory item
    public void DecreaseCount() 
    {
        if (count > 0)
            count--;
            CustomeValue = new ExitGames.Client.Photon.Hashtable();
            CustomeValue.Add(name, count);
            PhotonNetwork.CurrentRoom.SetCustomProperties(CustomeValue);
    }

    private void ChangeImageAlpha(float newAlpha)
    {
        var tempColor = image.color;
        tempColor.a = newAlpha;
        image.color = tempColor;
    }
}
