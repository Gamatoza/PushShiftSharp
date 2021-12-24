# PushShiftSharp
Sharp lib for parsing pushshift

> Example to usage in tests if you need this

# How to use

```c#
PushShiftSearch search = new PushShiftSearch("some_subreddit");
var list = search
    .Search();
//And that's all, heh
```
### Methods after Search
~~.Before()~~
~~.After()~~

.ScoreMoreThan|.ScoreLessThan|.ScoreEquals = search by karma on post

.Author          = search by author

.SelfText        = search by inner text (contains)
.Title           = search by title (contains)

.AvoidDeleted    = avoid deleted posts 
.AvoidVideos     = avoid videos in post (set as default)
.AvoidNSFW       = avoid NSFW and SFW posts


