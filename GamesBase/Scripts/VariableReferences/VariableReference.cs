using System;
using DejaMoo.SoVariables;
using UnityEngine;

namespace DejaMoo.VariableReferences
{
    [Serializable]
    public class VariableReference<T> where T : IComparable, IEquatable<T>
    {
        [SerializeField] protected bool useConstant;
        [SerializeField] protected T constantValue;
        [SerializeField] protected SoVariable<T> variable;

        public event Action<T> ValueChanged;

        public VariableReference()
        { }

        public VariableReference(T value)
        {
            useConstant = true;
            constantValue = value;
        }

        public static implicit operator T(VariableReference<T> reference)
        {
            return reference.Value;
        }

        protected virtual void OnValueChanged(T value)
        {
            ValueChanged?.Invoke(value);
        }
    
        public virtual T Value
        {
            get => useConstant ? constantValue : variable.value;
            set
            {
                if (useConstant)
                {
                    if (constantValue.Equals(value)) return;
                    constantValue = value;
                    OnValueChanged(constantValue);
                }
                else
                {
                    if (variable.value.Equals(value)) return;
                    variable.value = value;
                    OnValueChanged(variable.value);
                }
            }
        }
    }
}
