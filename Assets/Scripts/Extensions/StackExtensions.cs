using System.Collections.Generic;

namespace Extensions
{
    public static class StackExtensions
    {
        public static T PeekOrNull<T>(this Stack<T> stack) where T : class
            => stack.Count > 0 ? stack.Peek() : null;
    }
}