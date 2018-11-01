package grocerly.fhict.com.grocerly.services.interfaces;

import java.util.List;
import java.util.Map;

import grocerly.fhict.com.grocerly.models.Product;
import retrofit2.Call;
import retrofit2.http.GET;
import retrofit2.http.Query;
import retrofit2.http.QueryMap;

public interface IProductService {

    @GET("api/products/")
    Call<List<Product>> allProducts(@QueryMap Map<String, String> parameters);
}
