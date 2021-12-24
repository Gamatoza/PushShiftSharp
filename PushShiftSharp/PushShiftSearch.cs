using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PushShiftSharp
{
    public class PushShiftSearch
    {
        //TODO: Switch between possts and comments
        //now searching only for posts
        private string url = "https://api.pushshift.io/reddit/search/submission/?subreddit=";
        public PushShiftSearch(string reddit)
        {
            url += reddit;
        }
        public List<PushShiftResult> Search()
        {
            url = getFullUrl();
            Console.WriteLine($"pushshift-url: {url}");

            var client = new RestClient();
            var request = new RestRequest(url, Method.GET);
            try
            {
                var response = client.DownloadData(request) ?? throw new ArgumentNullException("", "response was null");
                var result = Encoding.UTF8.GetString(response).Trim().Replace(" ", "").Replace("\n", "").Replace("\t", "");
                if (!string.IsNullOrEmpty(result))
                {
                    var regex = new Regex(@"\[{*.*}]");
                    var dataJson = regex.Match(result).Value;
                    var list = JsonConvert.DeserializeObject<List<PushShiftResult>>(dataJson)
                                ?? throw new ArgumentNullException("", "data was null ");

                    for (int i = 0; i < list.Count; i++)
                    {
                        if (list[i].no_follow)
                            list.RemoveAt(i--);
                        else
                        if (_avoidURLsInText && list[i].isLink || list[i].selftext.ItHasURL())
                        {
                            list.RemoveAt(i--);
                        }
                        else
                        if (_avoidVideos && list[i].hasVideo)
                        {
                            list.RemoveAt(i--);
                        }
                    }

                    return list;
                }
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"Reddit not found \n{ex.Message}=>{ex.StackTrace}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(($"{ex.Message}=>{ex.StackTrace}"));
            }
            return null;
        }

        #region FetchSettings
        private bool _avoidURLsInText { get; set; } = true;
        private bool _avoidVideos { get; set; } = true;

        public PushShiftSearch AvoidURL(bool avoid)
        {
            _avoidURLsInText = avoid;
            return this;
        }
        #endregion
        #region SearchSettings

        private string _before { get; set; } = null;
        private string _after { get; set; } = null;
        private string _score { get; set; } = null;
        private string _limit { get; set; } = null;
        private string _author { get; set; } = null;
        private string _selftext { get; set; } = null;
        private string _avoidDeleted { get; set; } = null;
        private string _avoidVideosStr { get; set; } = "&is_video=false";
        private string _avoidNTFS { get; set; } = "&over_18=false";
        private string _title { get; set; } = null;
        private string getFullUrl()
        {
            url += _before ?? "";
            url += _after ?? "";
            url += _score ?? "";
            url += _limit ?? "";
            url += _author ?? "";
            url += _selftext ?? "";
            url += _avoidDeleted ?? "";
            url += _avoidVideosStr ?? "";
            url += _avoidNTFS ?? "";
            url += _title ?? "";
            return url;
        }

        public PushShiftSearch Before(long before)
        {
            _before = "&before=" + before;
            return this;
        }

        public PushShiftSearch After(long after)
        {
            _after = "&after=" + after;
            return this;
        }

        public PushShiftSearch ScoreMoreThan(int score)
        {
            _score = "&score>=" + score;
            return this;
        }

        public PushShiftSearch ScoreLessThan(int score)
        {
            _score = "&score<=" + score;
            return this;
        }

        public PushShiftSearch ScoreEquals(int score)
        {
            _score = "&score=" + score;
            return this;
        }

        public PushShiftSearch Limit(int limit = 25)
        {
            _limit = "&limit=" + limit;
            return this;
        }

        public PushShiftSearch Author(params string[] author)
        {
            _author = String.Join("&author=", author);
            return this;
        }

        public PushShiftSearch SelfText(params string[] selftext)
        {
            _selftext = String.Join("&selftext=", selftext);
            return this;
        }

        public PushShiftSearch Title(string title)
        {
            _title = "&title=" + title;
            return this;
        }

        #region Avoid
        public PushShiftSearch AvoidDeleted(bool avoid)
        {
            if (avoid)
                _avoidDeleted = "&author!=[deleted]&selftext:not=[deleted]";
            return this;
        }

        public PushShiftSearch AvoidVideos(bool avoid = true)
        {
            if (avoid)
            {
                _avoidVideosStr = "&is_video=false";
            }
            else
            {
                _avoidVideosStr = null;
            }
            _avoidVideos = avoid;
            return this;
        }
        public PushShiftSearch AvoidNTFS(bool avoid = true)
        {
            if (avoid)
            {
                _avoidNTFS = "&over_18=false";
            }
            else
            {
                _avoidNTFS = null;
            }
            return this;
        }

        #endregion

        #endregion
    }
}