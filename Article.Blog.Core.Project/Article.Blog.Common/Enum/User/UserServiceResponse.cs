using System.Runtime.Serialization;

namespace Article.Blog.Common.Enum.User
{
    public enum UserServiceResponse
    {

        [EnumMember]
        Exception = 0,

        [EnumMember]
        Success = 1,

        [EnumMember]
        NotFound = -1,

        [EnumMember]
        InvalidPassword = -3,

        [EnumMember]
        InvalidName = -4,

        [EnumMember]
        InvalidEmail = -5,

        [EnumMember]
        InvalidSurname = -6,

        [EnumMember]
        EmailAlreadyExists = -7,

        [EnumMember]
        NameLengthTooLong = -8,

        [EnumMember]
        SurnameLengthTooLong = -9,
    }
}