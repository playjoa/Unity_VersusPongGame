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

    void UpdateStoreUI() 
    {
        targetItemImageStore.sprite = currentItemToBuy.sprItem;
        txtDescItem.text = currentItemToBuy.DescItem();

        if (currentItemToBuy.HasItem())
        {
            btnBuy.SetActive(false);
            txtPriceItem.text = Translate.GetTranslatedText("equipped");
            return;
        }

        btnBuy.SetActive(true);
        txtPriceItem.text = "$:" + currentItemToBuy.ItemPrice;
    }

    public void ReceiveItem(BuyableItem buyableItem)
    {
        currentItemToBuy = buyableItem;
        
        UpdateStoreUI();

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
