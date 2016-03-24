using System;
using System.ComponentModel;

namespace ConsoleRunner
{
    public class Argument<T> : Argument
    {
        public Argument(string name, bool isRequired, string defaultValue = "", Func<T, bool> customValidation = null)
            : base(name, isRequired, typeof(T), defaultValue, null)
        {
            CustomValidation = customValidation ?? (t => true);
        }

        public T Value => ValueAs<T>();

        public Func<T, bool> CustomValidation { get; internal set; }

        public override bool IsValid()
        {
            var isValid = base.IsValid();

            if (isValid)
            {
                isValid = CustomValidation.Invoke(Value);
            }

            return isValid;
        }
    }

    public class Argument
    {
        private readonly Func<string, bool> _customValidation;

        public Argument(string name, bool isRequired, Type type = null, string defaultValue = "",
            Func<string, bool> customValidation = null)
        {
            Type = type ?? typeof(string);
            _customValidation = customValidation ?? (s => true);
            Name = name;
            IsRequired = isRequired;
            StringValue = defaultValue;
        }

        public Type Type { get; }
        public string Name { get; }
        public bool IsRequired { get; set; }
        public string StringValue { get; set; }

        public T ValueAs<T>()
        {
            if (typeof(T) != Type)
            {
                throw new CommandLineArgumentException($"Tried to parse into the wrong type. Supported: {Type} Actual: {typeof(T)}");
            }

            var result = ConvertCurrentValue();

            return (T)result;
        }

        private object ConvertCurrentValue()
        {
            var converter = TypeDescriptor.GetConverter(Type);
            var result = converter.ConvertFromString(StringValue);
            return result;
        }

        public virtual bool IsValid()
        {
            try
            {
                ConvertCurrentValue();
            }
            catch (Exception)
            {
                return false;
            }

            return !(string.IsNullOrEmpty(StringValue) && IsRequired) && _customValidation(StringValue);
        }

        public override string ToString()
        {
            return StringValue;
        }
    }
}