using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    [Header("Main Menu Reference")]
    public MainPanel mainPanel;

    [Header("Character Prices")]
    public int jalmePrice = 20;
    public int samPrice = 30;
    public int clairePrice = 40;
    public int katePrice = 40;

    [Header("Buy Buttons")]
    public Button buyJalmeButton;
    public Button buySamButton;
    public Button buyClaireButton;
    public Button buyKateButton;

    [Header("Buttons Text (Optional)")]
    public TextMeshProUGUI jalmeText;
    public TextMeshProUGUI samText;
    public TextMeshProUGUI claireText;
    public TextMeshProUGUI kateText;

    private void Start()
    {
        UpdateShopUI();
    }

    public void UpdateShopUI()
    {
        if (DataManager.Instance == null) return;

        
        bool hasJalme = PlayerPrefs.GetInt("Bought_Jalme", 0) == 1;
        if (hasJalme) SetButtonAsOwned(buyJalmeButton, jalmeText);

       
        bool hasSam = PlayerPrefs.GetInt("Bought_Sam", 0) == 1;
        if (hasSam) SetButtonAsOwned(buySamButton, samText);

       
        bool hasClaire = PlayerPrefs.GetInt("Bought_Claire", 0) == 1;
        if (hasClaire) SetButtonAsOwned(buyClaireButton, claireText);

        
        bool hasKate = PlayerPrefs.GetInt("Bought_Kate", 0) == 1;
        if (hasKate) SetButtonAsOwned(buyKateButton, kateText);
    }

    private void SetButtonAsOwned(Button button, TextMeshProUGUI text)
    {
        button.interactable = false; 
        if (text != null) text.text = "Owned";
    }

    public void BuyJalme()
    {
        TryPurchaseCharacter("Bought_Jalme", jalmePrice, buyJalmeButton, jalmeText);
    }

    public void BuySam()
    {
        TryPurchaseCharacter("Bought_Sam", samPrice, buySamButton, samText);
    }

    public void BuyClaire()
    {
        TryPurchaseCharacter("Bought_Claire", clairePrice, buyClaireButton, claireText);
    }

    public void BuyKate()
    {
        TryPurchaseCharacter("Bought_Kate", katePrice, buyKateButton, kateText);
    }

    
    private void TryPurchaseCharacter(string saveKey, int price, Button button, TextMeshProUGUI text)
    {
        if (DataManager.Instance != null)
        {
            
            if (DataManager.Instance.currency >= price)
            {
                DataManager.Instance.currency -= price; 
                DataManager.Instance.SaveData(); 

                PlayerPrefs.SetInt(saveKey, 1);
                PlayerPrefs.Save();

                SetButtonAsOwned(button, text);

                if (mainPanel != null)
                {
                    mainPanel.UpdateCoinsText();
                    mainPanel.PlaySoundButton();
                }

                Debug.Log("Compra exitosa para: " + saveKey);
            }
            else
            {
                Debug.LogWarning("No tienes monedas suficientes para comprar este personaje.");
            }
        }
    }
}
