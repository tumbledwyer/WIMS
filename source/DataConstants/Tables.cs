namespace WhereIsMyShit.DataConstants
{
    public class Tables
    {
        public class LoanItem
        {
            public const string NAME = "LoanItems";

            public class Columns
            {
                public const string ID = "Id";
                public const string NAME = "Name";
            }
        }


        public class Borrower
        {
            public const string NAME = "Borrowers";

            public class Columns
            {
                public const string ID = "Id";
                public const string NAME = "Name";
                public const string SURNAME = "Surname";
                public const string EMAIL = "Email";
                public const string PHONENUMBER = "PhoneNumber";
                public const string PHOTO = "Photo";
            }
        }
    }
}
