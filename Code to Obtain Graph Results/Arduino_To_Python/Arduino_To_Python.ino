unsigned long previousMillis = 0; // To store the last time the data was sent
const long interval = 1000; // Interval in milliseconds (e.g., 1000 ms = 1 second)

void setup() {
  Serial.begin(19200);
  pinMode(A0, INPUT); // For Hall-Effect Sensor
  pinMode(6, OUTPUT); // For Coin Vibration Motor
}

void loop() {
  unsigned long currentMillis = millis(); // Get the current time in milliseconds

  
  if (currentMillis - previousMillis >= interval) {
    previousMillis = currentMillis; // Update the time

    // Read and calibrate the sensor value
    float Index_Finger = calibration(analogRead(A0)); // Probably with Float???
    Serial.print(currentMillis / 1000.0); // Print time in seconds
    Serial.print(","); // Comma separator for CSV format
    Serial.println(Index_Finger); // Display angle position


  // Vibrate once it reaches -80 degrees
  if (Index_Finger <= -80) {
    vibrate();
  }
  }
}

/// Function for Calibration with Linear Regression ///
int calibration(int x){
  x = 1.25 * x + -704; // 15 to -90 degrees - experiment 5: works great :)
  return x;
}

/// To make motor vibrate ///
void vibrate(){
  digitalWrite(6, HIGH); // ON
  delay(500);
  digitalWrite(6, LOW); // OFF
  delay(500);
}
