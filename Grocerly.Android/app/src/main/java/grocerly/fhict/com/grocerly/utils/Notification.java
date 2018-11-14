package grocerly.fhict.com.grocerly.utils;

import android.app.NotificationChannel;
import android.app.NotificationManager;
import android.app.PendingIntent;
import android.content.Context;
import android.content.Intent;
import android.os.Build;
import android.support.v4.app.NotificationCompat;

import grocerly.fhict.com.grocerly.R;

public class Notification {

    private static NotificationManager notifManager;

    public static void createNotification(String aMessage, Context context, Intent intent) {
        final int NOTIFY_ID = 0; // ID of notification
        String id = "kaas";
        String title = "kaas";
        PendingIntent pendingIntent;
        NotificationCompat.Builder builder;
        if (notifManager == null) {
            notifManager = (NotificationManager)context.getSystemService(Context.NOTIFICATION_SERVICE);
        }
        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.O) {
            int importance = NotificationManager.IMPORTANCE_HIGH;
            NotificationChannel mChannel = notifManager.getNotificationChannel(id);
            if (mChannel == null) {
                mChannel = new NotificationChannel(id, title, importance);
                mChannel.enableVibration(true);
                mChannel.setVibrationPattern(new long[]{100, 200, 300, 400, 500, 400, 300, 200, 400});
                notifManager.createNotificationChannel(mChannel);
            }

        }
            builder = new NotificationCompat.Builder(context, id);
        int uniqueInt = (int) (System.currentTimeMillis() & 0xfffffff);
            pendingIntent = PendingIntent.getActivity(context, uniqueInt, intent, PendingIntent.FLAG_UPDATE_CURRENT);
            builder.setContentTitle(aMessage)                            // required
                    .setSmallIcon(R.drawable.grocerly_icon)   // required
                    .setContentText(context.getString(R.string.app_name)) // required
                    .setDefaults(android.app.Notification.DEFAULT_ALL)
                    .setAutoCancel(true)
                    .setContentIntent(pendingIntent)
                    .setTicker(aMessage)
                    .setVibrate(new long[]{100, 200, 300, 400, 500, 400, 300, 200, 400})
                    .setPriority(android.app.Notification.PRIORITY_HIGH);

        android.app.Notification notification = builder.build();
        notifManager.notify(NOTIFY_ID, notification);
    }
}
