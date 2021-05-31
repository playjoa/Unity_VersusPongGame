using UnityEngine.UI;
using UnityEngine;

public class StoreManager : MonoBehaviour
{
    [SerializeField]
    private int playerIDBuyingInStore = 0;

    [SerializeField] 
    private Text txtPriceItem, txtDescItem;

    [SerializeField]
    private GameObject btnBuy;

    [SerializeField]
    private Image targetItemImageStore;
    
    private BuyableItem currentItemToBuy;
    public int PlayerIDInStore => playerIDBuyingInStore;

    public void ReceiveItem(BuyableItem buyableItem)
    {
        currentItemToBuy = buyableItem;
        UpdateStoreUI();
    }

    void UpdateStoreUI() 
    {
        SetUIFixedItemInfo();

        if (currentItemToBuy.HasItem())
        {
            SetUIForOwnedItem();
            return;
        }

        SetUIForAvailableToBuyItem();
    }

    void SetUIFixedItemInfo() 
    {
        targetItemImageStore.sprite = currentItemToBuy.sprItem;
        txtDescItem.text = currentItemToBuy.DescItem();
    }

    void SetUIForOwnedItem() 
    {
        btnBuy.SetActive(false);
        txtPriceItem.text = Translate.GetTranslatedText("equipped");
    }

    void SetUIForAvailableToBuyItem() 
    {
        btnBuy.SetActive(true);
        txtPriceItem.text = "$:" + currentItemToBuy.ItemPrice;
    }

    public void ClickBuyItem() 
    {
        if(currentItemToBuy.BuyItem())
        {
            SoundManager.PlaySoundInList("buy", 1);
            UpdateStoreUI();
            return;
        }

        SoundManager.PlaySoundInList("error", 1);
    }
}