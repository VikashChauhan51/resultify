using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;


namespace ResultifyCore.AspNetCore;

public static class ResultExtensions
{
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
