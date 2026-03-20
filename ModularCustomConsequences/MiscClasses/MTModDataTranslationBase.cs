using System;
using System.Collections.Generic;

namespace MTCustomScripts.MiscClasses
{
    public interface IDataTranslation
    {
        Type GrabDataType();
        int EqualOperator();
        int SuperiorOperator();
        int InferiorOperator();
    }

    public abstract class DataTranslationBase<T>(T data, string originComparatorKey, T realComparatorKey) : IDataTranslation
    {
        protected T Data { get; } = data;
        protected string OriginComparatorKey { get; } = originComparatorKey;
        protected T RealComparatorKey { get; private set; } = realComparatorKey;

        protected DataTranslationBase(T data, string originComparatorKey) : this(data, originComparatorKey, default) { }

        public abstract Type GrabDataType();
        public virtual void SetRealComparatorKey(T realData) => RealComparatorKey = realData;
        public abstract int EqualOperator();
        public abstract int SuperiorOperator();
        public abstract int InferiorOperator();
    }

    public class DataTranslation_String(string data, string comparatorKey) : DataTranslationBase<string>(data, comparatorKey)
    {
        public override Type GrabDataType() => typeof(string);
        public override int EqualOperator() => Data?.Equals(OriginComparatorKey) == true ? 1 : 0;
        public override int SuperiorOperator() => Data?.Contains(OriginComparatorKey) == true ? 1 : 0;
        public override int InferiorOperator() => OriginComparatorKey?.Contains(Data) == true ? 1 : 0;
    }

    public class DataTranslation_Integer(int data, string comparatorKey, int realComparatorKey = 0) : DataTranslationBase<int>(data, comparatorKey, realComparatorKey)
    {
        public override Type GrabDataType() => typeof(int);
        public override int EqualOperator() => Data == RealComparatorKey ? 1 : 0;
        public override int SuperiorOperator() => Data > RealComparatorKey ? 1 : 0;
        public override int InferiorOperator() => Data < RealComparatorKey ? 1 : 0;
    }
}
