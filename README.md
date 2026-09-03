# DemoMicroServiceWithKafka

Lightweight .NET 8 microservice demo that demonstrates producing and consuming messages with Apache Kafka.

## Overview

This repository contains a small example service intended for learning or prototyping event-driven patterns using .NET 8 and Kafka. It includes producer and consumer code examples and a minimal configuration approach.

## Tech stack

- .NET 8
- Apache Kafka

## Prerequisites

- .NET SDK 8.0: https://dotnet.microsoft.com/download
- Kafka running and accessible (default: `localhost:9092`)
- Optional: Docker and Docker Compose to run Kafka locally

## Project layout (example)

- `src/` - application projects
- `tests/` - unit/integration tests
- `docker/` - optional Docker compose files for local dependencies (Kafka)

Adjust paths to match the actual solution structure in this repository.

## Configuration

Configuration is typically read from `appsettings.json` and environment variables. Common settings:

- `Kafka:BootstrapServers` - e.g. `localhost:9092`
- `Kafka:Topic` - topic name used by producer/consumer

Example `appsettings.json` snippet:

```
{
  "Kafka": {
    "BootstrapServers": "localhost:9092",
    "Topic": "orders"
  }
}
```

You can override settings with environment variables using the usual ASP.NET Core configuration conventions (for example `Kafka__BootstrapServers`).

## Build

From the repository root:

```
dotnet build
```

To build a specific project:

```
dotnet build <path-to-csproj>
```

## Run

Ensure Kafka is available, then run the service:

```
dotnet run --project <path-to-your-service-csproj>
```

Replace `<path-to-your-service-csproj>` with the real project file path.

## Running Kafka locally (Docker Compose)

A minimal approach is to start Kafka with Docker Compose. From a directory containing a `docker-compose.yml` that defines Zookeeper and Kafka:

```
docker compose up -d
```

Then run the service configured to use the container's address (commonly `localhost:9092`).

## Tests

Run tests with:

```
dotnet test
```

## Notes

- Update `appsettings.json` or set environment variables for your environment before running.
- If you add open-source code or intend to publish the repository, add a `LICENSE` file.

## Contributing

Open issues or pull requests to suggest improvements or fixes.

