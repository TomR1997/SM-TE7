package grocerly.fhict.com.grocerly;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.os.Bundle;
import android.support.v4.content.LocalBroadcastManager;
import android.util.Log;

import grocerly.fhict.com.grocerly.services.ReceiverService;
import grocerly.fhict.com.grocerly.services.SenderService;

public class ShoppingListActivity extends BaseActivity {

    private Intent senderService;
    private Intent receiverService;

    private BroadcastReceiver listener;

    private LocalBroadcastManager localBroadcastManager;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        localBroadcastManager = LocalBroadcastManager.getInstance(this);

        senderService = new Intent(this, SenderService.class);
        receiverService = new Intent(this, ReceiverService.class);
        startService(senderService);
        startService(receiverService);


        listener = new BroadcastReceiver() {
            @Override
            public void onReceive( Context context, Intent intent ) {
                String data = intent.getStringExtra("DATA");
                Log.d( "Received data : ", data);
            }
        };

        localBroadcastManager.registerReceiver(listener, new IntentFilter("VOLUNTEER_FOUND"));

    }

    @Override
    protected void onDestroy() {

        if (senderService != null) {
            stopService(senderService);
        }

        if (receiverService != null) {
            stopService(receiverService);
        }

        localBroadcastManager.unregisterReceiver(listener);

        super.onDestroy();
    }

    @Override
    protected int getLayoutResourceId() {
        return R.layout.activity_shopping_list;
    }

    @Override
    protected int getActivityID() {
        return 1;
    }



}
