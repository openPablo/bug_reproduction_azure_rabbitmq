# bug_reproduction_azure_rabbitmq

## run locally
- Install azure-functions-core-tools
- Install docker compose

```
docker-compose build
docker-compose up
func start
```
This will start:
- a rabbitmq instance
- a python script that starts spamming rabbitmq and fills it with 200k messages.
- an azure function which reads rabbitmq and outputs time spent

Rabbitmq dashboard:
- http://localhost:15672
  - username:guest, password:guest

## how to reproduce bug:

- Run this locally, confirm how much time this took, and open rabbitmq dashboard to see metrics
- Go to csproj file and update "Microsoft.Azure.Functions.Worker.Extensions.RabbitMQ" from "2.0.3"
- Restart docker compose, restart function, and confirm it was much slower

