using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyManager : MonoBehaviour, ICurrency
{
    #region Properties

    [SerializeField]
    private Text incomeCounterText;

    [SerializeField]
    private Text buySpeedText;

    [SerializeField]
    private Text buyIncomeMultiplierText;

    [SerializeField]
    private Text buySpawnRateText;

    [SerializeField]
    private CurrencyModel currencyModel;

    [SerializeField]
    private float incomeCounterStartTime;

    [SerializeField]
    private float incomeCounterStartDelay = 5.0f;

    private int speedCostMultiplier = 1;
    private int incomeCostMultiplier = 1;
    private int spawnCostMultiplier = 1;
    private int incomeCounter;

    private Button buySpeedButton
    {
        get
        {
            return buySpeedText.GetComponentInParent<Button>();
        }
    }

    private Button buyIncomeMultiplierButton
    {
        get
        {
            return buyIncomeMultiplierText.GetComponentInParent<Button>();
        }
    }

    private Button buySpawnRateButton
    {
        get
        {
            return buySpawnRateText.GetComponentInParent<Button>();
        }
    }

    private int buySpeedCost
    {
        get
        {
            return currencyModel.SpeedCost * speedCostMultiplier;
        }
    }

    private int buyIncomeCost
    {
        get
        {
            return currencyModel.IncomeCost * incomeCostMultiplier;
        }
    }


    private int buySpawnRateCost
    {
        get
        {
            return currencyModel.SpawnRateCost * spawnCostMultiplier;
        }
    }

    private int speedMultiplier = 10;

    #endregion

    #region Delegates

    public delegate void OnIncomeMultiplierChange(int incomeMultiplier);
    public static event OnIncomeMultiplierChange IncomeMultiplierChangeDelegate;

    public delegate void OnSpawnRateChange(int spawnRateMultiplier);
    public static event OnSpawnRateChange SpawnRateChangeDelegate;

    #endregion

    #region Lifecycle

    private void Start()
    {
        Debug.Assert(incomeCounterText, "incomeCounterText should not be nil");
        Debug.Assert(buySpeedText, "buySpeedText should not be nil");
        Debug.Assert(buyIncomeMultiplierText, "buyIncomeMultiplierText should not be nil");
        Debug.Assert(buySpawnRateText, "buySpawnRateText should not be nil");
        Debug.Assert(currencyModel, "currencyModel should not be nil");

        InvokeRepeating(nameof(CalculateCounter), incomeCounterStartTime, incomeCounterStartDelay);
    }

    private void Update()
    {
        incomeCounterText.text = "Income Counter: $" + incomeCounter;
        buySpeedText.text = "Buy Speed\n$" + buySpeedCost;
        buyIncomeMultiplierText.text = "Buy Income Multiplier\n$" + buyIncomeCost;
        buySpawnRateText.text = "Buy Spawn Rate\n$" + buySpawnRateCost;

        buySpeedButton.interactable = buySpeedCost <= incomeCounter;
        buyIncomeMultiplierButton.interactable = buyIncomeCost <= incomeCounter;
        buySpawnRateButton.interactable = buySpawnRateCost <= incomeCounter;
    }

    #endregion

    #region Private Methods

    private void CalculateCounter()
    {
        incomeCounter += (speedMultiplier * speedCostMultiplier);
    }

    private void CalculateBuyButtons(int buttonCost, ref int costMultiplier)
    {
        incomeCounter -= buttonCost;
        costMultiplier += 1;
    }

    #endregion

    #region Button Event

    public void BuySpeedAction()
    {
        CalculateBuyButtons(buySpeedCost, ref speedCostMultiplier);
    }

    public void BuyIncomeMultiplierAction()
    {
        CalculateBuyButtons(buyIncomeCost, ref incomeCostMultiplier);
        IncomeMultiplierChangeDelegate?.Invoke(incomeCostMultiplier);
    }

    public void BuySpawnRateAction()
    {
        CalculateBuyButtons(buySpawnRateCost, ref spawnCostMultiplier);
        SpawnRateChangeDelegate?.Invoke(spawnCostMultiplier);
    }

    public void IncreaseIncomeCounter(int value)
    {
        incomeCounter += value;
    }

    #endregion
}
