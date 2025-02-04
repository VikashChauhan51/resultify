
# ResultifyCore

![ResultifyCore](resultifycore.ico)


**ResultifyCore** is a .NET library providing `Result`, `Option`, `Outcome`, `OneOf` and `Guard` patterns to simplify error handling, optional values, and type discrimination. It includes extension methods for fluent API support and enhanced readability.

## Installation

You can install the ResultifyCore package via NuGet:

[![NuGet Version](https://img.shields.io/nuget/v/ResultifyCore.svg?style=flat-square)](https://www.nuget.org/packages/ResultifyCore/)
[![NuGet Downloads](https://img.shields.io/nuget/dt/ResultifyCore.svg?style=flat-square)](https://www.nuget.org/packages/ResultifyCore/)
[![Build Status](https://github.com/VikashChauhan51/resultify/actions/workflows/build.yml/badge.svg)](https://github.com/VikashChauhan51/resultify/actions)
[![License](https://img.shields.io/github/license/VikashChauhan51/resultify.svg?style=flat-square)](https://github.com/VikashChauhan51/resultify/blob/main/LICENSE)


```shell
dotnet add package ResultifyCore
```

Or you can use the NuGet Package Manager:

```shell
Install-Package ResultifyCore
```

You can install the ResultifyCore.AspNetCore package via NuGet:

[![NuGet Version](https://img.shields.io/nuget/v/ResultifyCore.AspNetCore.svg?style=flat-square)](https://www.nuget.org/packages/ResultifyCore.AspNetCore/)
[![NuGet Downloads](https://img.shields.io/nuget/dt/ResultifyCore.AspNetCore.svg?style=flat-square)](https://www.nuget.org/packages/ResultifyCore/)
[![Build Status](https://github.com/VikashChauhan51/resultify/actions/workflows/build.yml/badge.svg)](https://github.com/VikashChauhan51/resultify/actions)
[![License](https://img.shields.io/github/license/VikashChauhan51/resultify.svg?style=flat-square)](https://github.com/VikashChauhan51/resultify/blob/main/LICENSE)

```shell
dotnet add package ResultifyCore.AspNetCore
```

Or you can use the NuGet Package Manager:

```shell
Install-Package ResultifyCore.AspNetCore
```

## Usage

### Result Pattern

The `Result` pattern represents the outcome of an operation that can either succeed or fail. It allows for better error handling and makes the flow of success or failure explicit.

#### Creating a Result

```csharp
using ResultifyCore;

var successResult = Result<int>.Success(42);  // A successful result containing the value 42
var failureResult = Result<int>.Failure(new Exception("Something went wrong")); // A failed result containing an exception
```

#### `Success` and `Failure` Extension Methods

You can create successful and failure results more easily using these extension methods.

##### Example of Success Extension Method:

```csharp
using ResultifyCore;

var result = "Success!".Success(); // Create a successful result with a value
Console.WriteLine(result.IsSuccess);  // Outputs: True
Console.WriteLine(result.Value);     // Outputs: Success!

// If an exception is passed, an InvalidOperationException will be thrown
try
{
    var resultWithException = new Exception("Test Exception").Success();
}
catch (InvalidOperationException ex)
{
    Console.WriteLine(ex.Message);  // Outputs: Cannot use an Exception as a value.
}
```

##### Example of Failure Extension Method:

```csharp
using ResultifyCore;

var failureResult = new Exception("Failure reason").Failure<string>(); // Create a failure result containing an exception
Console.WriteLine(failureResult.IsSuccess);  // Outputs: False
Console.WriteLine(failureResult.Exception.Message); // Outputs: Failure reason

// If the exception is null, an ArgumentNullException will be thrown
try
{
    Exception exception = null;
    var resultWithNullException = exception.Failure<string>();
}
catch (ArgumentNullException ex)
{
    Console.WriteLine(ex.Message);  // Outputs: Exception cannot be null.
}
```

#### Method Chaining with `Result`

Method chaining allows you to perform multiple operations on the result, including transforming it, checking success or failure, or applying side-effects:

```csharp
var successResult = 42.Success();

successResult
    .Do(res => Console.WriteLine("Executing common action"))     // Executes regardless of the result state
    .Tap(value => Console.WriteLine($"Tap into value: {value}"))  // Executes a side-effect
    .Map(value => value * 2)                                     // Transforms the value
    .OnSuccess(value => Console.WriteLine($"Transformed value: {value}"));  // Executes if result is successful
```

For a failed result, the same chaining pattern applies:

```csharp
var failureResult = new Exception("Something went wrong").Failure<int>();

failureResult
    .Do(res => Console.WriteLine("Executing common action"))
    .Tap(value => Console.WriteLine($"Tap into value: {value}"))
    .Map(value => value * 2)
    .OnSuccess(value => Console.WriteLine($"Transformed value: {value}"));
```

### Option Pattern

The `Option` pattern represents an optional value that may or may not be present. It allows you to avoid `null` references and explicitly handle the absence of values.

#### Creating an Option

```csharp
using ResultifyCore;

var someOption = Option<int>.Some(42);  // Option containing a value
var noneOption = Option<int>.None;      // Option with no value
 var optionResult = 42.Some();
```

#### Match Method for Option

```csharp
var matchOptionResult = someOption.Match<string>(
    onSome: value => "Value is present",
    onNone: () => "No value"
);
Console.WriteLine(matchOptionResult);  // Outputs: Value is present
```

### OneOf Pattern

The `OneOf` pattern allows you to encapsulate multiple possible types for a single value. This is useful for cases where a value can belong to one of several types.

#### Example:

```csharp
using ResultifyCore;

OneOf<int, string> numberOrString = new OneOf<int, string>("Hello");

# Accessing Values
if (numberOrString.IsT1)
{
    Console.WriteLine($"Value is of type T1: {numberOrString.AsT1}");
}
else if (numberOrString.IsT2)
{
    Console.WriteLine($"Value is of type T2: {numberOrString.AsT2}");
}

# Match Method
var matchResult = numberOrString.Match(
    matchT1: number => $"Number: {number}",
    matchT2: text => $"String: {text}"
);
Console.WriteLine(matchResult);  // Outputs: String: Hello
```

### Outcome Pattern

The `Outcome` pattern provides an alternative to exceptions for managing errors and failures, offering a clean, functional approach to success and failure scenarios.

#### Example:

```csharp

var optionResult = 42.SuccessOutcome();

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
### Guard Pattern

The Guard pattern helps enforce preconditions in your code by validating arguments.
 It ensures your methods receive valid input and fail early with meaningful exceptions when they don't.

 #### Example of Guard Usage

 ```csharp
public class Calculator
{
    public static int Divide(int dividend, int divisor)
    {
        // Validate arguments using Guard
        Guard.ThrowIfArgumentIsNegative(dividend, nameof(dividend));
        Guard.ThrowIfArgumentIsNegativeOrZero(divisor, nameof(divisor));

        return dividend / divisor;
    }
}
 ```

### Extensions

ResultifyCore provides several extension methods to facilitate fluent API and method chaining.

#### Result Extensions

- **`Do`**: Executes an action regardless of the result state.
- **`OnSuccess`**: Executes an action if the result is successful.
- **`OnError`**: Executes an action if the result is failed.
- **`Map`**: Transforms the value inside a successful result.
- **`Bind`**: Chains multiple operations that return results.
- **`Tap`**: Executes a side-effect action without altering the result.

#### Option Extensions

- **`Do`**: Executes an action regardless of whether the option has a value.
- **`OnSome`**: Executes an action if the option has a value.
- **`OnNone`**: Executes an action if the option does not have a value.
- **`Map`**: Transforms the value inside an option.
- **`Bind`**: Chains multiple operations that return options.
- **`Tap`**: Executes a side-effect action without altering the option.

#### `IActionResult` Extensions

The following extension methods convert `Outcome` and `Result` objects to `IActionResult` in ASP.NET Core:

```csharp
public static IActionResult ToActionResult<T>(this Outcome<T> result, ControllerBase controller, string? url = null)
public static IActionResult ToActionResult(this Outcome result, ControllerBase controller)
public static IActionResult ToActionResult<T>(this Result<T> result, ControllerBase controller, string? url = null)
public static IActionResult ToActionResult(this Result result, ControllerBase controller)
```

#### `IResult` Extensions

The following extension methods convert `Outcome` and `Result` objects to `IResult` in ASP.NET Core:

```csharp
public static IResult ToResult<T>(this Outcome<T> result, string? url = null)
public static IResult ToResult(this Outcome result)
public static IResult ToResult<T>(this Result<T> result, string? url = null)
public static IResult ToResult(this Result result)
```

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.
