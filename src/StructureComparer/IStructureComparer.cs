using System;
using StructureComparer.Models;

namespace StructureComparer
{
    public interface IStructureComparer
    {
        StructureComparisonResult Compare(Type baseType, Type toCompareType);
        StructureComparisonResult Compare<T1, T2>();
    }
}