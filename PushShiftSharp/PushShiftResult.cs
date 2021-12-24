using System;
using System.Collections.Generic;
using System.Text;

namespace PushShiftSharp
{
    public class PushShiftResult
    {
        public string selftext { get; set; }
        public string full_link { get; set; }
        public string score { get; set; }
        public string url { get; set; }
        public string post_hint { get; set; }
        public int num_comments { get; set; }
        public bool no_follow { get; set; }
        public bool hasVideo
        {
            get
            {
                if (post_hint != null)
                    return post_hint.Contains("video");
                return false;
            }
        }
        public bool isLink
        {
            get
            {
                if (post_hint != null)
                    return post_hint.Contains("link");
                return false;
            }
        }



    }
}
