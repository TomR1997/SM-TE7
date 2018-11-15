package grocerly.fhict.com.grocerly;

import android.Manifest;
import android.content.Context;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.content.res.Configuration;
import android.os.Bundle;
import android.speech.RecognizerIntent;
import android.support.annotation.NonNull;
import android.support.design.widget.NavigationView;
import android.support.v4.app.ActivityCompat;
import android.support.v4.content.ContextCompat;
import android.support.v4.view.GravityCompat;
import android.support.v4.widget.DrawerLayout;
import android.support.v7.app.ActionBarDrawerToggle;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.Toolbar;
import android.text.Editable;
import android.text.TextWatcher;
import android.util.Log;
import android.view.KeyEvent;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.view.inputmethod.EditorInfo;
import android.view.inputmethod.InputMethodManager;
import android.widget.EditText;
import android.widget.GridView;
import android.widget.ImageButton;
import android.widget.ProgressBar;
import android.widget.TextView;
import android.widget.Toast;

import java.util.ArrayList;
import java.util.List;
import java.util.Locale;

import grocerly.fhict.com.grocerly.adapters.ProductsGridViewAdapter;
import grocerly.fhict.com.grocerly.models.Product;
import grocerly.fhict.com.grocerly.services.ProductService;
import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class MainActivity extends BaseActivity {

    private static final int MY_PERMISSION_REQUEST = 1;
    private static final int REQ_CODE_SPEECH_INPUT = 100;

    GridView productsGrid;
    ProgressBar progressBar;
    EditText searchView;
    ProductService productService;
    ImageButton microphoneButton;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        productsGrid = findViewById(R.id.products_grid);
        progressBar = findViewById(R.id.progressBar);

        productsGrid.setNumColumns(calculateColumns(null));

        productService = new ProductService();
        searchProducts(12, 1, "");

        searchView = findViewById(R.id.searchView);
        microphoneButton = findViewById(R.id.microphoneButton);
        microphoneButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                startVoiceInput();
            }
        });

        searchView.setOnEditorActionListener(new TextView.OnEditorActionListener() {
            @Override
            public boolean onEditorAction(TextView v, int actionId, KeyEvent event) {
                if (actionId == EditorInfo.IME_ACTION_SEARCH){
                    if (event == null || !event.isShiftPressed()) {
                        searchProducts(6, 1, v.getText().toString());

                        InputMethodManager imm = (InputMethodManager)getSystemService(Context.INPUT_METHOD_SERVICE);
                        imm.hideSoftInputFromWindow(searchView.getWindowToken(),
                                InputMethodManager.RESULT_UNCHANGED_SHOWN);

                        return true;
                    }
                }
                return false;
            }
        });

    }

    @Override
    protected int getLayoutResourceId() {
        return R.layout.activity_main;
    }

    @Override
    protected int getActivityID() {
        return 0;
    }

    private void startVoiceInput(){
        if(requestPermission(this)){
            Intent intent = new Intent(RecognizerIntent.ACTION_RECOGNIZE_SPEECH);
            intent.putExtra(RecognizerIntent.EXTRA_LANGUAGE_MODEL, RecognizerIntent.LANGUAGE_MODEL_FREE_FORM);
            intent.putExtra(RecognizerIntent.EXTRA_LANGUAGE, Locale.getDefault());
            intent.putExtra(RecognizerIntent.EXTRA_PROMPT, "Hallo, welk product wil je zoeken?");
            startActivityForResult(intent, REQ_CODE_SPEECH_INPUT);
        }
    }

    @Override
    public void onRequestPermissionsResult(int requestCode, @NonNull String[] permissions, @NonNull int[] grantResults){
        switch (requestCode){
            case MY_PERMISSION_REQUEST: {
                if (grantResults.length > 0 && grantResults[0] == PackageManager.PERMISSION_GRANTED){
                    if (ContextCompat.checkSelfPermission(MainActivity.this,
                            Manifest.permission.READ_EXTERNAL_STORAGE) == PackageManager.PERMISSION_GRANTED){
                        Toast.makeText(this, "Permission granted!", Toast.LENGTH_SHORT).show();
                    }
                } else {
                    Toast.makeText(this, "No permission granted!", Toast.LENGTH_SHORT).show();
                    finish();
                }
                break;
            }
        }
    }

    private boolean requestPermission(Context context){
        MainActivity mainActivity = (MainActivity) context;
        if (ContextCompat.checkSelfPermission(context,
                Manifest.permission.RECORD_AUDIO) != PackageManager.PERMISSION_GRANTED) {
            if (ActivityCompat.shouldShowRequestPermissionRationale(mainActivity,
                    Manifest.permission.RECORD_AUDIO)){
                Toast.makeText(context,
                        "Microphone permission is needed to search products by speech.",
                        Toast.LENGTH_SHORT).show();
            }
            ActivityCompat.requestPermissions(mainActivity,
                    new String[]{Manifest.permission.RECORD_AUDIO}, MY_PERMISSION_REQUEST);
        } else {
            return true;
        }
        return false;
    }

    @Override
    protected void onActivityResult(int requestCode, int resultCode, Intent data){
        super.onActivityResult(requestCode, resultCode, data);

        switch(requestCode){
            case REQ_CODE_SPEECH_INPUT:{
                if (resultCode == RESULT_OK && data != null){
                    ArrayList<String> result = data.getStringArrayListExtra(RecognizerIntent.EXTRA_RESULTS);
                    searchView.setText(result.get(0));
                    searchProducts(4, 1, searchView.getText().toString());
                }
                break;
            }
        }
    }

    private void searchProducts(int numberOfRows, int page, String searchTerm){
        productService.allProducts(numberOfRows, page, searchTerm,
                new Callback<List<Product>>() {

                    @Override
                    public void onResponse(Call<List<Product>> call, Response<List<Product>> response) {
                        List<Product> products = response.body();
                        if (products == null)
                            return;
                        productsGrid.setAdapter(
                                new ProductsGridViewAdapter(products)
                        );

                        progressBar.setVisibility(View.INVISIBLE);
                    }

                    @Override
                    public void onFailure(Call<List<Product>> call, Throwable throwable) {
                        Log.e("products", throwable.getMessage());
                        progressBar.setVisibility(View.INVISIBLE);
                    }
                });
    }

    @Override
    public void onConfigurationChanged(Configuration newConfig) {
        super.onConfigurationChanged(newConfig);

        productsGrid.setNumColumns(calculateColumns(newConfig));
    }

    private int calculateColumns(Configuration config){
        int columns;
        if (isTablet(this))
            columns = 3;
        else
            columns = 2;

        if (config != null && config.orientation == Configuration.ORIENTATION_LANDSCAPE)
            columns *= 2;

        return columns;
    }

    public static boolean isTablet(Context context) {
        return (context.getResources().getConfiguration().screenLayout
                & Configuration.SCREENLAYOUT_SIZE_MASK)
                >= Configuration.SCREENLAYOUT_SIZE_LARGE;
    }
}
