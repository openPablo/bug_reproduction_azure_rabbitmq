# Install pika with: pip install pika
import pika
import json
import random

def send_metrics():
    connection = pika.BlockingConnection(pika.ConnectionParameters('localhost'))
    channel = connection.channel()
    channel.queue_declare(queue='metrics')

    for _ in range(5):
        metric_data = {
            'cpu_usage': random.randint(0, 100),
            'memory_usage': random.randint(0, 100),
            'disk_usage': random.randint(0, 100)
        }
        message = json.dumps(metric_data)
        channel.basic_publish(exchange='', routing_key='metrics', body=message)
        print(f"Sent: {message}")

    connection.close()

def read_metrics():
    connection = pika.BlockingConnection(pika.ConnectionParameters('localhost'))
    channel = connection.channel()
    channel.queue_declare(queue='metrics')

    def callback(ch, method, properties, body):
        message = json.loads(body)
        print(f"Received: {message}")

    channel.basic_consume(queue='metrics', on_message_callback=callback, auto_ack=True)
    print('Waiting for messages. To exit press CTRL+C')
    channel.start_consuming()

if __name__ == "__main__":
    send_metrics()
