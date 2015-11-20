using System;

namespace StructureComparer.Validators.Flows.Factories
{
    internal interface IValidationFlowFactory
    {
        IValidationFlow Create(Type type);
    }
}