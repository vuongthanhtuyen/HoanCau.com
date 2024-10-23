using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS.WebUI.Common
{
    public class BaseAdminPage: System.Web.UI.Page
    {
        protected bool IsValid
        {
            get
            {
                return CurrentValidationPromptControl.Count == 0 && PromptControdClientIDs.Count == 0;
            }
        }
        private List<WebControl> listValidationPromptControl;
        protected List<WebControl> CurrentValidationPromptControl
        {
            get
            {

                if (listValidationPromptControl == null)
                    listValidationPromptControl = new List<WebControl>();
                return listValidationPromptControl;
            }
        }
        protected void AddErrorPrompt(string clientID, string message)
        {
            string msgValid = string.Format("{0}", message);
            int u = -1;// PromptControdClientIDs.FindIndex(delegate(string p) { return p.Equals(clientID); });
            if (u > -1)
            {
                if (!string.IsNullOrEmpty(PromptErrorMessages[u]))
                {
                    PromptErrorMessages[u] += msgValid;
                }
            }
            else
            {
                PromptControdClientIDs.Add(clientID);
                PromptErrorMessages.Add(msgValid);
            }
        }

        protected List<string> m_PromptErrorMessages;

        public List<string> PromptErrorMessages
        {
            get
            {
                if (m_PromptErrorMessages == null)
                    m_PromptErrorMessages = new List<string>();
                return m_PromptErrorMessages;
            }
            set
            {
                m_PromptErrorMessages = value;
            }
        }



        protected List<string> m_PromptControdClientIDs;
        public List<string> PromptControdClientIDs
        {
            get
            {
                if (m_PromptControdClientIDs == null)
                    m_PromptControdClientIDs = new List<string>();
                return m_PromptControdClientIDs;
            }
            set
            {
                m_PromptControdClientIDs = value;
            }
        }
        protected bool ShowErrorPrompt()
        {
            StringBuilder promptScript = new StringBuilder();
            if (PromptControdClientIDs.Count > 0 && PromptControdClientIDs.Count == PromptErrorMessages.Count)
            {
                for (int i = 0; i < PromptControdClientIDs.Count; i++)
                    promptScript.AppendFormat("$('#{0}').validationEngine('showPrompt', '{1}', 'error','topLeft', true); ", PromptControdClientIDs[i], PromptErrorMessages[i].Replace("'", "\'"));

                string script = promptScript.ToString();
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "RunScript", script, true);
                return true;
            }
            return false;
        }


        public string GetStatusText(object strStatus)
        {
            try
            {
                switch (strStatus.ToString())
                {
                    case BasicStatusHelper.Active:
                    case "True":
                    case "true":
                    case "TRUE":
                        return "<span class='badge badge-success'>Kích hoạt</span>";
                    case BasicStatusHelper.InActive:
                    case "False":
                    case "false":
                    case "FALSE":
                        return "<span class='badge badge-danger'>Khóa</span>";
                    case ContactStatusHelper.New:
                        return "<span class='badge badge-info'>Mới</span>";
                    case ContactStatusHelper.Processed:
                        return "<span class='badge badge-warning'>Đã xử lý</span>";
                    case ContactStatusHelper.Duplicated:
                        return "<span class='badge badge-secondary'>Trùng lặp</span>";
                    case ContactStatusHelper.Invalid:
                        return "<span class='badge badge-dark'>Lỗi</span>";
                    case ArticleStatusHelper.Draft:
                        return "<span class='badge badge-light'>Nháp</span>";
                    case ArticleStatusHelper.WaitForApprove:
                        return "<span class='badge badge-primary'>Chờ duyệt</span>";
                    case ArticleStatusHelper.Published:
                        return "<span class='badge badge-success'>Xuất bản</span>";
                    case ArticleStatusHelper.UnPublished:
                        return "<span class='badge badge-danger'>Không xuất bản</span>";
                    case "Publishing":
                        return "<span class='badge badge-success'>Còn hiệu lực</span>";
                    case "Expired":
                        return "<span class='badge badge-danger'>Hết hiệu lực</span>";
                    default:
                        return string.Format("<span class='badge badge-light'>{0}</span>", strStatus.ToString());
                }
            }
            catch
            {
                return strStatus.ToString();
            }
        }
        public class ContactStatusHelper
        {
            public const string New = "New";
            public const string Processed = "Processed";
            public const string Duplicated = "Duplicated";
            public const string Invalid = "Invalid";
            public const string Deleted = "Deleted";
            public const string Processing = "Processing";

        }
        public class ArticleStatusHelper
        {
            public const string New = "New";
            public const string WaitForApprove = "WaitForApprove";
            public const string Draft = "Draft";
            public const string Published = "Published";
            public const string UnPublished = "UnPublished";
            public const string Deleted = "Deleted";
        }
        public class BasicStatusHelper
        {
            public const string InActive = "InActive";
            public const string Active = "Active";
            public const string Draft = "Draft";
            public const string NotApproved = "NotApproved";
            public const string Deleted = "Deleted";
        }

    }
}