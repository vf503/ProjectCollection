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
    
    public partial class Project
    {
        public System.Guid ProjectId { get; set; }
        public System.Guid ProjectPlanId { get; set; }
        public System.Guid ProjectTypeId { get; set; }
        public string ProjectNo { get; set; }
        public System.DateTime SendingDate { get; set; }
        public Nullable<System.Guid> emergency { get; set; }
        public System.Guid WorkType { get; set; }
        public string CourseName { get; set; }
        public Nullable<System.Guid> lecture { get; set; }
        public Nullable<System.Guid> notice { get; set; }
        public Nullable<System.Guid> headline { get; set; }
        public string TextCategory { get; set; }
        public string lecturer { get; set; }
        public string LecturerJob { get; set; }
        public System.Guid progress { get; set; }
        public Nullable<System.Guid> CaptureNeeds { get; set; }
        public Nullable<System.Guid> ContentNeeds { get; set; }
        public Nullable<System.Guid> productCourseType { get; set; }
        public Nullable<System.Guid> PublishNeeds { get; set; }
        public Nullable<System.Guid> CapturePersonInCharge { get; set; }
        public Nullable<System.Guid> CaptureOperator { get; set; }
        public Nullable<System.Guid> CaptureProgress { get; set; }
        public string CaptureFeedBackNote { get; set; }
        public Nullable<System.Guid> ShorthandPersonInCharge { get; set; }
        public Nullable<System.Guid> ShorthandOperator { get; set; }
        public Nullable<System.Guid> ShorthandProgress { get; set; }
        public string ShorthandFeedBackNote { get; set; }
        public Nullable<System.Guid> ContentPersonInCharge { get; set; }
        public Nullable<System.Guid> ContentOperator { get; set; }
        public Nullable<System.Guid> ContentProgress { get; set; }
        public string ContentFeedBackNote { get; set; }
        public Nullable<System.Guid> ProductionPersonInCharge { get; set; }
        public Nullable<System.Guid> ProductionOperator { get; set; }
        public Nullable<System.Guid> ProductionProgress { get; set; }
        public string ProductionFeedBackNote { get; set; }
        public Nullable<System.Guid> PublishPersonInCharge { get; set; }
        public Nullable<System.Guid> PublishOperator { get; set; }
        public Nullable<System.Guid> PublishProgress { get; set; }
        public string PublishFeedBackNote { get; set; }
        public Nullable<System.Guid> CheckPersonInCharge { get; set; }
        public Nullable<System.Guid> CheckOperator { get; set; }
        public Nullable<System.Guid> CheckProgress { get; set; }
        public string CheckFeedBackNote { get; set; }
        public Nullable<short> CaptureDuration { get; set; }
        public Nullable<System.DateTime> CaptureReceiveDate { get; set; }
        public Nullable<System.DateTime> CaptureFinishDate { get; set; }
        public Nullable<System.Guid> CaptureVideoNeeds { get; set; }
        public Nullable<System.Guid> CaptureAudioNeeds { get; set; }
        public Nullable<System.Guid> CaptureVideoVideoQuality { get; set; }
        public Nullable<System.Guid> CaptureVideoAudioQuality { get; set; }
        public Nullable<System.Guid> CaptureAudioQuality { get; set; }
        public Nullable<System.Guid> CaptureSoundTrack { get; set; }
        public string CaptureFilePath { get; set; }
        public string CaptureNote { get; set; }
        public Nullable<System.DateTime> ShorthandReceiveDate { get; set; }
        public Nullable<System.DateTime> ShorthandAudioReceiveDate { get; set; }
        public Nullable<System.DateTime> ShorthandFinishDate { get; set; }
        public string ShorthandPurveyor { get; set; }
        public Nullable<System.Guid> ShorthandQuality { get; set; }
        public string ShorthandNote { get; set; }
        public string ShorthandFilePath { get; set; }
        public Nullable<System.DateTime> ContentReceiveDate { get; set; }
        public Nullable<System.DateTime> ContentAssignmentDate { get; set; }
        public Nullable<System.DateTime> ContentEstimatedDate { get; set; }
        public Nullable<System.DateTime> ContentFinishDate { get; set; }
        public string ContentDelayNote { get; set; }
        public Nullable<System.Guid> ContentCourseNameConfirm { get; set; }
        public string ContentChangedCourseName { get; set; }
        public Nullable<System.Guid> ContentCourseRecommend { get; set; }
        public Nullable<System.Guid> ContentPPTAdvice { get; set; }
        public Nullable<short> ContentExercises { get; set; }
        public string ContentOperateNote { get; set; }
        public Nullable<System.Guid> ContentCourseIntroductionQuality { get; set; }
        public Nullable<System.Guid> ContentResumeQuality { get; set; }
        public Nullable<System.Guid> ContentPPTQuality { get; set; }
        public Nullable<System.Guid> ContentExercisesQuality { get; set; }
        public Nullable<System.Guid> ContentTextQuality { get; set; }
        public Nullable<System.Guid> ContentIsTimely { get; set; }
        public Nullable<System.DateTime> ContentCheckDate { get; set; }
        public string ContentCheckNote { get; set; }
        public Nullable<System.Guid> ContentPPTNeeds { get; set; }
        public Nullable<System.Guid> ContentCourseIntroNeeds { get; set; }
        public Nullable<System.Guid> ContentLecturerResumeNeeds { get; set; }
        public Nullable<System.Guid> ContentExercisesNeeds { get; set; }
        public Nullable<System.Guid> ContentTextEditNeeds { get; set; }
        public string ContentPPTFilePath { get; set; }
        public Nullable<System.DateTime> ProductionReceiveDate { get; set; }
        public Nullable<System.DateTime> ProductionAssignmentDate { get; set; }
        public Nullable<System.DateTime> ProductionEstimatedDate { get; set; }
        public Nullable<System.DateTime> ProductionFinishDate { get; set; }
        public string ProductionDelayNote { get; set; }
        public Nullable<System.Guid> ProductionVideoQuality { get; set; }
        public Nullable<System.Guid> ProductionAudioQuality { get; set; }
        public Nullable<System.Guid> ProductionFileBackUp { get; set; }
        public string ProductionOperateNote { get; set; }
        public Nullable<System.Guid> ProductionVideoEditCheck { get; set; }
        public Nullable<System.Guid> ProductionAudioEditCheck { get; set; }
        public Nullable<System.Guid> ProductionProductCheck { get; set; }
        public Nullable<System.DateTime> ProductionCheckDate { get; set; }
        public string ProductionCheckNote { get; set; }
        public Nullable<System.DateTime> PublishReceiveContentDate { get; set; }
        public Nullable<System.DateTime> PublishReceiveProductionDate { get; set; }
        public Nullable<System.DateTime> PublishPublishDate { get; set; }
        public Nullable<System.Guid> PublishTopNewsNeeds { get; set; }
        public Nullable<System.Guid> PublishNoticeNeeds { get; set; }
        public Nullable<System.Guid> PublishPageState { get; set; }
        public Nullable<System.Guid> PublishPlayState { get; set; }
        public string PublishCommonCategory { get; set; }
        public string PublishGovernmentCategory { get; set; }
        public string PublishFinanceCategory { get; set; }
        public string PublishBankCategory { get; set; }
        public string PublishNote { get; set; }
        public Nullable<System.DateTime> CheckTaskCheckDate { get; set; }
        public Nullable<System.Guid> CheckTaskCategoryCheck { get; set; }
        public Nullable<System.Guid> CheckTaskCourseCommend { get; set; }
        public Nullable<System.DateTime> CheckTaskCancelCommendDate { get; set; }
        public System.Guid InCharge { get; set; }
        public string CreateNote { get; set; }
        public Nullable<System.Guid> ProductionIsTimely { get; set; }
        public Nullable<System.Guid> CheckTaskCourseCancelCommend { get; set; }
        public string CheckTaskNote { get; set; }
        public Nullable<System.Guid> CaptureCheckPersonInCharge { get; set; }
        public Nullable<System.DateTime> CaptureCheckDate { get; set; }
        public string CaptureCheckNote { get; set; }
        public Nullable<System.DateTime> ContentRecheckDate { get; set; }
        public string ContentRecheckNote { get; set; }
        public Nullable<System.Guid> ContentRecheckPersonInCharge { get; set; }
        public string ContentReceiveNote { get; set; }
        public Nullable<System.DateTime> ContentLastModifyDate { get; set; }
        public Nullable<System.DateTime> ProductionLastModifyDate { get; set; }
        public Nullable<System.Guid> SourceProjectId { get; set; }
        public Nullable<System.DateTime> CaptureReceiveDelayDate { get; set; }
        public string CaptureReceiveDelayNote { get; set; }
        public Nullable<System.DateTime> ProductionReceiveDelayDate { get; set; }
        public string ProductionReceiveDelayNote { get; set; }
        public Nullable<System.DateTime> ExecutionDate { get; set; }
        public Nullable<System.DateTime> DeadLine { get; set; }
        public string ExtraNote { get; set; }
        public string ProductionReceiveNote { get; set; }
        public System.Guid CanBeSold { get; set; }
        public Nullable<System.Guid> ContentCheckPersonInCharge { get; set; }
        public Nullable<short> EpisodeCount { get; set; }
        public Nullable<System.DateTime> VideoEncodeFinishDate { get; set; }
        public string ContentCheckScore { get; set; }
        public string MakeType { get; set; }
        public Nullable<int> duration { get; set; }
        public string LecturerNote { get; set; }
        public string ContentCheckSlideScore { get; set; }
        public string STTType { get; set; }
        public string CourseType { get; set; }
    }
}
