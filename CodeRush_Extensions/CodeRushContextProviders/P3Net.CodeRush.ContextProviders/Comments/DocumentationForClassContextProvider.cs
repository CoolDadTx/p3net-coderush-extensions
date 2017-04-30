using System;

using DevExpress.CodeRush.Foundation.Contexts;

using Microsoft.CodeAnalysis.CSharp;

namespace P3Net.CodeRush.ContextProviders.Comments
{
    /// <summary>Provides a context provider to detect doc comments on classes.</summary>
    [ExportContextProvider]
    public class DocumentationForClassContextProvider : CommentContextProvider
    {      
        public DocumentationForClassContextProvider ()
        {
            IsDocumentation = true;
            ForElement = SyntaxKind.ClassDeclaration;
        }

        public override string Name
        {
            get { return "InDocumentationForClass"; }
        }
    }
}
