using System.Collections.Generic;
using System.Globalization;
using System.Linq.Expressions;

/*
 * Copyright 2011, Andrew L Barber
 * www.AndrewBarber.com
 * www.Facebook.com/InkwellData
 * 
 * Licensed for free use, with attribution when redistributing.
 * No warranty provided
 */

namespace System.Web.Mvc
{
    public static class CKEditorHelpers
    {
        /* The following values should be configured to your application/desires. */
        /// <summary>
        /// The CSS Class for CKEditor instances, for internal use by these helpers
        /// </summary>
        private const string CK_Ed_Class = "CkEditor";
        /// <summary>
        /// The virtual, rooted directory where CKEditor can be found. Should include trailing slash. eg /CKEditor/
        /// </summary>
        private const string CK_Ed_Location = "/Scripts/CkEditor/";
        /// <summary>
        /// The default rows of textarea/em height of CKEditor
        /// </summary>
        private const int DefaultTextAreaRows = 20;
        /// <summary>
        /// The default columns of textarea/em width of CKEditor
        /// </summary>
        private const int DefaultTextAreaColumns = 50;
        /* The above values should be configured to your application/desires. */


        #region Weak-Typed Helpers CKEditor()
        /// <summary>
        /// Create a weak-typed CKEditor instance. Must use CKEditorHeaderScripts to enable, and CKEditorSubmitButtonUpdateFunction for client-side validation.
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="name">Field name</param>
        /// <returns>MvcHtmlString output of CKEditor instance</returns>
        public static MvcHtmlString CKEditor(this HtmlHelper htmlHelper, string name)
        {
            return CKEditor(htmlHelper, name, String.Empty, String.Empty, htmlAttributesDict: null);
        }
        /// <summary>
        /// Create a weak-typed CKEditor instance. Must use CKEditorHeaderScripts to enable, and CKEditorSubmitButtonUpdateFunction for client-side validation.
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="name">Field name</param>
        /// <param name="value">Field value</param>
        /// <returns>MvcHtmlString output of CKEditor instance</returns>
        public static MvcHtmlString CKEditor(this HtmlHelper htmlHelper, string name, string value)
        {
            return CKEditor(htmlHelper, name, value, String.Empty, htmlAttributesDict: null);
        }
        /// <summary>
        /// Create a weak-typed CKEditor instance. Must use CKEditorHeaderScripts to enable, and CKEditorSubmitButtonUpdateFunction for client-side validation.
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="name">Field name</param>
        /// <param name="value">Field value</param>
        /// <param name="ckEditorConfig">CKEditor Javascript config string eg: {height:20em, width:30em}</param>
        /// <returns>MvcHtmlString output of CKEditor instance</returns>
        public static MvcHtmlString CKEditor(this HtmlHelper htmlHelper, string name, string value, string ckEditorConfig)
        {
            return CKEditor(htmlHelper, name, value, ckEditorConfig, htmlAttributesDict: null);
        }
        /// <summary>
        /// Create a weak-typed CKEditor instance. Must use CKEditorHeaderScripts to enable, and CKEditorSubmitButtonUpdateFunction for client-side validation.
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="name">Field name</param>
        /// <param name="value">Field value</param>
        /// <param name="ckEditorConfig">CKEditor Javascript config string eg: {height:20em, width:30em}</param>
        /// <param name="htmlAttributesObj">Html Attributes for textarea</param>
        /// <returns>MvcHtmlString output of CKEditor instance</returns>
        public static MvcHtmlString CKEditor(this HtmlHelper htmlHelper, string name, string value, string ckEditorConfig, object htmlAttributesObj)
        {
            return CKEditor(htmlHelper, name, value, ckEditorConfig, htmlAttributesDict: HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributesObj));
        }

        /// <summary>
        /// Create a weak-typed CKEditor instance. Must use CKEditorHeaderScripts to enable, and CKEditorSubmitButtonUpdateFunction for client-side validation.
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="name">Field name</param>
        /// <param name="value">Field value</param>
        /// <param name="ckEditorConfig">CKEditor Javascript config string eg: {height:20em, width:30em}</param>
        /// <param name="htmlAttributesDict">Html Attributes for textarea</param>
        /// <returns>MvcHtmlString output of CKEditor instance</returns>
        public static MvcHtmlString CKEditor(this HtmlHelper htmlHelper, string name, string value, string ckEditorConfig, IDictionary<string, object> htmlAttributesDict)
        {
            ModelMetadata metadata = ModelMetadata.FromStringExpression(name, htmlHelper.ViewContext.ViewData);
            if (value != null)
            {
                metadata.Model = value;
            }

            return CKEditorHelper(htmlHelper, metadata, name, implicitRowsAndColumns, htmlAttributesDict, ckEditorConfig);
        }

        /// <summary>
        /// Create a weak-typed CKEditor instance. Must use CKEditorHeaderScripts to enable, and CKEditorSubmitButtonUpdateFunction for client-side validation.
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="name">Field name</param>
        /// <param name="value">Field value</param>
        /// <param name="rows">Number of rows/em height</param>
        /// <param name="columns">Number of columns/em width</param>
        /// <param name="htmlAttributesObj">Html Attributes for textarea</param>
        /// <returns>MvcHtmlString output of CKEditor instance</returns>
        public static MvcHtmlString CKEditor(this HtmlHelper htmlHelper, string name, string value, int rows, int columns)
        {
            return CKEditor(htmlHelper, name, value, rows, columns, String.Empty, htmlAttributesDict: null);
        }
        /// <summary>
        /// Create a weak-typed CKEditor instance. Must use CKEditorHeaderScripts to enable, and CKEditorSubmitButtonUpdateFunction for client-side validation.
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="name">Field name</param>
        /// <param name="value">Field value</param>
        /// <param name="rows">Number of rows/em height</param>
        /// <param name="columns">Number of columns/em width</param>
        /// <param name="ckEditorConfig">CKEditor Javascript config string eg: {height:20em, width:30em} Overrides rows/columns values for CKEditor</param>
        /// <returns>MvcHtmlString output of CKEditor instance</returns>
        public static MvcHtmlString CKEditor(this HtmlHelper htmlHelper, string name, string value, int rows, int columns, string ckEditorConfig)
        {
            return CKEditor(htmlHelper, name, value, rows, columns, ckEditorConfig, htmlAttributesDict: null);
        }
        /// <summary>
        /// Create a weak-typed CKEditor instance. Must use CKEditorHeaderScripts to enable, and CKEditorSubmitButtonUpdateFunction for client-side validation.
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="name">Field name</param>
        /// <param name="value">Field value</param>
        /// <param name="rows">Number of rows/em height</param>
        /// <param name="columns">Number of columns/em width</param>
        /// <param name="ckEditorConfig">CKEditor Javascript config string eg: {height:20em, width:30em}</param>
        /// <param name="htmlAttributesObj">Html Attributes for textarea</param>
        /// <returns>MvcHtmlString output of CKEditor instance</returns>
        public static MvcHtmlString CKEditor(this HtmlHelper htmlHelper, string name, string value, int rows, int columns, string ckEditorConfig, object htmlAttributesObj)
        {
            return CKEditor(htmlHelper, name, value, rows, columns, ckEditorConfig, htmlAttributesDict: HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributesObj));
        }

        /// <summary>
        /// Create a weak-typed CKEditor instance. Must use CKEditorHeaderScripts to enable, and CKEditorSubmitButtonUpdateFunction for client-side validation.
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="name">Field name</param>
        /// <param name="value">Field value</param>
        /// <param name="rows">Number of rows/em height</param>
        /// <param name="columns">Number of columns/em width</param>
        /// <param name="ckEditorConfig">CKEditor Javascript config string eg: {height:20em, width:30em} Overrides rows/columns values for CKEditor</param>
        /// <param name="htmlAttributesDict">Html Attributes for textarea</param>
        /// <returns>MvcHtmlString output of CKEditor instance</returns>
        public static MvcHtmlString CKEditor(this HtmlHelper htmlHelper, string name, string value, int rows, int columns, string ckEditorConfig, IDictionary<string, object> htmlAttributesDict)
        {
            ModelMetadata metadata = ModelMetadata.FromStringExpression(name, htmlHelper.ViewContext.ViewData);
            if (value != null)
            {
                metadata.Model = value;
            }

            return CKEditorHelper(htmlHelper, metadata, name, GetRowsAndColumnsDictionary(rows, columns), htmlAttributesDict, ckEditorConfig);
        }

        #endregion

        #region Strong-Typed Helpers - CKEditorFor<TModel, TProperty>()
        /// <summary>
        /// Create a strong-typed CKEditor instance. Must use CKEditorHeaderScripts to enable, and CKEditorSubmitButtonUpdateFunction for client-side validation.
        /// </summary>
        /// <typeparam name="TModel">Model Class</typeparam>
        /// <typeparam name="TProperty">Property Value Type</typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression">Binding Expression</param>
        /// <returns>MvcHtmlString output of CKEditor instance</returns>
        public static MvcHtmlString CKEditorFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            return CKEditorFor(htmlHelper, expression, String.Empty, htmlAttributesDict: null);
        }
        /// <summary>
        /// Create a strong-typed CKEditor instance. Must use CKEditorHeaderScripts to enable, and CKEditorSubmitButtonUpdateFunction for client-side validation.
        /// </summary>
        /// <typeparam name="TModel">Model Class</typeparam>
        /// <typeparam name="TProperty">Property Value Type</typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression">Binding Expression</param>
        /// <param name="ckEditorConfig">CKEditor Javascript config string eg: {height:20em, width:30em} Overrides rows/columns values for CKEditor</param>
        /// <returns>MvcHtmlString output of CKEditor instance</returns>
        public static MvcHtmlString CKEditorFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string ckEditorConfig)
        {
            return CKEditorFor<TModel, TProperty>(htmlHelper, expression, ckEditorConfig, htmlAttributesDict: null);
        }
        /// <summary>
        /// Create a strong-typed CKEditor instance. Must use CKEditorHeaderScripts to enable, and CKEditorSubmitButtonUpdateFunction for client-side validation.
        /// </summary>
        /// <typeparam name="TModel">Model Class</typeparam>
        /// <typeparam name="TProperty">Property Value Type</typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression">Binding Expression</param>
        /// <param name="ckEditorConfig">CKEditor Javascript config string eg: {height:20em, width:30em} Overrides rows/columns values for CKEditor</param>
        /// <param name="htmlAttributesObj">Html Attributes for textarea</param>
        /// <returns>MvcHtmlString output of CKEditor instance</returns>
        public static MvcHtmlString CKEditorFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string ckEditorConfig, object htmlAttributesObj)
        {
            return CKEditorFor(htmlHelper, expression, ckEditorConfig, htmlAttributesDict: HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributesObj));
        }

        /// <summary>
        /// Create a strong-typed CKEditor instance. Must use CKEditorHeaderScripts to enable, and CKEditorSubmitButtonUpdateFunction for client-side validation.
        /// </summary>
        /// <typeparam name="TModel">Model Class</typeparam>
        /// <typeparam name="TProperty">Property Value Type</typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression">Binding Expression</param>
        /// <param name="ckEditorConfig">CKEditor Javascript config string eg: {height:20em, width:30em} Overrides rows/columns values for CKEditor</param>
        /// <param name="htmlAttributesDict">Html Attributes for textarea</param>
        /// <returns>MvcHtmlString output of CKEditor instance</returns>
        public static MvcHtmlString CKEditorFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string ckEditorConfig, IDictionary<string, object> htmlAttributesDict)
        {
            if (expression == null)
            {
                throw new ArgumentNullException("expression");
            }

            return CKEditorHelper(htmlHelper,
                                                        ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData),
                                                        ExpressionHelper.GetExpressionText(expression),
                                                        implicitRowsAndColumns,
                                                        htmlAttributesDict,
                                                        ckEditorConfig
                                                        );
        }

        /// <summary>
        /// Create a strong-typed CKEditor instance. Must use CKEditorHeaderScripts to enable, and CKEditorSubmitButtonUpdateFunction for client-side validation.
        /// </summary>
        /// <typeparam name="TModel">Model Class</typeparam>
        /// <typeparam name="TProperty">Property Value Type</typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression">Binding Expression</param>
        /// <param name="rows">Number of rows/em height</param>
        /// <param name="columns">Number of columns/em width</param>
        /// <returns>MvcHtmlString output of CKEditor instance</returns>
        public static MvcHtmlString CKEditorFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, int rows, int columns)
        {
            return CKEditorFor<TModel, TProperty>(htmlHelper, expression, rows, columns, String.Empty, htmlAttributesDict: null);
        }
        /// <summary>
        /// Create a strong-typed CKEditor instance. Must use CKEditorHeaderScripts to enable, and CKEditorSubmitButtonUpdateFunction for client-side validation.
        /// </summary>
        /// <typeparam name="TModel">Model Class</typeparam>
        /// <typeparam name="TProperty">Property Value Type</typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression">Binding Expression</param>
        /// <param name="rows">Number of rows/em height</param>
        /// <param name="columns">Number of columns/em width</param>
        /// <returns>MvcHtmlString output of CKEditor instance</returns>
        public static MvcHtmlString CKEditorFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, int rows, int columns, string ckEditorConfig)
        {
            return CKEditorFor<TModel, TProperty>(htmlHelper, expression, rows, columns, ckEditorConfig, htmlAttributesDict: null);
        }
        /// <summary>
        /// Create a strong-typed CKEditor instance. Must use CKEditorHeaderScripts to enable, and CKEditorSubmitButtonUpdateFunction for client-side validation.
        /// </summary>
        /// <typeparam name="TModel">Model Class</typeparam>
        /// <typeparam name="TProperty">Property Value Type</typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression">Binding Expression</param>
        /// <param name="rows">Number of rows/em height</param>
        /// <param name="columns">Number of columns/em width</param>
        /// <param name="ckEditorConfig">CKEditor Javascript config string eg: {height:20em, width:30em} Overrides rows/columns values for CKEditor</param>
        /// <param name="htmlAttributesObj">Html Attributes for textarea</param>
        /// <returns>MvcHtmlString output of CKEditor instance</returns>
        public static MvcHtmlString CKEditorFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, int rows, int columns, string ckEditorConfig, object htmlAttributesObj)
        {
            return CKEditorFor(htmlHelper, expression, rows, columns, ckEditorConfig, htmlAttributesDict: HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributesObj));
        }
        /// <summary>
        /// Create a strong-typed CKEditor instance. Must use CKEditorHeaderScripts to enable, and CKEditorSubmitButtonUpdateFunction for client-side validation.
        /// </summary>
        /// <typeparam name="TModel">Model Class</typeparam>
        /// <typeparam name="TProperty">Property Value Type</typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression">Binding Expression</param>
        /// <param name="rows">Number of rows/em height</param>
        /// <param name="columns">Number of columns/em width</param>
        /// <param name="ckEditorConfig">CKEditor Javascript config string eg: {height:20em, width:30em} Overrides rows/columns values for CKEditor</param>
        /// <param name="htmlAttributesDict">Html Attributes for textarea</param>
        /// <returns>MvcHtmlString output of CKEditor instance</returns>
        public static MvcHtmlString CKEditorFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, int rows, int columns, string ckEditorConfig, IDictionary<string, object> htmlAttributesDict)
        {
            if (expression == null)
            {
                throw new ArgumentNullException("expression");
            }

            return CKEditorHelper(htmlHelper,
                                                        ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData),
                                                        ExpressionHelper.GetExpressionText(expression),
                                                        GetRowsAndColumnsDictionary(rows, columns),
                                                        htmlAttributesDict,
                                                        ckEditorConfig
                                                        );
        }

        #endregion

        #region Related HTML Helpers
        /// <summary>
        /// Produces HTML to insert scripts needed to insert CKEditor instances. To be used once per page, toward the top of a view.
        /// </summary>
        public static MvcHtmlString CKEditorHeaderScripts(this HtmlHelper help)
        {
            return MvcHtmlString.Create(@"<script src=""" + CK_Ed_Location + @"ckeditor.js"" type=""text/javascript""></script>
<script src=""" + CK_Ed_Location + @"adapters/jquery.js"" type=""text/javascript""></script>
<script	type=""text/javascript""> function UpdateCKEditors() { $('." + CK_Ed_Class + @"').ckeditorGet().updateElement(); } </script>");
        }
        /// <summary>
        /// Outputs simple call to function that updates CKEditors before client-side validators are called.
        /// </summary>
        /// <example>Razor View: &lt;input type="text" value="submit" onclick="@Html.CKEditorSubmitButtonUpdateFunction()"/&gt;</example>
        /// <returns>MvcHtmlString literal: javascript:UpdateCKEditors()</returns>
        public static MvcHtmlString CKEditorSubmitButtonUpdateFunction(this HtmlHelper help)
        {
            return MvcHtmlString.Create("javascript:UpdateCKEditors()");
        }

        #endregion

        private static MvcHtmlString CKEditorHelper(HtmlHelper htmlHelper, ModelMetadata modelMetadata, string name, IDictionary<string, object> rowsAndColumns, IDictionary<string, object> htmlAttributes, string ckEditorConfigOptions)
        {
            string fullName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);
            if (String.IsNullOrEmpty(fullName))
            {
                throw new ArgumentException("string parameter is null or empty", "name");
            }

            TagBuilder textAreaBuilder = new TagBuilder("textarea");
            textAreaBuilder.GenerateId(fullName);
            textAreaBuilder.MergeAttributes(htmlAttributes, true);
            textAreaBuilder.MergeAttribute("name", fullName, true);
            string style = "";
            if (textAreaBuilder.Attributes.ContainsKey("style"))
            {
                style = textAreaBuilder.Attributes["style"];
            }
            if (string.IsNullOrWhiteSpace(style)) style = "";
            style += string.Format("height:{0}em; width:{1}em;", rowsAndColumns["rows"], rowsAndColumns["cols"]);
            textAreaBuilder.MergeAttribute("style", style, true);
            // If there are any errors for a named field, we add the CSS attribute.
            ModelState modelState;
            if (htmlHelper.ViewData.ModelState.TryGetValue(fullName, out modelState) && modelState.Errors.Count > 0)
            {
                textAreaBuilder.AddCssClass(HtmlHelper.ValidationInputCssClassName);
            }

            textAreaBuilder.MergeAttributes(htmlHelper.GetUnobtrusiveValidationAttributes(name));

            string value;
            if (modelState != null && modelState.Value != null)
            {
                value = modelState.Value.AttemptedValue;
            }
            else if (modelMetadata.Model != null)
            {
                value = modelMetadata.Model.ToString();
            }
            else
            {
                value = String.Empty;
            }

            // The first newline is always trimmed when a TextArea is rendered, so we add an extra one
            // in case the value being rendered is something like "\r\nHello".
            textAreaBuilder.SetInnerText(Environment.NewLine + value);

            TagBuilder scriptBuilder = new TagBuilder("script");
            scriptBuilder.MergeAttribute("type", "text/javascript");
            if (string.IsNullOrEmpty(ckEditorConfigOptions))
            {
                ckEditorConfigOptions = string.Format("{{ width:'{0}em', height:'{1}em' }}", rowsAndColumns["cols"], rowsAndColumns["rows"]);
            }
            if (!ckEditorConfigOptions.Trim().StartsWith("{")) ckEditorConfigOptions = "{" + ckEditorConfigOptions;
            if (!ckEditorConfigOptions.Trim().EndsWith("}")) ckEditorConfigOptions += "}";
            scriptBuilder.InnerHtml = string.Format(" $('#{0}').ckeditor({1}).addClass('{2}'); ",
                fullName,
                ckEditorConfigOptions,
                CK_Ed_Class
                );
            return MvcHtmlString.Create(textAreaBuilder.ToString() + "\n" + scriptBuilder.ToString());
        }

        private static Dictionary<string, object> implicitRowsAndColumns = new Dictionary<string, object> {
			{ "rows", DefaultTextAreaRows.ToString(CultureInfo.InvariantCulture) },
			{ "cols", DefaultTextAreaColumns.ToString(CultureInfo.InvariantCulture) },
    };

        private static Dictionary<string, object> GetRowsAndColumnsDictionary(int rows, int columns)
        {
            if (rows < 0)
            {
                throw new ArgumentOutOfRangeException("rows", "A text area parameter is out of range");
            }
            if (columns < 0)
            {
                throw new ArgumentOutOfRangeException("columns", "A text area parameter is out of range");
            }

            Dictionary<string, object> result = new Dictionary<string, object>();
            if (rows > 0)
            {
                result.Add("rows", rows.ToString(CultureInfo.InvariantCulture));
            }
            if (columns > 0)
            {
                result.Add("cols", columns.ToString(CultureInfo.InvariantCulture));
            }

            return result;
        }
    }
}