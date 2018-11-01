package grocerly.fhict.com.grocerly.adapters;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.ImageView;
import android.widget.TextView;

import com.squareup.picasso.Picasso;

import java.text.NumberFormat;
import java.util.ArrayList;
import java.util.List;

import grocerly.fhict.com.grocerly.R;
import grocerly.fhict.com.grocerly.models.Product;

public final class ProductsGridViewAdapter extends BaseAdapter {

    private final ArrayList<Product> mItems;
    private final int mCount;

    public ProductsGridViewAdapter(final List<Product> items) {
        mCount = items.size();
        mItems = new ArrayList<>(items);
    }

    @Override
    public int getCount() {
        return mCount;
    }

    @Override
    public Object getItem(final int position) {
        return mItems.get(position);
    }

    @Override
    public long getItemId(final int position) {
        return position;
    }

    @Override
    public View getView(final int position, final View convertView, final ViewGroup parent) {

        View view = convertView;

        if (view == null) {
            view = LayoutInflater.from(parent.getContext()).inflate(R.layout.product_cell, parent, false);
        }

        Product product = mItems.get(position);

        ImageView productImage = view.findViewById(R.id.product_image);
        Picasso.get().load(product.getImageUrl()).into(productImage);

        TextView productName = view.findViewById(R.id.product_name);
        TextView productVolume = view.findViewById(R.id.product_volume);
        TextView productPrice = view.findViewById(R.id.product_price);

        productName.setText(product.getName());
        productVolume.setText(product.getVolume());

        NumberFormat format = NumberFormat.getCurrencyInstance();
        productPrice.setText(format.format(product.getPrice()));

        return view;
    }
}
