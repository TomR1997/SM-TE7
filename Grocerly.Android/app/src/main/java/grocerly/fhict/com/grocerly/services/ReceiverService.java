package grocerly.fhict.com.grocerly.services;

import android.app.Notification;
import android.app.NotificationManager;
import android.app.Service;
import android.content.Context;
import android.content.Intent;
import android.os.Handler;
import android.os.HandlerThread;
import android.os.IBinder;
import android.os.Looper;
import android.os.Message;
import android.support.v4.content.LocalBroadcastManager;
import android.util.Log;
import android.widget.Toast;

import com.rabbitmq.client.AMQP;
import com.rabbitmq.client.Channel;
import com.rabbitmq.client.Connection;
import com.rabbitmq.client.ConnectionFactory;
import com.rabbitmq.client.DefaultConsumer;
import com.rabbitmq.client.Envelope;

import java.io.IOException;

import grocerly.fhict.com.grocerly.models.User;
import grocerly.fhict.com.grocerly.utils.Convert;

public class ReceiverService extends Service {

    private final static String EXCHANGE_NAME = "ORDERS";
    private final static String QUEUE_NAME = "ORDERS_REPLY";
    private  final static String ORDER_ID = "a1727265-c504-41cd-aa84-39512c7f89e2";

    private static boolean isRunning = false;

    private ServiceHandler mServiceHandler;

    private ConnectionFactory factory;

    private LocalBroadcastManager localBroadcastManager;

    private final class ServiceHandler extends Handler {

        ServiceHandler(Looper looper) {
            super(looper);
        }

        @Override
        public void handleMessage(final Message msg) {
            try {
                Connection connection = factory.newConnection();
                final Channel channel = connection.createChannel();
                channel.queueDeclare(QUEUE_NAME,true, false, false, null);
                channel.queueBind(QUEUE_NAME, EXCHANGE_NAME, ORDER_ID);

                channel.basicConsume(QUEUE_NAME, false,
                        new DefaultConsumer(channel) {
                            @Override
                            public void handleDelivery(String consumerTag,
                                                       Envelope envelope,
                                                       AMQP.BasicProperties properties,
                                                       byte[] body)
                                    throws IOException {
                                long deliveryTag = envelope.getDeliveryTag();

                                Intent localIntent = new Intent("VOLUNTEER_FOUND");
                                localIntent.putExtra("DATA", new String(body, "UTF8"));

                                User volunteer = new User();
                                volunteer.setName("Bob Janssen");
                                volunteer.setProfileImage("https://i340824core.venus.fhict.nl/media/1/e8d5a3b0428a63a29ed46df8e687d753.jpg");
                                String json = Convert.classToString(volunteer);

                                localIntent.putExtra("USER", json);

                                localBroadcastManager.sendBroadcast(localIntent);
                                channel.basicAck(deliveryTag, false);



                            }
                        });
            }catch (Exception e){
                Log.e("kaas", e.getMessage());
            }
        }
    }

    @Override
    public void onCreate() {

        factory = new ConnectionFactory();
        factory.setUsername("grocerly");
        factory.setPassword("1234");
        factory.setVirtualHost("/");
        factory.setHost("13.69.136.31");

        HandlerThread thread = new HandlerThread("ServiceStartArguments");
        thread.start();

        Looper mServiceLooper = thread.getLooper();
        mServiceHandler = new ServiceHandler(mServiceLooper);

        localBroadcastManager = LocalBroadcastManager.getInstance(this);

    }

    @Override
    public int onStartCommand(Intent intent, int flags, int startId) {
        if (!isRunning) {
            Toast.makeText(this, "service starting", Toast.LENGTH_SHORT).show();

            Message msg = mServiceHandler.obtainMessage();
            msg.arg1 = startId;
            mServiceHandler.sendMessage(msg);
        }

        return START_STICKY;
    }

    @Override
    public IBinder onBind(Intent intent) {
        //TODO for communication return IBinder implementation
        return null;
    }

    @Override
    public void onDestroy() {
        super.onDestroy();
    }
}
