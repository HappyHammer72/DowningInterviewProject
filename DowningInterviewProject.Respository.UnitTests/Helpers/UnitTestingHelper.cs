namespace DowningInterviewProject.Respository.UnitTests.Helpers;

using System;
using System.Collections;
using System.Collections.Generic;

using Xunit;

public static class UnitTestingHelper
{
    public static void AssertPublicPropertiesEqual(object expected, object actual)
    {
        AssertPublicPropertiesEqual(expected, actual, null, null, null, new Stack<object>());
    }

    public static void AssertPublicPropertiesEqual(
        object? expected,
        object? actual,
        string? objectDescription,
        string? message,
        IgnoreProperties? ignoreProperties,
        Stack<object> visitedObjects)
    {
        if (string.IsNullOrEmpty(objectDescription))
        {
            objectDescription = "<object>";
        }

        var assertMsg = string.IsNullOrEmpty(objectDescription)
            ? string.Empty
            : "(Property: " + objectDescription + ")";
        if (!string.IsNullOrEmpty(message))
        {
            assertMsg += (assertMsg.Length > 0 ? " " : string.Empty) + message;
        }

        if (expected == null || actual == null)
        {
            // Either expected or actual is null, so assert that both are.
            Assert.Equal(expected, actual);
        }
        else
        {
            // Neither expected nor actual is null.
            if (StartVisit(expected, visitedObjects, out bool haveAddedToVisitedObjects))
            {
                // Looks like the caller's original type contains a circular reference - we have already seen [expected].
                return;
            }

            // Assert that expected and actual are of the same type.
            Assert.Same(
                expected.GetType(),
                actual.GetType());
            var objectType = expected.GetType();

            var checkObjectPublicProperties =
                !(objectType.IsPrimitive || objectType.IsEnum || expected is string || expected is DateTime);

            // For these types there is no need to check the public properties.
            var isValueTypeObjectWithoutRefProperties = objectType.IsValueType;

            // While checking public properties, we will keep track of whether the object is a value type that contains only value type properties (in which case we will check bitwise equality).
            if (checkObjectPublicProperties)
            {
                // See if the caller has supplied an ignore list.
                var hasIgnoreList = ignoreProperties != null && ignoreProperties.Count > 0;

                foreach (var currProperty in objectType.GetProperties())
                {
                    isValueTypeObjectWithoutRefProperties &= currProperty.PropertyType.IsValueType;

                    var getterMethod = currProperty.GetGetMethod();
                    if (getterMethod != null)
                    {
                        var isStaticProperty = getterMethod.IsStatic;
                        var isIndexedProperty = getterMethod.GetParameters().Length > 0;

                        if (!isStaticProperty && !isIndexedProperty
                                              && (!hasIgnoreList || !ignoreProperties!.IgnoreProperty(objectType, currProperty.Name)))
                        {
                            // This is not a static property, not an indexed property and it is not on the ignore list so check it.
                            var expectedPropValue = currProperty.GetValue(expected, null);
                            var actualPropValue = currProperty.GetValue(actual, null);
                            var propertyDescription = objectDescription == null
                                ? currProperty.Name
                                : $"{objectDescription}.{currProperty.Name}";

                            AssertPublicPropertiesEqual(
                                expectedPropValue,
                                actualPropValue,
                                propertyDescription,
                                message,
                                ignoreProperties,
                                visitedObjects);
                        }

                        // is non-indexed and not on ignore list
                    }

                    // has a Get method
                }

                // foreach property in object
                if (typeof(IEnumerable).IsAssignableFrom(objectType))
                {
                    // Object is some kind of collection.  Enumerate through all the objects it contains.
                    var expectedEnumerator = ((IEnumerable)expected).GetEnumerator();
                    var actualEnumerator = ((IEnumerable)actual).GetEnumerator();
                    var count = 0;
                    var moreItemsInExpected = true;
                    while (moreItemsInExpected)
                    {
                        if (moreItemsInExpected = expectedEnumerator.MoveNext())
                        {
                            Assert.True(
                                actualEnumerator.MoveNext(),
                                $"Expected more items in collection; actual has only {count} item(s). {assertMsg}");
                            count++;
                            AssertPublicPropertiesEqual(
                                expectedEnumerator.Current,
                                actualEnumerator.Current,
                                $"{objectDescription}[{count - 1}]",
                                message,
                                ignoreProperties,
                                visitedObjects);
                        }
                        else
                        {
                            Assert.False(
                                actualEnumerator.MoveNext(),
                                $"Expected {count} items in collection, but actual has more than that. {assertMsg}");
                        }
                    }

                    //// while there are more items in the enumeration
                }

                //// object is enumerable
            }

            //// check object's public properties
            var checkObjectValueEquality = isValueTypeObjectWithoutRefProperties || expected is string;
            if (checkObjectValueEquality)
            {
                Assert.Equal(expected, actual);
            }

            if (haveAddedToVisitedObjects)
            {
                EndVisit(visitedObjects);
            }
        }

        // object is not null
    }

    private static void EndVisit(Stack<object> visitedObjects)
    {
        visitedObjects.Pop();
    }

    private static bool StartVisit(object obj, Stack<object> visitedObjects, out bool havePushed)
    {
        havePushed = false;

        // Returns true if the object has already been visited, otherwise adds it to the visited stack.
        // Always returns false for value types and strings.
        if (obj.GetType().IsValueType || obj is string)
        {
            return false;
        }

        var haveVisited = false;
        foreach (var currObject in visitedObjects)
        {
            haveVisited = currObject == obj;

            // This weird test forces the use of the base Object.Equals(), because we always want reference equality.
            if (haveVisited)
            {
                break;
            }
        }

        if (!haveVisited)
        {
            visitedObjects.Push(obj);
            havePushed = true;
        }

        return haveVisited;
    }
}