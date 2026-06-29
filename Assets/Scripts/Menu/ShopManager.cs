using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [Header("Main Menu Reference")]
    public MainPanel mainPanel;

    [Header("Character Prices")]
    public int jalmePrice = 20;
    public int samPrice = 30;
    public int clairePrice = 40;
    public int katePrice = 40;

    [Header("Row Lists (Individual Purchase)")]
    public List<Button> jalmeButtons = new List<Button>();
    public List<Button> samButtons = new List<Button>();
    public List<Button> claireButtons = new List<Button>();
    public List<Button> kateButtons = new List<Button>();

    private void Start()
    {
        InitializeShopButtons();
        UpdateShopUI();
    }

    private void InitializeShopButtons()
    {
        for (int i = 0; i < jalmeButtons.Count; i++)
        {
            int index = i;
            if (jalmeButtons[i] != null)
            {
                jalmeButtons[i].onClick.AddListener(() => BuyJalmeIndividual(index));
            }
        }

        for (int i = 0; i < samButtons.Count; i++)
        {
            int index = i;
            if (samButtons[i] != null)
            {
                samButtons[i].onClick.AddListener(() => BuySamIndividual(index));
            }
        }

        for (int i = 0; i < claireButtons.Count; i++)
        {
            int index = i;
            if (claireButtons[i] != null)
            {
                claireButtons[i].onClick.AddListener(() => BuyClaireIndividual(index));
            }
        }

        for (int i = 0; i < kateButtons.Count; i++)
        {
            int index = i;
            if (kateButtons[i] != null)
            {
                kateButtons[i].onClick.AddListener(() => BuyKateIndividual(index));
            }
        }
    }

    public void UpdateShopUI()
    {
        for (int i = 0; i < jalmeButtons.Count; i++)
        {
            if (PlayerPrefs.GetInt($"Bought_Jalme_{i}", 0) == 1) SetButtonAsOwned(jalmeButtons[i]);
        }

        for (int i = 0; i < samButtons.Count; i++)
        {
            if (PlayerPrefs.GetInt($"Bought_Sam_{i}", 0) == 1) SetButtonAsOwned(samButtons[i]);
        }

        for (int i = 0; i < claireButtons.Count; i++)
        {
            if (PlayerPrefs.GetInt($"Bought_Claire_{i}", 0) == 1) SetButtonAsOwned(claireButtons[i]);
        }

        for (int i = 0; i < kateButtons.Count; i++)
        {
            if (PlayerPrefs.GetInt($"Bought_Kate_{i}", 0) == 1) SetButtonAsOwned(kateButtons[i]);
        }
    }

    private void SetButtonAsOwned(Button button)
    {
        if (button == null) return;
        button.interactable = false;
    }

    private void BuyJalmeIndividual(int index)
    {
        TryPurchaseCharacter($"Bought_Jalme_{index}", jalmePrice, jalmeButtons[index]);
    }

    private void BuySamIndividual(int index)
    {
        TryPurchaseCharacter($"Bought_Sam_{index}", samPrice, samButtons[index]);
    }

    private void BuyClaireIndividual(int index)
    {
        TryPurchaseCharacter($"Bought_Claire_{index}", clairePrice, claireButtons[index]);
    }

    private void BuyKateIndividual(int index)
    {
        TryPurchaseCharacter($"Bought_Kate_{index}", katePrice, kateButtons[index]);
    }

    private void TryPurchaseCharacter(string saveKey, int price, Button button)
    {
        if (DataManager.Instance != null)
        {
            if (DataManager.Instance.currency >= price)
            {
                DataManager.Instance.currency -= price;
                DataManager.Instance.SaveData();

                PlayerPrefs.SetInt(saveKey, 1);
                PlayerPrefs.Save();

                SetButtonAsOwned(button);

                if (mainPanel != null)
                {
                    mainPanel.UpdateCoinsText();
                    mainPanel.PlaySoundButton();
                }

                Debug.Log("Compra individual exitosa para la clave: " + saveKey);
            }
            else
            {
                Debug.LogWarning("Monedas insuficientes.");
            }
        }
    }
}
