package grocerly.fhict.com.grocerly;

import android.os.Bundle;

public class VolunteerActivity extends BaseActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
    }

    @Override
    protected int getLayoutResourceId() {
        return R.layout.activity_volunteer;
    }

    @Override
    protected int getActivityID() {
        return 2;
    }
}
