using AutomobileLibrary.BusinessObject;
using AutomobileLibrary.Repository;

namespace AutomobileSolution
{
    public partial class frmCarManagement : Form
    {
        ICarRepository carRepository = new CarRepository();
        //Create a data source
        BindingSource source;
        //---------------------------------------------------

        public frmCarManagement()
        {
            InitializeComponent();
        }
        //---------------------------------------------------
        private void frmCarManagement_Load(object sender, EventArgs e)
        {
            btnDelete.Enabled = false;
            //Register this event to open the frmCarDetails form that performs updating
            dgvCarList.CellDoubleClick += DgvCarList_CellDoubleClick;
        }
        //---------------------------------------------------
        private void DgvCarList_CellDoubleClick(object sender, EventArgs e)
        {
            frmCarDetails frmCarDetails = new frmCarDetails()
            {
                Text = "Update car",
                InsertOrUpdate = true,
                CarInfo = GetCarObject(),
                CarRepository = carRepository
            };
            if(frmCarDetails.ShowDialog() == DialogResult.OK)
            {
                LoadCarList();
                //Set focus car updated
                source.Position = source.Count - 1;
            }
        }

        //Clear text on Textboxes
        private void ClearText()
        {
            txtCarID.Text = string.Empty;
            txtCarName.Text = string.Empty;
            txtManufacturer.Text = string.Empty;
            txtPrice.Text = string.Empty;
            txtReleaseYear.Text = string.Empty;
        }
        //-----------------------------------------------------
        private Car GetCarObject()
        {
            Car car = null;
            try
            {
                car = new Car
                {
                    CarID = int.Parse(txtCarID.Text),
                    CarName = txtCarName.Text,
                    Manufacturer = txtManufacturer.Text,
                    Price = decimal.Parse(txtPrice.Text),
                    ReleaseYear = int.Parse(txtReleaseYear.Text)
                };
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Get Car");
            }
            return car;
        }//end GetCarObject
        //-----------------------------------------------------
        public void LoadCarList()
        {
            var cars = carRepository.GetCars();
            try
            {
                //The BindingSource component is designed to simplify
                //The process of binding controls to an underlying data source
                source = new BindingSource();
                source.DataSource = cars;

                txtCarID.DataBindings.Clear();
                txtCarName.DataBindings.Clear();
                txtManufacturer.DataBindings.Clear();
                txtPrice.DataBindings.Clear();
                txtReleaseYear.DataBindings.Clear();

                txtCarID.DataBindings.Add("Text", source, "CarID");
                txtCarName.DataBindings.Add("Text", source, "CarName");
                txtManufacturer.DataBindings.Add("Text", source, "Manufacturer");
                txtPrice.DataBindings.Add("Text", source, "Price");
                txtReleaseYear.DataBindings.Add("Text", source, "ReleaseYear");

                dgvCarList.DataSource = null;
                dgvCarList.DataSource = source;
                if(cars.Count() == 0)
                {
                    ClearText();
                    btnDelete.Enabled = false;
                }
                else
                {
                    btnDelete.Enabled = true;
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Load car list");
            }
        }//end LoadCarList
        //---------------------------------------------------------------
        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadCarList();
        }//end btnLoad_Click
        //---------------------------------------------------------------

        private void btnClose_Click(object sender, EventArgs e) => Close();
        //---------------------------------------------------------------
        private void btnNew_CLick(object sender, EventArgs e)
        {
            frmCarDetails frmCarDetails = new frmCarDetails
            {
                Text = "Add car",
                InsertOrUpdate = false,
                CarRepository = carRepository
            };
            if(frmCarDetails.ShowDialog() == DialogResult.OK)
            {
                LoadCarList();
                //Set focus car inserted
                source.Position = source.Count - 1;
            }
        }
        //---------------------------------------------------------------
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                var car = GetCarObject();
                carRepository.DeleteCar(car.CarID);
                LoadCarList();
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Delete a car");
            }
        }
    }
}