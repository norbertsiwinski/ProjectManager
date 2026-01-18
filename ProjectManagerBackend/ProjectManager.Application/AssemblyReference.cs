using System.Reflection;

namespace ProjectManager.Application;

internal static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}