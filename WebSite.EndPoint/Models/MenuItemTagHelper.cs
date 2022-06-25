using Application.Catalogs.GetMenuItem;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Collections.Generic;
using System.Linq;

namespace WebSite.EndPoint.Models
{
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    [HtmlTargetElement("menu-item")]
    public class MenuItemTagHelper : TagHelper
    {
        private readonly IGetMenuItemService _getMenuService;
        public MenuItemTagHelper(IGetMenuItemService getMenuService)
        {
            _getMenuService = getMenuService;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "ul";
            output.Content.AppendHtml(GetContent());
        }

        private TagBuilder GetContent()
        {
            var data = _getMenuService.Execute();
            TagBuilder mainLi = null;

            foreach (var mainMenu in data.Where(m => m.ParentId is null))
            {
                if (mainLi is null)
                {
                    mainLi = CreateLi(mainMenu.Name, "");
                }
                else
                {
                    mainLi.InnerHtml.AppendHtml(CreateLi(mainMenu.Name, ""));
                }
                var ul = new TagBuilder("ul");
                ul.AddCssClass("row");
                var allLi = CreateLi($"همه دسته بندی های {mainMenu.Name}", "#");
                allLi.AddCssClass("col-12");
                ul.InnerHtml.AppendHtml(allLi);

                TagBuilder liCol_1 = CreateLi();
                liCol_1.AddCssClass("col-3");
                TagBuilder liCol_2 = CreateLi();
                liCol_2.AddCssClass("col-3");
                TagBuilder liCol_3 = CreateLi();
                liCol_3.AddCssClass("col-3");
                TagBuilder liCol_4 = CreateLi();
                liCol_4.AddCssClass("col-3");

                int index = 0;

                foreach (var sub1 in data.Where(m => m.ParentId == mainMenu.Id))
                {
                    var link = $"/product?CatalogTypeId={sub1.Id}";
                    if (index < 13)
                    {
                        var a = new TagBuilder("a");
                        a.MergeAttribute("href", link);
                        a.InnerHtml.Append(sub1.Name);
                        liCol_1.InnerHtml.AppendHtml(a);
                        liCol_1.InnerHtml.AppendHtml(CreateSub(data, sub1, index, out index));
                        index++;
                    }
                    else if (index < 26)
                    {
                        var a = new TagBuilder("a");
                        a.MergeAttribute("href", link);
                        a.InnerHtml.Append(sub1.Name);
                        liCol_2.InnerHtml.AppendHtml(a);
                        liCol_2.InnerHtml.AppendHtml(CreateSub(data, sub1, index, out index));
                        index++;
                    }
                    else if (index < 39)
                    {
                        var a = new TagBuilder("a");
                        a.MergeAttribute("href", link);
                        a.InnerHtml.Append(sub1.Name);
                        liCol_3.InnerHtml.AppendHtml(a);
                        liCol_3.InnerHtml.AppendHtml(CreateSub(data, sub1, index, out index));
                        index++;
                    }
                    else
                    {
                        var a = new TagBuilder("a");
                        a.MergeAttribute("href", link);
                        a.InnerHtml.Append(sub1.Name);
                        liCol_4.InnerHtml.AppendHtml(a);
                        liCol_4.InnerHtml.AppendHtml(CreateSub(data, sub1, index, out index));
                        index++;
                    }
                }

                ul.InnerHtml.AppendHtml(liCol_1);
                ul.InnerHtml.AppendHtml(liCol_2);
                ul.InnerHtml.AppendHtml(liCol_3);
                ul.InnerHtml.AppendHtml(liCol_4);

                mainLi.InnerHtml.AppendHtml(ul);
            }

            return mainLi;
        }

        private TagBuilder CreateLi(string text, string link)
        {
            var a = new TagBuilder("a");
            a.MergeAttribute("href", $"{link}");
            a.MergeAttribute("title", text);
            a.InnerHtml.Append(text);

            var li = new TagBuilder("li");
            li.InnerHtml.AppendHtml(a);

            return li;
        }

        private TagBuilder CreateLi()
        {
            var li = new TagBuilder("li");
            return li;
        }

        private TagBuilder CreateSub(List<MenuItemDto> data, MenuItemDto sub1, int count, out int IndexCount)
        {
            IndexCount = count;
            var ulSub2 = new TagBuilder("ul");
            foreach (var sub2 in data.Where(s => s.ParentId == sub1.Id))
            {
                ulSub2.InnerHtml.AppendHtml(CreateLi(sub2.Name, $"/product?CatalogTypeId={sub2.Id}"));
                IndexCount++;
            }
            return ulSub2;
        }
    }
}
