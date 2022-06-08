using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dominio;
using Negocio;

namespace WindowsFormsMascotas
{
    public partial class Form1 : Form
    {
        List<Mascota> lMascotas = new List<Mascota>();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            habilitar(false);
            cargarLista();
            cargarComboEspecie();
        }

        private void ocultarColumnas()
        {
            dgvMascotas.Columns["Especie"].Visible=false;
            dgvMascotas.Columns["Sexo"].Visible = false;
            dgvMascotas.Columns["FechaNacimiento"].Visible = false;

        }
        private void cargarLista()
        {
            MascotaNegocio negocio = new MascotaNegocio();
            try
            {
                lMascotas = negocio.listar();
                dgvMascotas.DataSource = lMascotas;
                ocultarColumnas();
                
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void dgvMascotas_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvMascotas!=null)
            {
                Mascota seleccionada = (Mascota)dgvMascotas.CurrentRow.DataBoundItem;
                txtCodigo.Enabled = false;
                txtCodigo.Text= seleccionada.Codigo.ToString();
                txtNombre.Text = seleccionada.Nombre;
                cboEspecie.SelectedValue = seleccionada.Especie.IdEspecie;
                if (seleccionada.Sexo==1)
                {
                    rbtMacho.Checked = true;
                }
                else
                {
                    rbtHembra.Checked = true;
                }
                dtpFechaNacimiento.Value = seleccionada.FechaNacimiento;
               
                
            }
        }
        public void cargarComboEspecie()
        {
            EspecieNegocio especieNegocio = new EspecieNegocio();
            try
            {
                cboEspecie.DataSource = especieNegocio.listar();
                cboEspecie.ValueMember = "idEspecie";
                cboEspecie.DisplayMember = "nombreEspecie";

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            DialogResult respuesta = MessageBox.Show("¿Seguro Que Quiere Salir?", "Saliendo",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2
                );
            if (respuesta == DialogResult.Yes)
            {
                Close();
            }
        }

        private void habilitar(bool x)
        {
            txtCodigo.Enabled = x;
            txtNombre.Enabled = x;
            cboEspecie.Enabled = x;
            rbtHembra.Enabled = x;
            rbtMacho.Enabled = x;
            dtpFechaNacimiento.Enabled = x;
            btnGrabar.Enabled = x;
            btnNuevo.Enabled = !x;
            BtnEliminar.Enabled = x;
            btnSalir.Enabled=x;
          
        }
        public void limpiar()
        {
            txtCodigo.Text = "";
            txtNombre.Text = "";
            cboEspecie.SelectedIndex = -1;
            rbtHembra.Checked = false;
            rbtMacho.Checked = false;
            dtpFechaNacimiento.Value = DateTime.Today;


        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            habilitar(true);
            limpiar();
            txtCodigo.Focus();
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {

            MascotaNegocio negocio = new MascotaNegocio();
            if (validarDatos())
            {
                
                Mascota mascota = new Mascota();
                mascota.Codigo = int.Parse(txtCodigo.Text);
                mascota.Nombre = txtNombre.Text;
                mascota.Especie = (Especie)cboEspecie.SelectedItem;
                if (rbtMacho.Checked==true)
                {
                    mascota.Sexo = 1;
                }
                else
                {
                    mascota.Sexo = 2;
                }
                mascota.FechaNacimiento = dtpFechaNacimiento.Value;

                if (!existe(mascota))
                {
                    negocio.agregar(mascota);
                    MessageBox.Show("Acaba De Agregar Exitosamente Una Mascota");
                    cargarLista();
                }
                else
                {

                    
                    negocio.modificar(mascota);
                    MessageBox.Show("Acaba De Modificar Exitosamente Una Mascota");
                    cargarLista();


                }

            }
        }

        private bool validarDatos()
        {
            if (txtCodigo.Text=="")
            {
                MessageBox.Show("Debe Ingresar Un Codigo,Por Favor");
                txtCodigo.Focus();
                return false;
            }
            else
            {
                try
                {
                   int.Parse(txtCodigo.Text);
                }
                catch (Exception)
                {

                    MessageBox.Show("Debe Ingresar Valores Numericos,Por Favor");
                    txtCodigo.Focus();
                    return false;
                }
            }

            if(txtNombre.Text=="")
            {
                MessageBox.Show("Debe Ingresar Un Nombre,Por Favor");
                txtNombre.Focus();
                return false;
            }
            if (cboEspecie.SelectedIndex==-1)
            {
                MessageBox.Show("Debe seleccionar Una Especie,Por Favor");
                cboEspecie.Focus();
                return false;
            }
            if (!rbtHembra.Checked && !rbtMacho.Checked)
            {
                MessageBox.Show("Debe seleccionar un sexo,Por Favor");
                rbtMacho.Focus();
                return false;
            }

            if (DateTime.Today.Year - dtpFechaNacimiento.Value.Year > 10)
            {
                MessageBox.Show("Mascotas Mayores A 10 Años No Se Pueden Registrar, Ingrese Otra, Por Favor");
                dtpFechaNacimiento.Focus();
                return false;
            }
            return true;
        }

        private bool existe (Mascota nueva)
        {
            for (int i = 0; i < lMascotas.Count; i++)
            {
                if (lMascotas[i].Codigo == nueva.Codigo)
                    return true;

            }
            return false;
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            MascotaNegocio negocio = new MascotaNegocio();
            Mascota Seleccionada;
            try
            {
                DialogResult respuesta = MessageBox.Show("¿Seguro Que Quiere Eliminarlo?", "Eliminado",
               MessageBoxButtons.YesNo,
               MessageBoxIcon.Question,
               MessageBoxDefaultButton.Button2
               );
                if (respuesta == DialogResult.Yes)
                {
                    Seleccionada = (Mascota)dgvMascotas.CurrentRow.DataBoundItem;
                    negocio.eliminar(Seleccionada.Codigo);
                    cargarLista();
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
    }
}
