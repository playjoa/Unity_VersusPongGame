using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class BuyableItem : MonoBehaviour
{
    [SerializeField]
    private StoreManager targetStore;

    [SerializeField]
    private int indexItem = 0;

    [SerializeField]
    protected int priceItem = 0;

    [SerializeField]
    protected Image imgItemInStore;

    private Action OnBuyItem;

    public Sprite sprItem => imgItemInStore.sprite;
    public int ItemPrice => priceItem;
    public int idPlayerBuyer => targetStore.PlayerIDInStore;
    public int ItemIndex => indexItem;

    protected void SetImageToItem(Sprite sprToSet)
    {
        imgItemInStore.sprite = sprToSet;
    }

    public virtual void OnEnable()
    {
        OnBuyItem += OnItemBought;
    }

    public virtual void OnDisable()
    {
        OnBuyItem -= OnItemBought;
    }

    public void SelectItemForStore() 
    {
        if (!targetStore)
            return;

        targetStore.ReceiveItem(this);
    }

    public virtual bool HasItem() 
    {
        return false;
    }

    public virtual bool BuyItem()
    {
        if (Economy.ChargePlayer(idPlayerBuyer, priceItem))
        {
            OnBuyItem?.Invoke();
            return true;
        }

        return false;
    }

    public virtual void OnItemBought() { }

    public virtual string DescItem()
    {
        return "";
    }
}