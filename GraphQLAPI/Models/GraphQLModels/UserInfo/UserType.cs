using GraphQL.Types;

namespace UserInfoGraphQL.Types
{
    public abstract class UserInfoClass
    {
        public int UserID { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string MobileNo { get; set; } = string.Empty;
    }

    public class UserInfo : UserInfoClass
    {
        public string CitizenID { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public int LineCommand { get; set; }
        public List<MemberAccount> MemberAccounts { get; set; } = [];
    }

    public class MemberAccount
    {
        public string AccountID { get; set; } = string.Empty;
        public string AccountName { get; set; } = string.Empty;
        public Guid UUID { get; set; }
        public List<BalanceYear> BalanceYears { get; set; } = [];
    }

    public class BalanceYear
    {
        public string BalanceYearID { get; set; } = string.Empty;
        public string Year { get; set; } = string.Empty;
        public decimal Balance { get; set; }
    }

    public class Colleague : UserInfo
    {
        public List<UserInfo> Colleagues { get; set; } = [];
    }

    public class UserType : ObjectGraphType<UserInfo>
    {
        public UserType(UserInfoData data)
        {
            Name = "UserInformation";
            Description = "User Information";

            Field(u => u.UserID).Description("User ID");
            Field(u => u.UserName).Description("User name");
            Field(u => u.Password).Description("Password");
            Field(u => u.Email).Description("Email");
            Field(u => u.MobileNo).Description("Mobile No.");
            Field(u => u.CitizenID).Description("Citizen ID");
            Field(u => u.Address).Description("Address");
            Field("lineOfCommand", u => data.GetLineofCommandByID(data, u.LineCommand), nullable: true);
            Field<ListGraphType<MemberAccountType>>("memberAccount")
                .ResolveAsync(async context => await data.GetUserAccounts(context.Source));

            Interface<UserInfoInterface>();
        }
    }

    public class MemberAccountType : ObjectGraphType<MemberAccount>
    {
        public MemberAccountType(UserInfoData data)
        {
            Name = "MemberAccount";
            Description = "Member Account";
            Field(acc => acc.AccountID).Description("Account ID");
            Field(acc => acc.AccountName).Description("Account Name");
            Field(acc => acc.UUID).Description("Account identity");
            Field<ListGraphType<BalanceYearType>>("balanceYears")
                .ResolveAsync(async context => await data.GetBalanceYears(context.Source));
        }
    }

    public class BalanceYearType : ObjectGraphType<BalanceYear>
    {
        public BalanceYearType()
        {
            Name = "BalanceYears";
            Description = "Balance each year";
            Field(bl => bl.BalanceYearID).Description("Balance");
            Field(bl => bl.Year).Description("Year");
            Field(bl => bl.Balance).Description("Balance");
        }
    }

    public class UserInfoInterface : InterfaceGraphType<UserInfoClass>
    {
        public UserInfoInterface()
        {
            Name = "UserInfo";
            Description = "User Info";

            Field(u => u.UserID).Description("User ID");
            Field(u => u.UserName).Description("User name");
            Field(u => u.Password).Description("Password");
            Field(u => u.Email).Description("Email");
            Field(u => u.MobileNo).Description("Mobile No.");
        }
    }
}