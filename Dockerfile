FROM python:3.9-slim
WORKDIR /app
COPY spam.py .
RUN pip install pika
CMD ["python", "spam.py"]