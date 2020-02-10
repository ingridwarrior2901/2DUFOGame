using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(GoldNugget))]
public class GoldDataObject : MonoBehaviour, IGoldData
{
    #region Properties
    [Header("Scriptable object")]
    [SerializeField]
    private GoldNugget goldNugget;

    [Header("Gold data values")]
    private SpriteRenderer spriteRenderer;
    private int incomeMultiplier = 1;

    public int IncomeMultiplierTotal
    {
        get
        {
            return incomeMultiplier * goldNugget.Price;
        }
    }

    #endregion

    #region Lifecycle

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        Debug.Assert(spriteRenderer, "spriteRenderer should not be nil, make sure to include SpriteRenderer Component");
        Debug.Assert(goldNugget, "goldNugget should not be nil, make sure to include GoldNugget Component");
        spriteRenderer.sprite = goldNugget.Icon;
    }

    private void OnEnable()
    {
        CurrencyManager.IncomeMultiplierChangeDelegate += SetIncomeMultiplier;
    }


    private void OnDisable()
    {
        CurrencyManager.IncomeMultiplierChangeDelegate -= SetIncomeMultiplier;
    }

    #endregion

    #region Public Methods

    public void SetIncomeMultiplier(int incomeMultiplier)
    {
        this.incomeMultiplier = incomeMultiplier;
    }

    public int GetIncomeMultiplierTotal()
    {
        return IncomeMultiplierTotal;
    }

    #endregion
}
