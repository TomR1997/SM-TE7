package grocerly.fhict.com.grocerly;

import android.support.test.filters.LargeTest;
import android.support.test.rule.ActivityTestRule;
import android.support.test.runner.AndroidJUnit4;

import org.junit.Before;
import org.junit.Rule;
import org.junit.Test;
import org.junit.runner.RunWith;

import static android.support.test.InstrumentationRegistry.getInstrumentation;
import static android.support.test.espresso.Espresso.onView;
import static android.support.test.espresso.action.ViewActions.pressImeActionButton;
import static android.support.test.espresso.action.ViewActions.typeText;
import static android.support.test.espresso.assertion.ViewAssertions.matches;
import static android.support.test.espresso.matcher.ViewMatchers.hasChildCount;
import static android.support.test.espresso.matcher.ViewMatchers.withId;


@RunWith(AndroidJUnit4.class)
@LargeTest
public class MainActivityTest {

    private String searchKey;

    @Rule
    public ActivityTestRule<MainActivity> mActivityRule
            = new ActivityTestRule<>(MainActivity.class);

    @Before
    public void setup(){
        searchKey = "koekjes";
    }

    @Test
    public void testSearch(){
        onView(withId(R.id.searchView))
                .perform(typeText(searchKey))
                .perform(pressImeActionButton());

        getInstrumentation().waitForIdleSync();

        onView(withId(R.id.products_grid))
                .check(matches(hasChildCount(2)));

    }
}
