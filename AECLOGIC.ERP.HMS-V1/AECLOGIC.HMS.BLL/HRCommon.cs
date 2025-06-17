using System;
using System.Data;
using System.Configuration;
using System.Web;


/// <summary>
/// Summary description for HRCommon
/// </summary>
/// 
namespace AECLOGIC.HMS.BLL
{
    public class HRCommon
    {
        public HRCommon()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        #region PrivateSection

        private int _PosID = 0;
        private string _Position = "";
        private double _DeptID = 0;
        private string _Description = "";
        private int _Posts = 0;
        private DateTime? _FromDate;
        private DateTime? _ToDate;
        private string _Timings;
        private string _Qualifications;
        private int _InterviewTypeID;
        private bool _Status;
        private string _AppStatus;
        private string _Remarks;


        private double _Salary;
        private DateTime? _ReqDate;
        private DateTime? _AccDated;
        private string _Designation;
        private string _OfferLetter;
        private string _ImageType;
        private double _Trade;

        private int _AppID;
        private string _FName;
        private string _MName;
        private string _LName;
        private string _Email;
        private string _Mobile;
        private string _Phone;
        private DateTime? _DOB;
        private string _Father;
        private string _Passport;
        private DateTime? _PDOI;
        private string _PDOIPlace;
        private DateTime? _PDOE;
        private byte _MaritalSta;
        private double _CurrentCTC;
        private double _ExpectedCT;
        private string _Resume;
        private int _JVTransID;
        private int _GID;


        public int JVTransID
        {
            get { return _JVTransID; }
            set { _JVTransID = value; }
        }

        public int GID
        {
            get { return _GID; }
            set { _GID = value; }
        }



        public double Trade
        {
            get { return _Trade; }
            set { _Trade = value; }
        }

        private int _Month;

        public int Month
        {
            get { return _Month; }
            set { _Month = value; }
        }
        private int _Year;

        public int Year
        {
            get { return _Year; }
            set { _Year = value; }
        }
        private int _DocID;

        public int DocID
        {
            get { return _DocID; }
            set { _DocID = value; }
        }
        private string _DocName;

        public string DocName
        {
            get { return _DocName; }
            set { _DocName = value; }
        }
        private string _DocText;

        public string DocText
        {
            get { return _DocText; }
            set { _DocText = value; }
        }

        private string _Duties;

        public string Duties
        {
            get { return _Duties; }
            set { _Duties = value; }
        }
        private string _Achievemets;

        public string Achievemets
        {
            get { return _Achievemets; }
            set { _Achievemets = value; }
        }

        private int _AchievmentID;

        public int AchievmentID
        {
            get { return _AchievmentID; }
            set { _AchievmentID = value; }
        }
        private int _TradeID;
        public int TradeID
        {
            get { return _TradeID; }
            set { _TradeID = value; }
        }
        private string _Qualification;
        private string _Institute;
        private int _YOP;
        private string _Specialization;
        private double _Percentage;
        private string _Mode;

        private string _Organization;
        private string _City;
        private string _Type;
        private int _Ranking;

        public int Ranking
        {
            get { return _Ranking; }
            set { _Ranking = value; }
        }

        private string _Address;

        private string _State;
        private String _Country;
        private int _Pin;
        private int _CountryID;

        private int _UserID;
        private int _SiteID;

        private double _EmpID;
        private int _SubResID;
        public int SubResID
        {
            get { return _SubResID; }
            set { _SubResID = value; }
        }

        private double _BasicCost;
        public double BasicCost
        {
            get { return _BasicCost; }
            set { _BasicCost = value; }
        }
        private string _Life;
        public string Life
        {
            get { return _Life; }
            set { _Life = value; }
        }
        private string _OutPut;
        public string OutPut
        {
            get { return _OutPut; }
            set { _OutPut = value; }
        }
        private string _LPH;
        public string LPH
        {
            get { return _LPH; }
            set { _LPH = value; }
        }


        public double EmpID
        {
            get { return _EmpID; }
            set { _EmpID = value; }
        }

        private int _RoleID;
        public int RoleID
        {
            get { return _RoleID; }
            set { _RoleID = value; }
        }

        private string _RoleName;

        public string RoleName
        {
            get { return _RoleName; }
            set { _RoleName = value; }
        }

        private string _OldEmpID;

        public string OldEmpID
        {
            get { return _OldEmpID; }
            set { _OldEmpID = value; }
        }
        string _OldEmployeeID = null;

        public string OldEmployeeID
        {
            get { return _OldEmployeeID; }
            set { _OldEmployeeID = value; }
        }

        private string _SiteName;

        public string SiteName
        {
            get { return _SiteName; }
            set { _SiteName = value; }
        }

        private string _OldPassWord;

        public string OldPassWord
        {
            get { return _OldPassWord; }
            set { _OldPassWord = value; }
        }

        private string _NewPassWord;

        private int _IsActive;

        public int IsActive
        {
            get { return _IsActive; }
            set { _IsActive = value; }
        }
        public string NewPassWord
        {
            get { return _NewPassWord; }
            set { _NewPassWord = value; }
        }

        private string _MasterPageName;

        public string MasterPageName
        {
            get { return _MasterPageName; }
            set { _MasterPageName = value; }
        }

        private bool _Add;

        public bool Add
        {
            get { return _Add; }
            set { _Add = value; }
        }

        private int _PageSize;
        private int _CurrentPage;
        private int _NoofRecords;
        private int _TotalPages;

        public int TotalPages
        {
            get { return _TotalPages; }
            set { _TotalPages = value; }
        }

        public int NoofRecords
        {
            get { return _NoofRecords; }
            set { _NoofRecords = value; }
        }

        public int CurrentPage
        {
            get { return _CurrentPage; }
            set { _CurrentPage = value; }
        }
        private int _LID;

        public int LID
        {
            get { return _LID; }
            set { _LID = value; }
        }
        public int PageSize
        {
            get { return _PageSize; }
            set { _PageSize = value; }
        }

        private bool _Edit;
        public bool Edit
        {
            get { return _Edit; }
            set { _Edit = value; }
        }

        private bool _Del;

        public bool Del
        {
            get { return _Del; }
            set { _Del = value; }
        }
        private bool _View;

        public bool View
        {
            get { return _View; }
            set { _View = value; }
        }

       

        private double _CTC;

        private string _ext;
        public string Ext
        {
            get { return _ext; }
            set { _ext = value; }
        }

        public int SiteID
        {
            get { return _SiteID; }
            set { _SiteID = value; }
        }

        public bool Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        public string ImageType
        {
            get { return _ImageType; }
            set { _ImageType = value; }
        }

        public int UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }
        public string Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        public string AppStatus
        {
            get { return _AppStatus; }
            set { _AppStatus = value; }
        }
        public string Timings
        {
            get { return _Timings; }
            set { _Timings = value; }
        }
        public int PosID
        {
            get { return _PosID; }
            set { _PosID = value; }
        }

        public string Position
        {
            get { return _Position; }
            set { _Position = value; }
        }

        public double DeptID
        {
            get { return _DeptID; }
            set { _DeptID = value; }
        }

        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        public int Posts
        {
            get { return _Posts; }
            set { _Posts = value; }
        }

        public DateTime? FromDate
        {
            get { return _FromDate; }
            set { _FromDate = value; }
        }

        public DateTime? ToDate
        {
            get { return _ToDate; }
            set { _ToDate = value; }
        }

       
        public int InterviewTypeID
        {
            get { return _InterviewTypeID; }
            set { _InterviewTypeID = value; }
        }

        public int AppID
        {
            get { return _AppID; }
            set { _AppID = value; }
        }

        public string FName
        {
            get { return _FName; }
            set { _FName = value; }
        }

        public string MName
        {
            get { return _MName; }
            set { _MName = value; }
        }

        public string LName
        {
            get { return _LName; }
            set { _LName = value; }
        }

        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }
        private string _AEmail;

        public string AEmail
        {
            get { return _AEmail; }
            set { _AEmail = value; }
        }

        public string Mobile
        {
            get { return _Mobile; }
            set { _Mobile = value; }
        }

        private string _Mobile2;

        public string Mobile2
        {
            get { return _Mobile2; }
            set { _Mobile2 = value; }
        }

        public string Phone
        {
            get { return _Phone; }
            set { _Phone = value; }
        }

        public DateTime? DOB
        {
            get { return _DOB; }
            set { _DOB = value; }
        }

        public string Father
        {
            get { return _Father; }
            set { _Father = value; }
        }

        public string Passport
        {
            get { return _Passport; }
            set { _Passport = value; }
        }

        public DateTime? PDOI
        {
            get { return _PDOI; }
            set { _PDOI = value; }
        }

        public string PDOIPlace
        {
            get { return _PDOIPlace; }
            set { _PDOIPlace = value; }
        }

        public DateTime? PDOE
        {
            get { return _PDOE; }
            set { _PDOE = value; }
        }

        public byte MaritalSta
        {
            get { return _MaritalSta; }
            set { _MaritalSta = value; }
        }

        public double CurrentCTC
        {
            get { return _CurrentCTC; }
            set { _CurrentCTC = value; }
        }

        public double ExpectedCT
        {
            get { return _ExpectedCT; }
            set { _ExpectedCT = value; }
        }

        public string Resume
        {
            get { return _Resume; }
            set { _Resume = value; }
        }
        private string _CurrentLocation;

        public string CurrentLocation
        {
            get { return _CurrentLocation; }
            set { _CurrentLocation = value; }
        }

        private string _Gender;
        public string Gender
        {
            get { return _Gender; }
            set { _Gender = value; }
        }

        public string Qualification
        {
            get { return _Qualification; }
            set { _Qualification = value; }
        }

        public string Institute
        {
            get { return _Institute; }
            set { _Institute = value; }
        }

        public int YOP
        {
            get { return _YOP; }
            set { _YOP = value; }
        }

        public string Specialization
        {
            get { return _Specialization; }
            set { _Specialization = value; }
        }

        public double Percentage
        {
            get { return _Percentage; }
            set { _Percentage = value; }
        }

        public string Mode
        {
            get { return _Mode; }
            set { _Mode = value; }
        }


        public string Organization
        {
            get { return _Organization; }
            set { _Organization = value; }
        }

        public string City
        {
            get { return _City; }
            set { _City = value; }
        }

        public string Type
        {
            get { return _Type; }
            set { _Type = value; }
        }


        public string Designation
        {
            get { return _Designation; }
            set { _Designation = value; }
        }

        public DateTime? ReqDate
        {
            get { return _ReqDate; }
            set { _ReqDate = value; }
        }

        public double Salary
        {
            get { return _Salary; }
            set { _Salary = value; }
        }

        public DateTime? AccDated
        {
            get { return _AccDated; }
            set { _AccDated = value; }
        }

        private int _JobType;
        public int JobType
        {
            get { return _JobType; }
            set { _JobType = value; }
        }
        public string OfferLetter
        {
            get { return _OfferLetter; }
            set { _OfferLetter = value; }
        }




        public string Address
        {
            get { return _Address; }
            set { _Address = value; }
        }



        public string State
        {
            get { return _State; }
            set { _State = value; }
        }

        public string Country
        {
            get { return _Country; }
            set { _Country = value; }
        }
        public int CountryID
        {
            get { return _CountryID; }
            set { _CountryID = value; }
        }
        public int Pin
        {
            get { return _Pin; }
            set { _Pin = value; }
        }

        private string _Username;

        public string Username
        {
            get { return _Username; }
            set { _Username = value; }
        }
        private string _UserPWD;

        public string UserPWD
        {
            get { return _UserPWD; }
            set { _UserPWD = value; }
        }
        private string _SkypeID;

        public string SkypeID
        {
            get { return _SkypeID; }
            set { _SkypeID = value; }
        }
        private int _Mgnr;

        public int Mgnr
        {
            get { return _Mgnr; }
            set { _Mgnr = value; }
        }

        private string _Image;

        public string Image
        {
            get { return _Image; }
            set { _Image = value; }
        }
        private char _CurrentStatus;

        public char CurrentStatus
        {
            get { return _CurrentStatus; }
            set { _CurrentStatus = value; }
        }

        private string _PRAddress;

        public string PRAddress
        {
            get { return _PRAddress; }
            set { _PRAddress = value; }
        }

        private string _PrCity;
        public string PrCity
        {
            get { return _PrCity; }
            set { _PrCity = value; }
        }

        private string _PrState;
        public string PrState
        {
            get { return _PrState; }
            set { _PrState = value; }
        }
        private string _PrCountry;
        public string PrCountry
        {
            get { return _PrCountry; }
            set { _PrCountry = value; }
        }
        private int _PrPin;
        public int PrPin
        {
            get { return _PrPin; }
            set { _PrPin = value; }
        }
        private string _PrPhone;
        public string PrPhone
        {
            get { return _PrPhone; }
            set { _PrPhone = value; }
        }

        private string _BGroup;
        public string BGroup
        {
            get { return _BGroup; }
            set { _BGroup = value; }
        }

        private string _Mole1;
        public string Mole1
        {
            get { return _Mole1; }
            set { _Mole1 = value; }
        }

        private string _Mole2;

        public string Mole2
        {
            get { return _Mole2; }
            set { _Mole2 = value; }
        }
        private string _SameAddress;

        public string SameAddress
        {
            get { return _SameAddress; }
            set { _SameAddress = value; }
        }

        private int _DesigID;

        public int DesigID
        {
            get { return _DesigID; }
            set { _DesigID = value; }
        }
        #endregion PrivateSection


        # region Contacts

        private int _ContactID;

        public int ContactID
        {
            get { return _ContactID; }
            set { _ContactID = value; }
        }

        private int _CID;
        public int CID
        {
            get { return _CID; }
            set { _CID = value; }
        }
        private string _ConPhone1;
        public string ConPhone1
        {
            get { return _ConPhone1; }
            set { _ConPhone1 = value; }
        }
        private string _ConPhone2;
        public string ConPhone2
        {
            get { return _ConPhone2; }
            set { _ConPhone2 = value; }
        }
        private string _ConPhone3;
        public string ConPhone3
        {
            get { return _ConPhone3; }
            set { _ConPhone3 = value; }
        }
        private string _Reference;
        public string Reference
        {
            get { return _Reference; }
            set { _Reference = value; }
        }
        private string _RepName;
        public string RepName
        {
            get { return _RepName; }
            set { _RepName = value; }
        }
        private string _ContacsAddress;
        public string ContacsAddress
        {
            get { return _ContacsAddress; }
            set { _ContacsAddress = value; }
        }
        private string _Notes;
        public string Notes
        {
            get { return _Notes; }
            set { _Notes = value; }
        }
        private string _Category;
        public string Category
        {
            get { return _Category; }
            set { _Category = value; }
        }

        private string _Others;

        public string Others
        {
            get { return _Others; }
            set { _Others = value; }
        }

        private int _ResponID;

        public int ResponID
        {
            get { return _ResponID; }
            set { _ResponID = value; }
        }


        private string _Responsible;

        public string Responsible
        {
            get { return _Responsible; }
            set { _Responsible = value; }


        }
        private int _TaskID;

        public int TaskID
        {
            get { return _TaskID; }
            set { _TaskID = value; }
        }

        private string _TaskName;
        public string TaskName
        {
            get { return _TaskName; }
            set { _TaskName = value; }
        }
        private DateTime? _DueDate;
        public DateTime? DueDate
        {
            get { return _DueDate; }
            set { _DueDate = value; }
        }

        private int _ListID;

        public int ListID
        {
            get { return _ListID; }
            set { _ListID = value; }
        }
        private string _ListName;
        public string ListName
        {
            get { return _ListName; }
            set { _ListName = value; }
        }
        private string _Authority;
        public string Authority
        {
            get { return _Authority; }
            set { _Authority = value; }
        }

        private int _ChkListID;

        public int ChkListID
        {
            get { return _ChkListID; }
            set { _ChkListID = value; }
        }

        private int _StatusID;
        public int StatusID
        {
            get { return _StatusID; }
            set { _StatusID = value; }
        }

        #endregion
    }
}