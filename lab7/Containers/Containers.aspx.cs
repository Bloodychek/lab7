using lab7.Data;
using lab7.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace lab7.Containers
{
    public partial class Contaiers : System.Web.UI.Page
    {
        private readonly Petrol_StationContext _context = new Petrol_StationContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
                GetContainers();
        }

        private void GetContainers()
        {
            IEnumerable<Container> container = _context.Containers.ToList();
            ContainersGridView.DataSource = container;
            ContainersGridView.DataBind();
        }

        protected void ContainersGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ContainersGridView.PageIndex = e.NewPageIndex;
            GetContainers();
        }

        protected void AddContainersButton_Click(object sender, EventArgs e)
        {
            int number = int.Parse(ContainersNumberTextBox.Text);
            double tankCapacity = double.Parse(ContainersTankCapacityTextBox.Text);

            if (CheckValues(number, tankCapacity))
            {
                Container container = new Container
                {
                    Number = number,
                    TankCapacity = tankCapacity
                };

                _context.Containers.Add(container);
                _context.SaveChanges();

                ContainersNumberTextBox.Text = string.Empty;
                ContainersTankCapacityTextBox.Text = string.Empty;

                AddStatusLabel.Text = "Containers was successfully added.";

                ContainersGridView.PageIndex = ContainersGridView.PageCount;
                GetContainers();
            }
        }

        protected void ContainersGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            ContainersGridView.EditIndex = e.NewEditIndex;
            GetContainers();
        }

        protected void ContainersGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            ContainersGridView.EditIndex = -1;
            GetContainers();
        }

        protected void ContainersGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int number = int.Parse((string)e.NewValues["Number"]);
            double tankCapacity = double.Parse((string)e.NewValues["TankCapacity"]);

            if (CheckValues(number, tankCapacity))
            {
                var row = ContainersGridView.Rows[e.RowIndex];
                int id = int.Parse(row.Cells[1].Text);

                Container container = _context.Containers.FirstOrDefault(g => g.ContainerId == id);

                container.Number = number;
                container.TankCapacity = tankCapacity;

                _context.SaveChanges();

                AddStatusLabel.Text = "Containers was successfully updated.";

                ContainersGridView.EditIndex = -1;
                GetContainers();
            }
        }

        protected void ContainersGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var row = ContainersGridView.Rows[e.RowIndex];
            int id = int.Parse(row.Cells[1].Text);

            Container container = _context.Containers.FirstOrDefault(g => g.ContainerId == id);
            _context.Containers.Remove(container);
            _context.SaveChanges();

            AddStatusLabel.Text = "Containers was successfully deleted.";

            GetContainers();
        }

        public bool CheckValues(int number, double tankCapacity)
        {
            if (number == default)
            {
                AddStatusLabel.Text = "Incorrect 'Number' data.";
                return false;
            }

            if (tankCapacity == default)
            {
                AddStatusLabel.Text = "Incorrect 'TankCapacity' data.";
                return false;
            }

            return true;
        }
    }
}