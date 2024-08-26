void setup() {
  Serial.begin(19200);
  pinMode(A0, INPUT); // For Hall-Effect Sensor
  pinMode(6, OUTPUT); // For Coin Vibration Motor
}

void loop() {
  /// Use it to Calibrate the sensor first: Read (raw data vs Angle) to find linear regression ///
  // int raw = analogRead(A0);
  // Serial.println(raw);
  // delay(100); // use it to read raw data

  /// With calibration ///
  float Index_Finger = calibration(analogRead(A0)); //
  Serial.println(Index_Finger); // displaying angle position

  /// Vibrate if it reaches -80 degrees ///
  if(Index_Finger <= -80){
    vibrate();
  }
}

/// Function for Calibration with Linear Regression ///
int calibration(int x){
  x = 1.24 * x + -696; // 15 to -90 degrees - experiment 5: works great :)
  return x;
}

/// To make motor vibrate ///
void vibrate(){
  digitalWrite(6, HIGH); // ON
  delay(500);
  digitalWrite(6, LOW); // OFF
  delay(500);
}
