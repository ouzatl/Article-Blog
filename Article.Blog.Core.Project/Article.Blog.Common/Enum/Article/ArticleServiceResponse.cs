using System.Runtime.Serialization;

namespace Article.Blog.Common.Enum.Article
{
    public enum ArticleServiceResponse
    {
        [EnumMember]
        Exception = 0,

        [EnumMember]
        Success = 1,

        [EnumMember]
        NotFound = -1,

        [EnumMember]
        ArticleTitleAlreadyExists = 2,

        [EnumMember]
        ArticleTitleLengthTooLong = 3,

        [EnumMember]
        ArticleLengthTooShort = -3,

        [EnumMember]
        ArticleSubTitleLengthTooLong = 4,

        [EnumMember]
        ArticleSubLengthTooShort = -4,

        [EnumMember]
        ArticleDescriptionLengthTooLong = 5,

        [EnumMember]
        ArticleDescriptionLengthTooShort = -5,
    }
}