using System;
using System.Linq;
using JavaPropertiesParser.Utils;

namespace JavaPropertiesParser.Expressions
{
    public class Document
    {
        public Document(params ITopLevelExpression[] expressions)
        {
            Expressions = expressions;
        }

        public ITopLevelExpression[] Expressions { get; }

        public Document Mutate(Func<ITopLevelExpression, ITopLevelExpression> mutator)
        {
            var mutatedExpressions = Expressions.Mutate(mutator);
            if (ReferenceEquals(Expressions, mutatedExpressions))
                return this;
            
            return new Document(mutatedExpressions);
        }

        public override string ToString()
        {
            return string.Join("", Expressions.Select(e => e.ToString()));
        }

        protected bool Equals(Document other)
        {
            return Expressions.SequenceEqual(other.Expressions);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Document) obj);
        }

        public override int GetHashCode()
        {
            return Expressions.AggregateHashCode();
        }
    }
}