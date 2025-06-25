namespace SouthHome.Backend.Common
{
    public class UserToken : TokenBase
    {

        public long UserId { get; protected set; }  // 用户Id

        public Role Role { get; protected set; }

        public static UserToken CreateInstance(long userId, Role role) => new(userId, role);

        public static UserToken CreateInstance(string token) => new(token);

        private UserToken(long UserId, Role role)
        {
            this.UserId = UserId;
            Claims[nameof(UserId)] = UserId.ToString();
            Claims[nameof(Role)] = role.ToString();
        }

        private UserToken(string token)
        {
            Token = token;
            UserId = Convert.ToInt64(Claims[nameof(UserId)]);
            Role = (Role)Enum.Parse(typeof(Role), Claims[nameof(Role)]);
        }
    }
}
