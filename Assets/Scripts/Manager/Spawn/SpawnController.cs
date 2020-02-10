using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    #region Properties

    [Header("Spawn fields")]
    [SerializeField]
    private List<Transform> spawnPoints;

    [SerializeField]
    private GoldDataObject goldDataObject;

    [SerializeField]
    private float spawnStartTime;

    [SerializeField]
    private float spawnDelayTime = 6.0f;

    private bool isNumberOfGoldDataObjectsEmpty
    {
        get
        {
            return FindObjectsOfType(typeof(GoldDataObject)).IsEmpty();
        }
    }

    #endregion

    #region Lifecycle

    private void Start()
    {
        InvokeRepeating(nameof(SpawnObjects), spawnStartTime, spawnDelayTime);
    }

    private void OnEnable()
    {
        CurrencyManager.SpawnRateChangeDelegate += SetSpawnRate;
    }


    private void OnDisable()
    {
        CurrencyManager.SpawnRateChangeDelegate -= SetSpawnRate;
    }

    #endregion

    #region Private Methods

    private void SpawnObjects()
    {
        if (isNumberOfGoldDataObjectsEmpty)
        {
            foreach (Transform spawnPoint in spawnPoints)
            {
                GoldDataObject goldData = Instantiate<GoldDataObject>(goldDataObject);
                goldData.transform.parent = transform;
                goldData.transform.position = spawnPoint.position;
            }
        }
    }

    private void SetSpawnRate(int spawnRateMultiplier)
    {
        if (spawnDelayTime > 0)
        {
            spawnDelayTime -= 0.5f;
        }
    }

    #endregion

}
