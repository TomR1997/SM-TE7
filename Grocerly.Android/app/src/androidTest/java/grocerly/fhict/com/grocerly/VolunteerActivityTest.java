package grocerly.fhict.com.grocerly;

import android.app.Activity;
import android.app.Instrumentation;
import android.content.Intent;

import org.junit.Rule;
import org.junit.Test;
import org.junit.runner.RunWith;

import androidx.test.espresso.intent.rule.IntentsTestRule;
import androidx.test.ext.junit.runners.AndroidJUnit4;
import androidx.test.filters.LargeTest;

import static androidx.test.espresso.Espresso.onView;
import static androidx.test.espresso.action.ViewActions.click;
import static androidx.test.espresso.intent.Intents.intended;
import static androidx.test.espresso.intent.Intents.intending;
import static androidx.test.espresso.intent.matcher.IntentMatchers.toPackage;
import static androidx.test.espresso.matcher.ViewMatchers.withId;

@RunWith(AndroidJUnit4.class)
@LargeTest
public class VolunteerActivityTest {

    @Rule
    public IntentsTestRule<VolunteerActivity> vIntentRule
            = new IntentsTestRule<>(VolunteerActivity.class);

    @Test
    public void startBarcodeScannerTest(){

        Intent resultData = new Intent();
        String barcode = "12345678";
        resultData.putExtra("Barcode", barcode);
        Instrumentation.ActivityResult result =
                new Instrumentation.ActivityResult(Activity.RESULT_OK, resultData);

        onView(withId(R.id.startBarcodeScanner))
                .perform(click());

        intending(toPackage("grocerly.fhict.com.grocerly"))
            .respondWith(result);
    }
}
