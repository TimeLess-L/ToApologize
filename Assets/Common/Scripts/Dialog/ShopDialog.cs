//using UnityEngine.Purchasing;
using UnityEngine;
using UnityEngine.UI;

public class ShopDialog : Dialog
{
    public GameObject rewardTags, freeRuby1, freeRuby2;
    public Text[] rubyNumbers;
    public Text[] prices;

    protected override void Start()
    {
        base.Start();
        Purchaser.instance.onItemPurchased += OnItemPurchased;

        for(int i = 0; i < 5; i++)
        {
            var item = Purchaser.instance.iapItems[i];
            rubyNumbers[i].text = item.value + " rubies";
            prices[i].text = item.price + "$";
        }

        bool enabled = ConfigController.Config.enableFacebookFeatures;
        rewardTags.SetActive(enabled);
        freeRuby1.SetActive(enabled);
        freeRuby2.SetActive(!enabled);
    }

    public void OnBuyProduct(int index)
	{
        //购买物品
		Sound.instance.PlayButton();
        Purchaser.instance.BuyProduct(index);

        CFirebase.LogEvent("iap", "item_clicked_" + index);
    }

    private void OnItemPurchased(IAPItem item, int index)
    {
        // A consumable product has been purchased by this user.
//        if (item.productType == ProductType.Consumable)
//        {
//            CurrencyController.CreditBalance(item.value);
//            Toast.instance.ShowMessage("Your purchase is successful");
//            CUtils.SetBuyItem();

//#if !UNITY_EDITOR
//            CFirebase.LogEvent("iap", "item_purchased_" + index);
//#endif
//        }
//        // Or ... a non-consumable product has been purchased by this user.
//        else if (item.productType == ProductType.NonConsumable)
//        {
//            // TODO: The non-consumable item has been successfully purchased, grant this item to the player.
//        }
//        // Or ... a subscription product has been purchased by this user.
//        else if (item.productType == ProductType.Subscription)
//        {
//            // TODO: The subscription item has been successfully purchased, grant this to the player.
//        }
    }
	
	private void OnDestroy()
    {
        Purchaser.instance.onItemPurchased -= OnItemPurchased;
    }
}
