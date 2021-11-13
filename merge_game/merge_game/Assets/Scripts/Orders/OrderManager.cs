using UnityEngine.UI;
using UnityEngine;
using System;
using TMPro;

public class OrderManager : MonoBehaviour
{
    public static OrderManager instance;

    [SerializeField] TextMeshProUGUI scoreTxt;
    [SerializeField] Image orderIcon;

    private Vector2Int orderLevelRange = new Vector2Int(1, Enum.GetNames(typeof(ItemType)).Length);
    public static ItemType currentOrder;

    public static int score = 0;
    private bool orderLoadedFromSave = false;

    private void Awake() => instance = this;
    private void Start()
    {
        if(!orderLoadedFromSave)
        GenerateOrder(false);
        UpdateScoreText();
    }
    public void InitializeScore(int _score) => score = _score;
    public void InitializeOrder(int order)
    {
        currentOrder = (ItemType)order;
        GenerateOrder(true);
        orderLoadedFromSave = true;
    }
    public void GenerateOrder(bool fromSave) 
    {
        currentOrder = fromSave ? currentOrder : (ItemType)UnityEngine.Random.Range(orderLevelRange.x, orderLevelRange.y);
        orderIcon.sprite = Storage.GetItemIcon(currentOrder);
    }
    public void CheckOrder() 
    {
        if (SlotManager.instance.FindItemInSlots(currentOrder))
        {
            score += 1;
            UpdateScoreText();
            GenerateOrder(false);
        }
    }
    private void UpdateScoreText() => scoreTxt.text = "Orders: " + score.ToString();
}
