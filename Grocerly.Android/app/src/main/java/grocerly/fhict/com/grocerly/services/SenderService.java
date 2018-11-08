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

    private final class ServiceHandler extends Handler {

        ServiceHandler(Looper looper) {
            super(looper);
        }

        @Override
        public void handleMessage(Message msg) {
            try {
                ConnectionFactory factory = new ConnectionFactory();
                factory.setUsername("grocerly");
                factory.setPassword("1234");
                factory.setVirtualHost("/");
                factory.setHost("145.93.128.114");

                Connection connection = factory.newConnection();

                Channel channel = connection.createChannel();

                channel.queueDeclare("my-queue", true, false, false, null);

                String message = "A message";

                channel.basicPublish("", "my-queue", null, message.getBytes());

                Thread.sleep(5000);
            } catch (InterruptedException | IOException | TimeoutException e) {
                Thread.currentThread().interrupt();
            }
            stopSelf(msg.arg1);
        }
    }

    @Override
    public void onCreate() {
        HandlerThread thread = new HandlerThread("ServiceStartArguments");
        thread.start();

        // Get the HandlerThread's Looper and use it for our Handler
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