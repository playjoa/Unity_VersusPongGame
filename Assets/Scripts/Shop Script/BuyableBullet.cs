using UnityEngine;
using UnityEngine.UI;

public class BuyableBullet : BuyableItem
{
    [SerializeField]
    private BulletStats bulletToBuy;

    [SerializeField] 
    private Image backGroundItem;

    [SerializeField]
    private Color colorSelected = Color.green, colorNotSelected = Color.red, colorHasItem = Color.blue;

    public override void OnEnable()
    {
        base.OnEnable();
        InitializeItemInStore();
    }

    void InitializeItemInStore() 
    {
        SetImageToItem(bulletToBuy.sprBullet);
        PrepareUI_ITem();

        if (ItemEquiped())
            SelectItemForStore();
    }

    public void ItemClick()
    {
        SelectItemForStore();

        if (HasItem())
            SelectItem();

        PrepareUI_ITem();
    }

    public void PrepareUI_ITem()
    {
        if (ItemEquiped())
        {
            SetUIForItemEquiped();
            return;
        }

        if (HasItem())
        {
            SetUIForItemOwned();
            return;
        }
        SetUIForItemAvailableToPurchase();
    }

    void SetUIForItemEquiped() 
    {
        backGroundItem.color = colorSelected;
    }

    void SetUIForItemOwned()
    {
        backGroundItem.color = colorHasItem;
    }

    void SetUIForItemAvailableToPurchase()
    {
        backGroundItem.color = colorNotSelected;
    }

    bool ItemEquiped()
    {
        if (PlayersDataManager.PlayerCurrentBulletLoaded(idPlayerBuyer) == ItemIndex)
            return true;

        return false;
    }

    public override bool HasItem()
    {
        return PlayersDataManager.PlayerHasBulletItem(idPlayerBuyer, ItemIndex);
    }

    public override void OnItemBought()
    {
        PlayersDataManager.SetNewPlayerNewBulletItemValue(idPlayerBuyer, ItemIndex, true);
        SelectItem();
    }

    public override string DescItem()
    {
        string descItem = "";

        descItem += FormatDescriptionItem("damage", bulletToBuy.BaseBulletDamage.ToString());
        descItem += FormatDescriptionItem("speed", bulletToBuy.BulletMovement.BulletVelocity.ToString());
        descItem += FormatDescriptionItem("maxbounce", bulletToBuy.BulletMovement.BulletMaxBounces.ToString());

        return descItem;
    } 

    string FormatDescriptionItem(string idText, string valueToSet)
    {
        return " - " + Translate.GetTranslatedText(idText) + " " + valueToSet + " \n";
    }

    void SelectItem()
    {
        PlayersDataManager.SetNewPlayerCurrentBullet(idPlayerBuyer, ItemIndex);
        UpdateAllItens();
    }

    void UpdateAllItens() 
    {
        BuyableBullet[] allBulletsForPurchase =  FindObjectsOfType<BuyableBullet>();

        foreach (BuyableBullet buyableBullet in allBulletsForPurchase)
            buyableBullet.PrepareUI_ITem();
    }
}