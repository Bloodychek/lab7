using lab7.Data;
using lab7.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity;

namespace lab7.IncomeAndExpensesOfGsms
{
    public partial class IncomeAndExpensesOfGsms : System.Web.UI.Page
    {
        private readonly Petrol_StationContext _context = new Petrol_StationContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
                GetIncomeAndExpensesOfGsms();
        }

        private void GetIncomeAndExpensesOfGsms()
        {
            IEnumerable<IncomeAndExpensesOfGsm> incomes = _context.IncomeAndExpensesOfGsms.Include(s => s.Container).ToList();
            IncomeAndExpensesOfGsmsGridView.DataSource = incomes;
            ContainersSqlDataSource.Select(DataSourceSelectArguments.Empty);
            IncomeAndExpensesOfGsmsGridView.DataBind();
        }

        protected void IncomeAndExpensesOfGsmsGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            IncomeAndExpensesOfGsmsGridView.PageIndex = e.NewPageIndex;
            GetIncomeAndExpensesOfGsms();
        }

        protected void AddButton_Click(object sender, EventArgs e)
        {
            int numberOfCapacity = int.Parse(IncomeAndExpensesOfGsmsNumberOfCapacityTextBox.Text);
            DateTime dateAndTimeOfTheOperationIncomeOrExpense = IncomeAndExpensesOfGsmsDateAndTimeOfTheOperationIncomeOrExpenseTextBox.SelectedDate;
            int incomeOrExpensePerliter = int.Parse(IncomeAndExpensesOfGsmsIncomeOrExpensePerliterTextBox.Text);
            int containerId = int.Parse(IncomeAndExpensesOfGsmsDropDownList1.SelectedValue);
            string responsibleForTheOperation = IncomeAndExpensesOfGsmsResponsibleForTheOperationTextBox.Text;

            if (CheckValues(numberOfCapacity, incomeOrExpensePerliter, dateAndTimeOfTheOperationIncomeOrExpense, responsibleForTheOperation))
            {
                IncomeAndExpensesOfGsm income = new IncomeAndExpensesOfGsm
                {
                    NumberOfCapacity = numberOfCapacity,
                    IncomeOrExpensePerliter = incomeOrExpensePerliter,
                    ContainerId = containerId,
                    DateAndTimeOfTheOperationIncomeOrExpense = dateAndTimeOfTheOperationIncomeOrExpense,
                    ResponsibleForTheOperation = responsibleForTheOperation
                };

                _context.IncomeAndExpensesOfGsms.Add(income);
                _context.SaveChanges();

                AddStatusLabel.Text = "IncomeAndExpensesOfGsm was successfully added.";

                IncomeAndExpensesOfGsmsGridView.PageIndex = IncomeAndExpensesOfGsmsGridView.PageCount;
                GetIncomeAndExpensesOfGsms();
            }
        }

        protected void IncomeAndExpensesOfGsmsGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            IncomeAndExpensesOfGsmsGridView.EditIndex = e.NewEditIndex;
            GetIncomeAndExpensesOfGsms();
        }

        protected void IncomeAndExpensesOfGsmsGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            IncomeAndExpensesOfGsmsGridView.EditIndex = -1;
            GetIncomeAndExpensesOfGsms();
        }

        protected void IncomeAndExpensesOfGsmsGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int numberOfCapacity = int.Parse((string)e.NewValues["NumberOfCapacity"]);
            int incomeOrExpensePerliter = int.Parse((string)e.NewValues["IncomeOrExpensePerliter"]);
            int containerId = int.Parse((string)e.NewValues["ContainerId"]);
            DateTime dateAndTimeOfTheOperationIncomeOrExpense = (DateTime)e.NewValues["DateAndTimeOfTheOperationIncomeOrExpense"];
            int staffId = int.Parse((string)e.NewValues["StaffId"]);
            string responsibleForTheOperation = (string)e.NewValues["ResponsibleForTheOperation"];

            if (CheckValues(numberOfCapacity, incomeOrExpensePerliter, dateAndTimeOfTheOperationIncomeOrExpense, responsibleForTheOperation))
            {
                var row = IncomeAndExpensesOfGsmsGridView.Rows[e.RowIndex];
                int id = int.Parse(row.Cells[1].Text);

                IncomeAndExpensesOfGsm income = _context.IncomeAndExpensesOfGsms.FirstOrDefault(s => s.IncomeAndExpenseOfGsmid == id);

                income.NumberOfCapacity = numberOfCapacity;
                income.IncomeOrExpensePerliter = incomeOrExpensePerliter;
                income.ContainerId = containerId;
                income.DateAndTimeOfTheOperationIncomeOrExpense = dateAndTimeOfTheOperationIncomeOrExpense;
                income.ResponsibleForTheOperation = responsibleForTheOperation;

                _context.SaveChanges();

                AddStatusLabel.Text = "IncomeAndExpensesOfGsm was successfully updated.";

                IncomeAndExpensesOfGsmsGridView.EditIndex = -1;
                GetIncomeAndExpensesOfGsms();
            }
        }

        protected void IncomeAndExpensesOfGsmsGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var row = IncomeAndExpensesOfGsmsGridView.Rows[e.RowIndex];
            int id = int.Parse(row.Cells[1].Text);

            IncomeAndExpensesOfGsm income = _context.IncomeAndExpensesOfGsms.FirstOrDefault(s => s.IncomeAndExpenseOfGsmid == id);
            _context.IncomeAndExpensesOfGsms.Remove(income);
            _context.SaveChanges();

            AddStatusLabel.Text = "IncomeAndExpensesOfGsm was successfully deleted.";
            GetIncomeAndExpensesOfGsms();
        }

        public bool CheckValues(int numberOfCapacity, int incomeOrExpensePerliter, DateTime dateAndTimeOfTheOperationIncomeOrExpense, string responsibleForTheOperation)
        {
            if (numberOfCapacity == default)
            {
                AddStatusLabel.Text = "Incorrect 'NumberOfCapacity' data.";
                return false;
            }

            if (incomeOrExpensePerliter == default)
            {
                AddStatusLabel.Text = "Incorrect 'IncomeOrExpensePerliter' data";
                return false;
            }

            if (dateAndTimeOfTheOperationIncomeOrExpense == default)
            {
                AddStatusLabel.Text = "Incorrect 'DateAndTimeOfTheOperationIncomeOrExpense' data";
                return false;
            }

            if (string.IsNullOrEmpty(responsibleForTheOperation))
            {
                AddStatusLabel.Text = "Incorrect 'ResponsibleForTheOperation' data";
                return false;
            }

            return true;
        }
    }
}