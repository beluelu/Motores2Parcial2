using UnityEngine;
using System;
#if UNITY_ANDROID || UNITY_IOS
using Unity.Notifications.Android;
using Unity.Notifications.iOS;
#endif

public class MobileNotificationManager : MonoBehaviour
{
    public static MobileNotificationManager Instance { get; private set; }

    private const string CHANNEL_ID = "game_reminders_channel";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            RegisterNotificationChannel();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void RegisterNotificationChannel()
    {
#if UNITY_ANDROID
        var channel = new AndroidNotificationChannel()
        {
            Id = CHANNEL_ID,
            Name = "Recordatorios del Juego",
            Importance = Importance.High,
            Description = "Notificaciones sobre energía, retención y ofertas de la tienda.",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(channel);
#endif
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
        {
            TriggerNotificationSequence();
        }
    }

    private void OnApplicationPause(bool isPaused)
    {
        if (isPaused)
        {
            TriggerNotificationSequence();
        }
    }

    private void TriggerNotificationSequence()
    {
        Debug.Log("¡Salida detectada! Programando recordatorios en segundo plano...");

#if UNITY_ANDROID
        AndroidNotificationCenter.CancelAllNotifications();
#endif

        ScheduleNotification(
            "¡Energía al Máximo! ⚡",
            "Tu stamina se ha recargado por completo. ¡Es hora de volver a correr de la horda!",
            TimeSpan.FromMinutes(5)
        );

        ScheduleNotification(
            "Los zombis te extrañan... 🧟‍♂️",
            "Pasó bastante tiempo desde tu última carrera. ¿Podrás superar tu récord hoy?",
            TimeSpan.FromDays(1)
        );

        ScheduleNotification(
            "¡Liquidación en la Tienda! 🛒",
            "Los precios de los personajes bajaron por las próximas 2 horas. ¡Aprovechá tus monedas acumuladas!",
            TimeSpan.FromHours(6)
        );
    }

    private void ScheduleNotification(string title, string text, TimeSpan delay)
    {
        Debug.Log($"<color=cyan><b>[NOTIF CONFIGURADA]</b> Se enviará: <b>{title}</b> en {delay.TotalMinutes} minutos.</color>");

#if UNITY_ANDROID
        var notification = new AndroidNotification();
        notification.Title = title;
        notification.Text = text;
        notification.FireTime = DateTime.Now.Add(delay);
        notification.SmallIcon = "icon_0";
        notification.LargeIcon = "icon_1";

        AndroidNotificationCenter.SendNotification(notification, CHANNEL_ID);
#endif
    }
}
