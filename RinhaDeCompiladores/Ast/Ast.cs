using RinhaDeCompiladores.Enums;

namespace RinhaDeCompiladores.Ast;

public class AstRoot
{
    public string Name { get; set; }
    public Term Expression { get; set; }
    public Location Location { get; set; }
}

public class Term
{
    public AstKind Kind { get; set; }
}

public class Location
{
    public int Start { get; set; }
    public int End { get; set; }
    public string Filename { get; set; }
}

public class File : Term
{
    public string Name { get; set; }
    public Term Expression { get; set; }
    public Location Location { get; set; }
}

public class Parameter
{
    public string Text { get; set; }
    public Location Location { get; set; }
}

public class Var : Term
{
    public string Text { get; set; }
    public Location Location { get; set; }
}
public class Function : Term
{
    public Parameter[] Parameters { get; set; }
    public Term Value { get; set; }
    public Location Location { get; set; }
}
public class Str : Term
{
    public string Value { get; set; }
    public Location Location { get; set; }
}
public class Bool : Term
{
    public bool Value { get; set; }
    public Location Location { get; set; }
}
public class Int : Term
{
    public int Value { get; set; }
    public Location Location { get; set; }
}
public class Tuple : Term
{
    public Term First { get; set; }
    public Term Second { get; set; }
    public Location Location { get; set; }
}
public class Print : Term
{
    public Term Value { get; set; }
    public Location Location { get; set; }
}
public class Let : Term
{
    public Parameter Name { get; set; }
    public Term Value { get; set; }
    public Term Next { get; set; }
    public Location Location { get; set; }
}
public class If : Term
{
    public Term Condition { get; set; }
    public Term Then { get; set; }
    public Term Otherwise { get; set; }
    public Location Location { get; set; }
}
public class Call : Term
{
    public Term Callee { get; set; }
    public Term[] Arguments { get; set; }
    public Location Location { get; set; }
}
public class Binary : Term
{
    public Term Lhs { get; set; }
    public BinaryOp Op { get; set; }
    public Term Rhs { get; set; }
    public Location Location { get; set; }
}
public class First : Term
{
    public Term Value { get; set; }
    public Location Location { get; set; }
}
public class Second : Term
{
    public Term Value { get; set; }
    public Location Location { get; set; }
}

public enum AstKind
{
    Binary,
    Bool,
    Call,
    Int,
    File,
    First,
    Function,
    If,
    Let,
    Parameter,
    Print,
    Second,
    Str,
    Term,
    Tuple,
    Var
}