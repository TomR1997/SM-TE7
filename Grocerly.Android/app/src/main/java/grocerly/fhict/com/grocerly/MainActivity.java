package grocerly.fhict.com.grocerly;

import android.content.Context;
import android.content.res.Configuration;
import android.graphics.drawable.AnimatedVectorDrawable;
import android.graphics.drawable.AnimationDrawable;
import android.graphics.drawable.VectorDrawable;
import android.os.Bundle;
import android.support.design.widget.FloatingActionButton;
import android.support.design.widget.Snackbar;
import android.util.Log;
import android.view.View;
import android.support.design.widget.NavigationView;
import android.support.v4.view.GravityCompat;
import android.support.v4.widget.DrawerLayout;
import android.support.v7.app.ActionBarDrawerToggle;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.Toolbar;
import android.view.Menu;
import android.view.MenuItem;
import android.widget.GridView;
import android.widget.ImageView;
import android.widget.ProgressBar;

import java.util.ArrayList;
import java.util.List;

import grocerly.fhict.com.grocerly.adapters.ProductsGridViewAdapter;
import grocerly.fhict.com.grocerly.models.Product;
import grocerly.fhict.com.grocerly.services.ProductService;
import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class MainActivity extends AppCompatActivity
        implements NavigationView.OnNavigationItemSelectedListener {

    GridView productsGrid;
    ProgressBar progressBar;

    ProductService productService;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        Toolbar toolbar = findViewById(R.id.toolbar);
        setSupportActionBar(toolbar);

        DrawerLayout drawer = findViewById(R.id.drawer_layout);
        ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(
                this, drawer, toolbar, R.string.navigation_drawer_open, R.string.navigation_drawer_close);
        drawer.addDrawerListener(toggle);
        toggle.syncState();

        NavigationView navigationView = findViewById(R.id.nav_view);
        navigationView.setNavigationItemSelectedListener(this);


        productsGrid = findViewById(R.id.products_grid);
        progressBar = findViewById(R.id.progressBar);

//        List<Product> products = new ArrayList<>();
//        for(int i = 0; i < 9; i++ ) {
//            products.add(new Product(
//                    "https://www.vomar.nl/cmsstatic/gewoonpindakaasnaturel600gr.jpg",
//                    "Calve Pindakaas",
//                    "350 gram",
//                    3.50
//            ));
//
//            products.add(new Product(
//                    "https://static.webshopapp.com/shops/241343/files/190291118/900x900x2/calve-pindakaas-met-stukjes-pinda.jpg",
//                    "Calve Pindakaas",
//                    "350 gram",
//                    3.50
//            ));
//
//        }
//      productsGrid.setAdapter(new ProductsGridViewAdapter(products));

        productsGrid.setNumColumns(calculateColumns(null));

        productService = new ProductService();
        searchProducts(12, 1, "");

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
    public void onBackPressed() {
        DrawerLayout drawer = (DrawerLayout) findViewById(R.id.drawer_layout);
        if (drawer.isDrawerOpen(GravityCompat.START)) {
            drawer.closeDrawer(GravityCompat.START);
        } else {
            super.onBackPressed();
        }
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.main, menu);
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        // Handle action bar item clicks here. The action bar will
        // automatically handle clicks on the Home/Up button, so long
        // as you specify a parent activity in AndroidManifest.xml.
        int id = item.getItemId();

        //noinspection SimplifiableIfStatement
        if (id == R.id.action_settings) {
            return true;
        }

        return super.onOptionsItemSelected(item);
    }

    @SuppressWarnings("StatementWithEmptyBody")
    @Override
    public boolean onNavigationItemSelected(MenuItem item) {
        // Handle navigation view item clicks here.
        int id = item.getItemId();

        if (id == R.id.nav_camera) {
            // Handle the camera action
        } else if (id == R.id.nav_gallery) {

        } else if (id == R.id.nav_slideshow) {

        } else if (id == R.id.nav_manage) {

        } else if (id == R.id.nav_share) {

        } else if (id == R.id.nav_send) {

        }

        DrawerLayout drawer = (DrawerLayout) findViewById(R.id.drawer_layout);
        drawer.closeDrawer(GravityCompat.START);
        return true;
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
