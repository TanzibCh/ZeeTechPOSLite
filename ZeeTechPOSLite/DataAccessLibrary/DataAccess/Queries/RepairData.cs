using DataAccessLibrary.DataAccess.Internal.SQLiteDataAccess;
using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccessLibrary.DataAccess.Queries
{
	public class RepairData
	{
		private SQLiteDataAccess _db = new SQLiteDataAccess();
		private const string _connectionStringName = "SQLiteDB";

		#region Save Repair

		public void AddNewRepair(RepairModel repair)
		{
			// Query to save Repair data
			string sql = @"INSERT INTO Repair
						  (RepairNumber, BookingDate, DiagnosticReport, DiagnosticCharged,
						  DiagnosticAmount, QuotationAmount, BalanceToPay, DiagnosticDone,
						  ReportGiven, IsApproved, IsRepaired, IsRepairable, IsActive)
						  VALUES (@repairNumber, @bookingDate, @diagnosticReport, @diagnosticCharged,
						  @diagnosticAmount, @quotationAmount, @balanceToPay, @diagnosticDone,
						  @reportGiven, @isApproved, @isRepaired, @isRepairable, @isActive);";


			// Insert Address data into the database
			_db.SaveData(sql, new
			{
				repairNumber = GetNewRepairNumber(),
				bookingDate = repair.BookingDate,
				diagnosticReport = repair.DiagnosticReport,
				diagnosticCharged = repair.DiagnosticCharged,
				diagnosticAmount = repair.DiagnosticAmount,
				quotationAmount = repair.QuotationAmount,
				balanceToPay = repair.BalanceToPay,
				diagnosticDone = repair.DiagnosticDone,
				reportGiven = repair.ReportGiven,
				isApproved = repair.IsApproved,
				isRepaired = repair.IsRepaired,
				isRepairable = repair.IsRepairable,
				isActive = repair.IsActive
			}, _connectionStringName);
		}

		// Get new Repair Number
		private int GetNewRepairNumber()
		{
			string sql = @"SELECT RepairNumber
						   FROM Repair
						   ORDER BY RepairNumber DESC
						   LIMIT 1;";

			RepairModel repair = _db.LoadData<RepairModel, dynamic>(
				sql, new { }, _connectionStringName).FirstOrDefault();

			int output;
			if (repair == null)
			{
				output = 1;
			}
			else
			{
				output = repair.RepairNumber + 1;
			}

			return output;
		}

		public void UpdateProduct(RepairModel repair)
		{
			string sql = @"UPDATE Repair
						   SET BookingDate = @bookingDate,
						   DiagnosticReport = @diagnosticReport,
						   DiagnosticCharged = @diagnosticCharged,
						   DiagnosticAmount = @diagnosticAmount,
						   QuotationAmount = @quotationAmount,
						   BalanceToPay = @balanceToPay,
						   DiagnosticDone = @diagnosticDone,
						   ReportGiven = @reportGiven,
						   IsApproved = @isApproved,
						   IsRepaired = @isRepaired,
						   IsRepairable = @isRepairable,
						   IsActive = @isActive;";

			_db.SaveData(sql, new
			{
				bookingDate = repair.BookingDate,
				diagnosticReport = repair.DiagnosticReport,
				diagnosticCharged = repair.DiagnosticCharged,
				diagnosticAmount = repair.DiagnosticAmount,
				quotationAmount = repair.QuotationAmount,
				balanceToPay = repair.BalanceToPay,
				diagnosticDone = repair.DiagnosticDone,
				reportGiven = repair.ReportGiven,
				isApproved = repair.IsApproved,
				isRepaired = repair.IsRepaired,
				isRepairable = repair.IsRepairable,
				isActive = repair.IsActive
			}, _connectionStringName);
		}
		#endregion

		#region Get Repair

		public List<RepairModel> GetAllActiveRepairs()
		{
			string sql = @"SELECT Id, RepairNumber, BookingDate, DiagnosticReport, DiagnosticCharged,
						  DiagnosticAmount, QuotationAmount, BalanceToPay, DiagnosticDone,
						  ReportGiven, IsApproved, IsRepaired, IsRepairable, IsActive
						  FROM Repair
						  WHERE IsActive = 1";

			return _db.LoadData<RepairModel, dynamic>(sql, new { }, _connectionStringName);
		}

		public List<RepairModel> GetAllInActiveRepairs()
		{
			string sql = @"SELECT Id, RepairNumber, BookingDate, DiagnosticReport, DiagnosticCharged,
						  DiagnosticAmount, QuotationAmount, BalanceToPay, DiagnosticDone,
						  ReportGiven, IsApproved, IsRepaired, IsRepairable, IsActive
						  FROM Repair
						  WHERE IsActive = 0";

			return _db.LoadData<RepairModel, dynamic>(sql, new { }, _connectionStringName);
		}

		public RepairModel GetRepairById(int id)
		{
			string sql = @"SELECT Id, RepairNumber, BookingDate, DiagnosticReport, DiagnosticCharged,
						  DiagnosticAmount, QuotationAmount, BalanceToPay, DiagnosticDone,
						  ReportGiven, IsApproved, IsRepaired, IsRepairable, IsActive
						  FROM Repair
						  WHERE Id = @id";

			return _db.LoadData<RepairModel, dynamic>(sql, new { id }, _connectionStringName).FirstOrDefault();
		}

		public RepairModel GetRepairByRepairNumber(int repairNumber)
		{
			string sql = @"SELECT Id, RepairNumber, BookingDate, DiagnosticReport, DiagnosticCharged,
						  DiagnosticAmount, QuotationAmount, BalanceToPay, DiagnosticDone,
						  ReportGiven, IsApproved, IsRepaired, IsRepairable, IsActive
						  FROM Repair
						  WHERE RepairNumber = @repairNumber";

			return _db.LoadData<RepairModel, dynamic>(sql, new { repairNumber }, _connectionStringName).FirstOrDefault();
		}
		#endregion
	}
}
