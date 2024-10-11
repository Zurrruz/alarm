using UnityEngine;

public class ThiefDetector : MonoBehaviour
{
    [SerializeField] Alarm _alarm;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Thief thief))
            _alarm.StartAlarm();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Thief thief))
            _alarm.StopAlarm();
    }
}
