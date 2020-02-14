using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

public static class UploadHelper
{
    public static MvcHtmlString Upload(this HtmlHelper helper, string name, object htmlAttributes = null)
    {
        //helper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(ExpressionHelper.GetExpressionText(expression))
        TagBuilder input = new TagBuilder("input");
        input.Attributes.Add("type", "file");
        input.Attributes.Add("id", helper.ViewData.TemplateInfo.GetFullHtmlFieldId(name));
        input.Attributes.Add("name", helper.ViewData.TemplateInfo.GetFullHtmlFieldName(name));

        if (htmlAttributes != null)
        {
            var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            input.MergeAttributes(attributes);
        }

        return new MvcHtmlString(input.ToString());
    }

    public static MvcHtmlString UploadFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression, object htmlAttributes = null)
    {
        //helper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(ExpressionHelper.GetExpressionText(expression))
        var data = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);
        TagBuilder input = new TagBuilder("input");
        input.Attributes.Add("type", "file");
        input.Attributes.Add("id", helper.ViewData.TemplateInfo.GetFullHtmlFieldId(ExpressionHelper.GetExpressionText(expression)));
        input.Attributes.Add("name", helper.ViewData.TemplateInfo.GetFullHtmlFieldName(ExpressionHelper.GetExpressionText(expression)));

        if (htmlAttributes != null)
        {
            var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            input.MergeAttributes(attributes);
        }

        return new MvcHtmlString(input.ToString());
    }
}