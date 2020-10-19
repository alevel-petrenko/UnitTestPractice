using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
	[TestFixture]
	public class HousekeeperServiceTests
	{
		private Mock<IUnitOfWork> unitOfWork;
		private Mock<IStatementGenerator> statementGenerator;
		private Mock<IEmailSender> emailSender;
		private Mock<IXtraMessageBox> xtraMessageBox;
		private HousekeeperService service;
		private readonly DateTime statementDate = new DateTime(2020, 02, 01);
		private Housekeeper housekeeper;

		[SetUp]
		public void SetUp()
		{
			housekeeper = new Housekeeper { Email = "a", FullName = "b", Oid = 1, StatementEmailBody = "c" };
			unitOfWork = new Mock<IUnitOfWork>();
			statementGenerator = new Mock<IStatementGenerator>();
			emailSender = new Mock<IEmailSender>();
			xtraMessageBox = new Mock<IXtraMessageBox>();

			unitOfWork.Setup(uow => uow.Query<Housekeeper>()).Returns(new List<Housekeeper>
			{
				housekeeper
			}.AsQueryable());
			service = new HousekeeperService(unitOfWork.Object, statementGenerator.Object, emailSender.Object, xtraMessageBox.Object);
		}

		[Test]
		public void SendStatementEmails_WhenCalled_GenerateStatements()
		{
			service.SendStatementEmails(statementDate);

			statementGenerator.Verify(sg => sg.SaveStatement(housekeeper.Oid, housekeeper.FullName, statementDate));
		}

		[Test]
		[TestCase("")]
		[TestCase("  ")]
		[TestCase(null)]
		public void SendStatementEmails_HousekeeperEmailHasNoValue_NotGenerateStatement(string email)
		{
			housekeeper.Email = email;

			service.SendStatementEmails(statementDate);

			statementGenerator.Verify(sg => sg.SaveStatement(housekeeper.Oid, housekeeper.FullName, statementDate), Times.Never);
		}

		[Test]
		public void SendStatementEmails_WhenCalled_EmailTheStatement()
		{
			statementGenerator.Setup(sg => sg.SaveStatement(housekeeper.Oid, housekeeper.FullName, statementDate)).Returns("name");

			service.SendStatementEmails(statementDate);

			emailSender.Verify(sender => sender.EmailFile(housekeeper.Email, housekeeper.StatementEmailBody, "name", It.IsAny<string>()));
		}

		[Test]
		[TestCase("")]
		[TestCase("  ")]
		[TestCase(null)]
		public void SendStatementEmails_FileNameHasNoName_NotEmailTheStatement(string name)
		{
			statementGenerator.Setup(sg => sg.SaveStatement(housekeeper.Oid, housekeeper.FullName, statementDate)).Returns(name);

			service.SendStatementEmails(statementDate);

			emailSender.Verify(sender => sender.EmailFile(housekeeper.Email, housekeeper.StatementEmailBody, name, It.IsAny<string>()), Times.Never);
		}


		[Test]
		public void SendStatementEmails_EmailSendingFails_DisplayMessageBox(string name)
		{
			emailSender.Setup(sender => sender.EmailFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
				.Throws<Exception>();

			service.SendStatementEmails(statementDate);

			xtraMessageBox.Verify(box => box.Show(It.IsAny<string>(), It.IsAny<string>(), MessageBoxButtons.OK));
		}
	}
}