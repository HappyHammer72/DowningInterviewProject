namespace DowningInterviewProject.Model.Tests.Helpers
{
    using System;

    internal class StringHelpers
    {
        internal static string? GetSpecificLengthString(int length)
        {
            return new string('A', length);
        }
    }
}
