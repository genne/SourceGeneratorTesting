using System.Collections.Immutable;
using System.Linq.Expressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SourceGenerator;

[Generator]
public class GenerateExtensionMethodGenerator : ISourceGenerator
{
    public void Initialize(GeneratorInitializationContext context)
    {
    }

    public void Execute(GeneratorExecutionContext context)
    {
        foreach (var syntaxTree in context.Compilation.SyntaxTrees)
        {
            var semanticModel = context.Compilation.GetSemanticModel(syntaxTree);
            foreach (var classSymbol in syntaxTree
                         .GetRoot()
                         .DescendantNodes()
                         .OfType<ClassDeclarationSyntax>()
                         .Select(c => semanticModel.GetDeclaredSymbol(c))
                         .OfType<ITypeSymbol>())
            {
                context.AddSource($"{classSymbol.Name}.Extensions.g.cs",
                    GenerateExtensionClass());
            }
        }
    }

    private string GenerateExtensionClass()
    {
        return "";
        // return "fail";
    }
}
