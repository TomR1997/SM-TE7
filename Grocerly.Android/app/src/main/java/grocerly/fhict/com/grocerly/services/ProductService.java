package grocerly.fhict.com.grocerly.services;

import java.io.IOException;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import grocerly.fhict.com.grocerly.models.Product;
import grocerly.fhict.com.grocerly.services.interfaces.IProductService;
import okhttp3.Interceptor;
import okhttp3.OkHttpClient;
import okhttp3.Request;
import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

public class ProductService {

    private IProductService service;

    private static String JWT = "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1lIjoiVXNlciAxIiwiaWQiOiJlYTc4NDdlNS03ZTM0LTQzYWItYjhmMC0yZTc3YWMyOTQ4NWUiLCJyb2xlcyI6IlVzZXIiLCJuYmYiOjE1NDA3Mjg2ODMsImV4cCI6MTU0MTMzMzQ4MywiaWF0IjoxNTQwNzI4NjgzfQ.ZrnC9xSMowfWduNoAay5upIvB4J5Cw34ENhTVat1RvU";

    public ProductService(){

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

        service = retrofit.create(IProductService.class);
    }

    public void allProducts(int numberOfRows, int page, String searchTerm, Callback<List<Product>> callBack) {

        Map<String, String> params = new HashMap<>();
        params.put("numberOfRows", Integer.toString(numberOfRows));
        params.put("page", Integer.toString(page));

        if (!"".equals(searchTerm))
            params.put("name", searchTerm);

        Call<List<Product>> call = service.allProducts(params);
        call.enqueue(callBack);
    }
}
