using UnityEngine;

public class TutorialSpriteToggler : MonoBehaviour
{
    [System.Serializable]
    public class Item
    {
        public string eventName;      // произвольный ключ события
        public GameObject spriteObj;  // включить/выключить
    }

    public Item[] items;

    private void OnEnable()
    {
        TutorialEvents.OnEvent += Handle;
    }

    private void OnDisable()
    {
        TutorialEvents.OnEvent -= Handle;
    }

    private void Handle(string evt)
    {
        foreach (var i in items)
            if (i.eventName == evt && i.spriteObj != null)
                i.spriteObj.SetActive(!i.spriteObj.activeSelf);
    }
}

public static class TutorialEvents
{
    public static System.Action<string> OnEvent;
    public static void Fire(string evt) => OnEvent?.Invoke(evt);
}
