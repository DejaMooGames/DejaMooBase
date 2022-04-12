using System;
using DejaMoo.Extensions;

namespace DejaMoo.VariableReferences
{
    [Serializable]
    public class FloatReference : VariableReference<float>
    {
        public override float Value
        {
            get => useConstant ? constantValue : variable.value;
            set
            {
                if (useConstant)
                {
                    if (constantValue.Approx(value, float.Epsilon)) return;
                    constantValue = value;
                    OnValueChanged(constantValue);
                }
                else
                {
                    if (variable.value.Approx(value, float.Epsilon)) return;
                    variable.value = value;
                    OnValueChanged(variable.value);
                }
            }
        }
    }
}
