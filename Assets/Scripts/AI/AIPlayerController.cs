using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AIPlayerController : MonoBehaviour
{
    #region Editor Properties

    [SerializeField]
    private CurrencyManager currencyManager;

    [SerializeField]
    private float movementSpeed;

    #endregion

    #region Properties

    private Vector3 initialPosition;
    
    private bool canMoveTowards
    {
        get
        {
            return goldDataObjects.Length > 0;
        }
    }

    private GoldDataObject[] goldDataObjects
    {
        get
        {
            return FindObjectsOfType(typeof(GoldDataObject)) as GoldDataObject[];
        }
    }

    #endregion

    #region Lifecycle

    private void Start()
    {
        initialPosition = transform.position;
    }

    
    void Update()
    {
        Vector2 targetPosition = canMoveTowards ? GetNearGold().position : initialPosition;
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementSpeed * Time.deltaTime);
    }

    #endregion

    #region Private Methods

    private void OnTriggerEnter2D(Collider2D other)
    {
        IGoldData goldDataInterface = (IGoldData)other.gameObject.GetComponent<IGoldData>();
        if (goldDataInterface != null)
        {
            currencyManager.IncreaseIncomeCounter(goldDataInterface.GetIncomeMultiplierTotal());
            Destroy(other.gameObject);
        }
    }

    private Transform GetNearGold()
    {
        Transform minPosition = null;
        float distance = Mathf.Infinity;

        Vector3 playerPosition = transform.position;
        foreach (GoldDataObject goldDataObject in goldDataObjects)
        {
            float calculateDistance = Vector3.Distance(goldDataObject.transform.position, playerPosition);
            if (calculateDistance < distance)
            {
                minPosition = goldDataObject.transform;
                distance = calculateDistance;
            }
        }
        return minPosition;
    }

    #endregion
}
