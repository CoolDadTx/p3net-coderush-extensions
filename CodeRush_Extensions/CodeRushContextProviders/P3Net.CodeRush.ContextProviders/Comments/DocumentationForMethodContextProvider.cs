using System;

using DevExpress.CodeRush.Foundation.Contexts;

using Microsoft.CodeAnalysis.CSharp;

namespace P3Net.CodeRush.ContextProviders.Comments
{
    /// <summary>Provides a context provider to detect doc comments on methods.</summary>
    [ExportContextProvider]
    public class DocumentationForMethodContextProvider : CommentContextProvider
    {      
        public DocumentationForMethodContextProvider ()
        {
            IsDocumentation = true;
            ForElement = SyntaxKind.MethodDeclaration;
        }

        public override string Name
        {
            get { return "InDocumentationForMethod"; }
        }
    }
}
