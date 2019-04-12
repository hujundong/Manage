using Comment.Model.Query;
using System;
using System.Collections.Generic;

namespace Comment.Model.TransformProviders
{
    public class UnixTimeTransformProvider : ITransformProvider
    {
        public bool Match(ConditionItem item, Type type)
        {
            return item.Method == QueryMethod.DateBlockEqual;
        }

        public IEnumerable<ConditionItem> Transform(ConditionItem item, Type type)
        {
            DateTime willTime;
            if (DateTime.TryParse(item.Value.ToString(), out willTime))
            {
                var method = item.Method;

                if (method == QueryMethod.LessThan || method == QueryMethod.LessThanOrEqual)
                {
                    method = QueryMethod.DateTimeLessThanOrEqual;
                    if (willTime.Hour == 0 && willTime.Minute == 0 && willTime.Second == 0)
                    {
                        willTime = willTime.AddDays(1).AddMilliseconds(-1);
                    }
                }
                else if (method == QueryMethod.DateBlockEqual)
                {
                    DateTime startTime = DateTime.MinValue, endTime = DateTime.MaxValue;
                    string[] dateArray = item.Value.ToString().Replace("/", "-").Split('-');
                    switch (dateArray.Length)
                    {
                        case 1:
                            endTime = GetEndTime(item.Value + "-1-1", out startTime, "year");
                            break;

                        case 2:
                            endTime = GetEndTime(item.Value.ToString(), out startTime, "month");
                            break;

                        case 3:
                            endTime = GetEndTime(item.Value.ToString(), out startTime, "day");
                            break;
                    }

                    return new[]
                       {
                           new ConditionItem(item.Field, QueryMethod.GreaterThanOrEqual,startTime),
                           new ConditionItem(item.Field, QueryMethod.LessThan,endTime)
                       };
                }
                object value = null;
                if (type == typeof(DateTime))
                {
                    value = willTime;
                }
                else if (type == typeof(int))
                {
                    value = (int)UnixTime.FromDateTime(willTime);
                }
                else if (type == typeof(long))
                {
                    value = UnixTime.FromDateTime(willTime);
                }
                else if (type == typeof(Nullable<DateTime>))
                {
                    value = willTime;
                }
                return new[] { new ConditionItem(item.Field, method, value) };
            }
            else
            {
                DateTime startTime = DateTime.MinValue, endTime = DateTime.MaxValue;
                endTime = GetEndTime(item.Value + "-1-1", out startTime, "year");
                return new[]
                       {
                           new ConditionItem(item.Field, QueryMethod.GreaterThanOrEqual,startTime),
                           new ConditionItem(item.Field, QueryMethod.LessThan,endTime)
                       };
            }
        }

        private DateTime GetEndTime(string value, out DateTime time, string type)
        {
            DateTime endtime;
            if (type == "year")
            {
                if (DateTime.TryParse(value, out time))
                {
                    endtime = time.AddYears(1);
                }
                else
                {
                    throw new Exception("查询的时间格式不正确，错误的时间：" + value);
                }
            }
            else if (type == "month")
            {
                if (DateTime.TryParse(value, out time))
                {
                    endtime = time.AddMonths(1);
                }
                else
                {
                    throw new Exception("查询的时间格式不正确，错误的时间：" + value);
                }
            }
            else
            {
                if (DateTime.TryParse(value, out time))
                {
                    endtime = time.AddDays(1);
                }
                else
                {
                    throw new Exception("查询的时间格式不正确，错误的时间：" + value);
                }
            }
            return endtime;
        }
    }
}