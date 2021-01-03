using System;
using System.Collections.Generic;
using Wakening.Messages;
namespace Wakening
{
    public class Operator : Condition
    {
        public enum Operations
        {
            AND,
            OR
        }
        public Condition[] operands;

        public Operations operationName;

        public override void Listen() => Subscribe<Condition, ValueChanged<bool>>((source, args) => { ops[operationName](operands); });


        public static readonly Dictionary<Operations, Func<Condition[], bool>> ops = new Dictionary<Operations, Func<Condition[], bool>>()
        {
            {
                Operations.AND , (Condition[] conditions) => {
                    foreach (Condition c in conditions)
                        if (!c.Value) return false;
                    return true;
                }
            },
            {
                Operations.OR, (Condition[] conditions) => {
                    foreach (Condition c in conditions)
                        if (c.Value) return true;
                    return false;
                }
            }
        };
    }
}
