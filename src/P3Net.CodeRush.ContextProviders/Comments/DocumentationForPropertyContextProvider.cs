using System;

using DevExpress.CodeRush.Foundation.Contexts;

using Microsoft.CodeAnalysis.CSharp;

namespace P3Net.CodeRush.ContextProviders.Comments
{
    /// <summary>Provides a context provider to detect doc comments on properties.</summary>
    [ExportContextProvider]
    public class DocumentationForPropertyContextProvider : CommentContextProvider
    {      
        public DocumentationForPropertyContextProvider ()
        {
            IsDocumentation = true;
            ForElement = SyntaxKind.PropertyDeclaration;
        }

        public override string Name
        {
            get { return "InDocumentationForProperty"; }
        }
    }
}
