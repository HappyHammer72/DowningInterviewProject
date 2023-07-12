namespace DowningInterviewProject.Respository.UnitTests.Helpers;

using System;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class PropertyComparisonExclusion
{
    public PropertyComparisonExclusion(Type type, string propertyName)
        : this(type, propertyName, PropertyComparisonExclusionTypeAction.MatchExactType)
    {
    }

    public PropertyComparisonExclusion(
        Type type, string propertyName, PropertyComparisonExclusionTypeAction typeAction)
    {
        TargetType = type;
        IgnorePropertyName = propertyName;
        TypeAction = typeAction;
    }

    public string IgnorePropertyName { get; set; }

    public Type TargetType { get; set; }

    public PropertyComparisonExclusionTypeAction TypeAction { get; set; }
}