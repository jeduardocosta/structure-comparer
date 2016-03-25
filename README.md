# Structure Comparer

[![Build status](https://ci.appveyor.com/api/projects/status/e25wawak9781jv0u?svg=true)](https://ci.appveyor.com/project/jeduardocosta/structure-comparer)
[![Coverage Status](https://coveralls.io/repos/jeduardocosta/structure-comparer/badge.svg)](https://coveralls.io/r/jeduardocosta/structure-comparer)
[![Nuget version](https://img.shields.io/nuget/v/StructureComparer.svg)](https://www.nuget.org/packages/StructureComparer/)
[![Nuget downloads](https://img.shields.io/nuget/dt/StructureComparer.svg)](https://www.nuget.org/packages/StructureComparer/)

Project Homepage: https://github.com/jeduardocosta/StructureComparer

Available on NuGet as well: https://www.nuget.org/packages/StructureComparer

###Description


A .NET library that it allows for comparison of primitive, value and enum types. It can be used to ensure the equality between entities.

###Example Usage###

Use StructureComparer class from  IStructureComparer contract and the Compare method to do comparisons.

```cs
IStructureComparer comparer = new StructureComparer();
```

#### Example 1 ( Check the equality using object types )####

```
var customer = new Customer();
var customerDto = new CustomerDTO();

var result = comparer.Compare(typeof(customer), typeof(customerDto));
```

#### Example 2 ( Check the equality using generics )####

```
var result = comparer.Compare<Customer, CustomerDTO>();
```

#### Comparison Result ####

The StructureComparisonResult class contains the result of comparison between entry types.

* AreEqual ( compared objects  are equal? )
* DifferencesString ( result with the differences found ) - Example:
```txt
Failed to validate structures. Type 1: 'Int32', Type 2: 'Int16'. Property name: 'Id'
Failed to validate structures. Type 1: 'FakeEnum', Type 2: 'FakeEnumDifferentNames'. Property name: 'Enum'
Failed to validate structures. Type 1: 'FakeEnum', Type 2: 'FakeEnumDifferentNames'. Reason: divergent enum names. Property name: 'Enum' from 'FakeClass3' from 'FakeClass2'
Failed to validate structures. Type 1: 'FakeClass1', Type 2: 'FakeClass1'. Reason: property name 'FullName' was not found in type 'FakeClass1'
