package grocerly.fhict.com.grocerly;

import android.Manifest;
import android.content.Context;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.os.Bundle;
import android.support.v4.app.ActivityCompat;
import android.util.Log;
import android.view.View;
import android.widget.ImageView;
import android.widget.TextView;
import android.widget.Toast;

import com.squareup.picasso.Picasso;

import java.util.List;

import grocerly.fhict.com.grocerly.models.Product;
import grocerly.fhict.com.grocerly.models.ShoppingList;
import grocerly.fhict.com.grocerly.services.ProductService;
import grocerly.fhict.com.grocerly.services.ShoppingListService;
import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class VolunteerActivity extends BaseActivity {

    private ShoppingListService shoppingListService;
    private ProductService productService;
    private List<ShoppingList> shoppingLists;
    private List<Product> products;
    private TextView productView;
    private ImageView imageView;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        productView = findViewById(R.id.productView);
        imageView = findViewById(R.id.productImageView);

        productService = new ProductService();
        searchProducts(12, 1, "");
        shoppingListService = new ShoppingListService();
    }

    private void requestPermission(Context context) {
        if (ActivityCompat.checkSelfPermission(getApplicationContext(),
                android.Manifest.permission.CAMERA) != PackageManager.PERMISSION_GRANTED) {
            VolunteerActivity volunteerActivity = (VolunteerActivity) context;

            if (ActivityCompat.shouldShowRequestPermissionRationale(volunteerActivity,
                    Manifest.permission.CAMERA)) {
                Toast.makeText(context,
                        "Camera permission is needed.",
                        Toast.LENGTH_SHORT).show();
            }
            ActivityCompat.requestPermissions(volunteerActivity,
                    new String[]{Manifest.permission.CAMERA}, 1);
        }
    }

    private void allShoppingLists(){
        shoppingListService.allShoppinglists(new Callback<List<ShoppingList>>() {
            @Override
            public void onResponse(Call<List<ShoppingList>> call, Response<List<ShoppingList>> response) {
                List<ShoppingList> allShoppingLists = response.body();
                if(shoppingLists == null){
                    return;
                }
                shoppingLists = allShoppingLists;
            }

            @Override
            public void onFailure(Call<List<ShoppingList>> call, Throwable t) {
                Log.e("shoppingLists", t.getMessage());
            }
        });
    }

    private void getShoppingListItems(){
        if (!shoppingLists.isEmpty()){
            shoppingListService.getShoppingListItems(shoppingLists.get(0).getId(), new Callback<List<Product>>() {
                @Override
                public void onResponse(Call<List<Product>> call, Response<List<Product>> response) {
                    List<Product> products = response.body();
                    if(products == null){
                        return;
                    }
                    //TODO set adapter
                }

                @Override
                public void onFailure(Call<List<Product>> call, Throwable t) {
                    Log.e("products", t.getMessage());
                }
            });
        }

    }

    private void searchProducts(int numberOfRows, int page, String searchTerm){
        productService.allProducts(numberOfRows, page, searchTerm,
                new Callback<List<Product>>() {

                    @Override
                    public void onResponse(Call<List<Product>> call, Response<List<Product>> response) {
                        products = response.body();
                        getProductFromIntent();
                    }

                    @Override
                    public void onFailure(Call<List<Product>> call, Throwable throwable) {
                        Log.e("products", throwable.getMessage());
                    }
                });
    }

    private Product findProductByBarcode(int barcode){
        if(products!=null){
            for (Product p : products) {
                if (p.getBarcode() == barcode){
                    return p;
                }
            }
        }
        return null;
    }

    private void getProductFromIntent(){
        int barcode;
        Intent intent = getIntent();
        if (intent != null){
            barcode = intent.getIntExtra("Barcode", 0);
            if (barcode != 0){
                Product product = findProductByBarcode(barcode);
                if(product != null){
                    productView.setText(product.getName());
                    Picasso.get().load(product.getImageUrl()).into(imageView);
                    productView.setVisibility(View.VISIBLE);
                    imageView.setVisibility(View.VISIBLE);
                }
            }
        }
    }

    @Override
    protected int getLayoutResourceId() {
        return R.layout.activity_volunteer;
    }

    @Override
    protected int getActivityID() {
        return 2;
    }

    public void startBarcodeScanner(View view) {
        requestPermission(this);
        startActivity(new Intent(this, BarcodeScannerActivity.class));
    }
}
