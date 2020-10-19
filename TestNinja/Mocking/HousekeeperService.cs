using System;
using System.Linq;

namespace TestNinja.Mocking
{
	public class HousekeeperService
	{
		private readonly IUnitOfWork unitOfWork;
		private readonly IStatementGenerator statementGenerator;
		private readonly IEmailSender emailSender;
		private readonly IXtraMessageBox messageBox;

		public HousekeeperService(IUnitOfWork unitOfWork, IStatementGenerator statementGenerator, IEmailSender emailSender, IXtraMessageBox messageBox)
		{
			this.unitOfWork = unitOfWork;
			this.statementGenerator = statementGenerator;
			this.emailSender = emailSender;
			this.messageBox = messageBox;
		}

		public void SendStatementEmails(DateTime statementDate)
		{
			IQueryable<Housekeeper> housekeepers = unitOfWork.Query<Housekeeper>();

			foreach (Housekeeper housekeeper in housekeepers)
			{
				if (string.IsNullOrWhiteSpace(housekeeper.Email))
					continue;

				string statementFilename = statementGenerator.SaveStatement(housekeeper.Oid, housekeeper.FullName, statementDate);

				if (string.IsNullOrWhiteSpace(statementFilename))
					continue;

				string emailAddress = housekeeper.Email;
				string emailBody = housekeeper.StatementEmailBody;

				try
				{
					emailSender.EmailFile(emailAddress, emailBody, statementFilename,
						string.Format("Sandpiper Statement {0:yyyy-MM} {1}", statementDate, housekeeper.FullName));
				}
				catch (Exception e)
				{
					messageBox.Show(e.Message, string.Format("Email failure: {0}", emailAddress), MessageBoxButtons.OK);
				}
			}
		}
	}

	public enum MessageBoxButtons
	{
		OK
	}

	public interface IXtraMessageBox
	{
		void Show(string s, string housekeeperStatements, MessageBoxButtons ok);
	}

	public class XtraMessageBox : IXtraMessageBox
	{
		public void Show(string s, string housekeeperStatements, MessageBoxButtons ok)
		{
		}
	}

	public class MainForm
	{
		public bool HousekeeperStatementsSending { get; set; }
	}

	public class DateForm
	{
		public DateForm(string statementDate, object endOfLastMonth)
		{
		}

		public DateTime Date { get; set; }

		public DialogResult ShowDialog()
		{
			return DialogResult.Abort;
		}
	}

	public enum DialogResult
	{
		Abort,
		OK
	}

	public class SystemSettingsHelper
	{
		public static string EmailSmtpHost { get; set; }
		public static int EmailPort { get; set; }
		public static string EmailUsername { get; set; }
		public static string EmailPassword { get; set; }
		public static string EmailFromEmail { get; set; }
		public static string EmailFromName { get; set; }
	}

	public class Housekeeper
	{
		public string Email { get; set; }
		public int Oid { get; set; }
		public string FullName { get; set; }
		public string StatementEmailBody { get; set; }
	}

	public class HousekeeperStatementReport
	{
		public HousekeeperStatementReport(int housekeeperOid, DateTime statementDate)
		{
		}

		public bool HasData { get; set; }

		public void CreateDocument()
		{
		}

		public void ExportToPdf(string filename)
		{
		}
	}
}