//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProjectCollection.WebUI.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class BatchProject
    {
        public string id { get; set; }
        public System.DateTime CreateDate { get; set; }
        public string progress { get; set; }
        public string custom { get; set; }
        public System.Guid CreatorId { get; set; }
        public string CreateNote { get; set; }
        public string TaskRequire { get; set; }
        public Nullable<int> CourseAmount { get; set; }
        public Nullable<System.Guid> signer { get; set; }
        public Nullable<System.DateTime> CheckDate { get; set; }
        public string CheckNote { get; set; }
        public Nullable<System.Guid> transactor { get; set; }
        public Nullable<System.DateTime> FinishDate { get; set; }
        public string FinishNote { get; set; }
        public string CourseData { get; set; }
        public Nullable<System.DateTime> DelayDate { get; set; }
        public string DelayNote { get; set; }
        public bool PicIsNeeded { get; set; }
        public Nullable<System.Guid> PicProducer { get; set; }
        public string PicRequireNote { get; set; }
        public Nullable<System.DateTime> PicFinishDate { get; set; }
        public bool TemplateIsNeeded { get; set; }
        public Nullable<System.Guid> TemplateProducer { get; set; }
        public string TemplateRequireNote { get; set; }
        public Nullable<System.DateTime> TemplateFinishDate { get; set; }
        public Nullable<System.DateTime> HelpSendingDate { get; set; }
        public Nullable<System.DateTime> HelperFinishDate { get; set; }
        public Nullable<System.Guid> helper { get; set; }
        public string HelperFinishNote { get; set; }
        public Nullable<System.DateTime> PicSendingDate { get; set; }
        public Nullable<System.DateTime> TemplateSendingDate { get; set; }
        public Nullable<System.DateTime> DeadLine { get; set; }
        public string PicFinishNote { get; set; }
        public string TemplateFinishNote { get; set; }
    
        public virtual user_info user_info { get; set; }
        public virtual user_info user_info1 { get; set; }
        public virtual user_info user_info2 { get; set; }
        public virtual user_info user_info3 { get; set; }
        public virtual user_info user_info4 { get; set; }
        public virtual user_info user_info5 { get; set; }
    }
}
