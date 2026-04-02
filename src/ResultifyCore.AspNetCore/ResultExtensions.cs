using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;


namespace ResultifyCore.AspNetCore;

/// <summary>
/// Provides extension methods for converting <see cref="Outcome"/>, <see cref="Result"/>,
/// and their generic counterparts to ASP.NET Core <see cref="IActionResult"/> and <see cref="IResult"/>
/// via the intermediate <see cref="IResult"/> adapter.
/// </summary>
public static class ResultExtensions
{
    // ========== IActionResult conversions (MVC / ControllerBase) ==========

    public static IActionResult ToActionResult<T>(this Outcome<T> outcome, ControllerBase controller, string? url = null)
    {
        var customResult = outcome.ToIResult(); // returns IResult<T>
        return CustomResultToActionResult(customResult, controller, url);
    }

    public static IActionResult ToActionResult(this Outcome outcome, ControllerBase controller)
    {
        var customResult = outcome.ToIResult(); // returns IResult
        return CustomResultToActionResult(customResult, controller);
    }

    public static IActionResult ToActionResult<T>(this Result<T> result, ControllerBase controller, string? url = null)
    {
        var customResult = result.ToIResult(); // assumes Result<T> has ToIResult<T>()
        return CustomResultToActionResult(customResult, controller, url);
    }

    public static IActionResult ToActionResult(this Result result, ControllerBase controller)
    {
        var customResult = result.ToIResult(); // assumes Result has ToIResult()
        return CustomResultToActionResult(customResult, controller);
    }

    // ========== Microsoft IResult conversions (Minimal APIs) ==========

    public static Microsoft.AspNetCore.Http.IResult ToResult<T>(this Outcome<T> outcome, string? url = null)
    {
        var customResult = outcome.ToIResult();
        return CustomResultToMinimalResult(customResult, url);
    }

    public static Microsoft.AspNetCore.Http.IResult ToResult(this Outcome outcome)
    {
        var customResult = outcome.ToIResult();
        return CustomResultToMinimalResult(customResult);
    }

    public static Microsoft.AspNetCore.Http.IResult ToResult<T>(this Result<T> result, string? url = null)
    {
        var customResult = result.ToIResult();
        return CustomResultToMinimalResult(customResult, url);
    }

    public static Microsoft.AspNetCore.Http.IResult ToResult(this Result result)
    {
        var customResult = result.ToIResult();
        return CustomResultToMinimalResult(customResult);
    }

    // ========== Core mapping logic from your IResult to ASP.NET Core results ==========

    private static IActionResult CustomResultToActionResult<T>(IResult<T> customResult, ControllerBase controller, string? url = null)
    {
        return customResult.Status switch
        {
            ResultState.Success => controller.Ok(customResult.Data),
            ResultState.Created => controller.Created(url, customResult.Data),
            ResultState.NoContent => controller.NoContent(),
            ResultState.Conflict => ConflictActionResult(controller, customResult.Errors),
            ResultState.Validation => ValidationActionResult(controller, customResult.Errors),
            ResultState.NotFound => NotFoundActionResult(controller, customResult.Errors),
            ResultState.Problem => ProblemActionResult(controller, customResult.Errors, StatusCodes.Status500InternalServerError, "A problem occurred"),
            ResultState.Unauthorized => UnauthorizedActionResult(controller, customResult.Errors),
            ResultState.Forbidden => ForbiddenActionResult(controller, customResult.Errors),
            ResultState.Failure => ProblemActionResult(controller, customResult.Errors, StatusCodes.Status500InternalServerError, "Something went wrong"),
            ResultState.CriticalError => ProblemActionResult(controller, customResult.Errors, StatusCodes.Status500InternalServerError, "Critical error"),
            ResultState.Unavailable => ProblemActionResult(controller, customResult.Errors, StatusCodes.Status503ServiceUnavailable, "Service unavailable"),
            _ => UnprocessableEntityActionResult(controller, customResult.Errors)
        };
    }

    private static IActionResult CustomResultToActionResult(IResult customResult, ControllerBase controller)
    {
        return customResult.Status switch
        {
            ResultState.Success => controller.Ok(),
            ResultState.Created => controller.Created(),
            ResultState.NoContent => controller.NoContent(),
            ResultState.Conflict => ConflictActionResult(controller, customResult.Errors),
            ResultState.Validation => ValidationActionResult(controller, customResult.Errors),
            ResultState.NotFound => NotFoundActionResult(controller, customResult.Errors),
            ResultState.Problem => ProblemActionResult(controller, customResult.Errors, StatusCodes.Status500InternalServerError, "A problem occurred"),
            ResultState.Unauthorized => UnauthorizedActionResult(controller, customResult.Errors),
            ResultState.Forbidden => ForbiddenActionResult(controller, customResult.Errors),
            ResultState.Failure => ProblemActionResult(controller, customResult.Errors, StatusCodes.Status500InternalServerError, "Something went wrong"),
            ResultState.CriticalError => ProblemActionResult(controller, customResult.Errors, StatusCodes.Status500InternalServerError, "Critical error"),
            ResultState.Unavailable => ProblemActionResult(controller, customResult.Errors, StatusCodes.Status503ServiceUnavailable, "Service unavailable"),
            _ => UnprocessableEntityActionResult(controller, customResult.Errors)
        };
    }

    private static Microsoft.AspNetCore.Http.IResult CustomResultToMinimalResult<T>(IResult<T> customResult, string? url = null)
    {
        return customResult.Status switch
        {
            ResultState.Success => Results.Ok(customResult.Data),
            ResultState.Created => Results.Created(url, customResult.Data),
            ResultState.NoContent => Results.NoContent(),
            ResultState.Conflict => ConflictResult(customResult.Errors),
            ResultState.Validation => ValidationResult(customResult.Errors),
            ResultState.NotFound => NotFoundResult(customResult.Errors),
            ResultState.Problem => ProblemResult(customResult.Errors, StatusCodes.Status500InternalServerError, "A problem occurred"),
            ResultState.Unauthorized => UnauthorizedResult(customResult.Errors),
            ResultState.Forbidden => ForbiddenResult(customResult.Errors),
            ResultState.Failure => ProblemResult(customResult.Errors, StatusCodes.Status500InternalServerError, "Something went wrong"),
            ResultState.CriticalError => ProblemResult(customResult.Errors, StatusCodes.Status500InternalServerError, "Critical error"),
            ResultState.Unavailable => ProblemResult(customResult.Errors, StatusCodes.Status503ServiceUnavailable, "Service unavailable"),
            _ => UnprocessableEntityResult(customResult.Errors)
        };
    }

    private static Microsoft.AspNetCore.Http.IResult CustomResultToMinimalResult(IResult customResult)
    {
        return customResult.Status switch
        {
            ResultState.Success => Results.Ok(),
            ResultState.Created => Results.Created(),
            ResultState.NoContent => Results.NoContent(),
            ResultState.Conflict => ConflictResult(customResult.Errors),
            ResultState.Validation => ValidationResult(customResult.Errors),
            ResultState.NotFound => NotFoundResult(customResult.Errors),
            ResultState.Problem => ProblemResult(customResult.Errors, StatusCodes.Status500InternalServerError, "A problem occurred"),
            ResultState.Unauthorized => UnauthorizedResult(customResult.Errors),
            ResultState.Forbidden => ForbiddenResult(customResult.Errors),
            ResultState.Failure => ProblemResult(customResult.Errors, StatusCodes.Status500InternalServerError, "Something went wrong"),
            ResultState.CriticalError => ProblemResult(customResult.Errors, StatusCodes.Status500InternalServerError, "Critical error"),
            ResultState.Unavailable => ProblemResult(customResult.Errors, StatusCodes.Status503ServiceUnavailable, "Service unavailable"),
            _ => UnprocessableEntityResult(customResult.Errors)
        };
    }

    // ========== Helpers to convert IReadOnlyDictionary<string, object> (your Errors) into HTTP responses ==========

    private static IActionResult ConflictActionResult(ControllerBase controller, IReadOnlyDictionary<string, object> errors)
        => controller.Conflict(SerializeErrors(errors));

    private static IActionResult ValidationActionResult(ControllerBase controller, IReadOnlyDictionary<string, object> errors)
        => controller.BadRequest(SerializeErrors(errors));

    private static IActionResult NotFoundActionResult(ControllerBase controller, IReadOnlyDictionary<string, object> errors)
        => controller.NotFound(SerializeErrors(errors));

    private static IActionResult UnprocessableEntityActionResult(ControllerBase controller, IReadOnlyDictionary<string, object> errors)
        => controller.UnprocessableEntity(SerializeErrors(errors));

    private static IActionResult UnauthorizedActionResult(ControllerBase controller, IReadOnlyDictionary<string, object> errors)
        => errors.Count == 0
            ? controller.Unauthorized()
            : controller.Problem(SerializeErrors(errors).ToString(), statusCode: StatusCodes.Status401Unauthorized, title: "Unauthorized");

    private static IActionResult ForbiddenActionResult(ControllerBase controller, IReadOnlyDictionary<string, object> errors)
        => errors.Count == 0
            ? controller.Forbid()
            : controller.Problem(SerializeErrors(errors).ToString(), statusCode: StatusCodes.Status403Forbidden, title: "Forbidden");

    private static IActionResult ProblemActionResult(ControllerBase controller, IReadOnlyDictionary<string, object> errors, int statusCode, string title)
        => controller.Problem(SerializeErrors(errors).ToString(), statusCode: statusCode, title: title);

    // Minimal API versions
    private static Microsoft.AspNetCore.Http.IResult ConflictResult(IReadOnlyDictionary<string, object> errors)
        => Results.Conflict(SerializeErrors(errors));

    private static Microsoft.AspNetCore.Http.IResult ValidationResult(IReadOnlyDictionary<string, object> errors)
        => Results.BadRequest(SerializeErrors(errors));

    private static Microsoft.AspNetCore.Http.IResult NotFoundResult(IReadOnlyDictionary<string, object> errors)
        => Results.NotFound(SerializeErrors(errors));

    private static Microsoft.AspNetCore.Http.IResult UnprocessableEntityResult(IReadOnlyDictionary<string, object> errors)
        => Results.UnprocessableEntity(SerializeErrors(errors));

    private static Microsoft.AspNetCore.Http.IResult UnauthorizedResult(IReadOnlyDictionary<string, object> errors)
        => errors.Count == 0
            ? Results.Unauthorized()
            : Results.Problem(SerializeErrors(errors).ToString(), statusCode: StatusCodes.Status401Unauthorized, title: "Unauthorized");

    private static Microsoft.AspNetCore.Http.IResult ForbiddenResult(IReadOnlyDictionary<string, object> errors)
        => errors.Count == 0
            ? Results.Forbid()
            : Results.Problem(SerializeErrors(errors).ToString(), statusCode: StatusCodes.Status403Forbidden, title: "Forbidden");

    private static Microsoft.AspNetCore.Http.IResult ProblemResult(IReadOnlyDictionary<string, object> errors, int statusCode, string title)
        => Results.Problem(SerializeErrors(errors).ToString(), statusCode: statusCode, title: title);

    /// <summary>
    /// Converts your <see cref="IReadOnlyDictionary{string, object}"/> errors into a JSON‑compatible object.
    /// If the dictionary already contains serializable values, we simply return it.
    /// You can customize this serialization logic (e.g., flatten, rename keys, etc.).
    /// </summary>
    private static object SerializeErrors(IReadOnlyDictionary<string, object> errors)
    {
        // If the dictionary is empty, return an empty object.
        if (errors == null || errors.Count == 0)
            return new { };

        // Attempt to convert any complex objects to strings or keep as is.
        // For safety, we deep‑clone via System.Text.Json to ensure no reference issues.
        // This also handles nested dictionaries, lists, etc.
        var serializable = JsonSerializer.Serialize(errors);
        return JsonSerializer.Deserialize<object>(serializable)!;
    }
}
