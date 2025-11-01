# bug_reproduction_azure_rabbitmq

run locally:
- Install azure-functions-core-tools
- Install docker compose

```
docker-compose up
func start
```
This will start:
- a rabbitmq instance
- a python script that starts spamming rabbitmq
- an azure function which reads rabbitmq
- a jaeger instance which gets opentelemetry data

Rabbitmq dashboard:
- http://localhost:15672

Jaeger dashboard:
- http://localhost:16686