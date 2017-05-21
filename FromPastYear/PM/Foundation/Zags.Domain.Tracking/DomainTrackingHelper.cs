using KellermanSoftware.CompareNetObjects;
using System.Collections.Generic;
using Zags.Logging.Events;

namespace Zags.Domain.Tracking
{
    public static class DomainTrackingHelper<T>
    {

        public static List<DomainTrackingDifference> Compare(T oldValue, T newValue)
        {
            List<DomainTrackingDifference> differences = new List<DomainTrackingDifference>();
            CompareLogic basicComparison = new CompareLogic()
            { Config = new ComparisonConfig() { MaxDifferences = int.MaxValue, CompareChildren = false } };
            List<Difference> diffs = basicComparison.Compare(oldValue, newValue).Differences;
            foreach (Difference diff in diffs)
            {
                DomainTrackingDifference difference = new DomainTrackingDifference();
                difference.PropertyName = diff.PropertyName;
                difference.OldValue = diff.Object1Value;
                difference.NewValue = diff.Object2Value;
                differences.Add(difference);
            }

            return differences;
        }
    }
}
