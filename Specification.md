# Math Operations Calculator

## Formulation of the problem
A calculator being written in C# shall be able to handle the following mathematical operations:
- Summation, operator `+`, binary
- Subtraction, operator `-`, binary
- Multiplication, operator `*`, binary
- Division, operator `/`, binary
- Fraction, operator `/`, binary
- Faculty, operator `!`, unary

## Examples
- `new Sum(5.2, 1.5)` returns `6.7`, type is `double`
- `new Division(30, new Sum(2, 3))` returns `6.0`, type is `double`
- `new Faculty(4)` returns `24`, type is `long`
- `new Multiplication(new Fraction(9,4), new Fraction(2,3))` returns `1.5`, type is `double`

## Calculation interface(s)
Each math operation object should have an interface declarations and it should implement the following methods:
- `double ToResult()` returns the math operation resulting value. <br/>
  By calling `.ToResult()` on any operation, the corresponding double value shall be returned. <br/>
  For instance, `new Multiplication(2, 5).ToResult()` shall return `10`.
- `string Print()` returns string representation of applied operation. <br/>
  By calling `.Print()` on any operation, the expression shall be printed in full. <br/>
  For instance, `new Multiplication(2, 5).Print()` shall return `(2 * 5) = 10`.
- `string PrintSentence()` returns string representation of applied operation in a form of human speaking language. <br/>
  By calling `.PrintSentence()`, an English speaking sentence shall be printed. <br/>
  For instance, `new Multiplication(2, 5).PrintSentence()` shall return `multiplication of 2 and 5 is 10`.

## Fitness tests
For the `ToResult()` method:
1. `new Sum(5.2, 1.5).ToResult()` should return `6.7`
2. `new Division(30, new Sum(2, 3)).ToResult()` should return `6`
3. `new Faculty(4).ToResult()` should return `24`
4. `new Multiplication(new Fraction(9,4), new Fraction(2,3)).ToResult()` should return `1.5`

For the `Print()` method:
1. `new Sum(5.2, 1.5).Print()` should return `(5.2 + 1.5) = 6.7`
2. `new Division(30, new Sum(2, 3)).Print()` should return `(30 / (2 + 3)) = 6`
3. `new Faculty(4).Print()` should return `(4!) = 24`
4. `new Multiplication(new Fraction(6,4), new Fraction(2,3)).Print()` should return `((9/4) * (2/3)) = 1.5`

For the `PrintSentence()` method:
1. `new Sum(5.2, 1.5).PrintSentence()` should return `sum of 5.2 and 1.5 is 6.7`
2. `new Division(30, new Sum(2, 3)).PrintSentence()` should return `division of 30 by sum of 2 and 3 is 6`
3. `new Faculty(4).PrintSentence()` should return `faculty of 4 is 24`
4. `new Multiplication(new Fraction(6,4), new Fraction(2,3)).PrintSentence()` should return `multiplication of 9/4 and 2/3 is 1.5`

## Design notes
📓 Kindly note that an operation can contain other operations.

📔 Final solution shall be production ready, whatever that means to developer. App design will be graded based on current 
Software Development best practices, including but not limited to SOLID, Clean Code principles, edge-case handling and alike.

📔 README.md docs should provide detailed instructions on how to run the app and 
how to add custom testing expressions to validate the implementation.
