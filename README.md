Project Overview
This project is a .NET-based application designed to demonstrate the use of observability features, including tracing, logging, and metrics. The project is structured into several key components and showcases how these observability features can be integrated into various systems such as APIs, RabbitMQ consumers, and more.

Folder Structure
The project consists of the following main components:

/DotNetObservabilitySample
│
├── /src
│   ├── Sample.TimeApi               # API for Time Service
│   ├── Sample.RabbitMQProcessor     # RabbitMQ Consumer for processing messages
│   ├── Sample.RabbitMQCollector     # RabbitMQ message collection and management
│   ├── Sample.MainApi               # Main API that aggregates different services
│   └── Sample.Common                # Shared utilities and constants across the application
├── docker-compose.yml               # Docker configuration for multi-container setup
├── Dockerfile                       # Dockerfile for building the application image
└── README.md                        # Project documentation

Components Breakdown
Sample.TimeApi: A web API service that provides time-related data and serves as the base for API-related observability.

Sample.RabbitMQProcessor: Contains logic for consuming messages from RabbitMQ queues, with observability implemented for tracing and logging of message processing.

Sample.RabbitMQCollector: A service for collecting RabbitMQ metrics and integrating them with observability tools like OpenTelemetry.

Sample.MainApi: The main entry point for aggregating all services and providing an interface for interacting with the rest of the system.

Sample.Common: This folder contains shared utilities, constants, and configurations used across multiple components in the application.


