# BiddingSystem in .NET (C#)

This README provides a step-by-step guide to setting up the Bidding System project with RabbitMQ (MassTransit) and Docker, performing database migrations, and running load tests with k6.

## Table of Contents

1. [Prerequisites](#prerequisites)
2. [Setting Up RabbitMQ in Docker](#setting-up-rabbitmq-in-docker)
3. [Setting Up the Database](#setting-up-the-database)
4. [Running the Application](#running-the-application)
5. [Load Testing with k6](#load-testing-with-k6)

## Prerequisites

Before starting, make sure you have the following tools installed:

- **Docker**: [Docker Installation Guide](https://docs.docker.com/get-docker/)
- **.NET SDK**: Make sure you have the appropriate version of the .NET SDK for your project. [Install .NET](https://dotnet.microsoft.com/download)
- **k6**: For load testing [Install k6](https://grafana.com/docs/k6/latest/set-up/install-k6/)

## Setting Up RabbitMQ in Docker

1. **Ensure Docker is Running**
   Make sure that Docker is installed and running on your machine. You can check if Docker is running by executing the following command:

`docker --version`

2. **Run RabbitMQ with Management Plugin**
   Use the following command to start RabbitMQ in a Docker container. This command runs RabbitMQ with the management plugin, which allows you to access a web-based UI to monitor RabbitMQ:

`docker run -d --hostname my-rabbit --name my-rabbit -p 5672:5672 -p 15672:15672 rabbitmq:management`

3. **Access RabbitMQ Management UI**
   Once RabbitMQ is running, you can access the RabbitMQ Management UI via a web browser at: http://localhost:15672

- Username: guest
- Password: guest
  Log in to the RabbitMQ Management UI to monitor queues, exchanges, and other RabbitMQ-related configurations.

## Setting Up the Database

For demo purposes, the project uses **SQLite** as the database. This provides a simple way to persist data without the complexity of setting up a full database server.

### 1. Migrations Folder

The project already includes a **Migrations** folder in `BiddingSystem.Infrastructure` project, which contains the necessary files for creating the database schema and applying any changes.

### 2. Run Migrations and create/update database

To generate the SQLite database file (`bidding.db`), you need to apply the migrations. Here's how you can do that:
If no migrations, then initialize them with the following commands:

1. `cd BiddingSystem.Api`
2. `dotnet ef migrations add InitialCreate --project ../BiddingSystem.Infrastructure --startup-project .`
3. `dotnet ef database update`

You should see a sqlite database (`bidding.db`) in the `BiddingSystem.Infrastructure/Data` directory.

**IMPORTANT!**: If you want to visualize the database schema use a tool like [DB Browser for SQLite](https://sqlitebrowser.org/) or [TablePlus](https://tableplus.com/).

## Running the Application

Start the BiddingSystem.Api project with the https start button or run the project with the
following command:

`dotnet run --project BiddingSystem.Api/BiddingSystem.Api.csproj`
Try-Out Post request (several options):

- Test with swagger
- Test with `BiddingSystem.Api.http` file

## Load Testing with k6

To simulate real-world usage, you can use k6 to load test the application.

1. Run the Load Test
   In the root of the `BiddingSystem` folder, I've the `load_test.js` script. This is used to run the load tests.
   use the following command to run it (duration of 10min in total):

`k6 run load_test.js`

2. Test Results:

```bash
     ✓ status is 202

     checks.........................: 100.00% 11464 out of 11464
     data_received..................: 3.4 MB  5.7 kB/s
     data_sent......................: 3.7 MB  6.2 kB/s
     http_req_blocked...............: avg=127.59µs min=0s    med=0s      max=712.35ms p(90)=0s       p(95)=0s
     http_req_connecting............: avg=2.34µs   min=0s    med=0s      max=2.3ms    p(90)=0s       p(95)=0s
     http_req_duration..............: avg=180.6ms  min=0s    med=57.66ms max=1.57s    p(90)=546.34ms p(95)=710.94ms
       { expected_response:true }...: avg=180.6ms  min=0s    med=57.66ms max=1.57s    p(90)=546.34ms p(95)=710.94ms
     http_req_failed................: 0.00%   0 out of 22928
     http_req_receiving.............: avg=30.98ms  min=0s    med=544.2µs max=932.56ms p(90)=102.91ms p(95)=167.98ms
     http_req_sending...............: avg=73.41µs  min=0s    med=0s      max=2.36ms   p(90)=509.49µs p(95)=540.79µs
     http_req_tls_handshaking.......: avg=122.64µs min=0s    med=0s      max=711.82ms p(90)=0s       p(95)=0s
     http_req_waiting...............: avg=149.54ms min=0s    med=53.4ms  max=1.54s    p(90)=439.9ms  p(95)=602.2ms
     http_reqs......................: 22928   38.209022/s
     iteration_duration.............: avg=2.36s    min=1.03s med=2.35s   max=4.62s    p(90)=3.18s    p(95)=3.4s
     iterations.....................: 11464   19.104511/s
     vus............................: 2       min=0              max=50
     vus_max........................: 50      min=50             max=50


running (10m00.1s), 00/50 VUs, 11464 complete and 0 interrupted iterations
loadTest ✓ [======================================] 00/50 VUs  10m0s
```
