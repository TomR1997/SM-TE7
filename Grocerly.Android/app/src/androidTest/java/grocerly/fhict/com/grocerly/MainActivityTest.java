package grocerly.fhict.com.grocerly;

import org.junit.Before;
import org.junit.Rule;
import org.junit.Test;
import org.junit.runner.RunWith;

import androidx.test.ext.junit.runners.AndroidJUnit4;
import androidx.test.filters.LargeTest;
import androidx.test.rule.ActivityTestRule;

import static androidx.test.espresso.Espresso.onView;
import static androidx.test.espresso.action.ViewActions.pressImeActionButton;
import static androidx.test.espresso.action.ViewActions.typeText;
import static androidx.test.espresso.assertion.ViewAssertions.matches;
import static androidx.test.espresso.matcher.ViewMatchers.hasChildCount;
import static androidx.test.espresso.matcher.ViewMatchers.withId;
import static androidx.test.platform.app.InstrumentationRegistry.getInstrumentation;

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
