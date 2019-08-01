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
        CommentLengthTooLong = 11,

        [EnumMember]
        CommentLengthTooShort = 12,
    }
}
