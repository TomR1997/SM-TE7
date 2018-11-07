package grocerly.fhict.com.grocerly;

import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;

public class ShoppingListActivity extends BaseActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_shopping_list);
    }

    @Override
    protected int getLayoutResourceId() {
        return 2;
    }

    @Override
    protected int getActivityID() {
        return 0;
    }
}
