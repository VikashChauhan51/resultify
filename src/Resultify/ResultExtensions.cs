namespace ResultifyCore;

public static class ResultExtensions
{
    /// <summary>
    /// Executes an action regardless of whether the result is successful or failed.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <param name="result">The result to apply the action on.</param>
    /// <param name="action">The action to execute.</param>
    /// <returns>The original result to allow method chaining.</returns>
    public static Result<T> Do<T>(this Result<T> result, Action<Result<T>> action)
    {
        action(result);
        return result;
    }

    /// <summary>
    /// Transforms the value inside a successful result.
    /// </summary>
    /// <typeparam name="T">The type of the original value.</typeparam>
    /// <typeparam name="U">The type of the transformed value.</typeparam>
    /// <param name="result">The result to transform.</param>
    /// <param name="map">The transformation function.</param>
    /// <returns>A new result with the transformed value if successful, otherwise the original failure result.</returns>
    public static Result<U> Map<T, U>(this Result<T> result, Func<T, U> map)
    {
        return result.IsSuccess ? Result<U>.Succ(map(result.Value!)) : Result<U>.Fail(result.Exception!);
    }

    /// <summary>
    /// Chains multiple operations that return results.
    /// </summary>
    /// <typeparam name="T">The type of the original value.</typeparam>
    /// <typeparam name="U">The type of the value of the next result.</typeparam>
    /// <param name="result">The result to chain from.</param>
    /// <param name="bind">The function that returns the next result.</param>
    /// <returns>The result of the next operation if the current result is successful, otherwise the original failure result.</returns>
    public static Result<U> Bind<T, U>(this Result<T> result, Func<T, Result<U>> bind)
    {
        return result.IsSuccess ? bind(result.Value!) : Result<U>.Fail(result.Exception!);
    }

    /// <summary>
    /// Executes a side-effect action without altering the result.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <param name="result">The result to tap into.</param>
    /// <param name="tapAction">The action to execute.</param>
    /// <returns>The original result to allow method chaining.</returns>
    public static Result<T> Tap<T>(this Result<T> result, Action<T> tapAction)
    {
        if (result.IsSuccess)
        {
            tapAction(result.Value!);
        }
        return result;
    }

}


