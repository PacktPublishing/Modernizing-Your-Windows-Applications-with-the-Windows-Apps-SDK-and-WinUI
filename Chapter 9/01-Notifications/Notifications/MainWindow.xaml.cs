using CommunityToolkit.WinUI.Notifications;
using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Notifications;

namespace Notifications
{
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
        }

        private void OnSendTextNotification(object sender, RoutedEventArgs e)
        {
            new ToastContentBuilder()
                .AddText("New message")
                .AddText("There's a new message for you!")
                .Show();
        }

        private void OnSendImageNotification(object sender, RoutedEventArgs e)
        {
            new ToastContentBuilder()
                .AddText("New message")
                .AddText("There's a new message for you!")
                .AddAppLogoOverride(new Uri("ms-appx:///Assets/Windows11.png"))
                .Show();
        }

        private void OnSendHeroImageNotification(object sender, RoutedEventArgs e)
        {
            // Photo by ArtHouse Studio from Pexels at https://www.pexels.com/photo/amazing-waterfall-with-lush-foliage-on-rocks-4534200/
            new ToastContentBuilder()
                .AddText("New message")
                .AddText("There's a new message for you!")
                .AddHeroImage(new Uri("ms-appx:///Assets/HeroImage.jpg"))
                .Show();
        }

        private void OnSendInlineHeroImageNotification(object sender, RoutedEventArgs e)
        {
            // Photo by ArtHouse Studio from Pexels at https://www.pexels.com/photo/amazing-waterfall-with-lush-foliage-on-rocks-4534200/
            new ToastContentBuilder()
                .AddText("New message")
                .AddText("There's a new message for you!")
                .AddInlineImage(new Uri("ms-appx:///Assets/HeroImage.jpg"))
                .Show();
        }

        private void OnCustomNotification(object sender, RoutedEventArgs e)
        {
            new ToastContentBuilder()
                .AddText("New message")
                .AddText("There's a new message for you!")
                .AddAttributionText("via my amazing WinUI app")
                .AddCustomTimeStamp(new DateTime(2022, 12, 25, 15, 0, 0))
                .Show();
        }

        private void OnTaggedNotification(object sender, RoutedEventArgs e)
        {
            new ToastContentBuilder()
                .AddText("Goal!")
                .AddText("The new score is 2-0")
                .Show(toast =>
                {
                    toast.Tag = "match1";
                });
        }

        private void OnScheduledNotification(object sender, RoutedEventArgs e)
        {
            new ToastContentBuilder()
            .AddText("Christmas party")
            .AddText("Party with family and friends")
            .Schedule(DateTimeOffset.Now.AddMinutes(5), toast =>
            {
                toast.Tag = "event1";
            });

        }

        private void OnDeleteScheduledNotification(object sender, RoutedEventArgs e)
        {
            ToastNotifierCompat notifier = ToastNotificationManagerCompat.CreateToastNotifier();

            IReadOnlyList<ScheduledToastNotification> scheduledToasts = notifier.GetScheduledToastNotifications();

            var notification = scheduledToasts.FirstOrDefault(i => i.Tag == "event1");
            if (notification != null)
            {
                notifier.RemoveFromSchedule(notification);
            }
        }

        private void OnSendInteractiveNotification(object sender, RoutedEventArgs e)
        {
            new ToastContentBuilder()
                .AddText("New message")
                .AddText("There's a new message for you!")
                .AddArgument("action", "click")
                .AddArgument("messageId", "1983")
                .Show();
        }

        private void OnSendNotificationWithButtons(object sender, RoutedEventArgs e)
        {
            new ToastContentBuilder()
                .AddText("New message")
                .AddText("There's a new message for you!")
                .AddArgument("action", "click")
                .AddArgument("messageId", "1983")

                .AddButton(new ToastButton()
                    .SetContent("Mark as read")
                    .AddArgument("action", "read")
                    .AddArgument("messageId", "1983"))

                .AddButton(new ToastButton()
                    .SetContent("Flag as important")
                    .AddArgument("action", "flag")
                    .AddArgument("messageId", "1983"))
                .Show();

        }

        private void OnSendNotificationWithReply(object sender, RoutedEventArgs e)
        {
            new ToastContentBuilder()
                .AddText("New message")
                .AddText("There's a new message for you!")
                .AddInputTextBox("reply", "Type your message", "Message")
                .AddButton(new ToastButton()
                    .SetContent("Send")
                    .AddArgument("action", "send")
                    .AddArgument("messageId", "1983")
                    .SetTextBoxId("reply"))
                .Show();

        }

        private void OnSendNotificationWithDropdown(object sender, RoutedEventArgs e)
        {
            new ToastContentBuilder()
                .AddText("New message")
                .AddText("There's a new message for you!")
                .AddToastInput(new ToastSelectionBox("response")
                {
                    DefaultSelectionBoxItemId = "ok",
                    Items =
                    {
                        new ToastSelectionBoxItem("ok", "Ok"),
                        new ToastSelectionBoxItem("late", "I'll be late"),
                        new ToastSelectionBoxItem("thanks", "Thanks!")
                    }
                })
                .AddButton(new ToastButton()
                    .SetContent("Ok")
                    .AddArgument("action", "send"))
                    .AddArgument("messageId", "1983")
                .Show();
        }

        private void OnSendNotificationWithReminder(object sender, RoutedEventArgs e)
        {
            new ToastContentBuilder()
                .SetToastScenario(ToastScenario.Reminder)
                .AddText("It's time!")
                .AddText("This is your reminder")
                .AddButton(new ToastButtonSnooze() { SelectionBoxId = "snoozeTime" })
                .AddButton(new ToastButtonDismiss())
                .AddToastInput(new ToastSelectionBox("snoozeTime")
                {
                    DefaultSelectionBoxItemId = "5",
                    Items =
                    {
                        new ToastSelectionBoxItem("5", "5 minutes"),
                        new ToastSelectionBoxItem("15", "15 minutes"),
                        new ToastSelectionBoxItem("30", "30 minutes"),
                        new ToastSelectionBoxItem("60", "1 hour"),
                    }
                })
                .Show();
        }

        private async void OnSendNotificationWithProgressBar(object sender, RoutedEventArgs e)
        {
            string tag = "report-progress";

            new ToastContentBuilder()
                .AddText("Report generation in progress...")
                .AddVisualChild(new AdaptiveProgressBar()
                {
                    Title = "Report generation",
                    Value = new BindableProgressBarValue("progressValue"),
                    ValueStringOverride = new BindableString("progressValueString"),
                    Status = "Generating report..."
                })
                .Show(toast =>
                {
                    toast.Data = new NotificationData();
                    toast.Data.Values["progressValue"] = "0.0";
                    toast.Data.Values["progressValueString"] = "0 %";

                    toast.Data.SequenceNumber = 1;

                    toast.Tag = tag;
                });

            for (uint cont = 0; cont <= 10; cont++)
            {
                await Task.Delay(1000);

                var data = new NotificationData
                {
                    SequenceNumber = cont
                };

                IFormatProvider provider = CultureInfo.CreateSpecificCulture("en-US");

                double progressValue = (double)cont / 10;
                string progressValueConverted = $"{progressValue * 100} %";

                data.Values["progressValue"] = progressValue.ToString(provider);
                data.Values["progressValueString"] = progressValueConverted;

                ToastNotificationManagerCompat.CreateToastNotifier().Update(data, tag);

            }
        }

        private void OnSendBadgeNumericNotification(object sender, RoutedEventArgs e)
        {
            BadgeNumericContent numeric = new BadgeNumericContent(15);
            var xml = numeric.GetXml();

            BadgeNotification notification = new BadgeNotification(xml);

            var badgeManager = BadgeUpdateManager.CreateBadgeUpdaterForApplication();
            badgeManager.Update(notification);
        }

        private void OnSendBadgeGlyphNotification(object sender, RoutedEventArgs e)
        {
            BadgeGlyphContent glyph = new BadgeGlyphContent(BadgeGlyphValue.Alert);
            var xml = glyph.GetXml();

            BadgeNotification notification = new BadgeNotification(xml);

            var badgeManager = BadgeUpdateManager.CreateBadgeUpdaterForApplication();
            badgeManager.Update(notification);

        }
    }
}
