using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;

    public SaveState state;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        instance = this;
    }
    private void Start() => LoadGame();

    public void SaveGame() 
    {
        state.score = OrderManager.score;
        state.secondsPassed = Timer.secondsPasssed;
        state.minutesPassed = Timer.minutesPassed;
        state.slotsStates = SlotManager.instance.GetSlotsEmptyState();
        state.slotsItems = SlotManager.instance.GetSlotsItems();
        state.order = (int)OrderManager.currentOrder;
        Debug.Log(state.order);

        PlayerPrefs.SetString("save", Serializing.Serialize<SaveState>(state));
    }

    public void LoadGame() 
    {
        if (PlayerPrefs.HasKey("save"))
        {
            state = Serializing.Deserialize<SaveState>(PlayerPrefs.GetString("save"));

            OrderManager.instance.InitializeScore(state.score);
            OrderManager.instance.InitializeOrder(state.order);
            Timer.instance.InitializeTimer(state.secondsPassed, state.minutesPassed);
            SlotManager.instance.InitializeSlotManager(state.slotsStates, state.slotsItems);
        }
        else 
        {
            state = new SaveState();
            SaveGame();
        }
    }

    private void OnApplicationQuit() => SaveGame();
}
