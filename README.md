# Ordring Service Implementation With gRPC

## Summary
<h4>This is an implementation for a mircoservice look alike using asp net core api and grpc (proto buffer).</h4>
<hr>

__The project consists of of two microservices wrapped by an asp net core api:__


1. inventory service 
this service has a static db mocking class that has a static list of items and a grpc service (InventoryService) that checks if the items in stock or not

2. payment service 
also this service has a static db mocking class that has a static list of users and a grpc service (ChargingService) that checks if the user can afford the items cost or not

3. asp net core api
an api that has an endpoint to place an order contains the userid and list of items that indicates whether the order is placed or not

<br>

## Tech


1. Asp Net Core Api

2. gRPC

3. Proto Buffer

4. Swagger

<br>

## Diagram

![project diagram](https://github.com/IslamKhalil314/OrederingServiceWithgRPC/blob/master/assets/Ordrering.drawio%20(1).png?raw=true)


