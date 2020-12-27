using lab7.Data;
using lab7.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace lab7.GSM
{
    public partial class GSM : System.Web.UI.Page
    {
        private readonly Petrol_StationContext _context = new Petrol_StationContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
                GetGsm();
        }

        private void GetGsm()
        {
            IEnumerable<Gsm> gsm = _context.Gsm.ToList();
            GsmGridView.DataSource = gsm;
            GsmGridView.DataBind();
        }

        protected void GSMGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GsmGridView.PageIndex = e.NewPageIndex;
            GetGsm();
        }

        protected void AddGsmButton_Click(object sender, EventArgs e)
        {
            string typeOfGsm = GsmtypeOfGSMTextBox.Text;
            string tTkofType = GsmtTKOfTypeTextBox.Text;

            if (CheckValues(typeOfGsm, tTkofType))
            {
                Gsm gsm = new Gsm
                {
                    TypeOfGsm = typeOfGsm,
                    TTkofType = tTkofType
                };

                _context.Gsm.Add(gsm);
                _context.SaveChanges();

                GsmtypeOfGSMTextBox.Text = string.Empty;
                GsmtTKOfTypeTextBox.Text = string.Empty;

                AddStatusLabel.Text = "Gsm was successfully added.";

                GsmGridView.PageIndex = GsmGridView.PageCount;
                GetGsm();
            }
        }

        protected void GsmGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GsmGridView.EditIndex = e.NewEditIndex;
            GetGsm();
        }

        protected void GsmGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GsmGridView.EditIndex = -1;
            GetGsm();
        }

        protected void GsmGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string typeOfGsm = (string)e.NewValues["TypeOfGsm"];
            string tTkofType = (string)e.NewValues["TTkofType"];

            if (CheckValues(typeOfGsm, tTkofType))
            {
                var row = GsmGridView.Rows[e.RowIndex];
                int id = int.Parse(row.Cells[1].Text);

                Gsm gsm = _context.Gsm.FirstOrDefault(g => g.GSmid == id);
                
                gsm.TypeOfGsm = typeOfGsm;
                gsm.TTkofType = tTkofType;

                _context.SaveChanges();

                AddStatusLabel.Text = "Gsm was successfully updated.";

                GsmGridView.EditIndex = -1;
                GetGsm();
            }
        }

        protected void GsmGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var row = GsmGridView.Rows[e.RowIndex];
            int id = int.Parse(row.Cells[1].Text);

            Gsm gsm = _context.Gsm.FirstOrDefault(g => g.GSmid == id);
            _context.Gsm.Remove(gsm);
            _context.SaveChanges();

            AddStatusLabel.Text = "Gsm was successfully deleted.";

            GetGsm();
        }

        public bool CheckValues(string typeOfGsm, string tTkofType)
        {
            if (string.IsNullOrEmpty(typeOfGsm))
            {
                AddStatusLabel.Text = "Incorrect 'TypeOfGsm' data.";
                return false;
            }

            if (string.IsNullOrEmpty(tTkofType))
            {
                AddStatusLabel.Text = "Incorrect 'TTkofType' data.";
                return false;
            }

            return true;
        }
    }
}