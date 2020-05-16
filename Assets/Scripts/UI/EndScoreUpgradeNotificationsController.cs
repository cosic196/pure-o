using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScoreUpgradeNotificationsController : MonoBehaviour
{
    [SerializeField]
    private GameObject _notificationPrefab;
    [SerializeField]
    private Color _activeColor;
    private Dictionary<string, string> _notifications;
    private Transform _transform;
    private int _activeNotifications = 0;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
    }

    void Start()
    {
        _notifications = new Dictionary<string, string>();
        EventManager.StartListening("EndScoreUnlockedUpgrade", AddUpgrade);
        EventManager.StartListening("ShowUpgradeNotifications", ShowNotificationsOrDisable);
        EventManager.StartListening("ClosedNotification", CheckIfNeedsToClose);
    }

    private void CheckIfNeedsToClose()
    {
        _activeNotifications--;
        if(_activeNotifications <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void ShowNotificationsOrDisable()
    {
        if(_notifications.Count > 0)
        {
            _activeNotifications = _notifications.Count;
            var image = GetComponent<Image>();
            image.color = _activeColor;
            image.raycastTarget = true;
            foreach (var notification in _notifications)
            {
                var go = Instantiate(_notificationPrefab, _transform);
                go.GetComponent<EndScoreUpgradeNotificationInitializer>().Initialize(notification.Key, notification.Value);
            }
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void AddUpgrade(string info)
    {
        _notifications.Add(info.Split('/')[0], info.Split('/')[1]);
    }
}
