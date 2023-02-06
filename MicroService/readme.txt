

C# Has amazing documentation:
    https://learn.microsoft.com/en-us/dotnet/csharp/

Why is the solution file structured this way:
    https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/ddd-oriented-microservice


Important topics to read about:
    (very important)
        Async/Task:
            https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/async/
        linq expressions:
            Please note that we in general only use linq-extensions.
            https://learn.microsoft.com/en-us/dotnet/standard/linq/
        Dependency injection:
	 	https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection
        Json serilization/deserialization
	 	https://www.newtonsoft.com/json

    (important)
          EntityFramework:
            https://learn.microsoft.com/en-us/ef/core/get-started/overview/first-app?tabs=netcore-cli
	  Delegates - Action & Func:
		https://medium.com/@kylia669/func-vs-action-in-net-564ee4f8a8ee
	  Time - Datetime & Timespan
		Datetime: https://learn.microsoft.com/en-us/dotnet/api/system.datetime?view=net-7.0
		Timespan: https://learn.microsoft.com/en-us/dotnet/api/system.timespan?view=net-7.0
	  Generics
	  	https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/types/generics

    (I feel lucky)
        HostedService(background service):
            https://learn.microsoft.com/en-us/aspnet/core/fundamentals/host/hosted-services?view=aspnetcore-7.0&tabs=visual-studio
	  string compare:  
		"bob" == "bob" => true
		"lol" == "bob" => false
    
		double quoutes: string
		single quoates: char
			technically a string is an array of chars aka char[]

        string concatination:
		"hello " + "bob" => "hello bob"
		var name = "bob";
		$"Hello {name}" => "Hello bob"

	  Get current date/time:
		Datetime.Now
		Datetime.UtcNow
		
        
