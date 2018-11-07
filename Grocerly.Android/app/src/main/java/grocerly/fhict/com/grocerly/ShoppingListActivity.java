package grocerly.fhict.com.grocerly;

import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;

public class ShoppingListActivity extends BaseActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
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
