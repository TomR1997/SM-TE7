package grocerly.fhict.com.grocerly;

import android.Manifest;
import android.content.Context;
import android.content.pm.PackageManager;
import android.os.Bundle;
import android.os.Vibrator;
import android.support.v4.app.ActivityCompat;
import android.util.SparseArray;
import android.view.SurfaceHolder;
import android.view.SurfaceView;
import android.view.View;
import android.widget.TextView;
import android.widget.Toast;

import com.google.android.gms.vision.CameraSource;
import com.google.android.gms.vision.Detector;
import com.google.android.gms.vision.barcode.Barcode;
import com.google.android.gms.vision.barcode.BarcodeDetector;

import java.io.IOException;

public class VolunteerActivity extends BaseActivity {

    private SurfaceView surfaceView;
    private CameraSource cameraSource;
    private TextView textView;
    private BarcodeDetector barcodeDetector;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        surfaceView = findViewById(R.id.camerapreview);
        textView = findViewById(R.id.textView);

        requestPermission(this);

        barcodeDetector = new BarcodeDetector.Builder(this)
                //.setBarcodeFormats(Barcode.QR_CODE)
                .setBarcodeFormats(Barcode.ALL_FORMATS)
                .build();

        cameraSource = new CameraSource.Builder(this, barcodeDetector)
                //.setRequestedPreviewSize(640, 480)
                .setAutoFocusEnabled(true)
                .build();

        surfaceView.getHolder().addCallback(new SurfaceHolder.Callback() {
            @Override
            public void surfaceCreated(SurfaceHolder holder) {
                if (ActivityCompat.checkSelfPermission(getApplicationContext(), Manifest.permission.CAMERA) != PackageManager.PERMISSION_GRANTED) {
                    return;
                }
                try {
                    cameraSource.start(holder);
                } catch(IOException ex){
                    ex.printStackTrace();
                }

            }

            @Override
            public void surfaceChanged(SurfaceHolder holder, int format, int width, int height) {

            }

            @Override
            public void surfaceDestroyed(SurfaceHolder holder) {
                cameraSource.stop();
            }
        });

        barcodeDetector.setProcessor(new Detector.Processor<Barcode>() {
            @Override
            public void release() {
                surfaceView.setVisibility(View.INVISIBLE);
                cameraSource.stop();
            }

            @Override
            public void receiveDetections(Detector.Detections<Barcode> detections) {
                final SparseArray<Barcode> qrCodes = detections.getDetectedItems();
                if(qrCodes.size() > 0){
                    textView.post(new Runnable() {
                        @Override
                        public void run() {
                            Vibrator vibrator = (Vibrator) getApplicationContext().getSystemService(Context.VIBRATOR_SERVICE);
                            assert vibrator != null;
                            vibrator.vibrate(1000);
                            textView.setText(qrCodes.valueAt(0).displayValue);
                            release();
                        }
                    });
                }
            }
        });
    }

    private void requestPermission(Context context) {
        if (ActivityCompat.checkSelfPermission(getApplicationContext(),
                android.Manifest.permission.CAMERA) != PackageManager.PERMISSION_GRANTED) {
            VolunteerActivity volunteerActivity = (VolunteerActivity) context;

            if (ActivityCompat.shouldShowRequestPermissionRationale(volunteerActivity,
                    Manifest.permission.CAMERA)) {
                Toast.makeText(context,
                        "Camera permission is needed.",
                        Toast.LENGTH_SHORT).show();
            }
            ActivityCompat.requestPermissions(volunteerActivity,
                    new String[]{Manifest.permission.CAMERA}, 1);
        }
    }


    @Override
    protected int getLayoutResourceId() {
        return R.layout.activity_volunteer;
    }

    @Override
    protected int getActivityID() {
        return 2;
    }
}
