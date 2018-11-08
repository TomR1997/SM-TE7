package grocerly.fhict.com.grocerly.services;

import android.app.Service;
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

public class ReceiverService extends Service {

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
                channel.basicConsume("my-queue", false, "myConsumerTag",
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
        factory.setHost("145.93.128.114");

        HandlerThread thread = new HandlerThread("ServiceStartArguments");
        thread.start();

        // Get the HandlerThread's Looper and use it for our Handler
        Looper mServiceLooper = thread.getLooper();
        mServiceHandler = new ServiceHandler(mServiceLooper);

        localBroadcastManager = LocalBroadcastManager.getInstance(this);

    }

    @Override
    public int onStartCommand(Intent intent, int flags, int startId) {
        Toast.makeText(this, "service starting", Toast.LENGTH_SHORT).show();

        Message msg = mServiceHandler.obtainMessage();
        msg.arg1 = startId;
        mServiceHandler.sendMessage(msg);

        // If we get killed, after returning from here, restart
        return START_STICKY;
    }

    @Override
    public IBinder onBind(Intent intent) {
        //TODO for communication return IBinder implementation
        return null;
    }

    @Override
    public void onDestroy() {
        Toast.makeText(this, "service done", Toast.LENGTH_SHORT).show();
    }
}
