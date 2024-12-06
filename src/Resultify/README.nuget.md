
# Resultify

Resultify is a .NET library providing `Result` and `Option` patterns to simplify error handling and optional values. It includes extension methods for fluent API support and enhanced readability.

## Installation

You can install the Resultify package via NuGet:

```shell
dotnet add package Resultify
```

Or you can use the NuGet Package Manager:

```shell
Install-Package Resultify
```

## Usage

### Result Pattern

The `Result` pattern represents the outcome of an operation that can either succeed or fail.

#### Creating a Result

```csharp
using Resultify;

var successResult = Result<int>.Succ(42);
var failureResult = Result<int>.Fail(new Exception("Something went wrong"));
```

#### Match Method

```csharp
var matchResult = successResult.Match<string>(
    onSuccess: value => "success",
    onFailure: ex => "error"
);
Console.WriteLine(matchResult);

```

#### Method Chaining

```csharp
successResult
    .Do(res => Console.WriteLine("Executing common action"))
    .OnSuccess(value => Console.WriteLine($"Success with value: {value}"))
    .OnError(ex => Console.WriteLine($"Error: {ex.Message}"))
    .Tap(value => Console.WriteLine($"Tap into value: {value}"))
    .Map(value => value * 2)
    .OnSuccess(value => Console.WriteLine($"Transformed value: {value}"));

failureResult
    .Do(res => Console.WriteLine("Executing common action"))
    .OnSuccess(value => Console.WriteLine($"Success with value: {value}"))
    .OnError(ex => Console.WriteLine($"Error: {ex.Message}"))
    .Tap(value => Console.WriteLine($"Tap into value: {value}"))
    .Map(value => value * 2)
    .OnSuccess(value => Console.WriteLine($"Transformed value: {value}"));
```

### Option Pattern

The `Option` pattern represents an optional value that may or may not be present.

#### Creating an Option

```csharp
using Resultify;

var someOption = Option<int>.Some(42);
var noneOption = Option<int>.None;
```



#### Match Method

```csharp
var matchOptionResult = someOption.Match<string>(
    onSome: value => "some",
    onNone: () => "none"
);
Console.WriteLine(matchOptionResult);

```
#### Method Chaining

```csharp
someOption
    .Do(opt => Console.WriteLine("Executing common action"))
    .OnSome(value => Console.WriteLine($"Option has value: {value}"))
    .OnNone(() => Console.WriteLine("Option has no value"))
    .Tap(value => Console.WriteLine($"Tap into value: {value}"))
    .Map(value => value * 2)
    .OnSome(value => Console.WriteLine($"Transformed value: {value}"));

noneOption
    .Do(opt => Console.WriteLine("Executing common action"))
    .OnSome(value => Console.WriteLine($"Option has value: {value}"))
    .OnNone(() => Console.WriteLine("Option has no value"))
    .Tap(value => Console.WriteLine($"Tap into value: {value}"))
    .Map(value => value * 2)
    .OnSome(value => Console.WriteLine($"Transformed value: {value}"));
```

### Extensions

Resultify provides several extension methods to facilitate fluent API and method chaining.

#### Result Extensions

- `Do`: Executes an action regardless of the result state.
- `OnSuccess`: Executes an action if the result is successful.
- `OnError`: Executes an action if the result is failed.
- `Map`: Transforms the value inside a successful result.
- `Bind`: Chains multiple operations that return results.
- `Tap`: Executes a side-effect action without altering the result.

#### Option Extensions

- `Do`: Executes an action regardless of whether the option has a value.
- `OnSome`: Executes an action if the option has a value.
- `OnNone`: Executes an action if the option does not have a value.
- `Map`: Transforms the value inside an option.
- `Bind`: Chains multiple operations that return options.
- `Tap`: Executes a side-effect action without altering the option.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.
