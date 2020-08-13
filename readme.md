This library provides a way to parse Java `.properties` files in .NET. This parser is intended for scenarios where you care about non-semantic elements such as comments, whitespace, newlines, and escape sequences. Usage is simple:

```C#
using JavaPropertiesParser;

// ...

var document = Parser.Parse(input);
```

Rather than parsing to a dictionary of key/value pairs, this parser gives you an AST that you can query and manipulate as required.

- [`DocumentFixture`](./source/JavaPropertiesParser.Tests/DocumentFixture.cs) has an example of mutating the values in a `.properties` document while still preserving formatting and other non-semantic elements.
- [`ParserFixture`](./source/JavaPropertiesParser.Tests/ParserFixture.cs) demonstrates the various `.properties` constructs that are supported and how they get represented in an AST.

# Building .properties Files
If you want to build a `.properties` file, consider using the [`Build`](./source/JavaPropertiesParser/Build.cs) class:

```C#
using static JavaPropertiesParser.Build;

// ...

Doc(
    Pair(
        Key("key"),
        Separator(":"),
        Value("value")
    ),
    Whitespace("\n"),
    Pair(
        Key("key:2", Encode.Key("key:2")),
        Separator("="),
        Value("value")
    )
);
```