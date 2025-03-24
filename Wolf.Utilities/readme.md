# Wolf.Utilities

## ExceptionHandler

### Usage
```csharp
try
{
	// Code that may throw an exception
}
catch (Exception exception)
{
	throw ExceptionHandler<BusinessExceptionFactory>.Create(logger, exception,
		(nameof(parameter1), parameter1),
		(nameof(parameter2), parameter2));
}
```
