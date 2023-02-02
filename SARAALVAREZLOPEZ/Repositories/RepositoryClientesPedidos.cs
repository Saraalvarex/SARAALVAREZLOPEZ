﻿using SARAALVAREZLOPEZ.Helpers;
using SARAALVAREZLOPEZ.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Contracts;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace SARAALVAREZLOPEZ.Repositories
{
    public class RepositoryClientesPedidos
    {
        SqlConnection cn;
        SqlCommand com;
        SqlDataReader rdr;

        public RepositoryClientesPedidos()
        {
            string connectionString = "Data Source=T06W02\\DESARROLLO;Initial Catalog=PRACTICAADO;User ID=sa;Password=MCSD2022";
            this.cn = new SqlConnection(connectionString);
            this.com = new SqlCommand();
            this.com.Connection = this.cn;
        }
        public List<Cliente> GetClientes()
        {
            this.com.CommandType = CommandType.StoredProcedure;
            this.com.CommandText = "SP_CLIENTES";
            List<Cliente> clientes = new List<Cliente>();
            this.cn.Open();
            this.rdr = this.com.ExecuteReader();
            while (this.rdr.Read())
            {
                Cliente cliente = new Cliente();
                //cliente.CodCli = this.rdr["CODIGOCLIENTE"].ToString();
                cliente.Contacto = this.rdr["CONTACTO"].ToString();
                //cliente.Cargo = this.rdr["CARGO"].ToString();
                //cliente.Empresa = this.rdr["EMPRESA"].ToString();
                clientes.Add(cliente);
            }
            this.rdr.Close();
            this.cn.Close();
            return clientes;
        }

        public Cliente GetCliente(string nombre=null)
        {
            SqlParameter pamnombre = new SqlParameter("@NOMBRE", nombre);
            this.com.Parameters.Add(pamnombre);
            this.com.CommandType = CommandType.StoredProcedure;
            this.com.CommandText = "SP_CLIENTES";
            Cliente cliente = new Cliente();
            this.cn.Open();
            this.rdr = this.com.ExecuteReader();
            while (this.rdr.Read())
            {
                cliente.CodCli = this.rdr["CODIGOCLIENTE"].ToString();
                cliente.Empresa = this.rdr["EMPRESA"].ToString();
                cliente.Contacto = this.rdr["CONTACTO"].ToString();
                cliente.Cargo = this.rdr["CARGO"].ToString();
                cliente.Ciudad = this.rdr["CIUDAD"].ToString();
                cliente.Tlf = int.Parse(this.rdr["TELEFONO"].ToString());
            }
            this.rdr.Close();
            this.cn.Close();
            this.com.Parameters.Clear();
            return cliente;
        }

        public List<Pedido> GetPedidos(string cli = null)
        {
            SqlParameter pamcli = new SqlParameter("@NOMBRE", cli);
            this.com.Parameters.Add(pamcli);
            this.com.CommandType = CommandType.StoredProcedure;
            this.com.CommandText = "SP_PEDIDOS";
            List<Pedido> pedidos = new List<Pedido>();
            this.cn.Open();
            this.rdr = this.com.ExecuteReader();
            while (this.rdr.Read())
            {
                Pedido cliente = new Pedido();
                cliente.CodPed = this.rdr["CODIGOPEDIDO"].ToString();
                pedidos.Add(cliente);
            }
            this.rdr.Close();
            this.cn.Close();
            this.com.Parameters.Clear();
            return pedidos;
        }
        public Pedido GetPedido(string codped = null)
        {
            SqlParameter pamcod = new SqlParameter("@CODPEDIDO", codped);
            this.com.Parameters.Add(pamcod);
            this.com.CommandType = CommandType.StoredProcedure;
            this.com.CommandText = "SP_DATOS_PEDIDO";
            Pedido pedido = new Pedido();
            this.cn.Open();
            this.rdr = this.com.ExecuteReader();
            while (this.rdr.Read())
            {
                pedido.CodCli = this.rdr["CODIGOCLIENTE"].ToString();
                pedido.CodPed = this.rdr["CODIGOPEDIDO"].ToString();
                pedido.Importe = int.Parse(this.rdr["IMPORTE"].ToString());
                pedido.Envio = this.rdr["FORMAENVIO"].ToString();
                pedido.Fecha = DateTime.Parse(this.rdr["FECHAENTREGA"].ToString());
            }
            this.rdr.Close();
            this.cn.Close();
            this.com.Parameters.Clear();
            return pedido;
        }
        #region SP_UPDATE_CLIENTES
        //ALTER PROCEDURE SP_UPDATE_CLIENTES
        //(@EMPRESA NVARCHAR(50),
        //@CONTACTO NVARCHAR(50),
        //@CARGO NVARCHAR(50),
        //@CIUDAD NVARCHAR(50),
        //@TLF INT)
        //AS
        //    DECLARE @CODCLI NVARCHAR(4)

        //    SELECT* FROM CLIENTES
        //    WHERE Contacto=@CONTACTO
        //    UPDATE CLIENTES
        //    SET EMPRESA=@EMPRESA, Contacto=@CONTACTO,
        // Cargo=@CARGO, Ciudad=@CIUDAD, Telefono=@TLF
        //    WHERE CodigoCliente=@CODCLI
        //GO
        #endregion
        public void UpdateCliente(string empresa, string contacto, string cargo, string ciudad, int tlf)
        {
            SqlParameter pamem = new SqlParameter("@EMPRESA", empresa);
            this.com.Parameters.Add(pamem);
            SqlParameter pamcont = new SqlParameter("@CONTACTO", contacto);
            this.com.Parameters.Add(pamcont);
            SqlParameter pamcargo = new SqlParameter("@CARGO", cargo);
            this.com.Parameters.Add(pamcargo);
            SqlParameter pamciudad = new SqlParameter("@CIUDAD", ciudad);
            this.com.Parameters.Add(pamciudad);
            SqlParameter pamtlf = new SqlParameter("@TLF", tlf);
            this.com.Parameters.Add(pamtlf);
            this.com.CommandType = CommandType.StoredProcedure;
            this.com.CommandText = "SP_UPDATE_CLIENTES";
            this.cn.Open();
            int modificados = this.com.ExecuteNonQuery();
            this.rdr.Close();
            this.cn.Close();
            this.com.Parameters.Clear();
            MessageBox.Show("Clientes modficiados: " + modificados);
        }

    }
}
