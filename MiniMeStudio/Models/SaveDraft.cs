using System;
using System.Collections.Generic;
using System.Text;

namespace MiniMeStudio.Models
{
    public class SaveDraft
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        public class FontSizes
        {
            public int font_small { get; set; }
            public int font_normal { get; set; }
            public int font_medium { get; set; }
            public int font_large { get; set; }
        }

        public class Font
        {
            public string font_family { get; set; }
            public FontSizes font_sizes { get; set; }
        }

        public class BrandTheme
        {
            public string primary_color { get; set; }
            public string secondary_color { get; set; }
            public string color_black { get; set; }
            public string color_white { get; set; }
            public string color_light_grey { get; set; }
            public string color_dark_grey { get; set; }
            public string logo { get; set; }
            public Font font { get; set; }
        }

        public class Authentication
        {
            public string auth_url { get; set; }
            public string redirect_url { get; set; }
            public string client_id { get; set; }
            public string client_secrect { get; set; }
        }

        public class GlobalSettings
        {
            public BrandTheme brand_theme { get; set; }
            public Authentication authentication { get; set; }
        }

        public class MiniLayout
        {
            public string type { get; set; }
            public int rows { get; set; }
            public int columns { get; set; }
        }

        public class Position
        {
            public int row { get; set; }
            public int column { get; set; }
        }

        public class Alignment
        {
            public string vertical_alignment { get; set; }
            public string horizontal_alignment { get; set; }
        }

        public class Controls
        {
            public string name { get; set; }
            public string type { get; set; }
            public string mapped_control { get; set; }
            public string text { get; set; }
            //public Position position { get; set; }
            //public Alignment alignment { get; set; }
            //public List<string> Events { get; set; }
            public string placeholder { get; set; }
            public string value { get; set; }
        }

        public class MiniPage
        {
            public string page_name { get; set; }
            public string default_page { get; set; }
            //public MiniLayout mini_layout { get; set; }
            public List<Controls> controls { get; set; }
        }

        public class MiniMain
        {
            public string mini_name { get; set; }
            public string accountid { get; set; }
            public string version { get; set; }
            public GlobalSettings global_settings { get; set; }
            public List<MiniPage> mini_pages { get; set; }
        }


    }
}
