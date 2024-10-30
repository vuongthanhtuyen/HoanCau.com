using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using SweetCMS.Core.Manager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Text;

namespace SweetCMS.Core.Helper
{
    public class AmazonMailServiceHelper
    {
        public static bool SendViaAmazon(string subject, string contentBody, string senderMail, string senderName, string toMail)
        {
            #region RegionName

            string accessKey = System.Configuration.ConfigurationManager.AppSettings["AWSAccessKey"].ToString();
            if (string.IsNullOrEmpty(accessKey))
                accessKey = "AKIAJIEI3VY5BE2AX5XQ";
         
            string secretAccessKey = System.Configuration.ConfigurationManager.AppSettings["AWSSecretKey"].ToString().Trim();
            if (string.IsNullOrEmpty(secretAccessKey))
                secretAccessKey = "lWM6OK/SLwzrc0wN6seg6b0rWLtRzKoIKy3xunAr";
           
            #endregion

            AmazonMailService service = new AmazonMailService();
            service.AWSAccessKey = accessKey;
            service.AWSSecretKey = secretAccessKey;
            service.SenderName = senderName;
            service.RegionPoint = RegionEndpoint.USWest2;
            service.SenderEmail = SettingManager.GetSettingValue(SettingNames.SmtpSenderEmail);
            service.ToEmail.Add(new MailAddress(toMail));
            service.Subject = subject;
            StringBuilder sbContent = new StringBuilder();
            try
            {
                sbContent.Append(contentBody);
                sbContent.Append("<br/>");
                sbContent.Append(SettingManager.GetSettingValue(SettingNames.EmailSignature));
            }
            catch
            {
                sbContent.Append(contentBody);
            }
            service.BodyContentHtml = sbContent.ToString();
            service.IsBodyHtml = true;
            //Amazon.Util.ProfileManager.RegisterProfile("development", accessKey, secretAccessKey);
            //var credentials = new StoredProfileAWSCredentials("development");
            //var s3Client = new AmazonS3Client(credentials, RegionEndpoint.USWest2);

            HttpStatusCode result = service.SendMail(); 

            return result == HttpStatusCode.OK;
        }
    }

    public class AmazonMailService
    {
        bool _isMailSent = false;

        public List<string> AttachedFile = new List<string>();

        public string SenderEmail = string.Empty;

        public string SenderName = string.Empty;

        public string AWSAccessKey = string.Empty;

        public string AWSSecretKey = string.Empty;

        public string Subject = string.Empty;

        public string BodyContentText = string.Empty;

        public string BodyContentHtml = string.Empty;

        public string BodyContentTemplate = string.Empty;

        public string _finalTemplate = string.Empty;

        public EmailContentType ContentType = EmailContentType.HTML;

        public bool IsBodyHtml = true;

        public RegionEndpoint RegionPoint = RegionEndpoint.USWest2;

        public Dictionary<string, string> TempateReplacements = new Dictionary<string, string>();

        public MailAddressCollection ToEmail = new MailAddressCollection();

        public string ReplyToAddress = string.Empty;

        public string Signature = string.Empty;

        public HttpStatusCode SendMail()
        {
            HttpStatusCode result = HttpStatusCode.BadRequest;
            try
            {
                MailMessage mailMessage = new MailMessage();
                if (mailMessage != null)
                {
                    foreach (MailAddress address in ToEmail) mailMessage.To.Add(address);

                    mailMessage.From = new MailAddress(SenderEmail, SenderName);

                    mailMessage.Priority = MailPriority.High;

                    mailMessage.Subject = this.Subject;

                    if (!string.IsNullOrEmpty(ReplyToAddress)) mailMessage.ReplyToList.Add(new MailAddress(ReplyToAddress));

                    mailMessage.IsBodyHtml = IsBodyHtml;

                    if (AttachedFile != null && AttachedFile.Count > 0)
                    {
                        mailMessage.Attachments.Clear();
                        foreach (string filePath in AttachedFile)
                        {
                            Attachment atchFile = new Attachment(filePath);
                            mailMessage.Attachments.Add(atchFile);
                        }
                    }

                    if (ContentType == EmailContentType.HTML) mailMessage.Body = BodyContentHtml;
                    else if (ContentType == EmailContentType.PlainText) mailMessage.Body = BodyContentText;
                    else
                    {
                        if (!string.IsNullOrEmpty(BodyContentTemplate))
                            _finalTemplate = File.ReadAllText(BodyContentTemplate);

                        if (TempateReplacements.Count > 0)
                        {
                            if (string.IsNullOrEmpty(_finalTemplate))
                            {
                                throw new Exception("Set Template field (i.e. file path) while using replacement field");
                            }

                            foreach (var item in TempateReplacements)
                            {
                                _finalTemplate = _finalTemplate.Replace("<%" + item.Key.ToString() + "%>", item.Value.ToString());
                            }
                        }
                        mailMessage.Body = _finalTemplate;
                    }

                    var message = mailMessage;
                    var stream = FromMailMessageToMemoryStream(message);

                    using (AmazonSimpleEmailServiceClient client = new AmazonSimpleEmailServiceClient(
                               AWSAccessKey,
                                AWSSecretKey,
                               RegionPoint))
                    {
                        var sendRequest = new SendRawEmailRequest { RawMessage = new RawMessage { Data = stream } };
                        try
                        {
                            SendRawEmailResponse response = client.SendRawEmail(sendRequest);
                            _isMailSent = true;
                            result = response.HttpStatusCode;
                        }
                        catch (Exception ex1)
                        {
                            
                        }
                    }
                }
                else
                {
                    _isMailSent = false;
                }
            }
            catch (Exception ex)
            {
                //throw ex;
                //HttpContext.Current.Response.Write("Error : " + ex.Message);
            }

            return result;
        }

        private MemoryStream FromMailMessageToMemoryStream(MailMessage message)
        {
            Assembly assembly = typeof(SmtpClient).Assembly;

            Type mailWriterType = assembly.GetType("System.Net.Mail.MailWriter");

            MemoryStream stream = new MemoryStream();

            ConstructorInfo mailWriterContructor =
               mailWriterType.GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null, new[] { typeof(Stream) }, null);
            object mailWriter = mailWriterContructor.Invoke(new object[] { stream });

            MethodInfo sendMethod =
               typeof(MailMessage).GetMethod("Send", BindingFlags.Instance | BindingFlags.NonPublic);

            if (sendMethod.GetParameters().Length == 3)
            {
                sendMethod.Invoke(message, BindingFlags.Instance | BindingFlags.NonPublic, null, new[] { mailWriter, true, true }, null); // .NET 4.x
            }
            else
            {
                sendMethod.Invoke(message, BindingFlags.Instance | BindingFlags.NonPublic, null, new[] { mailWriter, true }, null); // .NET < 4.0 
            }

            MethodInfo closeMethod =
               mailWriter.GetType().GetMethod("Close", BindingFlags.Instance | BindingFlags.NonPublic);
            closeMethod.Invoke(mailWriter, BindingFlags.Instance | BindingFlags.NonPublic, null, new object[] { }, null);

            return stream;
        }
    }

    public enum EmailContentType
    {
        [Description("PlainText")]
        PlainText,
        [Description("HTML")]
        HTML,
        [Description("Template")]
        Template
    }

    public enum EmailSendResult
    {
        [Description("Success")]
        Success,
        [Description("SenderAccountEmpty")]
        SenderAccountEmpty,
        [Description("SubjectEmpty")]
        SubjectEmpty,
        [Description("BodyContentEmpty")]
        BodyContentEmpty,
        [Description("AWSAccessKeyNotFound")]
        AWSAccessKeyNotFound,
        [Description("AWSSecretKeyNotFound")]
        AWSSecretKeyNotFound,
    }
}
