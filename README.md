Observable System Demo
Overview
This repository contains a simple demo system designed to demonstrate observability using multiple components like Elasticsearch, RabbitMQ, Prometheus, Jaeger, and Redis. The system is containerized using Docker and Docker Compose for ease of setup and execution. This project showcases how observability is integrated into a system through monitoring, distributed tracing, and logging.

System Architecture
The system includes the following components:

Elasticsearch: A search and analytics engine for storing logs and metrics data.
RabbitMQ: A message broker used for communication between services.
Prometheus: A system monitoring and alerting toolkit used to collect and store metrics.
Grafana: A visualization tool used for creating dashboards to visualize Prometheus metrics.
Jaeger: A distributed tracing system that collects and visualizes traces across microservices.
Redis: An in-memory key-value store used for caching and temporary data storage.
Docker Compose: Used to define and run multi-container Docker applications. It encapsulates all dependencies in a single file for easy execution.
Tools and Technologies
Elasticsearch: Provides centralized storage for logs and allows fast searches on log data.
RabbitMQ: Handles asynchronous communication between services, making it scalable.
Prometheus: Collects and stores metrics data, providing powerful querying capabilities.
Grafana: Displays Prometheus metrics data on dashboards for real-time monitoring.
Jaeger: A distributed tracing tool to trace requests across services for performance monitoring.
Redis: Used for session management and temporary data caching.


 +-----------------+        +------------------+       +-----------------+
 |   Microservice  | -----> |     RabbitMQ     | ----> |   Microservice  |
 +-----------------+        +------------------+       +-----------------+
            |                        |                        |
            |                        v                        v
            |                +------------------+       +------------------+
            |                |     Redis        |       |    Elasticsearch |
            |                +------------------+       +------------------+
            |                        |                        |
            v                        v                        v
       +-----------------+      +-------------------+     +-------------------+
       |    Jaeger       | ---> |   Prometheus      | --> |   Grafana         |
       +-----------------+      +-------------------+     +-------------------+
