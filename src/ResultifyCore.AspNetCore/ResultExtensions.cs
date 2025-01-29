using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;


namespace ResultifyCore.AspNetCore;

/// <summary>
/// Provides extension methods for converting <see cref="Outcome"/> and <see cref="Result"/> objects to <see cref="IActionResult"/> and <see cref="IResult"/>.
/// </summary>
public static class ResultExtensions
{
    /// <summary>
    /// Converts an <see cref="Outcome{T}"/> to an <see cref="IActionResult"/>.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <param name="result">The outcome result.</param>
    /// <param name="controller">The controller instance.</param>
    /// <param name="url">The URL for the created result.</param>
    /// <returns>An <see cref="IActionResult"/> representing the outcome.</returns>
    public static IActionResult ToActionResult<T>(this Outcome<T> result, ControllerBase controller, string? url = null)
    {
        return result.Status switch
        {
            ResultState.Success => controller.Ok(result.Value),
            ResultState.Created => controller.Created(url, result.Value),
            ResultState.NoContent => controller.NoContent(),
            ResultState.Conflict => controller.Conflict(result.Errors.SerializeErrors()),
            ResultState.Validation => controller.BadRequest(result.Errors.SerializeErrors()),
            ResultState.NotFound => controller.NotFound(result.Errors.SerializeErrors()),
            ResultState.Problem => controller.Problem(result.Errors.SerializeErrors(), statusCode: StatusCodes.Status500InternalServerError, title: "A problem occurred"),
            ResultState.Unauthorized => UnAuthorized(controller, result.Errors),
            ResultState.Forbidden => Forbidden(controller, result.Errors),
            ResultState.Failure => controller.StatusCode(StatusCodes.Status500InternalServerError, result.Errors.SerializeErrors()),
            ResultState.CriticalError => controller.StatusCode(StatusCodes.Status500InternalServerError, result.Errors.SerializeErrors()),
            ResultState.Unavailable => controller.StatusCode(StatusCodes.Status503ServiceUnavailable, result.Errors.SerializeErrors()),
            _ => controller.UnprocessableEntity(result.Errors.SerializeErrors())
        };
    }

    /// <summary>
    /// Converts an <see cref="Outcome"/> to an <see cref="IActionResult"/>.
    /// </summary>
    /// <param name="result">The outcome result.</param>
    /// <param name="controller">The controller instance.</param>
    /// <returns>An <see cref="IActionResult"/> representing the outcome.</returns>
    public static IActionResult ToActionResult(this Outcome result, ControllerBase controller)
    {
        return result.Status switch
        {
            ResultState.Success => controller.Ok(),
            ResultState.Created => controller.Created(),
            ResultState.NoContent => controller.NoContent(),
            ResultState.Conflict => controller.Conflict(result.Errors.SerializeErrors()),
            ResultState.Validation => controller.BadRequest(result.Errors.SerializeErrors()),
            ResultState.NotFound => controller.NotFound(),
            ResultState.Problem => controller.Problem(result.Errors.SerializeErrors(), statusCode: StatusCodes.Status500InternalServerError, title: "A problem occurred"),
            ResultState.Unauthorized => UnAuthorized(controller, result.Errors),
            ResultState.Forbidden => Forbidden(controller, result.Errors),
            ResultState.Failure => controller.StatusCode(StatusCodes.Status500InternalServerError, result.Errors.SerializeErrors()),
            ResultState.CriticalError => controller.StatusCode(StatusCodes.Status500InternalServerError, result.Errors.SerializeErrors()),
            ResultState.Unavailable => controller.StatusCode(StatusCodes.Status503ServiceUnavailable, result.Errors.SerializeErrors()),
            _ => controller.UnprocessableEntity(result.Errors.SerializeErrors())
        };
    }

    /// <summary>
    /// Converts a <see cref="Result{T}"/> to an <see cref="IActionResult"/>.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <param name="result">The result.</param>
    /// <param name="controller">The controller instance.</param>
    /// <param name="url">The URL for the created result.</param>
    /// <returns>An <see cref="IActionResult"/> representing the result.</returns>
    public static IActionResult ToActionResult<T>(this Result<T> result, ControllerBase controller, string? url = null)
    {
        return result.Status switch
        {
            ResultState.Success => controller.Ok(result.Value),
            ResultState.Created => controller.Created(url, result.Value),
            ResultState.NoContent => controller.NoContent(),
            ResultState.Conflict => controller.Conflict(result.Exception),
            ResultState.Validation => controller.BadRequest(result.Exception),
            ResultState.NotFound => controller.NotFound(result.Exception),
            ResultState.Problem => controller.Problem(result.Exception?.Message, statusCode: StatusCodes.Status500InternalServerError, title: "A problem occurred"),
            ResultState.Unauthorized => UnAuthorized(controller, result.Exception),
            ResultState.Forbidden => Forbidden(controller, result.Exception),
            ResultState.Failure => controller.StatusCode(StatusCodes.Status500InternalServerError, result.Exception),
            ResultState.CriticalError => controller.StatusCode(StatusCodes.Status500InternalServerError, result.Exception),
            ResultState.Unavailable => controller.StatusCode(StatusCodes.Status503ServiceUnavailable, result.Exception),
            _ => controller.UnprocessableEntity(result.Value)
        };
    }

    /// <summary>
    /// Converts a <see cref="Result"/> to an <see cref="IActionResult"/>.
    /// </summary>
    /// <param name="result">The result.</param>
    /// <param name="controller">The controller instance.</param>
    /// <returns>An <see cref="IActionResult"/> representing the result.</returns>
    public static IActionResult ToActionResult(this Result result, ControllerBase controller)
    {
        return result.Status switch
        {
            ResultState.Success => controller.Ok(),
            ResultState.Created => controller.Created(),
            ResultState.NoContent => controller.NoContent(),
            ResultState.Conflict => controller.Conflict(result.Exception),
            ResultState.Validation => controller.BadRequest(result.Exception),
            ResultState.NotFound => controller.NotFound(),
            ResultState.Problem => controller.Problem(result.Exception?.Message, statusCode: StatusCodes.Status500InternalServerError, title: "A problem occurred"),
            ResultState.Unauthorized => UnAuthorized(controller, result.Exception),
            ResultState.Forbidden => Forbidden(controller, result.Exception),
            ResultState.Failure => controller.StatusCode(StatusCodes.Status500InternalServerError, result.Exception),
            ResultState.CriticalError => controller.StatusCode(StatusCodes.Status500InternalServerError, result.Exception),
            ResultState.Unavailable => controller.StatusCode(StatusCodes.Status503ServiceUnavailable, result.Exception),
            _ => controller.UnprocessableEntity(result.Exception)
        };
    }

    /// <summary>
    /// Converts an <see cref="Outcome{T}"/> to an <see cref="IResult"/>.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <param name="result">The outcome result.</param>
    /// <param name="url">The URL for the created result.</param>
    /// <returns>An <see cref="IResult"/> representing the outcome.</returns>
    public static IResult ToResult<T>(this Outcome<T> result, string? url = null)
    {
        return result.Status switch
        {
            ResultState.Success => Results.Ok(result.Value),
            ResultState.Created => Results.Created(url, result.Value),
            ResultState.NoContent => Results.NoContent(),
            ResultState.Conflict => Results.Conflict(result.Errors.SerializeErrors()),
            ResultState.Validation => Results.BadRequest(result.Errors.SerializeErrors()),
            ResultState.NotFound => Results.NotFound(result.Errors.SerializeErrors()),
            ResultState.Problem => Results.Problem(result.Errors.SerializeErrors(), statusCode: StatusCodes.Status500InternalServerError, title: "A problem occurred"),
            ResultState.Unauthorized => UnAuthorized(result.Errors),
            ResultState.Forbidden => Forbidden(result.Errors),
            ResultState.Failure => Results.Problem(new ProblemDetails()
            {
                Title = "Something went wrong.",
                Detail = result.Errors.SerializeErrors(),
                Status = StatusCodes.Status500InternalServerError
            }),
            ResultState.CriticalError => Results.Problem(new ProblemDetails()
            {
                Title = "Something went wrong.",
                Detail = result.Errors.SerializeErrors(),
                Status = StatusCodes.Status500InternalServerError
            }),
            ResultState.Unavailable => Results.Problem(new ProblemDetails
            {
                Title = "Service unavailable.",
                Detail = result.Errors.SerializeErrors(),
                Status = StatusCodes.Status503ServiceUnavailable
            }),
            _ => Results.UnprocessableEntity(result.Errors.SerializeErrors())
        };
    }

    /// <summary>
    /// Converts an <see cref="Outcome"/> to an <see cref="IResult"/>.
    /// </summary>
    /// <param name="result">The outcome result.</param>
    /// <returns>An <see cref="IResult"/> representing the outcome.</returns>
    public static IResult ToResult(this Outcome result)
    {
        return result.Status switch
        {
            ResultState.Success => Results.Ok(),
            ResultState.Created => Results.Created(),
            ResultState.NoContent => Results.NoContent(),
            ResultState.Conflict => Results.Conflict(result.Errors.SerializeErrors()),
            ResultState.Validation => Results.BadRequest(result.Errors.SerializeErrors()),
            ResultState.NotFound => Forbidden(result.Errors),
            ResultState.Problem => Results.Problem(result.Errors.SerializeErrors(), statusCode: StatusCodes.Status500InternalServerError, title: "A problem occurred"),
            ResultState.Unauthorized => UnAuthorized(result.Errors),
            ResultState.Forbidden => Results.Forbid(),
            ResultState.Failure => Results.Problem(new ProblemDetails()
            {
                Title = "Something went wrong.",
                Detail = result.Errors.SerializeErrors(),
                Status = StatusCodes.Status500InternalServerError
            }),
            ResultState.CriticalError => Results.Problem(new ProblemDetails()
            {
                Title = "Something went wrong.",
                Detail = result.Errors.SerializeErrors(),
                Status = StatusCodes.Status500InternalServerError
            }),
            ResultState.Unavailable => Results.Problem(new ProblemDetails
            {
                Title = "Service unavailable.",
                Detail = result.Errors.SerializeErrors(),
                Status = StatusCodes.Status503ServiceUnavailable
            }),
            _ => Results.UnprocessableEntity(result.Errors.SerializeErrors())
        };
    }

    /// <summary>
    /// Converts a <see cref="Result{T}"/> to an <see cref="IResult"/>.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <param name="result">The result.</param>
    /// <param name="url">The URL for the created result.</param>
    /// <returns>An <see cref="IResult"/> representing the result.</returns>
    public static IResult ToResult<T>(this Result<T> result, string? url = null)
    {
        return result.Status switch
        {
            ResultState.Success => Results.Ok(result.Value),
            ResultState.Created => Results.Created(url, result.Value),
            ResultState.NoContent => Results.NoContent(),
            ResultState.Conflict => Results.Conflict(result.Exception),
            ResultState.Validation => Results.BadRequest(result.Exception),
            ResultState.NotFound => Results.NotFound(result.Exception),
            ResultState.Problem => Results.Problem(result.Exception?.Message, statusCode: StatusCodes.Status500InternalServerError, title: "A problem occurred"),
            ResultState.Unauthorized => UnAuthorized(result.Exception),
            ResultState.Forbidden => Forbidden(result.Exception),
            ResultState.Failure => Results.Problem(new ProblemDetails()
            {
                Title = "Something went wrong.",
                Detail = result.Exception?.Message,
                Status = StatusCodes.Status500InternalServerError
            }),
            ResultState.CriticalError => Results.Problem(new ProblemDetails()
            {
                Title = "Something went wrong.",
                Detail = result.Exception?.Message,
                Status = StatusCodes.Status500InternalServerError
            }),
            ResultState.Unavailable => Results.Problem(new ProblemDetails
            {
                Title = "Service unavailable.",
                Detail = result.Exception?.Message,
                Status = StatusCodes.Status503ServiceUnavailable
            }),
            _ => Results.UnprocessableEntity(result.Value)
        };
    }

    /// <summary>
    /// Converts a <see cref="Result"/> to an <see cref="IResult"/>.
    /// </summary>
    /// <param name="result">The result.</param>
    /// <returns>An <see cref="IResult"/> representing the result.</returns>
    public static IResult ToResult(this Result result)
    {
        return result.Status switch
        {
            ResultState.Success => Results.Ok(),
            ResultState.Created => Results.Created(),
            ResultState.NoContent => Results.NoContent(),
            ResultState.Conflict => Results.Conflict(result.Exception),
            ResultState.Validation => Results.BadRequest(result.Exception),
            ResultState.NotFound => Results.NotFound(),
            ResultState.Problem => Results.Problem(result.Exception?.Message, statusCode: StatusCodes.Status500InternalServerError, title: "A problem occurred"),
            ResultState.Unauthorized => UnAuthorized(result.Exception),
            ResultState.Forbidden => Forbidden(result.Exception),
            ResultState.Failure => Results.Problem(new ProblemDetails()
            {
                Title = "Something went wrong.",
                Detail = result.Exception?.Message,
                Status = StatusCodes.Status500InternalServerError
            }),
            ResultState.CriticalError => Results.Problem(new ProblemDetails()
            {
                Title = "Something went wrong.",
                Detail = result.Exception?.Message,
                Status = StatusCodes.Status500InternalServerError
            }),
            ResultState.Unavailable => Results.Problem(new ProblemDetails
            {
                Title = "Service unavailable.",
                Detail = result.Exception?.Message,
                Status = StatusCodes.Status503ServiceUnavailable
            }),
            _ => Results.UnprocessableEntity(result.Exception)
        };
    }

    /// <summary>
    /// Serializes a collection of <see cref="OutcomeError"/> to a JSON string.
    /// </summary>
    /// <param name="errors">The collection of errors.</param>
    /// <returns>A JSON string representing the errors.</returns>
    public static string SerializeErrors(this IEnumerable<OutcomeError> errors)
    {
        if (errors == null || !errors.Any())
        {
            return string.Empty;
        }
        return JsonSerializer.Serialize(errors);
    }

    private static IActionResult UnAuthorized(ControllerBase controller, IEnumerable<OutcomeError> errors)
    {
        if (errors != null && errors.Any())
        {
            return controller.Problem(errors.SerializeErrors(), statusCode: StatusCodes.Status401Unauthorized, title: "Unauthorized.");
        }
        else
        {
            return controller.Unauthorized();
        }
    }

    private static IResult UnAuthorized(IEnumerable<OutcomeError> errors)
    {
        if (errors != null && errors.Any())
        {
            return Results.Problem(new ProblemDetails
            {
                Title = "Unauthorized.",
                Detail = errors.SerializeErrors(),
                Status = StatusCodes.Status401Unauthorized
            });
        }
        else
        {
            return Results.Unauthorized();
        }
    }

    private static IActionResult UnAuthorized(ControllerBase controller, Exception? error)
    {
        if (error != null)
        {
            return controller.Problem(error.Message, statusCode: StatusCodes.Status401Unauthorized, title: "Unauthorized.");
        }
        else
        {
            return controller.Unauthorized();
        }
    }

    private static IResult UnAuthorized(Exception? error)
    {
        if (error != null)
        {
            return Results.Problem(new ProblemDetails
            {
                Title = "Unauthorized.",
                Detail = error.Message,
                Status = StatusCodes.Status401Unauthorized
            });
        }
        else
        {
            return Results.Unauthorized();
        }
    }

    private static IActionResult Forbidden(ControllerBase controller, IEnumerable<OutcomeError> errors)
    {
        if (errors != null && errors.Any())
        {
            return controller.Problem(errors.SerializeErrors(), statusCode: StatusCodes.Status403Forbidden, title: "Forbidden.");
        }
        else
        {
            return controller.Forbid();
        }
    }

    private static IResult Forbidden(IEnumerable<OutcomeError> errors)
    {
        if (errors != null && errors.Any())
        {
            return Results.Problem(new ProblemDetails
            {
                Title = "Forbidden.",
                Detail = errors.SerializeErrors(),
                Status = StatusCodes.Status403Forbidden
            });
        }
        else
        {
            return Results.Forbid();
        }
    }

    private static IActionResult Forbidden(ControllerBase controller, Exception? error)
    {
        if (error != null)
        {
            return controller.Problem(error.Message, statusCode: StatusCodes.Status403Forbidden, title: "Forbidden.");
        }
        else
        {
            return controller.Forbid();
        }
    }

    private static IResult Forbidden(Exception? error)
    {
        if (error != null)
        {
            return Results.Problem(new ProblemDetails
            {
                Title = "Forbidden.",
                Detail = error.Message,
                Status = StatusCodes.Status403Forbidden
            });
        }
        else
        {
            return Results.Forbid();
        }
    }
}
