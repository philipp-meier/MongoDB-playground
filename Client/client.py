#!/usr/bin/python3
import random
import requests
from datetime import datetime, timedelta

sensor_id = "91022"
max_entries = 1_000_000
timestamp = datetime(2023, 1, 1)
import_endpoint = 'http://localhost:5109/api/sensordata/import'

entries = []
for n in range(max_entries):
    timestamp = timestamp + timedelta(milliseconds=300)
    entry = {
        "metadata": {
            "sensorId": sensor_id,
            "version": "1.0"
        },
        "timestamp": timestamp.isoformat(),
        "temperature": round(random.uniform(0, 36), 2)
    }
    # A to L
    for letter in list(map(chr, range(65, 77))):
        if random.randint(0, 1) == 1:
            entry[letter] = random.randint(0, 5)
    
    entries.append(entry)

resp = requests.post(import_endpoint, json = entries)
print(f"Response time (in seconds): {resp.elapsed.total_seconds()}")