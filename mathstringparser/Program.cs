using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Console.WriteLine("Введіть математичний приклад (наприклад, 2 * 2 + 2):");
        string input = Console.ReadLine();

        double result = CalculateExpression(input);

        Console.WriteLine("Результат: " + result);
    }

    static double CalculateExpression(string expression)
    {
        string[] parts = expression.Split(' ');

        Stack<double> operandStack = new Stack<double>();
        Stack<char> operatorStack = new Stack<char>();

        foreach (string part in parts)
        {
            if (double.TryParse(part, out double number))
            {
                operandStack.Push(number);
            }
            else if (IsOperator(part))
            {
                while (operatorStack.Count > 0 && Priority(operatorStack.Peek()) >= Priority(part[0]))
                {
                    PerformOperation(operandStack, operatorStack);
                }
                operatorStack.Push(part[0]);
            }
        }

        while (operatorStack.Count > 0)
        {
            PerformOperation(operandStack, operatorStack);
        }

        return operandStack.Pop();
    }

    static int Priority(char op)
    {
        return op switch
        {
            '*' or '/' => 2,
            '+' or '-' => 1,
            _ => 0,
        };
    }

    static bool IsOperator(string op)
    {
        return op == "*" || op == "/" || op == "+" || op == "-";
    }

    static void PerformOperation(Stack<double> operandStack, Stack<char> operatorStack)
    {
        double operand2 = operandStack.Pop();
        double operand1 = operandStack.Pop();
        char operation = operatorStack.Pop();

        switch (operation)
        {
            case '+':
                operandStack.Push(operand1 + operand2);
                break;
            case '-':
                operandStack.Push(operand1 - operand2);
                break;
            case '*':
                operandStack.Push(operand1 * operand2);
                break;
            case '/':
                if (operand2 == 0)
                {
                    throw new DivideByZeroException("Ділення на нуль!");
                }
                operandStack.Push(operand1 / operand2);
                break;
        }
    }
}
