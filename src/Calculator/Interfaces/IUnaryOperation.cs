using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RamanM.Properti.Calculator.Interfaces;

/// <summary>
/// Defines unary operation with one operand.
/// </summary>
public interface IUnaryOperation : IOperation, IResultant, IPrintable
{
    IOperation Operand { get; }
}
