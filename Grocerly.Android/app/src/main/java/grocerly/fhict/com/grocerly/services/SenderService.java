package grocerly.fhict.com.grocerly.services;

import android.app.Service;
import android.content.Intent;
import android.os.Handler;
import android.os.HandlerThread;
import android.os.IBinder;
import android.os.Looper;
import android.os.Message;
import android.widget.Toast;

import com.rabbitmq.client.Channel;
import com.rabbitmq.client.Connection;
import com.rabbitmq.client.ConnectionFactory;

import java.io.IOException;
import java.util.concurrent.TimeoutException;


public class SenderService extends Service {

    private ServiceHandler mServiceHandler;
    private final static String EXCHANGE_NAME = "ORDERS";

    private  final static String ORDER_ID = "a1727265-c504-41cd-aa84-39512c7f89e2";

    private ConnectionFactory factory;

    private final class ServiceHandler extends Handler {

        ServiceHandler(Looper looper) {
            super(looper);
        }

        @Override
        public void handleMessage(Message msg) {
            try {
                Thread.sleep(5000);

                Connection connection = factory.newConnection();

                final Channel channel = connection.createChannel();

                String message = "A message";
                channel.exchangeDeclare(EXCHANGE_NAME, "direct");
                channel.basicPublish(EXCHANGE_NAME, ORDER_ID, null, message.getBytes());

            } catch (InterruptedException | IOException | TimeoutException e) {
                Thread.currentThread().interrupt();
            }
            stopSelf(msg.arg1);
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
    }

    @Override
    public int onStartCommand(Intent intent, int flags, int startId) {
        Toast.makeText(this, "service starting", Toast.LENGTH_SHORT).show();

        Message msg = mServiceHandler.obtainMessage();
        msg.arg1 = startId;
        mServiceHandler.sendMessage(msg);

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