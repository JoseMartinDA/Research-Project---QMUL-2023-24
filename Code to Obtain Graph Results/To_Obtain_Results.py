import serial
import time
import pandas as pd
import csv

# Replace 'COM3' with the appropriate port for your system
ser = serial.Serial('/dev/cu.usbmodem2101', 19200)

time.sleep(2)  # Wait for the serial connection to initialize

data = []
start_time = time.time()

while True:
    try:
        line = ser.readline().decode('utf-8').strip()
        print(line)  # Print the data for debugging

        # Split the line into time and Index_Finger values
        parts = line.split(',')
        if len(parts) == 2:
            elapsed_time = float(parts[0])
            index_finger_value = float(parts[1])
            data.append([elapsed_time, index_finger_value])

        # Stop after 60 seconds (you can adjust this)
        if time.time() - start_time > 60:
            break
    except KeyboardInterrupt:
        break  # Allow manual interruption with Ctrl+C

ser.close()

# Save the data to a CSV file
df = pd.DataFrame(data, columns=["Time (s)", "Index_Finger"])
df.to_csv("Index_Finger_Data.csv", index=False)

print("Data saved to Index_Finger_Data.csv")
