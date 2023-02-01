﻿namespace RamanM.Properti.Calculator.Interfaces;

/// <summary>
/// Defines binary operation with two operands.
/// </summary>
public interface IBinaryOperation : IOperation, IResultant, IPrintable
{
    /// <summary>
    /// First, left operand.
    /// </summary>
    IOperation Left { get; }
    
    /// <summary>
    /// Second, right operand.
    /// </summary>
    IOperation Right { get; }
}
