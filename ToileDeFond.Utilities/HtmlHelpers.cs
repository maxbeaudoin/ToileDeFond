﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ToileDeFond.Utilities
{
    public static partial class HtmlHelpers
    {
        public static MvcHtmlString MultiSelectList(this HtmlHelper htmlHelper, string name)
        {
            return DropDownListHelper(htmlHelper, name, null, null, null);
        }

        public static MvcHtmlString MultiSelectList(this HtmlHelper htmlHelper, string name,
                                                    IEnumerable<MultiSelectListItem> selectList)
        {
            return DropDownListHelper(htmlHelper, name, selectList, null, null);
        }

        public static MvcHtmlString MultiSelectList(this HtmlHelper htmlHelper, string name, string optionLabel)
        {
            return DropDownListHelper(htmlHelper, name, null, optionLabel, null);
        }

        public static MvcHtmlString MultiSelectList(this HtmlHelper htmlHelper, string name,
                                                    IEnumerable<MultiSelectListItem> selectList,
                                                    IDictionary<string, object> htmlAttributes)
        {
            return DropDownListHelper(htmlHelper, name, selectList, null, htmlAttributes);
        }

        public static MvcHtmlString MultiSelectList(this HtmlHelper htmlHelper, string name,
                                                    IEnumerable<MultiSelectListItem> selectList,
                                                    object htmlAttributes)
        {
            return DropDownListHelper(htmlHelper, name, selectList, null, new RouteValueDictionary(htmlAttributes));
        }

        public static MvcHtmlString MultiSelectList(this HtmlHelper htmlHelper, string name,
                                                    IEnumerable<MultiSelectListItem> selectList, string optionLabel)
        {
            return DropDownListHelper(htmlHelper, name, selectList, optionLabel, null);
        }

        public static MvcHtmlString MultiSelectList(this HtmlHelper htmlHelper, string name,
                                                    IEnumerable<MultiSelectListItem> selectList, string optionLabel,
                                                    IDictionary<string, object> htmlAttributes)
        {
            return DropDownListHelper(htmlHelper, name, selectList, optionLabel, htmlAttributes);
        }

        public static MvcHtmlString MultiSelectList(this HtmlHelper htmlHelper, string name,
                                                    IEnumerable<MultiSelectListItem> selectList, string optionLabel,
                                                    object htmlAttributes)
        {
            return DropDownListHelper(htmlHelper, name, selectList, optionLabel,
                                      new RouteValueDictionary(htmlAttributes));
        }

        public static MvcHtmlString MultiSelectListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
                                                                          Expression<Func<TModel, TProperty>>
                                                                              expression,
                                                                          IEnumerable<MultiSelectListItem>
                                                                              selectList)
        {
            return MultiSelectListFor(htmlHelper, expression, selectList, null /* optionLabel */, null
                /* htmlAttributes */);
        }

        public static MvcHtmlString MultiSelectListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
                                                                          Expression<Func<TModel, TProperty>>
                                                                              expression,
                                                                          IEnumerable<MultiSelectListItem>
                                                                              selectList, object htmlAttributes)
        {
            return MultiSelectListFor(htmlHelper, expression, selectList, null /* optionLabel */,
                                      new RouteValueDictionary(htmlAttributes));
        }

        public static MvcHtmlString MultiSelectListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
                                                                          Expression<Func<TModel, TProperty>>
                                                                              expression,
                                                                          IEnumerable<MultiSelectListItem>
                                                                              selectList,
                                                                          IDictionary<string, object> htmlAttributes)
        {
            return MultiSelectListFor(htmlHelper, expression, selectList, null /* optionLabel */, htmlAttributes);
        }

        public static MvcHtmlString MultiSelectListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
                                                                          Expression<Func<TModel, TProperty>>
                                                                              expression,
                                                                          IEnumerable<MultiSelectListItem>
                                                                              selectList, string optionLabel)
        {
            return MultiSelectListFor(htmlHelper, expression, selectList, optionLabel, null /* htmlAttributes */);
        }

        public static MvcHtmlString MultiSelectListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
                                                                          Expression<Func<TModel, TProperty>>
                                                                              expression,
                                                                          IEnumerable<MultiSelectListItem>
                                                                              selectList, string optionLabel,
                                                                          object htmlAttributes)
        {
            return MultiSelectListFor(htmlHelper, expression, selectList, optionLabel,
                                      new RouteValueDictionary(htmlAttributes));
        }

        public static MvcHtmlString MultiSelectListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
                                                                          Expression<Func<TModel, TProperty>>
                                                                              expression,
                                                                          IEnumerable<MultiSelectListItem>
                                                                              selectList, string optionLabel,
                                                                          IDictionary<string, object> htmlAttributes)
        {
            if (expression == null)
            {
                throw new ArgumentNullException("expression");
            }

            return DropDownListHelper(htmlHelper, ExpressionHelper.GetExpressionText(expression), selectList,
                                      optionLabel, htmlAttributes);
        }

        private static MvcHtmlString DropDownListHelper(HtmlHelper htmlHelper, string expression,
                                                        IEnumerable<MultiSelectListItem> selectList,
                                                        string optionLabel, IDictionary<string, object> htmlAttributes)
        {
            return SelectInternal(htmlHelper, optionLabel, expression, selectList, true /* allowMultiple */,
                                  htmlAttributes);
        }


        private static IEnumerable<MultiSelectListItem> GetSelectData(this HtmlHelper htmlHelper, string name)
        {
            object o = null;
            if (htmlHelper.ViewData != null)
            {
                o = htmlHelper.ViewData.Eval(name);
            }
            if (o == null)
            {
                throw new InvalidOperationException(
                    String.Format(
                        CultureInfo.CurrentCulture,
                        "Missing Select Data",
                        name,
                        "IEnumerable<MultiSelectListItem>"));
            }
            IEnumerable<MultiSelectListItem> selectList = o as IEnumerable<MultiSelectListItem>;
            if (selectList == null)
            {
                throw new InvalidOperationException(
                    String.Format(
                        CultureInfo.CurrentCulture,
                        "Wrong Select DataType",
                        name,
                        o.GetType().FullName,
                        "IEnumerable<MultiSelectListItem>"));
            }
            return selectList;
        }

        internal static string ListItemToOption(MultiSelectListItem item)
        {
            TagBuilder builder = new TagBuilder("option")
                {
                    InnerHtml = HttpUtility.HtmlEncode(item.Text)
                };
            if (item.Value != null)
            {
                builder.Attributes["value"] = item.Value;
            }
            if (item.Selected)
            {
                builder.Attributes["selected"] = "selected";
            }
            return builder.ToString(TagRenderMode.Normal);
        }

        private static MvcHtmlString SelectInternal(this HtmlHelper htmlHelper, string optionLabel, string name,
                                                    IEnumerable<MultiSelectListItem> selectList, bool allowMultiple,
                                                    IDictionary<string, object> htmlAttributes)
        {
            name = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);
            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Null Or Empty", "name");
            }

            bool usedViewData = false;

            // If we got a null selectList, try to use ViewData to get the list of items.
            if (selectList == null)
            {
                selectList = htmlHelper.GetSelectData(name);
                usedViewData = true;
            }

            object defaultValue = (allowMultiple)
                                      ? htmlHelper.GetModelStateValue(name, typeof (string[]))
                                      : htmlHelper.GetModelStateValue(name, typeof (string));

            // If we haven't already used ViewData to get the entire list of items then we need to
            // use the ViewData-supplied value before using the parameter-supplied value.
            if (!usedViewData)
            {
                if (defaultValue == null)
                {
                    defaultValue = htmlHelper.ViewData.Eval(name);
                }
            }

            if (defaultValue != null)
            {
                IEnumerable defaultValues = (allowMultiple) ? defaultValue as IEnumerable : new[] {defaultValue};
                IEnumerable<string> values = from object value in defaultValues
                                             select Convert.ToString(value, CultureInfo.CurrentCulture);
                HashSet<string> selectedValues = new HashSet<string>(values, StringComparer.OrdinalIgnoreCase);
                List<MultiSelectListItem> newSelectList = new List<MultiSelectListItem>();

                foreach (MultiSelectListItem item in selectList)
                {
                    item.Selected = (item.Value != null)
                                        ? selectedValues.Contains(item.Value)
                                        : selectedValues.Contains(item.Text);
                    newSelectList.Add(item);
                }
                selectList = newSelectList;
            }

            // Convert each ListItem to an <option> tag
            StringBuilder listItemBuilder = new StringBuilder();

            // Make optionLabel the first item that gets rendered.
            if (optionLabel != null)
            {
                listItemBuilder.AppendLine(
                    ListItemToOption(new MultiSelectListItem()
                        {
                            Text = optionLabel,
                            Value = String.Empty,
                            Selected = false
                        }));
            }

            foreach (var group in selectList.GroupBy(i => i.GroupKey))
            {
                string groupName =
                    selectList.Where(i => i.GroupKey == group.Key).Select(it => it.GroupName).FirstOrDefault();
                listItemBuilder.AppendLine(string.Format("<optgroup label=\"{0}\" value=\"{1}\">", groupName, group.Key));
                foreach (MultiSelectListItem item in group)
                {
                    listItemBuilder.AppendLine(ListItemToOption(item));
                }
                listItemBuilder.AppendLine("</optgroup>");
            }

            TagBuilder tagBuilder = new TagBuilder("select")
                {
                    InnerHtml = listItemBuilder.ToString()
                };
            tagBuilder.MergeAttributes(htmlAttributes);
            tagBuilder.MergeAttribute("name", name, true /* replaceExisting */);
            tagBuilder.GenerateId(name);
            if (allowMultiple)
            {
                tagBuilder.MergeAttribute("multiple", "multiple");
            }

            // If there are any errors for a named field, we add the css attribute.
            ModelState modelState;
            if (htmlHelper.ViewData.ModelState.TryGetValue(name, out modelState))
            {
                if (modelState.Errors.Count > 0)
                {
                    tagBuilder.AddCssClass(HtmlHelper.ValidationInputCssClassName);
                }
            }

            return MvcHtmlString.Create(tagBuilder.ToString());
        }

        internal static object GetModelStateValue(this HtmlHelper helper, string key, Type destinationType)
        {
            ModelState modelState;
            if (helper.ViewData.ModelState.TryGetValue(key, out modelState))
            {
                if (modelState.Value != null)
                {
                    return modelState.Value.ConvertTo(destinationType, null /* culture */);
                }
            }
            return null;
        }

    }
}