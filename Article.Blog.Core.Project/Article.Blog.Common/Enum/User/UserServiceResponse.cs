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
        AuthenticationRecordMustBeProvided = -10,

        [EnumMember]
        FormAuthenticationRecordRequired = -11,

        [EnumMember]
        InvalidPassword = -12,

        [EnumMember]
        InvalidName = -20,

        [EnumMember]
        InvalidSurname = -21,

        [EnumMember]
        InvalidEmailAddress = -30,

        [EnumMember]
        EmailAlreadyExists = -31,

        [EnumMember]
        InvalidPhone = -40,

        [EnumMember]
        PhoneAlreadyExists = -41,

        [EnumMember]
        InvalidSex = -50,

        [EnumMember]
        NameLengthTooLong = -60,

        [EnumMember]
        SurnameLengthTooLong = -61,

        [EnumMember]
        NameContainsNumericAndSpecialCharacters = 70,

        [EnumMember]
        SurnameContainsNumericAndSpecialCharacters = 71,

        [EnumMember]
        TooMuchWhiteSpaceForNameField = 80,

        [EnumMember]
        TooMuchWhiteSpaceForSurameField = 81
    }
}