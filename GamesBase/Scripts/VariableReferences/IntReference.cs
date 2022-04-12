namespace DejaMoo.VariableReferences
{
    [System.Serializable]
    public class IntReference : VariableReference<int>
    {
        public override int Value
        {
            get => useConstant ? constantValue : variable.value;
            set
            {
                if (useConstant)
                {
                    if (constantValue == value) return;
                    constantValue = value;
                    OnValueChanged(constantValue);
                }
                else
                {
                    if (variable.value == value) return;
                    variable.value = value;
                    OnValueChanged(variable.value);
                }
            }
        }
    }
}