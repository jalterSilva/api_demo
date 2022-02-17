using GSP.API.Core.Models.System;

namespace GSP.API.Core.Models.Authentication
{
    public class TokenResponseModel
    {
        #region Properties
        public string AccessToken { get; set; }
        public string TokenType { get; set; }
        public string JwtRefreshToken { get; set; }
        public ErrorModel Error { get; set; }
        #endregion

        #region Constructor
        public TokenResponseModel(string accessToken, string tokenType, string jwtRefreshToken)
        {
            AccessToken = accessToken;
            TokenType = tokenType;
            JwtRefreshToken = jwtRefreshToken;
            Error = null;
        }
        public TokenResponseModel(ErrorModel error)
        {
            Error = error;
            AccessToken = null;
            TokenType = null;
            JwtRefreshToken = null;
        }
        public TokenResponseModel()
        {

        }
        #endregion
    }
}
