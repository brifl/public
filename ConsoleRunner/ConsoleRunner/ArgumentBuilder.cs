using System;

namespace ConsoleRunner
{
    public static class ArgumentBuilder
    {
        public static Argument<T> AsArg<T>(this string s)
        {
            return new Argument<T>(s, false);
        }

        public static Argument<T> Required<T>(this Argument<T> arg)
        {
            arg.IsRequired = true;
            return arg;
        }

        public static Argument<T> WithDefault<T>(this Argument<T> arg, string defaultValue)
        {
            arg.StringValue = defaultValue;
            return arg;
        }

        public static Argument<T> WithCustomValidation<T>(this Argument<T> arg, Func<T, bool> customValidation)
        {
            arg.CustomValidation = customValidation;
            return arg;
        }
    }
}