package grocerly.fhict.com.grocerly.services;

import java.io.IOException;
import java.util.List;

import grocerly.fhict.com.grocerly.models.Product;
import grocerly.fhict.com.grocerly.models.ShoppingList;
import grocerly.fhict.com.grocerly.services.interfaces.IProductService;
import grocerly.fhict.com.grocerly.services.interfaces.IShoppingListService;
import okhttp3.Interceptor;
import okhttp3.OkHttpClient;
import okhttp3.Request;
import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

public class ShoppingListService {

    private IShoppingListService service;
    private static String JWT = "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1lIjoiVXNlciAxIiwiaWQiOiJlYTc4NDdlNS03ZTM0LTQzYWItYjhmMC0yZTc3YWMyOTQ4NWUiLCJyb2xlcyI6IlVzZXIiLCJuYmYiOjE1NDA3Mjg2ODMsImV4cCI6MTU0MTMzMzQ4MywiaWF0IjoxNTQwNzI4NjgzfQ.ZrnC9xSMowfWduNoAay5upIvB4J5Cw34ENhTVat1RvU";

    public ShoppingListService() {
        OkHttpClient.Builder httpClient = new OkHttpClient.Builder();

        httpClient.addInterceptor(new Interceptor() {
            @Override
            public okhttp3.Response intercept(Chain chain) throws IOException {
                Request request = chain.request().newBuilder().addHeader("Authorization", JWT).build();
                return chain.proceed(request);
            }
        });

        Retrofit retrofit = new Retrofit.Builder()
                .addConverterFactory(GsonConverterFactory.create())
                .baseUrl("http://i315103core.venus.fhict.nl")
                .client(httpClient.build())
                .build();

        service = retrofit.create(IShoppingListService.class);
    }

    public void getShoppingListItems(String id, Callback<List<Product>> callBack) {
        Call<List<Product>> call = service.getShoppingListItems(id);
        call.enqueue(callBack);
    }

    public void allShoppinglists(Callback<List<ShoppingList>> callBack) {
        Call<List<ShoppingList>> call = service.allShoppingLists();
        call.enqueue(callBack);
    }
}
