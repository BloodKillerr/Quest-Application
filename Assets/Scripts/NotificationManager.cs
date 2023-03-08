using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Notifications.Android;
using System;

public class NotificationManager : MonoBehaviour
{
    AndroidNotificationChannel channel;
    private void Start()
    {
        channel = new AndroidNotificationChannel()
        {
            Id = "main_channel",
            Name = "Main Channel",
            Importance = Importance.Default,
            Description = "Application notifications",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(channel);
        SendNewDayNotification();
        SendReminderNotification();
    }

    public void SendNewDayNotification()
    {
        AndroidNotification notification = new AndroidNotification();
        notification.Title = "!";
        notification.Text = "It's a new day! Don't forget to check your quests!";

        string newDate = string.Format("{0} 08:00:00", System.DateTime.Now.Date.AddDays(1).ToString("dd-MM-yyyy"));
        notification.FireTime = System.DateTime.ParseExact(newDate, "dd-MM-yyyy HH:mm:ss", null);
        notification.ShowTimestamp = true;
        notification.RepeatInterval = new TimeSpan(24, 0, 0);
        AndroidNotificationCenter.SendNotification(notification, "main_channel");
    }

    public void SendReminderNotification()
    {
        AndroidNotification notification = new AndroidNotification();
        notification.Title = "!";
        notification.Text = "It's almost over! Don't forget about your daily routine!";

        string newDate = string.Format("{0} 18:00:00", System.DateTime.Now.Date.AddDays(1).ToString("dd-MM-yyyy"));
        notification.FireTime = System.DateTime.ParseExact(newDate, "dd-MM-yyyy HH:mm:ss", null);
        notification.ShowTimestamp = true;
        notification.RepeatInterval = new TimeSpan(24, 0, 0);
        AndroidNotificationCenter.SendNotification(notification, "main_channel");
    }
}
