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
            backGroundItem.color = colorSelected;
            return;
        }

        if (HasItem())
        {
            backGroundItem.color = colorHasItem;
            return;
        }

        backGroundItem.color = colorNotSelected;
    }

    public override void OnItemBought()
    {
        PlayersDataManager.SetNewPlayerNewBulletItemValue(idPlayerBuyer, ItemIndex, true);
        SelectItem();
    }

    public override string DescItem()
    {
        string descItem = "";

        descItem += DescBulletItem("damage", bulletToBuy.BaseBulletDamage.ToString());
        descItem += DescBulletItem("speed", bulletToBuy.BulletMovement.BulletVelocity.ToString());
        descItem += DescBulletItem("maxbounce", bulletToBuy.BulletMovement.BulletMaxBounces.ToString());

        return descItem;
    }

    public override bool HasItem()
    {
        return PlayersDataManager.PlayerHasBulletItem(idPlayerBuyer, ItemIndex);
    }

    bool ItemEquiped()
    {
        if (PlayersDataManager.PlayerCurrentBulletLoaded(idPlayerBuyer) == ItemIndex)
            return true;

        return false;
    }

    string DescBulletItem(string idText, string valueToSet)
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