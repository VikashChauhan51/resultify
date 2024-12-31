
# ResultifyCore

ResultifyCore is a .NET library providing `Result`, `Option`, `Outcome` and `OneOf` patterns to simplify error handling, optional values, and type discrimination.
It includes extension methods for fluent API support and enhanced readability.

## Installation

You can install the ResultifyCore package via NuGet:

```shell
dotnet add package ResultifyCore
```

Or you can use the NuGet Package Manager:

```shell
Install-Package ResultifyCore
```

## Usage

### Result Pattern

The `Result` pattern represents the outcome of an operation that can either succeed or fail.

#### Creating a Result

```csharp
using ResultifyCore;

var successResult = Result<int>.Success(42);
var failureResult = Result<int>.Failure(new Exception("Something went wrong"));
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
    .Tap(value => Console.WriteLine($"Tap into value: {value}"))
    .Map(value => value * 2)
    .OnSuccess(value => Console.WriteLine($"Transformed value: {value}"));

failureResult
    .Do(res => Console.WriteLine("Executing common action"))
    .Tap(value => Console.WriteLine($"Tap into value: {value}"))
    .Map(value => value * 2)
    .OnSuccess(value => Console.WriteLine($"Transformed value: {value}"));
```

### Option Pattern

The `Option` pattern represents an optional value that may or may not be present.

#### Creating an Option

```csharp
using ResultifyCore;

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

### OneOf Pattern

The OneOf pattern allows you to encapsulate multiple possible types for a single value. This is useful for cases where a value can belong to one of several types.

#### Example:

```csharp
using ResultifyCore;

OneOf<int, string> numberOrString = new OneOf<int, string>("Hello");

#Accessing Values
if (numberOrString.IsT1)
{
    Console.WriteLine($"Value is of type T1: {numberOrString.AsT1}");
}
else if (numberOrString.IsT2)
{
    Console.WriteLine($"Value is of type T2: {numberOrString.AsT2}");
}

#Match Method
var matchResult = numberOrString.Match(
    matchT1: number => $"Number: {number}",
    matchT2: text => $"String: {text}"
);
Console.WriteLine(matchResult);

```

### Outcome Pattern

The Outcome pattern is a clean, functional approach to handling success and failure scenarios in code, providing an alternative to exceptions for managing errors and failures.

### Example:

```csharp
public static Outcome ValidateInput(string input)
{
    if (string.IsNullOrWhiteSpace(input))
    {
        return Outcome.Failure(new OutcomeError("VALIDATION_ERROR", "Input cannot be null or whitespace."));
    }

    return Outcome.Success();
}

public static Outcome<int> ParseNumber(string input)
{
    if (int.TryParse(input, out var number))
    {
        return Outcome<int>.Success(number);
    }

    return Outcome<int>.Failure(new OutcomeError("PARSE_ERROR", "Invalid number format."));
}

var result = ParseNumber("abc");

result.Match(
        onSuccess: value => Console.WriteLine($"Parsed number: {value}"),
        onFailure: errors => Console.WriteLine($"Failed to parse number: {string.Join(", ", errors)}")
    );

```
### Extensions

ResultifyCore provides several extension methods to facilitate fluent API and method chaining.

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
