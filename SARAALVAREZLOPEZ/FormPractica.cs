using SARAALVAREZLOPEZ.Models;
using SARAALVAREZLOPEZ.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SARAALVAREZLOPEZ
{
    public partial class FormPractica : Form
    {
        RepositoryClientesPedidos repo;
        public FormPractica()
        {
            InitializeComponent();
            this.repo = new RepositoryClientesPedidos();
            foreach (Cliente cli in this.repo.GetClientes())
            {
                this.cmbclientes.Items.Add(cli.Contacto.ToString());
            }

        }

        private void cmbclientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cliente cliente = this.repo.GetCliente(this.cmbclientes.SelectedItem.ToString());
            this.txtempresa.Text = cliente.Empresa;
            this.txtcontacto.Text = cliente.Contacto;
            this.txtciudad.Text = cliente.Ciudad;
            this.txtcargo.Text = cliente.Cargo;
            this.txttelefono.Text = cliente.Tlf.ToString();
            foreach (Pedido pedido in this.repo.GetPedidos(this.cmbclientes.SelectedItem.ToString()))
            {
                this.lstpedidos.Items.Add(pedido.CodPed);
            }
        }

        private void lstpedidos_SelectedIndexChanged(object sender, EventArgs e)
        {
            Pedido pedido = this.repo.GetPedido(this.lstpedidos.SelectedItem.ToString());
            this.txtcodigopedido.Text=pedido.CodPed;
            this.txtimporte.Text = pedido.Importe.ToString();
            this.txtfechaentrega.Text = pedido.Fecha.ToString();
            this.txtformaenvio.Text = pedido.Envio;
        }

        private void btnmodificarcliente_Click(object sender, EventArgs e)
        {
            string cargo=this.txtcargo.Text;
            int telefono=int.Parse(this.txttelefono.Text);
            string empresa = this.txtempresa.Text;
            string ciudad=this.txtciudad.Text;
            string contacto = this.txtcontacto.Text;

            this.repo.UpdateCliente(empresa, contacto, cargo, ciudad, telefono);
        }

        private void btnnuevopedido_Click(object sender, EventArgs e)
        {
            string cod=this.txtcodigopedido.Text;
            int imp= int.Parse(this.txtimporte.Text);
            DateTime fech= DateTime.Parse(this.txtfechaentrega.Text.ToString());
            string form= this.txtformaenvio.Text;
            this.repo.InsertPedido(cod, fech, form, imp);
        }
    }
}
