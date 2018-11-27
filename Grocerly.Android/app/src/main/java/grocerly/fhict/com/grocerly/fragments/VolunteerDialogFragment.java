package grocerly.fhict.com.grocerly.fragments;

import android.app.AlertDialog;
import android.app.Dialog;
import android.content.DialogInterface;
import android.os.Bundle;
import android.support.v4.app.DialogFragment;
import android.view.View;
import android.widget.ImageView;
import android.widget.TextView;

import com.squareup.picasso.Picasso;

import grocerly.fhict.com.grocerly.R;


public class VolunteerDialogFragment extends DialogFragment {

    @Override
    public Dialog onCreateDialog( Bundle savedInstanceState) {

        Bundle data = getArguments();
        String name = null;
        String image = null;
        if (data != null) {
            name = data.getString("NAME");
            image = data.getString("IMAGE");
        }

        AlertDialog.Builder builder = new AlertDialog.Builder(getActivity());
        builder.setMessage("We hebben een vrijwilliger voor je gevonden!")
                .setPositiveButton("OK", new DialogInterface.OnClickListener() {
                    public void onClick(DialogInterface dialog, int id) {
                        VolunteerDialogFragment.this.getDialog().dismiss();
                    }
                });

        View view = View.inflate(getContext(), R.layout.volunteer_dialog, null);
        view.setClipToOutline(true);

        ImageView profilePic = view.findViewById(R.id.volunteer_image);
        TextView nameText = view.findViewById(R.id.volunteer_name);

        Picasso.get().load(image).placeholder(R.drawable.profile_placeholder).into(profilePic);
        nameText.setText(name);

        builder.setView(view);

        return builder.create();
    }
}
