package grocerly.fhict.com.grocerly;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.os.Bundle;
import android.support.v4.app.DialogFragment;
import android.support.v4.content.LocalBroadcastManager;
import android.util.Log;
import android.view.View;
import android.widget.Button;

import grocerly.fhict.com.grocerly.fragments.VolunteerDialogFragment;
import grocerly.fhict.com.grocerly.models.User;
import grocerly.fhict.com.grocerly.services.ReceiverService;
import grocerly.fhict.com.grocerly.services.SenderService;
import grocerly.fhict.com.grocerly.utils.Convert;

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
        startService(receiverService);


        listener = new BroadcastReceiver() {
            @Override
            public void onReceive( Context context, Intent intent ) {
                String data = intent.getStringExtra("DATA");
                String json = intent.getStringExtra("USER");

                User volunteer = Convert.stringToClass(json, User.class);
                showDialog(volunteer);

                Log.d( "Received data : ", data);
            }
        };

        Button orderBtn = findViewById(R.id.order_btn);
        orderBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                orderOnClick();
            }
        });

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


    private void orderOnClick(){
        startService(senderService);
    }

    private void showDialog(User volunteer){
        DialogFragment dialog = new VolunteerDialogFragment();

        Bundle data = new Bundle();
        data.putString("NAME", volunteer.getName());
        data.putString("IMAGE", volunteer.getProfileImage());

        dialog.setArguments(data);
        dialog.show(getSupportFragmentManager(), "Volunteer");
    }

}
