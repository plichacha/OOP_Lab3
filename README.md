# Virtual Item Shop — OOP Lab-3

A C# console application developed as part of a practical assignment in **Object-Oriented Programming**.

The project demonstrates the principles of **encapsulation**, **properties**, **access modifiers**, **exception handling**, **constructor overloading**, **constructor chaining**, and **method overloading**.

## Project Overview

The application simulates a shop for virtual RPG items such as weapons, artifacts, armor, and other game items.

Each item is represented by the `VirtualItem` class and has a rarity represented by the `ItemRarity` enumeration.

The program allows the user to:

* add virtual items using randomly selected constructors;
* view all stored items;
* find an item;
* demonstrate overloaded methods and item behavior;
* delete an item;
* exit the application.

## Technologies

* C#
* .NET
* Object-Oriented Programming
* Console Application

## Project Structure

```text
VirtualItemShop/
│
├── Program.cs
├── VirtualItem.cs
├── ItemRarity.cs
└── README.md
```

## How to Run

### Using Visual Studio

1. Clone the repository:

```bash
git clone https://github.com/plichacha/OOP_Lab3.git
```

2. Open the solution file in Visual Studio.
3. Build the solution.
4. Run the project.

### Using .NET CLI

Navigate to the project directory and run:

```bash
dotnet restore
dotnet build
dotnet run
```

## Main Class

### `VirtualItem`

The `VirtualItem` class represents a virtual RPG item and contains the following properties:

| **Property**  | **Type**     | **Description**                          |
| ------------- | ------------ | ---------------------------------------- |
| `Name`        | `string`     | Item name                                |
| `Rarity`      | `ItemRarity` | Item rarity                              |
| `Price`       | `double`     | Base item price                          |
| `Durability`  | `int`        | Item durability from 0 to 100            |
| `IsTradable`  | `bool`       | Indicates whether the item can be traded |
| `CreatedDate` | `DateTime`   | Item creation date                       |
| `UsesCount`   | `int`        | Number of times the item was used        |
| `Summary`     | `string`     | Computed description of the item         |
| `SellPrice`   | `double`     | Computed selling price                   |

## Encapsulation

All internal fields of `VirtualItem` are declared as `private`.

Public properties control access to the object's data and validate values before modifying the object's state.

The class uses different access levels for property getters and setters, as well as private helper methods to hide implementation details.

## Properties

### Property with validation

`Name`, `Rarity`, `Price`, and `Durability` validate input values before updating the corresponding fields.

Invalid values cause exceptions such as `ArgumentException` and `ArgumentOutOfRangeException`.

### Auto-property

```csharp
public bool IsTradable { get; set; } = false;
```

The property has a default value of `false`.

### Property with different access levels

```csharp
public DateTime CreatedDate { get; private set; }
```

The creation date can be accessed from outside the class but can only be modified inside the class.

`UsesCount` also has a public getter and a private setter.

### Computed properties

```csharp
public string Summary => ...;

public double SellPrice => ComputeSellPrice();
```

These properties calculate their values using the object's data and methods instead of storing separate values.

## Constructor Overloading

The `VirtualItem` class provides multiple constructors to initialize objects in different ways.

The constructors support different levels of initialization, from randomly generated item properties to explicitly specified values.

Constructor overloading allows objects to be created with different sets of arguments.

### Constructor chaining

The constructors use `this(...)` to call other constructors and reuse initialization logic.

This reduces code duplication and helps maintain consistent object initialization.

### Random constructor selection

When adding a new item, the program randomly selects one of four constructor options and informs the user which constructor was used.

This demonstrates different approaches to object initialization in practice.

## Method Overloading

The class provides overloaded methods that perform similar operations with different parameter lists.

### Use

```csharp
Use();
Use(int times);
```

The methods demonstrate item usage with different ways of specifying the number of uses.

### Repair

```csharp
Repair();
Repair(int amount);
```

The methods repair an item using the default repair behavior or a specified amount.

Durability cannot exceed `100`.

### Enchant

```csharp
Enchant();
Enchant(int levels);
```

The methods increase item rarity using the default behavior or a specified number of levels.

The rarity cannot exceed `MYTHIC`.

The behavior demonstration menu allows the user to test the overloaded methods interactively.

## Item Rarity

The `ItemRarity` enumeration contains six levels:

```text
COMMON
UNCOMMON
RARE
EPIC
LEGENDARY
MYTHIC
```

## Encapsulation Through Private Methods

The class contains private helper methods such as:

* `CanUse()`
* `ApplyUsage()`
* `CanRepair()`
* `ApplyRepair()`
* `IsMaxRarity()`
* `ComputeSellPrice()`

These methods encapsulate internal logic and are called by public methods when necessary.

For example, `Use()` checks whether an item can be used before applying changes to its durability and usage counter.

## Exception Handling

Invalid property values and user input are handled using exceptions and `try-catch` blocks.

The application displays informative error messages and validates user input to prevent invalid object states.

## Console Menu

```text
1 - Add item
2 - View all items
3 - Find item
4 - Demonstrate behavior
5 - Delete item
0 - Exit
```

## Testing

The application should be tested with both valid and invalid input values.

Testing scenarios include:

* invalid maximum number of items;
* empty or invalid item names;
* invalid rarity values;
* negative or excessive prices;
* durability values outside the range `0..100`;
* invalid boolean input;
* maximum number of stored objects;
* item search and deletion;
* all overloaded constructors;
* constructor chaining;
* random constructor selection;
* overloaded `Use()` methods;
* overloaded `Repair()` methods;
* overloaded `Enchant()` methods;
* selling price calculation;
* correct program termination.

## Purpose of the Lab

The purpose of this practical assignment is to demonstrate:

1. Encapsulation of class fields.
2. Controlled access through properties.
3. Validation of property values.
4. Automatic and computed properties.
5. Different access levels for `get` and `set`.
6. Private helper methods.
7. Exception handling.
8. Constructor overloading.
9. Constructor chaining using `this`.
10. Method overloading.
11. Object initialization using different constructors.
12. Interaction with objects through a console menu.

## Repository

Source code is available on GitHub:

https://github.com/plichacha/OOP_Lab3

## Author

**Iryna Nesteruk**

Object-Oriented Programming — Practical Work Lab-3
