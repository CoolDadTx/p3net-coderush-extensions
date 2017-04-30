using System;

using DevExpress.CodeRush.Foundation.Contexts;

using Microsoft.CodeAnalysis.CSharp;

namespace P3Net.CodeRush.ContextProviders.Comments
{
    /// <summary>Provides a context provider to detect doc comments on events.</summary>
    [ExportContextProvider]
    public class DocumentationForEventContextProvider : CommentContextProvider
    {      
        public DocumentationForEventContextProvider ()
        {
            IsDocumentation = true;
            ForElement = SyntaxKind.EventDeclaration;
        }

        public override string Name
        {
            get { return "InDocumentationForEvent"; }
        }
    }
}
