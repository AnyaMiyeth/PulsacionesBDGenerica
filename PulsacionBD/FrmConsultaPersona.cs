﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
namespace PulsacionBD
{
    public partial class FrmConsultaPersona : Form
    {
        PersonaService personaService;
        public FrmConsultaPersona()
        {
             personaService = new PersonaService(ConfigConnection.connectionString, ConfigConnection.ProviderName);
            InitializeComponent();
        }

        private void BtnConsultar_Click(object sender, EventArgs e)
        {
            ConsultaPersonaRespuesta respuesta = new ConsultaPersonaRespuesta();
            
            string tipo = CmbTipo.Text;
            if (tipo == "Todos")
            {
                DtgPersona.DataSource = null;
                respuesta = personaService.ConsultarTodos();
                DtgPersona.DataSource = respuesta.Personas;
                TxtTotal.Text = personaService.Totalizar().Cuenta.ToString();
                TxtTotalMujeres.Text = personaService.TotalizarTipo("F").Cuenta.ToString();
                TxtTotalHombres.Text = personaService.TotalizarTipo("M").Cuenta.ToString();

            }
            else
            {
                MessageBox.Show("Debe Seleccionar una opción ", "Busqueda", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
           

            MessageBox.Show(respuesta.Mensaje, "Busqueda", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
    }
}
