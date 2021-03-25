using Microsoft.AspNetCore.Razor.TagHelpers;
using Proje.ToDo.Business.Interfaces;
using Proje.ToDo.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proje.ToDo.Web.TagHelpers
{
    [HtmlTargetElement("getirGorevAppUserId")]
    public class GorevAppUserIdHelper : TagHelper
    {
        private readonly IGorevService _gorevService;
        public GorevAppUserIdHelper(IGorevService gorevService)
        {
            _gorevService = gorevService;
        }
        public int AppUserId { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            List<Gorev> gorevler = _gorevService.GetirileAppUserId(AppUserId);
            int tamamlananGorevSayisi = gorevler.Where(I => I.Durum).Count();
            int ustundeCalistigiGorevSayisi = gorevler.Where(I => !I.Durum).Count();

            string htmlString = $"<strong> Tamamladığı görev sayısı:</strong> {tamamlananGorevSayisi} " +
                $"<br> <strong> Üstünde çalıştığı görev sayisi:</strong> {ustundeCalistigiGorevSayisi}";

            output.Content.SetHtmlContent(htmlString);
        }
    }
}
