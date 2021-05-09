using System;
using System.Collections.Generic;
using System.Linq;
using Task1.DoNotChange;

namespace Task1
{
    public static class LinqTask
    {
        public static IEnumerable<Customer> Linq1(IEnumerable<Customer> customers, decimal limit)
        {
            return customers.Where(customer => customer.Orders.Sum(order => order.Total) > limit);
        }

        public static IEnumerable<(Customer customer, IEnumerable<Supplier> suppliers)> Linq2(
            IEnumerable<Customer> customers,
            IEnumerable<Supplier> suppliers
        )
        {
            return customers.Select((customer, num) => new ValueTuple<Customer, IEnumerable<Supplier>>(customer, suppliers.Where(supplier => supplier.Country == customer.Country && supplier.City == customer.City)));
        }

        public static IEnumerable<(Customer customer, IEnumerable<Supplier> suppliers)> Linq2UsingGroup(
            IEnumerable<Customer> customers,
            IEnumerable<Supplier> suppliers
        )
        {
            return customers.GroupJoin(
                suppliers,
                customer => customer,
                supplier => new Customer(),
                (c, s) => new ValueTuple<Customer, IEnumerable<Supplier>>(c, s.Where(supplier => supplier.Country == c.Country && supplier.City == c.City)));
        }

        public static IEnumerable<Customer> Linq3(IEnumerable<Customer> customers, decimal limit)
        {
            return customers.Where(customer => customer.Orders.Any(order => order.Total >= limit));
        }

        public static IEnumerable<(Customer customer, DateTime dateOfEntry)> Linq4(
            IEnumerable<Customer> customers
        )
        {
            return customers.Where(customer => customer.Orders.Count() > 0).Select(customer => new ValueTuple<Customer, DateTime>(customer, customer.Orders.Min(order => order.OrderDate)));
        }

        public static IEnumerable<(Customer customer, DateTime dateOfEntry)> Linq5(
            IEnumerable<Customer> customers
        )
        {
            return Linq4(customers)
                .OrderBy(tuple => tuple.dateOfEntry.Year)
                .ThenBy(tuple => tuple.dateOfEntry.Month)
                .ThenByDescending(tuple => tuple.customer.Orders.Sum(order => order.Total));
        }

        public static IEnumerable<Customer> Linq6(IEnumerable<Customer> customers)
        {
            return customers.Where(customer => string.IsNullOrWhiteSpace(customer.Region) || string.IsNullOrWhiteSpace(customer.PostalCode) || string.IsNullOrWhiteSpace(customer.Fax) || !customer.Phone.Contains("(") || !customer.PostalCode.Any(ch => char.IsNumber(ch)));
        }

        public static IEnumerable<Linq7CategoryGroup> Linq7(IEnumerable<Product> products)
        {
            return products.GroupBy(product => product.Category).Select(category => new Linq7CategoryGroup { Category = category.Key }).Join(
                products,
                selectorInner => selectorInner.Category,
                selectorOuter => selectorOuter.Category,
                (category, product) =>
                {
                    category.UnitsInStockGroup = category.UnitsInStockGroup ?? new List<Linq7UnitsInStockGroup>();
                    if (category.UnitsInStockGroup.Any(unit => unit.UnitsInStock == product.UnitsInStock))
                    {
                        category.UnitsInStockGroup.FirstOrDefault(unit => unit.UnitsInStock == product.UnitsInStock).Prices.ToList().Add(product.UnitPrice);
                    }
                    else
                    {
                        category.UnitsInStockGroup.ToList().Add(new Linq7UnitsInStockGroup { UnitsInStock = product.UnitsInStock, Prices = new List<decimal> { product.UnitPrice } });
                    }

                    category.UnitsInStockGroup.ToList().Add(new Linq7UnitsInStockGroup { Prices = new HashSet<decimal> { product.UnitPrice }, UnitsInStock = product.UnitsInStock });
                    return category;
                });
        }

        public static IEnumerable<(decimal category, IEnumerable<Product> products)> Linq8(
            IEnumerable<Product> products,
            decimal cheap,
            decimal middle,
            decimal expensive
        )
        {
            throw new NotImplementedException();
        }

        public static IEnumerable<(string city, int averageIncome, int averageIntensity)> Linq9(
            IEnumerable<Customer> customers
        )
        {
            throw new NotImplementedException();
        }

        public static string Linq10(IEnumerable<Supplier> suppliers)
        {
            throw new NotImplementedException();
        }
    }
}
