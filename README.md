# grpc-demo-example
GRPC Server &amp; Client example with C# 

* Customer & Greet Service with a couple of getters. 
* In order to make it run you have to keep the proto files (client & server) in sync.
* Make sure that you rebuild your solution every time you modify the proto buffers, so you can override the methods to generate a valid response.
* Remember to update the Configure method of the Startup class every time you add a new service. 

# When should I use GRPC? Microsoft suggests a couple of use cases:
* When dealing with IoT devices, since they may be memory|bandwidth|hard-drive space restricted. 
Those devices are good candidates to use GRPC because it allows really tight small quick communications between client-server and back.
* Microservices: considering that they have to communicate with each other asynchronously and in a near time but not in real-time using something like a message queue. 

# Advantages?
* More efficient that Web REST APIs.
* Simpler to do, there are a lot fewer footprints in some ways.

# Disadvantages? 
* The need of sharing a proto file: in an API you only expose a call and there it is. 
* Almost every device in the world can consume an API, that's not the case for GRPC. 
