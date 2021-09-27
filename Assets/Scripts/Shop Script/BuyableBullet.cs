using TranslationSystem.Base;
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

    private void InitializeItemInStore() 
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

    private void PrepareUI_ITem()
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

    private void SetUIForItemEquiped() => backGroundItem.color = colorSelected;
    private void SetUIForItemOwned() => backGroundItem.color = colorHasItem;
    private void SetUIForItemAvailableToPurchase() => backGroundItem.color = colorNotSelected;
    private bool ItemEquiped() =>PlayersDataManager.PlayerCurrentBulletLoaded(idPlayerBuyer) == ItemIndex;
    public override bool HasItem() =>PlayersDataManager.PlayerHasBulletItem(idPlayerBuyer, ItemIndex);

    public override void OnItemBought()
    {
        PlayersDataManager.SetNewPlayerNewBulletItemValue(idPlayerBuyer, ItemIndex, true);
        SelectItem();
    }

    public override string DescItem()
    {
        var descItem = "";

        descItem += FormatDescriptionItem("damage", bulletToBuy.BaseBulletDamage.ToString());
        descItem += FormatDescriptionItem("speed", bulletToBuy.BulletMovement.BulletVelocity.ToString());
        descItem += FormatDescriptionItem("maxbounce", bulletToBuy.BulletMovement.BulletMaxBounces.ToString());

        return descItem;
    }

    private string FormatDescriptionItem(string idText, string valueToSet) =>
        " - " + Translate.GetTranslatedText(idText) + " " + valueToSet + " \n";

    private void SelectItem()
    {
        PlayersDataManager.SetNewPlayerCurrentBullet(idPlayerBuyer, ItemIndex);
        UpdateAllItens();
    }

    private void UpdateAllItens() 
    {
        var allBulletsForPurchase =  FindObjectsOfType<BuyableBullet>();

        foreach (var buyableBullet in allBulletsForPurchase)
            buyableBullet.PrepareUI_ITem();
    }
}