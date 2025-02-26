using System.Linq.Expressions;
using System.Reflection;

namespace Ambev.DeveloperEvaluation.ORM
{
    public static class QueryableOrderer
    {
        public static IQueryable<T> OrderByDynamic<T>(this IQueryable<T> query, string propertyName, string direction, bool thenBy)
        {
            var type = typeof(T);
            var property = type.GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);

            if (property == null)
                throw new ArgumentException($"Property '{propertyName}' not found on '{type.Name}'.");

            var parameter = Expression.Parameter(type, "x");
            var propertyAccess = Expression.Property(parameter, property);
            var orderByExp = Expression.Lambda(propertyAccess, parameter);

            string methodName;

            if (thenBy)
            {
                methodName = direction == "descending" ? "ThenByDescending" : "ThenBy";
            }
            else
            {
                methodName = direction == "descending" ? "OrderByDescending" : "OrderBy";
            }

            var resultExp = Expression.Call(typeof(Queryable), methodName, new Type[] { type, property.PropertyType }, query.Expression, Expression.Quote(orderByExp));

            return query.Provider.CreateQuery<T>(resultExp);
        }

        public static IQueryable<T> ApplyOrdering<T>(IQueryable<T> query, string order) where T : class
        {
            var orderParams = order.Split(',').Select(o => o.Trim().Split(' '));
            for (int i = 0; i < orderParams.Count(); i++)
            {
                var param = orderParams.ElementAt(i);
                if (param.Length == 2)
                {
                    var property = param[0].Trim();
                    var direction = param[1].Trim().ToLower() == "desc" ? "descending" : "ascending";
                    query = query.OrderByDynamic(property, direction, i > 0);
                }
            }

            return query;
        }
    }

}
