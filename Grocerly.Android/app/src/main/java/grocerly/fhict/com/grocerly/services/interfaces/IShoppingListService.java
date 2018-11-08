package grocerly.fhict.com.grocerly.services.interfaces;

import java.util.List;

import grocerly.fhict.com.grocerly.models.Product;
import grocerly.fhict.com.grocerly.models.ShoppingList;
import retrofit2.Call;
import retrofit2.http.GET;
import retrofit2.http.Path;

public interface IShoppingListService {
    @GET("api/shoppinglist/{id}/products")
    Call<List<Product>> getShoppingListItems(@Path("id") String id);

    @GET("api/shoppinglist/")
    Call<List<ShoppingList>> allShoppingLists();

}
