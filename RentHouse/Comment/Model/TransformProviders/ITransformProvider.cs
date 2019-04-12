using Comment.Model.Query;
using System;
using System.Collections.Generic;

namespace Comment.Model.TransformProviders
{
    public interface ITransformProvider
    {
        bool Match(ConditionItem item, Type type);

        IEnumerable<ConditionItem> Transform(ConditionItem item, Type type);
    }
}