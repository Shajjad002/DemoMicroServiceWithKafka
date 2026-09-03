# DemoMicroServiceWithKafka

Minimal demo .NET microservice project that integrates with Apache Kafka.

## Summary

This repository contains a sample .NET 8 microservice demonstrating how to produce and consume messages using Kafka. It is intended as a small starting point for learning or prototyping event-driven microservices.

## Prerequisites

- .NET SDK 8.0 (https://dotnet.microsoft.com/download)
- Apache Kafka running and accessible (default: `localhost:9092`)
- (Optional) Docker and Docker Compose if you want to run Kafka locally via containers

## Build

From the repository root:

```
dotnet build
```

Or build a specific project:

```
dotnet build <path-to-csproj>
```

## Run

Ensure Kafka is running, then run the service from the repository root:

```
dotnet run --project <path-to-your-service-csproj>
```

Replace `<path-to-your-service-csproj>` with the actual project file path (for example `src/NotificationService/NotificationService.csproj`).

## Configuration

Configuration values (Kafka bootstrap servers, topics, connection strings, etc.) are typically defined in `appsettings.json` or environment variables. Update those values to match your environment.

## Docker (optional)

You can run Kafka locally using Docker Compose. Example steps:

1. Start Zookeeper and Kafka with a suitable `docker-compose.yml`.
2. Configure the service to use the container's bootstrap server address (usually `localhost:9092`).

## Contributing

Contributions are welcome. Open issues or pull requests to propose changes.

## License

This project does not include a license file. Add a `LICENSE` file if you intend to make the repository open source.
