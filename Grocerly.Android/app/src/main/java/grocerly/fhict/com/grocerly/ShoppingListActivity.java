package grocerly.fhict.com.grocerly;

import android.app.NotificationManager;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.content.SharedPreferences;
import android.graphics.drawable.AnimatedVectorDrawable;
import android.graphics.drawable.AnimationDrawable;
import android.os.Bundle;
import android.support.v4.app.DialogFragment;
import android.support.v4.content.LocalBroadcastManager;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.TextView;

import java.text.NumberFormat;

import grocerly.fhict.com.grocerly.fragments.VolunteerDialogFragment;
import grocerly.fhict.com.grocerly.models.User;
import grocerly.fhict.com.grocerly.services.ReceiverService;
import grocerly.fhict.com.grocerly.services.SenderService;
import grocerly.fhict.com.grocerly.utils.Convert;

public class ShoppingListActivity extends BaseActivity {

    private Intent senderService;
    private Intent receiverService;

    private boolean inBackground = true;

    private BroadcastReceiver listener;
    final String MY_PREFS_NAME = "MyPrefsFile";
    private static double currentPrice;
    private LocalBroadcastManager localBroadcastManager;
    NumberFormat format = NumberFormat.getCurrencyInstance();

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        localBroadcastManager = LocalBroadcastManager.getInstance(this);

        senderService = new Intent(this, SenderService.class);
        receiverService = new Intent(this, ReceiverService.class);
        startService(receiverService);

        SharedPreferences prefs = getSharedPreferences(MY_PREFS_NAME, MODE_PRIVATE);
        currentPrice = Double.longBitsToDouble(prefs.getLong("currentPrice", Double.doubleToLongBits(0)));

        listener = new BroadcastReceiver() {
            @Override
            public void onReceive( Context context, Intent intent ) {
                String data = intent.getStringExtra("DATA");

                User volunteer = loadVolunteer(intent);
                showDialog(volunteer);

                Log.d( "Received data : ", data);
            }
        };
        final Button priceBtn = findViewById(R.id.price_order_btn);
        Button orderBtn = findViewById(R.id.order_btn);
        orderBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                currentPrice = 0;
                SharedPreferences.Editor editor = getSharedPreferences(MY_PREFS_NAME, MODE_PRIVATE).edit();
                editor.putLong("currentPrice", Double.doubleToRawLongBits(currentPrice));
                editor.apply();

                priceBtn.setText(format.format(currentPrice));

                TextView textView = findViewById(R.id.request_sent);
                textView.setVisibility(View.VISIBLE);
                showAnim();
                orderOnClick();
            }
        });


        priceBtn.setText(String.valueOf(format.format(currentPrice)));
        localBroadcastManager.registerReceiver(listener, new IntentFilter("VOLUNTEER_FOUND"));

        inBackground = false;

    }

    @Override
    protected void onSaveInstanceState(Bundle outState) {
        super.onSaveInstanceState(outState);
        inBackground = true;
    }

    @Override
    protected void onResume() {
        super.onResume();
        inBackground = false;
        Intent intent = getIntent();
        User volunteer = loadVolunteer(intent);

        if (volunteer != null)
            showDialog(volunteer);
    }


    @Override
    protected void onDestroy() {

        if (senderService != null) {
            stopService(senderService);
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

    private User loadVolunteer(Intent intent){
        String json = intent.getStringExtra("USER");

        if (json == null)
            return null;

        return Convert.stringToClass(json, User.class);
    }

    private void orderOnClick(){
        startService(senderService);
    }

    private void showDialog(User volunteer){
        if (!inBackground) {
            DialogFragment dialog = new VolunteerDialogFragment();

            Bundle data = new Bundle();
            data.putString("NAME", volunteer.getName());
            data.putString("IMAGE", volunteer.getProfileImage());

            dialog.setArguments(data);
            dialog.show(getSupportFragmentManager(), "Volunteer");
        }
    }

    private void showAnim(){
        ImageView animView = findViewById(R.id.check_anim);
        animView.setImageResource(R.drawable.check_mark_anim);
        AnimatedVectorDrawable checkAnim = (AnimatedVectorDrawable) animView.getDrawable();
        checkAnim.start();
    }



}
