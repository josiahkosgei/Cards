using System.ComponentModel;

namespace Cards.Core.Enums
{
    public enum ResponseEnum
    {
        [Description("00:Successful")]
        Successful,
        [Description("01:Unsuccessful service call")]
        UnSuccessful,
        [Description("02:Invalid credentials")]
        InvalidCredentials,

    }


    public static class EnumExtensions
    {
        public static (string code, string desc) EnumFormat(this ResponseEnum val)
        {
            DescriptionAttribute[] attributes = (DescriptionAttribute[])val
               .GetType()
               .GetField(val.ToString())
               .GetCustomAttributes(typeof(DescriptionAttribute), false);

            var description = attributes.Length > 0 ? attributes[0].Description : string.Empty;

            var dscSPlit = description.Split(':');
            if (dscSPlit.Count() > 1)
                return (dscSPlit[0], dscSPlit[1]);

            return ("99", "Error description not properly formatted");

        }
    }
}
