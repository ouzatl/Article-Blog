using System.Runtime.Serialization;

namespace Article.Blog.Common.Enum.Comment
{
    public enum CommentServiceResponse
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
        CommentLengthTooLong = -60,

        [EnumMember]
        NameContainsNumericAndSpecialCharacters = 70,

        [EnumMember]
        TooMuchWhiteSpaceForNameField = 80,

        [EnumMember]
        TooMuchWhiteSpaceForSurameField = 81
    }
}
