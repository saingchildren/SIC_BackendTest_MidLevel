using System.ComponentModel.DataAnnotations;

namespace MercuryTest.Models
{
    public class MyOffice_ACPD
    {
        [StringLength(20, ErrorMessage = "SID 長度不超過20字元")]
        public string ACPD_SID { get; set; } = string.Empty;

        /// <example>王亦瀧</example>
        [StringLength(60, ErrorMessage = "中文名稱不超過60字元")]
        public string? ACPD_Cname { get; set; }

        /// <example>SIC</example>
        [StringLength(40)]
        public string? ACPD_Ename { get; set; }

        /// <example>SIC</example>
        [StringLength(40)]
        public string? ACPD_Sname { get; set; }

        /// <example>sic900626@gmail.com</example>
        [StringLength(60)]
        [EmailAddress(ErrorMessage = "Email 格式不正確")]
        public string? ACPD_Email { get; set; }

        /// <example>1</example>
        public byte? ACPD_Status { get; set; }

        /// <example>false</example>
        public bool? ACPD_Stop { get; set; }

        /// <example></example>
        [StringLength(60)]
        public string? ACPD_StopMemo { get; set; }

        /// <example>sic</example>
        [StringLength(30)]
        public string? ACPD_LoginID { get; set; }

        /// <example>sic</example>
        [StringLength(60)]
        public string? ACPD_LoginPWD { get; set; }

        /// <example>這是透過測試資料產生的帳號</example>
        [StringLength(600)]
        public string? ACPD_Memo { get; set; }
        public DateTime? ACPD_NowDateTime { get; set; }

        /// <example>ADMIN</example>
        [StringLength(20)]
        public string? ACPD_NowID { get; set; }
        public DateTime? ACPD_UPDDateTime { get; set; }

        [StringLength(20)]
        public string? ACPD_UPDID { get; set; }
    }
}
