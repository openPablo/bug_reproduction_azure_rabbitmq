import pika
import json
import random
import time
import os

def send_metrics(i, channel):
    for _ in range(i):
        message = json.dumps({
            'cpu_usage': random.randint(0, 100),
            'memory_usage': random.randint(0, 100),
            'disk_usage': random.randint(0, 100)
            })
        channel.basic_publish(exchange='', routing_key='metrics', body=message)
    print(f"Sent: {i} messages")

if __name__ == "__main__":
     connected = False
     while not connected:
         try:
             connection = pika.BlockingConnection(pika.ConnectionParameters(os.environ['RABBITMQ_HOST']))
             channel = connection.channel()
             channel.queue_declare(queue='metrics')
             connected = True
         except Exception as e:
            print(e)
            time.sleep(5)
     while True:
         send_metrics(50000, channel)

