namespace ResultifyCore;

public static class OptionExtensions
{
    /// <summary>
    /// Executes an action regardless of whether the option has a value or not.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <param name="option">The option to apply the action on.</param>
    /// <param name="action">The action to execute.</param>
    /// <returns>The original option to allow method chaining.</returns>
    public static Option<T> Do<T>(this Option<T> option, Action<Option<T>> action)
    {
        action(option);
        return option;
    }

    /// <summary>
    /// Executes an action if the option has a value.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <param name="option">The option to apply the action on.</param>
    /// <param name="onSome">The action to execute if the option has a value.</param>
    /// <returns>The original option to allow method chaining.</returns>
    public static Option<T> OnSome<T>(this Option<T> option, Action<T> onSome)
    {
        if (option.IsSome)
        {
            onSome(option.Unwrap()!);
        }
        return option;
    }

    /// <summary>
    /// Executes an action if the option does not have a value.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <param name="option">The option to apply the action on.</param>
    /// <param name="onNone">The action to execute if the option does not have a value.</param>
    /// <returns>The original option to allow method chaining.</returns>
    public static Option<T> OnNone<T>(this Option<T> option, Action onNone)
    {
        if (!option.IsSome)
        {
            onNone();
        }
        return option;
    }

    /// <summary>
    /// Transforms the value inside an option.
    /// </summary>
    /// <typeparam name="T">The type of the original value.</typeparam>
    /// <typeparam name="U">The type of the transformed value.</typeparam>
    /// <param name="option">The option to transform.</param>
    /// <param name="map">The transformation function.</param>
    /// <returns>A new option with the transformed value if present, otherwise a none option.</returns>
    public static Option<U> Map<T, U>(this Option<T> option, Func<T, U> map)
    {
        return option.IsSome ? Option<U>.Some(map(option.Unwrap()!)) : Option<U>.None;
    }

    /// <summary>
    /// Chains multiple operations that return options.
    /// </summary>
    /// <typeparam name="T">The type of the original value.</typeparam>
    /// <typeparam name="U">The type of the value of the next option.</typeparam>
    /// <param name="option">The option to chain from.</param>
    /// <param name="bind">The function that returns the next option.</param>
    /// <returns>The result of the next operation if the current option has a value, otherwise a none option.</returns>
    public static Option<U> Bind<T, U>(this Option<T> option, Func<T, Option<U>> bind)
    {
        return option.IsSome ? bind(option.Unwrap()!) : Option<U>.None;
    }

    /// <summary>
    /// Executes a side-effect action without altering the option.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <param name="option">The option to tap into.</param>
    /// <param name="tapAction">The action to execute.</param>
    /// <returns>The original option to allow method chaining.</returns>
    public static Option<T> Tap<T>(this Option<T> option, Action<T> tapAction)
    {
        if (option.IsSome)
        {
            tapAction(option.Unwrap()!);
        }
        return option;
    }
}


