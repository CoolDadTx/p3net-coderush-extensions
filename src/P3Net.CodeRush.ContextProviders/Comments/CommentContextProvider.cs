using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using DevExpress.CodeAnalysis.Workspaces;
using DevExpress.CodeRush.Foundation.Contexts;
using DevExpress.CodeRush.Foundation.Parameters;
using DevExpress.CodeRush.Platform;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace P3Net.CodeRush.ContextProviders.Comments
{
    /// <summary>Provides the base implementation for context providers to detect comments.</summary>
    public abstract class CommentContextProvider : DefaultContextProvider
    {       
        protected SyntaxKind ForElement { get; set; }

        protected bool IsDocumentation { get; set; }

        public override string Language
        {
            get { return KnownLanguageNames.CSharp; }
        }

        public override string Category
        {
            get { return @"P3Net\Code"; }
        }

        public override async Task<bool> IsSatisfiedAsync ( IProviderContext context, ParameterCollection parameters )
        {
            try
            {
                var currentDocument = context.ActiveDocument.GetCodeAnalysisDocument();
                var root = currentDocument != null ? await currentDocument.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false) : null;
                CurrentNode = root?.FindNode(context.SelectionSpan, true);
                if (CurrentNode == null)
                    return false;

                if (IsDocumentation && !InDocumentationElement(CurrentNode)
                    || !IsDocumentation && !InCommentElement(CurrentNode))
                    return false;

                return IsCommentFor(ForElement);
            } finally
            {
                CurrentNode = null;
            };
        }

        #region Private Members

        //Determine if we are in a documentation comment
        private bool InDocumentationElement ( SyntaxNode node )
        {
            switch (node.Kind())
            {
                case SyntaxKind.XmlText:
                case SyntaxKind.XmlElement:
                case SyntaxKind.SingleLineDocumentationCommentTrivia:
                case SyntaxKind.MultiLineDocumentationCommentTrivia:
                return true;
            };

            return false;
        }

        //Determine if we are in a comment
        private bool InCommentElement ( SyntaxNode node )
        {
            var kind = node.Kind();
            return kind == SyntaxKind.SingleLineCommentTrivia || kind == SyntaxKind.MultiLineCommentTrivia;
        }

        //Determine if the current node is a comment for the given element
        private bool IsCommentFor ( SyntaxKind forElement )
        {
            try
            {
                //Look for the comment associated with element
                var current = CurrentNode;
                while (current != null)
                {
                    //If this is a comment or doc comment element then we've found the root
                    if (InDocumentationElement(current))
                        break;

                    current = current.Parent;
                };

                if (current == null)
                    return false;

                //Get the parent element
                var trivia = GetRootTrivia(current);

                //Get the parent associated with the trivia
                var parent = GetParentOfTrivia(trivia);

                return parent?.IsKind(ForElement) ?? false;
            } catch
            {
                return false;
            };
        }

        private SyntaxNode GetParentOfTrivia ( SyntaxNode node )
        {
            var parent = node.Parent;
            if (parent == null && (node is IStructuredTriviaSyntax structuredTrivia))
            {
                parent = structuredTrivia.ParentTrivia.Token.Parent;
            };

            return parent;
        }

        private SyntaxNode GetRootTrivia ( SyntaxNode node )
        {
            var root = node;
            while (root.Parent != null && (InDocumentationElement(root.Parent) || InCommentElement(root.Parent)))
                root = node.Parent;

            return root;
        }

        private SyntaxNode CurrentNode { get; set; }
        #endregion
    }
}
