using AutoMapper;
using GSP.API.Core.Models.Application;
using GSP.API.Core.Models.Authentication;
using GSP.API.Core.Models.Broker;
using GSP.API.Core.Models.User;

namespace GSP.API.Application.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AuthenticationRequestModel, AuthenticationModel>();
            CreateMap<ChangeUserPasswordRequestModel, ChangeUserPasswordModel>();
            CreateMap<RecoverPasswordRequestModel, RecoverPasswordModel>();
            CreateMap<RegisterUserRequestModel, RegisterUserModel>();
            CreateMap<UserLogRequestModel, UserLogModel>();
            CreateMap<AddOrUpdateUserRequestModel, UserModel>();
            CreateMap<RecoverRegisterRequestModel, RecoverRegisterModel>();
            CreateMap<UserModel, UserResponseModel>();
            CreateMap<AddOrUpdateBrokerRequestModel, BrokerModel>();

        }
    }
}
