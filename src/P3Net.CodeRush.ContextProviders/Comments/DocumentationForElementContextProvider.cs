using System;
using System.ComponentModel;
using System.Threading.Tasks;

using DevExpress.CodeAnalysis.Workspaces;
using DevExpress.CodeRush.Foundation.Contexts;
using DevExpress.CodeRush.Foundation.Parameters;

using Microsoft.CodeAnalysis.CSharp;

namespace P3Net.CodeRush.ContextProviders.Comments
{
    /// <summary>Provides a context provider to detect doc comments on elements.</summary>
    [ExportContextProvider]
    public class DocumentationForElementContextProvider : CommentContextProvider
    {      
        public DocumentationForElementContextProvider ( )
        {
            IsDocumentation = true;
        }

        public override string Name
        {
            get { return "InDocumentationForElement"; }
        }

        public override async Task<bool> IsSatisfiedAsync ( IProviderContext context, ParameterCollection parameters )
        {
            ForElement = GetElementParameter(parameters);

            return await base.IsSatisfiedAsync(context, parameters);
        }

        #region Private Members

        private SyntaxKind GetElementParameter ( ParameterCollection parameters )
        {
            //Split by commas
            var tokens = parameters.All?.Split(',');
            if (tokens.Length > 0)
            {
                //First parameter is the kind of element
                var converter = TypeDescriptor.GetConverter(typeof(SyntaxKind));

                return (SyntaxKind)converter.ConvertFromString(tokens[0]);
            };

            return SyntaxKind.None;
        }
        #endregion
    }
}
