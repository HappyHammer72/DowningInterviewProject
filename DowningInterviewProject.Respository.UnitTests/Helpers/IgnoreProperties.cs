namespace DowningInterviewProject.Respository.UnitTests.Helpers;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class IgnoreProperties : List<PropertyComparisonExclusion>
{
    public bool IgnoreProperty(Type type, string propertyName)
    {
        return Find(
                   delegate (PropertyComparisonExclusion item)
                   {
                       switch (item.TypeAction)
                       {
                           case PropertyComparisonExclusionTypeAction.MatchTypeAndDerivedTypes:
                               if (item.IgnorePropertyName == propertyName)
                               {
                                   var currIgnoreItemIsRequiredType = false;
                                   var currType = type;
                                   while (currType != null
                                          && !(currIgnoreItemIsRequiredType = item.TargetType == currType))
                                   {
                                       currType = currType.BaseType;
                                   }

                                   return currIgnoreItemIsRequiredType;
                               }

                               return false;

                           case PropertyComparisonExclusionTypeAction.MatchExactType:
                               return item.TargetType == type && item.IgnorePropertyName == propertyName;
                           default:
                               return false; // this should never be reached.
                       }
                   }) != null;
    }
}