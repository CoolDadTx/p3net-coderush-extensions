using System;

using DevExpress.CodeRush.Foundation.Contexts;

using Microsoft.CodeAnalysis.CSharp;

namespace P3Net.CodeRush.ContextProviders.Comments
{
    /// <summary>Provides a context provider to detect doc comments on interfaces.</summary>
    [ExportContextProvider]
    public class DocumentationForInterfaceContextProvider : CommentContextProvider
    {      
        public DocumentationForInterfaceContextProvider ()
        {
            IsDocumentation = true;
            ForElement = SyntaxKind.InterfaceDeclaration;
        }

        public override string Name
        {
            get { return "InDocumentationForInterface"; }
        }
    }
}
