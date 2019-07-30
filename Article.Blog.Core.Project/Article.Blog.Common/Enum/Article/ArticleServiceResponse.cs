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
        AuthenticationRecordMustBeProvided = -10,

        [EnumMember]
        FormAuthenticationRecordRequired = -11,

        [EnumMember]
        ArticleAlreadyExists = -31,

        [EnumMember]
        ArticleLengthTooLong = -60,

        [EnumMember]
        TooMuchWhiteSpaceForNameField = 80,

        [EnumMember]
        TooMuchWhiteSpaceForSurameField = 81
    }
}