using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DifficultyOptions
{
    public class VariableReference<T>
    {
        public T Value { get; set; }

        public VariableReference(T item) {
            this.Value = item;
        }

    }
}
