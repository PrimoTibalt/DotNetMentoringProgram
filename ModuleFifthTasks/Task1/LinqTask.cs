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
            if (customers is null)
            {
                throw new ArgumentNullException(nameof(customers));
            }

            return customers.Where(customer => customer.Orders.Sum(order => order.Total) > limit);
        }

        public static IEnumerable<(Customer customer, IEnumerable<Supplier> suppliers)> Linq2(
            IEnumerable<Customer> customers,
            IEnumerable<Supplier> suppliers
        )
        {
            if (customers is null)
            {
                throw new ArgumentNullException(nameof(customers));
            }

            if (suppliers is null)
            {
                throw new ArgumentNullException(nameof(suppliers));
            }

            return customers.Select((customer, num) =>
            (
                customer,
                suppliers.Where(supplier => supplier.Country == customer.Country
                    && supplier.City == customer.City)
            ));
        }

        public static IEnumerable<(Customer customer, IEnumerable<Supplier> suppliers)> Linq2UsingGroup(
            IEnumerable<Customer> customers,
            IEnumerable<Supplier> suppliers
        )
        {
            if (customers is null)
            {
                throw new ArgumentNullException(nameof(customers));
            }

            if (suppliers is null)
            {
                throw new ArgumentNullException(nameof(suppliers));
            }

            return customers.GroupJoin(
                suppliers,
                customer => customer,
                supplier => new Customer(),
                (customer, suplier) =>
                (
                    customer,
                    suplier.Where(supplier => supplier.Country == customer.Country
                        && supplier.City == customer.City)
                ));
        }

        public static IEnumerable<Customer> Linq3(IEnumerable<Customer> customers, decimal limit)
        {
            if (customers is null)
            {
                throw new ArgumentNullException(nameof(customers));
            }

            return customers.Where(customer => customer.Orders.Any(order => order.Total >= limit));
        }

        public static IEnumerable<(Customer customer, DateTime dateOfEntry)> Linq4(
            IEnumerable<Customer> customers
        )
        {
            if (customers is null)
            {
                throw new ArgumentNullException(nameof(customers));
            }

            return customers
                .Where(customer => customer.Orders.Any())
                .Select(customer => (customer, customer.Orders.Min(order => order.OrderDate)));
        }

        public static IEnumerable<(Customer customer, DateTime dateOfEntry)> Linq5(
            IEnumerable<Customer> customers
        )
        {
            if (customers is null)
            {
                throw new ArgumentNullException(nameof(customers));
            }

            return Linq4(customers)
                .OrderBy(tuple => tuple.dateOfEntry.Year)
                .ThenBy(tuple => tuple.dateOfEntry.Month)
                .ThenByDescending(tuple => tuple.customer.Orders.Sum(order => order.Total))
                .ThenBy(tuple => tuple.customer.CompanyName);
        }

        public static IEnumerable<Customer> Linq6(IEnumerable<Customer> customers)
        {
            if (customers is null)
            {
                throw new ArgumentNullException(nameof(customers));
            }

            return customers.Where(customer => string.IsNullOrWhiteSpace(customer.Region) ||
                string.IsNullOrWhiteSpace(customer.PostalCode) ||
                string.IsNullOrWhiteSpace(customer.Fax) ||
                !customer.Phone.Contains("(") ||
                !customer.PostalCode.Any(ch => char.IsNumber(ch)));
        }

        public static IEnumerable<Linq7CategoryGroup> Linq7(IEnumerable<Product> products)
        {
            if (products is null)
            {
                throw new ArgumentNullException(nameof(products));
            }

            return products.GroupBy(product => product.Category, (category, products) => new Linq7CategoryGroup
            {
                Category = category,
                UnitsInStockGroup = products.GroupBy(
                    product => product.UnitsInStock,
                    product => product.UnitPrice,
                    (unitsInStock, groupedProducts) => new Linq7UnitsInStockGroup
                    {
                        UnitsInStock = unitsInStock,
                        Prices = groupedProducts.OrderBy(price => price)
                    })
            });
        }

        public static IEnumerable<(decimal category, IEnumerable<Product> products)> Linq8(
            IEnumerable<Product> products,
            decimal cheap,
            decimal middle,
            decimal expensive
        )
        {
            if (products is null)
            {
                throw new ArgumentNullException(nameof(products));
            }

            if (cheap > middle)
            {
                throw new ArgumentException(nameof(middle));
            }

            if (middle > expensive)
            {
                throw new ArgumentException(nameof(expensive));
            }

            return products.GroupBy(product =>
                    product.UnitPrice <= cheap ? cheap :
                    product.UnitPrice <= middle && product.UnitPrice > cheap ? middle :
                    expensive).Select(group => new ValueTuple<decimal, IEnumerable<Product>>(group.Key, group));
        }

        public static IEnumerable<(string city, int averageIncome, int averageIntensity)> Linq9(
            IEnumerable<Customer> customers
        )
        {
            if (customers is null)
            {
                throw new ArgumentNullException(nameof(customers));
            }

            return customers.GroupBy(customer => customer.City).Select(category =>
            {
                return (
                    category.Key,
                    Convert.ToInt32(customers
                        .Where(customer => customer.City == category.Key)
                        .Average(customer => customer.Orders.Any() ? customer.Orders.Sum(order => order.Total) : 0)),
                    (int)customers
                        .Where(customer => customer.City == category.Key)
                        .Average(customer => customer.Orders.Count()));
            });
        }

        public static string Linq10(IEnumerable<Supplier> suppliers)
        {
            if (suppliers is null)
            {
                throw new ArgumentNullException(nameof(suppliers));
            }

            return string.Join(
                string.Empty,
                suppliers.OrderBy(supplier => supplier.Country.Length)
                    .ThenBy(supplier => supplier.Country)
                    .GroupBy(supplier => supplier.Country)
                    .Select(supplier => supplier.Key));
        }
    }
}
