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
    
    public partial class user_info
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public user_info()
        {
            this.CustomProject = new HashSet<CustomProject>();
            this.CustomProject1 = new HashSet<CustomProject>();
            this.CustomProject2 = new HashSet<CustomProject>();
            this.CustomProject3 = new HashSet<CustomProject>();
            this.BatchProject = new HashSet<BatchProject>();
            this.BatchProject1 = new HashSet<BatchProject>();
            this.BatchProject2 = new HashSet<BatchProject>();
            this.BatchProject3 = new HashSet<BatchProject>();
            this.BatchProject4 = new HashSet<BatchProject>();
            this.BatchProject5 = new HashSet<BatchProject>();
            this.BatchProject6 = new HashSet<BatchProject>();
        }
    
        public System.Guid user_identity { get; set; }
        public string login_name { get; set; }
        public string password { get; set; }
        public string real_name { get; set; }
        public string Email_address { get; set; }
        public Nullable<System.Guid> role_identity { get; set; }
        public Nullable<System.Guid> SupervisorRole { get; set; }
        public string area { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomProject> CustomProject { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomProject> CustomProject1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomProject> CustomProject2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomProject> CustomProject3 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BatchProject> BatchProject { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BatchProject> BatchProject1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BatchProject> BatchProject2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BatchProject> BatchProject3 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BatchProject> BatchProject4 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BatchProject> BatchProject5 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BatchProject> BatchProject6 { get; set; }
    }
}
